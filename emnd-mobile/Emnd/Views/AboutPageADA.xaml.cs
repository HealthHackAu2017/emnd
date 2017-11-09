using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Serilog;
using Plugin.DeviceInfo;
using Plugin.Messaging;

namespace Emnd
{
    public partial class AboutPageADA : ContentPage
    {
        public AboutPageADA()
        {
            InitializeComponent();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                //Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
                //FormsPlugin.Iconize. Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            }

            string appVersionInfo = DisplayInfo.AppVersion;
#if QA
            appVersionInfo = DisplayInfo.VersionAndBuildNumber;
#endif
#if DEBUG
            appVersionInfo += " [DEBUG]";
#endif
            VersionTextCell.Detail = appVersionInfo;


            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnLegalAndPrivacyTapped;
            LegalAndPrivacy.GestureRecognizers.Add(tapGestureRecognizer);

            var tapEmailGestureRecognizer = new TapGestureRecognizer();
            tapEmailGestureRecognizer.Tapped += OnEmailTapped;
            EmailSupport.GestureRecognizers.Add(tapEmailGestureRecognizer);

            var tapWwwGestureRecognizer = new TapGestureRecognizer();
            tapWwwGestureRecognizer.Tapped += (sender, e) =>
            {
                App.Navigation.ShowHomePage();
            };
            WwwLabel.GestureRecognizers.Add(tapWwwGestureRecognizer);

        }

        void OnLegalAndPrivacyTapped(object sender, System.EventArgs e)
        {
            App.Navigation.ShowHomePage();
        }

        void OnCrashTapped(object sender, System.EventArgs e)
        {
            Log.Information("Testing a crash");
            Device.BeginInvokeOnMainThread(() =>
            {
                if (AppConstants.UseAzure)
                {
                    Microsoft.Azure.Mobile.Crashes.Crashes.GenerateTestCrash();
                }
            });
        }

        void OnEmailTapped(object sender, System.EventArgs e)
        {
            string deviceUID = DependencyService.Get<IDeviceService>().DeviceIdentifier();
            string appPlatform = Device.RuntimePlatform == Device.Android ? Device.Android.ToString() : Device.iOS.ToString();
            string appVersion = DisplayInfo.VersionAndBuildNumber;
            string appOS = CrossDeviceInfo.Current.Version;
            string appModel = CrossDeviceInfo.Current.Model;
            Log.Information("==============================================");
            Log.Information("=== SUPPORT REQUEST ===");
            Log.Information($"Device: {appModel} OS: {appPlatform} {appOS}");
            Log.Information($"Version: {appVersion} UID: {deviceUID}");

            var errorMessage = string.Empty;
            if (AppSettings.LastLogFile == string.Empty)
            {
                errorMessage = "No log files found";
            }
            else
            {
                try
                {
                    Log.Information($"Attach log file request {AppSettings.LastLogFile}");
                    //Log.CloseAndFlush();

                    var emailMessenger = CrossMessaging.Current.EmailMessenger;
                    if (emailMessenger.CanSendEmailAttachments)
                    {
                        // Construct email with attachment on Android.
                        Log.Information("Building sent");
 
                        var email = new EmailMessageBuilder()
                          .To("jholloway@gmail.com")
                          .Subject("Support request").Body("Log files for " + AppSettings.ParticipantName + "\n\nPlease describe the issue you are experiencing:\n")
                              .WithAttachment(AppSettings.LastLogFile, "text/*")
                              .Build();

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Log.Information("Sending email");
                            emailMessenger.SendEmail(email);
                        });
                    }
                    else
                    {
                        errorMessage = "Your device can't send email attachments.";
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }

            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                App.Navigation.ShowError(errorMessage);
            }
        }
    }
}
