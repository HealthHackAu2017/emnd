using System;
using System.IO;
using Xamarin.Forms;
using Serilog;
using UIKit;
using Foundation;
using QuickLook;
using Emnd;

[assembly: Xamarin.Forms.Dependency(typeof(Emnd.iOS.DeviceService))]
namespace Emnd.iOS
{
    public class DeviceService : IDeviceService
    {
        /// <summary>
        /// device id used for push notification subscription
        /// </summary>
        /// <returns></returns>
        public string DeviceIdentifier()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }

        public string GetFullPath(string folderPath, string filename)
        {
            string str = string.Empty;

            try
            {
                string documentsPath = "";
                documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + folderPath;
                if (filename.Trim() == "") return documentsPath;

                string filePath = Path.Combine(documentsPath, filename);

                if (Directory.Exists(documentsPath) == false)
                {
                    Directory.CreateDirectory(documentsPath);
                }

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                fs.Close();

                str = filePath;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
            }

            return str;
        }

        public bool AppendBytes(string folderPath, string fileName, byte[] bytes)
        {
            bool retVal = false;
            try
            {
                string filePath = GetFullPath(folderPath, fileName);
                Log.Information("Writing file to {0}", filePath);
                using (var f = File.Open(filePath, FileMode.Append))
                {
                    f.Write(bytes, 0, bytes.Length);
                }
                retVal = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
            }
            return retVal;
        }

        public bool DeleteFile(string folderPath, string filename)
        {
            bool retVal = false;
            try
            {
                string filePath = GetFullPath(folderPath, filename);
                File.Delete(filePath);
                retVal = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
            }
            return retVal;
        }

        public double CalculateWidth(string text)
        {
            double length = 0;

            try
            {
                var uiLabel = new UILabel();
                uiLabel.Text = text;
                length = uiLabel.Text.StringSize(uiLabel.Font).Width;
                return length;
            }
            catch (Exception ex)
            {
#if DEBUG                
                Log.Error("Could not calculate label width " + ex.Message);
#endif
            }
            return length;
        }


        public string OpenLocalFile(string filePath)
        {
            string retval = "";
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    bool Exists = new FileInfo(filePath).Exists;
                    Log.Information("Opening downloaded pdf (exists={0}) at {1} ", Exists, filePath);
                    if (Exists)
                    {
                        string name = Path.GetFileName(filePath);
                        QLPreviewItemFileSystem prevItem = new QLPreviewItemFileSystem(name, filePath);
                        UIApplication.SharedApplication.InvokeOnMainThread(() =>
                        {
                            QLPreviewController qlpreview = new QLPreviewController();
                            QLPreviewController previewController = new QLPreviewController();
                            previewController.DataSource = new PreviewControllerDS(prevItem);
                            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(
                                previewController, true, null);
                        });
                    }
                    else
                    {
                        retval = "File is inaccessible or did not complete downloading";
                    }
                }
                catch (Exception ex)
                {
                    retval = "Error " + ex.Message;
                }
            });
            return retval;
        }

        public class PreviewControllerDS : QLPreviewControllerDataSource
        {
            private QLPreviewItem _item;

            public PreviewControllerDS(QLPreviewItem item)
            {
                _item = item;
            }

            public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
            {
                return _item;
            }

            public override nint PreviewItemCount(QLPreviewController controller)
            {
                return 1;
            }
        }

        public class QLPreviewItemFileSystem : QLPreviewItem
        {
            string _fileName, _filePath;

            public QLPreviewItemFileSystem(string fileName, string filePath)
            {
                _fileName = fileName;
                _filePath = filePath;
            }

            public override string ItemTitle
            {
                get { return _fileName; }
            }

            public override NSUrl ItemUrl
            {
                get { return NSUrl.FromFilename(_filePath); }
            }
        }
    }
}