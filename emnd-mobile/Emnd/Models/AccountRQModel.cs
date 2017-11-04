namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class AccountRQModel
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

    public partial class AccountRQModel
    {
        public static AccountRQModel FromJson(string json) => JsonConvert.DeserializeObject<AccountRQModel>(json, Converter.Settings);
    }
}
