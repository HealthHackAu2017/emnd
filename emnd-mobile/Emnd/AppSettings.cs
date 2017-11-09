
// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Emnd
{
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    public static class AppSettings
    {
        private static ISettings ISettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string ParticipantName
        {
            get
            {
                return ISettings.GetValueOrDefault("ParticipantName", "");
            }
            set
            {
                ISettings.AddOrUpdateValue("ParticipantName", value);
            }
        }

        public static string ParticipantId
        {
            get
            {
                return ISettings.GetValueOrDefault("ParticipantId", "");
            }
            set
            {
                ISettings.AddOrUpdateValue("ParticipantId", value);
            }
        }

        public static double ParticipantWeight
        {
            get
            {
                return ISettings.GetValueOrDefault("ParticipantWeight", 0d);
            }
            set
            {
                ISettings.AddOrUpdateValue("ParticipantWeight", value);
            }
        }

        public static string Email
        {
            get
            {
                return ISettings.GetValueOrDefault("email", "");
            }
            set
            {
                ISettings.AddOrUpdateValue("email", value);
            }
        }

        public static string Password
        {
            get
            {
                return ISettings.GetValueOrDefault("password", "");
            }
            set
            {
                ISettings.AddOrUpdateValue("password", value);
            }
        }

        /// <summary>
        /// Has a token from a previous login (which may no longer be valid)
        /// </summary>
        public static bool HasHistory => ISettings.Contains("StoredAnswers");

        public static bool CanLogin
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
            }
        }


        /// <summary>
        /// QA HELPER: path to current logfile
        /// </summary>
        public static string LastLogFile
        {
            get { return ISettings.GetValueOrDefault("lastlogfile", string.Empty); }
            set { ISettings.AddOrUpdateValue("lastlogfile", value); }
        }

        public static List<long> StoredAnswers
        {
            get
            {
                string json = ISettings.GetValueOrDefault("StoredAnswers", "");
                return JsonData.FromJsonString<List<long>>(json);
                //return JsonConvert.DeserializeObject<List<long>>(json, Converter.Settings);
            }
            set
            {
                if (value == null)
                {
                    ISettings.Remove("StoredAnswers");
                }
                else
                {
                    ISettings.AddOrUpdateValue("StoredAnswers", JsonData.AsJsonString(value));
                }
            }
        }

        /// <summary>
        /// Clear down the account
        /// </summary>
        public static void LogoutUser()
        {
            AppSettings.Password = "";
            AppSettings.StoredAnswers = null;
        }

    }
}

