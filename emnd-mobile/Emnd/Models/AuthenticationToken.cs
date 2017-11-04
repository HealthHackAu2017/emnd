//  The 07 UserAuthenticationToken returned from /V1/Users/Authenticate
//
//    using RQConnected;
//
//    var data = UserAuthenticationToken.FromJson(jsonString);
//
namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class AuthenticationToken
    {
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("AuthenticationToken")]
        public string AuthToken { get; set; }

        [JsonProperty("ExpiryDateUTC")]
        public string ExpiryDateUTC { get; set; }

        [JsonProperty("UserID")]
        public string UserID { get; set; }
    }

    public partial class AuthenticationToken
    {
        public static AuthenticationToken FromJson(string json) => JsonConvert.DeserializeObject<AuthenticationToken>(json, Converter.Settings);
    }

}
