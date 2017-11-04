using System;

namespace Emnd
{
    public class AppConstants
    {
        public const string MobileCenter_iOS = "";
        public static bool UseAzure => !string.IsNullOrWhiteSpace(MobileCenter_iOS);

        public const string EmndServer = "http://192.168.0.2:5000/api/v1";

        public AppConstants()
        {
        }

    }
}
