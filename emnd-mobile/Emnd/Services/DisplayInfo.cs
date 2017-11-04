namespace Emnd
{
    public interface IDisplayInfo
    {
        int Width();
        int Height();
        int Scale();
        string AppVersion { get; }
        string BuildNumber { get; }
		string VersionAndBuildNumber { get; }
    }

    public static class DisplayInfo
    {
        public static int Width => Get.Width();
        public static int Height => Get.Height();
        public static int Scale => Get.Scale();
        public static string AppVersion => Get.AppVersion;
		public static string BuildNumber => Get.BuildNumber;
		public static string VersionAndBuildNumber => Get.VersionAndBuildNumber;


		static IDisplayInfo Get { get; set; }
        public static void Init(IDisplayInfo displayInfo)
        {
            Get = displayInfo;
            System.Diagnostics.Debug.WriteLine(string.Format("DisplayInfo.Init {0}x{1} ({2}x)", Width, Height, Scale));
        }
	}
}