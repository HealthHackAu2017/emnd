using UIKit;
using Emnd;

namespace Emnd.iOS
{
    public class DisplayInfo : IDisplayInfo
    {
        public int Height()
        {
            return (int)UIScreen.MainScreen.Bounds.Height;
        }

        public int Width()
        {
            return (int)UIScreen.MainScreen.Bounds.Width;
        }

        public int Scale()
        {
            return (int)UIScreen.MainScreen.Scale;
        }

        public string AppVersion => Foundation.NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
        public string BuildNumber => Foundation.NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

		public string VersionAndBuildNumber
		{
			get
			{
				return AppVersion + "." + BuildNumber;
			}
		}

	}
}