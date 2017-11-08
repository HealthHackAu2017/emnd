using System;

namespace Emnd
{
    public class AppConstants
    {
        public const string MobileCenter_iOS = "";
        public static bool UseAzure => !string.IsNullOrWhiteSpace(MobileCenter_iOS);

        public const string EmndServer = "http://10.243.125.179:8000";

        public AppConstants()
        {
        }

    }
}
