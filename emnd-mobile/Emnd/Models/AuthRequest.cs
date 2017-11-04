// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using RQConnected;
//
//    var data = AuthenticationRequest.FromJson(jsonString);
//
namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class AuthRequest
    {
        [J("DeviceOS")] public string DeviceOS { get; set; }
        [J("DeviceID")] public string DeviceID { get; set; }
        [J("Password")] public string Password { get; set; }
        [J("Username")] public string Username { get; set; }
    }

    public partial class AuthRequest
    {
        public static AuthRequest FromJson(string json) => JsonConvert.DeserializeObject<AuthRequest>(json, Converter.Settings);
    }

    public static class ToJsonString
    {
        public static string ToJson(this AuthRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

}


    

