// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = SubmissionResultDTO.FromJson(jsonString);
//
namespace Emnd
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The response from a survey submission which returns the calculated results to display
    /// </summary>
    public partial class SubmissionResultDTO
    {
        [JsonProperty("Breathing")]
        public long Breathing { get; set; }

        [JsonProperty("Mental Health")]
        public double MentalHealth { get; set; }

        [JsonProperty("Overall Health")]
        public double OverallHealth { get; set; }

        [JsonProperty("Saliva and Swallowing")]
        public double SalivaAndSwallowing { get; set; }
    }

    public partial class SubmissionResultDTO
    {
        public static SubmissionResultDTO FromJson(string json) => JsonConvert.DeserializeObject<SubmissionResultDTO>(json, Converter.Settings);
    }
}
