namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class ProfileModel
    {
        [J("ErrorMessage")] public string ErrorMessage { get; set; }
        [J("FirstName")] public string FirstName { get; set; }
        [J("LastName")] public string LastName { get; set; }
        [J("Success")] public bool Success { get; set; }
        [J("UserID")] public string UserID { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(FirstName) ? LastName : FirstName + " " + LastName;
            }
        }

    }

    public partial class ProfileModel
    {
        public static ProfileModel FromJson(string json) => JsonConvert.DeserializeObject<ProfileModel>(json, Converter.Settings);
    }
}
