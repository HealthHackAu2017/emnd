using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using Serilog;
using Xamarin.Forms;
using Plugin.DeviceInfo;
using Newtonsoft.Json;
using Acr.UserDialogs;

namespace Emnd
{
    public enum NetworkStatus
    {
        NotConnected,
        NotReachable,
        ReachableViaCarrierDataNetwork,
        ReachableViaWiFiNetwork,
        Good
    }

    public interface INetworkService
    {
        string Domain { get; set; }

        string AuthValue { get; set; }
        NetworkStatus Status { get; set; }

        Task<NetworkStatus> InternetAvailable();

        /// <summary> Asks for data from server </summary>
        /// <returns>string in json format</returns>
        Task<HttpResponseMessage> GET(string uri);

        /// <summary> Makes a POST to server </summary>
        /// <param name="apiuri">Should just be the api uri. Domain is stored in the service</param>
        /// <param name="data">May be null. Must be a Json formatted string</param>
        Task<HttpResponseMessage> POST(string apiuri, string data = null);

        Task<HttpResponseMessage> PUT(string api, string data);

        Task<HttpResponseMessage> DELETE(string api, string data);

        Task<string> ResponseHandler(HttpResponseMessage response);
        Task<IList<T>> PostData<T>(string api, string json, bool isArray = false);

        string HtmlDecode(string str);
        string HtmlEncode(string str);
        Task<HttpResponseMessage> GetExternal(string uri);
    }

    public class NetworkService : INetworkService
    {
        public string Domain { get; set; }

        public NetworkService()
        {
            Domain = AppConstants.EmndServer;
        }

        public const string Success = "OK";

        //headers
        const string AuthKey = "token";
        public string AuthValue { get; set; }

        const string AppPlatformKey = "x-rq-app-platform";
        private string AppPlatformValue = Device.RuntimePlatform;

		const string AppVersionKey = "x-rq-app-version";
        private string AppVersionValue = DisplayInfo.VersionAndBuildNumber;

        //Endpoints
        public const string UserAuthenticationToken = "/Users/Authenticate";


        HttpClient _client;
        HttpClient Client => _client ?? (_client = new HttpClient(Handler));
        HttpClientHandler _handler;
        HttpClientHandler Handler => _handler ?? (_handler = new HttpClientHandler {UseProxy = false});

        public NetworkStatus Status { get; set; }


        private void CheckConnection()
        {
            Log.Information("Checking connection to Rubin platform");
            CrossConnectivity.Current.ConnectivityTypeChanged += (sender, args) =>
            {
                Log.Information($"Connectivity changed to {args.IsConnected}");
                foreach (var t in args.ConnectionTypes)
                    Log.Information($"Connection Type {t}");
            };

            Task.Run(() => IsServerReachable().ContinueWith(resp =>
            {
                Log.Information($"Rubin platform is reachable = {resp.Result}");

                if (!resp.Result)
                {
                    //ToastMessage("Internet required", "Cannot connect to server " + AppConstants.RubinServer);
                    Log.Error("Cannot connect to server " + AppConstants.EmndServer);
                }
            }));
        }

        public async Task<bool> IsServerReachable()
        {
            Log.Information("********* Checking connection to Rubin platform");
            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected)
                return false;

            var reachable = await connectivity.IsRemoteReachable(AppConstants.EmndServer, TimeSpan.FromMilliseconds(5000));

            return reachable;
        }

        public async Task<NetworkStatus> InternetAvailable()
        {
            //Checking network connectivity
            if (CrossConnectivity.Current.IsConnected == false)
            {
                Status = NetworkStatus.NotConnected;
                return Status;
            }

            HttpResponseMessage response = null;
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Domain)
                };
                response = await Client.SendAsync(request);
                Log.Information((int) response.StatusCode + " : " + response.ReasonPhrase + " : " + response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Log.Error("Internet Availability Exception: " + ex.Message);
                Status = NetworkStatus.NotReachable;
                return Status;
            }

            if (response.IsSuccessStatusCode)
            {
                Log.Information("response received");
                Log.Information(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Log.Error("no response");
                Status = NetworkStatus.NotReachable;
                return Status;
            }
            Status = NetworkStatus.Good;
            return Status;
        }

        private async Task<HttpResponseMessage> NetworkRequest(string caller, HttpMethod method, string endpoint, string jsonContent = null)
        {
            try
            {
                //setup content for message (if any)
                StringContent content = jsonContent != null
                    ? new StringContent(
                        jsonContent,
                        Encoding.UTF8,
                        "application/json"
                    )
                    : null;

                var request = new HttpRequestMessage
                {
                    Method = method,
                    Content = content,
                    RequestUri = new Uri(Domain + endpoint)
                };
                Log.Information(request.Method != HttpMethod.Get
                    ? string.Format("Request: {0}, Uri: {1}, Content: {2}", request.Method, request.RequestUri,
                        request.Content != null ? request.Content.ReadAsStringAsync().Result : "No content")
                    : string.Format("Request: {0}, Uri {1}", request.Method, request.RequestUri)
                );

                //request.Headers.Add("Content-Type", "application / json");
                //request.Headers.Add(AppPlatformKey, AppPlatformValue);
                //request.Headers.Add(AppVersionKey, AppVersionValue);

                if (UserAuthenticationToken.Equals(endpoint))
                {
                    Log.Warning($"{endpoint} logging in - accesstoken does not need to be set");
                    //UserDialogs.Instance.ShowLoading("Authenticating");
                }
                else
                {
                    if (AppSettings.HasToken)
                    {
                        request.Headers.Add("Authorization", "APIToken");
                    }
                }

                var response = await Client.SendAsync(request);
                Log.Information(string.Format("Response: {0} {1}, Uri: {2}, Length(Byte): {3}\nContent: {4}",
                    (int) response.StatusCode, response.ReasonPhrase, request.RequestUri,
                    response.Content.ReadAsStringAsync().Result.Length, response.Content.ReadAsStringAsync().Result));
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Network Service: " + caller + " API Exception Caught: " + ex.Message + "\n" + ex.StackTrace);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("{\"ExceptionMessage\":" + ex.Message + "}")
                };
            }
        }

        public async Task<HttpResponseMessage> GET(string api)
        {
            return await NetworkRequest(nameof(GET), HttpMethod.Get, api, null);
        }

        public async Task<HttpResponseMessage> POST(string api, string data = null)
        {
            return await NetworkRequest(nameof(POST), HttpMethod.Post, api, data);
        }

        public async Task<HttpResponseMessage> PUT(string api, string data)
        {
            return await NetworkRequest(nameof(PUT), HttpMethod.Put, api, data);
        }

        public async Task<HttpResponseMessage> DELETE(string api, string data)
        {
            return await NetworkRequest(nameof(DELETE), HttpMethod.Delete, api, data);
        }

        public string HtmlDecode(string str)
        {
            return WebUtility.HtmlDecode(str);
        }

        public string HtmlEncode(string str)
        {
            return WebUtility.HtmlEncode(str);
        }

        public async Task<string> ResponseHandler(HttpResponseMessage response)
        {
            string userMessage = string.Empty;

            //make sure our response is good
            if (response.IsSuccessStatusCode)
            {
                return Success;
            }

            userMessage = "Server error: " + (int)response.StatusCode;
            if (!string.IsNullOrWhiteSpace(response.ReasonPhrase))
            {
                userMessage += " " + response.ReasonPhrase;
            }

            if (! string.IsNullOrWhiteSpace(response.Content.ReadAsStringAsync().Result))
            {
                // We have a detailed error
                try
                {
                    NetworkError error = NetworkError.Deserialize(response.Content.ReadAsStringAsync().Result);
                    if (!string.IsNullOrWhiteSpace(error.StackTrace))
                    {
                        userMessage += "\n" + error.StackTrace;
                    }
                    else if (!string.IsNullOrWhiteSpace(error.ExceptionMessage))
                    {
                        userMessage += "\n" + error.ExceptionMessage;
                    }
                    else if (! string.IsNullOrWhiteSpace(error.Message))
                    {
                        userMessage += "\n" + error.Message;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Failed to deserialize error message");
                    Log.Error(ex.StackTrace);
                    userMessage = "Server error: " + (int) response.StatusCode + "\nReason: " + response.ReasonPhrase;
                }
            }
            App.Navigation.ShowAPIError(userMessage);
            return userMessage;
        }

        //protected async Task<IList<T>> RequestHandler<T>(Func<Task<HttpResponseMessage>> request, string customLoadMsg = "Loading", bool showLoading = false, bool isArray = true, Func<HttpResponseMessage, Task<bool>> customResponseHandler = null)
        //{
        //    return await RequestHandler<T>(request, isArray: isArray, customResponseHandler: customResponseHandler);

        //}

        public async Task<IList<T>> RequestHandler<T>(Func<Task<HttpResponseMessage>> request, bool isArray = true, Func<HttpResponseMessage, Task<string>> customResponseHandler = null)
        {
            try
            {
                var response = await request();

                var goodResponse = await (customResponseHandler != null ? customResponseHandler(response) : ResponseHandler(response));
                if (goodResponse == Success)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        if (isArray)
                        {
                            var list = JsonData.FromJsonString<ListContainer<T>>(result).Data;
                            return list;
                        }
                        var item = JsonData.FromJsonString<T>(result);
                        return new List<T> {item};
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("NetworkService API Exception: " + ex.Message);
                Log.Error("Stack trace: " + ex.StackTrace);
            }
            return null;
        }
        
        public async Task<IList<T>> PostData<T>(string api, string json, bool isArray = false)
        {
            return await RequestHandler<T>(() => POST(api, json), isArray);
        }

        /// <summary>
        /// Fetch an external (non-Roubler) HTTP resource
        /// </summary>
        /// <returns>The external response.</returns>
        /// <param name="url">URL.</param>
        public async Task<HttpResponseMessage> GetExternal(string url)
        {
            try
            {
                //setup content for message (if any)
                StringContent content = null;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    Content = content,
                    RequestUri = new Uri(url)
                };
                Log.Information(string.Format("Request: {0}, Uri {1}", request.Method, request.RequestUri));

                var response = await Client.SendAsync(request);
                Log.Information(string.Format("Response: {0} {1}, Uri: {2}, Length(Byte): {3}\n",
                    (int) response.StatusCode, response.ReasonPhrase, request.RequestUri,
                    response.Content.ReadAsStringAsync().Result.Length));
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Network Service: API Exception Caught: " + ex.Message + "\n" + ex.StackTrace);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("{\"error\":" + ex.Message + "}")
                };
            }
        }
        public async Task<IList<T>> GetData<T>(string api, string id = "0", bool showLoading = false, bool isArray = true, Func<HttpResponseMessage, Task<string>> customResponseHandler = null)
        {
            var suffix = "";
            if (string.IsNullOrWhiteSpace(id) || (id == "0"))
            {
                suffix = "";
            }
            else
            {
                suffix = "/" + id;
            }
            var finalApi = api + suffix;
            return await RequestHandler<T>(() => GET(finalApi), isArray: isArray, customResponseHandler: customResponseHandler);
        }

        public async Task<IList<T>> PostData<T>(string api, string json, string customMessage = null, bool showLoading = true, bool isArray = false)
        {
            return await RequestHandler<T>(() => POST(api, json), isArray);
        }

        public async Task<IList<T>> PutData<T>(string api, string id, string json = null, bool showLoading = true)
        {
            var finalApi = api + (id != "0" ? "/" + id : "");
            return await RequestHandler<T>(() => PUT(finalApi, json));
        }

        public async Task<IList<T>> DeleteData<T>(string api, string json = null, string customMessage = null, bool showLoading = true, bool isArray = false)
        {
            return await RequestHandler<T>(() => DELETE(api, json), isArray);
        }

        //public async Task<bool> LoadList<T>(string api, ObservableCollection<T> list, bool showLoading = false, Func<HttpResponseMessage, Task<bool>> customResponseHandler = null)
        //{
        //    var res = await GetData<T>(api, showLoading: showLoading, customResponseHandler: customResponseHandler);
        //    if (res != null && res.Count > 0)
        //    {
        //        list.Clear();
        //        foreach (var item in res)
        //        {
        //            list.Add(item);
        //        }
        //        return true;
        //    }
        //    return false;
        //}

    }

    public class NetworkError
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("ExceptionMessage")]
        public string ExceptionMessage { get; set; }
        [JsonProperty("StackTrace")]
        public string StackTrace { get; set; }

        public static NetworkError Deserialize(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<NetworkError>(jsonData);
            }
            catch (JsonSerializationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return new NetworkError { Message = "Internal networkservice error." };
        }
    }

}