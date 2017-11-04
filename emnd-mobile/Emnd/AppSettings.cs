
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

        public static string UserName
        {
            get
            {
                return ISettings.GetValueOrDefault("username", "");
            }
            set
            {
                ISettings.AddOrUpdateValue("username", value);
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
        /// Whether the user has completed onboarding steps
        /// </summary>
        /// <value><c>true</c> if is onboarded; otherwise, <c>false</c>.</value>
        public static bool IsOnboarded
        {
            get
            {
                return ISettings.GetValueOrDefault("isonboarded", false);
            }
            set
            {
                ISettings.AddOrUpdateValue("isonboarded", value);
            }
        }

        /// <summary>
        /// Has a token from a previous login (which may no longer be valid)
        /// </summary>
        public static bool HasToken => ISettings.Contains("apitoken");

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
                string json = ISettings.GetValueOrDefault("qanswers", "");
                return JsonData.FromJsonString<List<long>>(json);
                //return JsonConvert.DeserializeObject<List<long>>(json, Converter.Settings);
            }
            set
            {
                if (value == null)
                {
                    ISettings.Remove("qanswers");
                }
                else
                {
                    ISettings.AddOrUpdateValue("qanswers", JsonData.AsJsonString(value));
                }
            }
        }

        /// <summary>
        /// Clear down the account
        /// </summary>
        public static void LogoutUser()
        {
            AppSettings.Password = "";
            AppSettings.IsOnboarded = false;
            AppSettings.StoredAnswers = null;
        }

    }
}

