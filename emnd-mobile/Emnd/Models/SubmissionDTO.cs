// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = SubmissionDTO.FromJson(jsonString);
//
namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class SubmissionDTO
    {
        [JsonProperty("d20a")]
        public double D20a { get; set; }

        [JsonProperty("d20b")]
        public double D20b { get; set; }

        [JsonProperty("d20c")]
        public double D20c { get; set; }

        [JsonProperty("d20d")]
        public double D20d { get; set; }

        [JsonProperty("d21a")]
        public double D21a { get; set; }

        [JsonProperty("d21b")]
        public double D21b { get; set; }

        [JsonProperty("d21c")]
        public double D21c { get; set; }

        [JsonProperty("d21d")]
        public double D21d { get; set; }

        [JsonProperty("d21e")]
        public double D21e { get; set; }

        [JsonProperty("d21f")]
        public double D21f { get; set; }

        [JsonProperty("d22a")]
        public double D22a { get; set; }

        [JsonProperty("d22b")]
        public double D22b { get; set; }

        [JsonProperty("d22c")]
        public double D22c { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonIgnore] //("submission_date")]
        public DateTime SubmissionDate { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public partial class SubmissionDTO
    {
        public static SubmissionDTO FromJson(string json) => JsonConvert.DeserializeObject<SubmissionDTO>(json, Converter.Settings);
    }

}
