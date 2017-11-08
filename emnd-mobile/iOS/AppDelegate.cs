using System;
using System.Diagnostics;
using System.IO;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using Foundation;
using UIKit;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Push;
using System.Collections.Generic;
using Plugin.Toasts;
using UserNotifications;

namespace Emnd.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        public static AppDelegate Application
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#if QA
            string loggingPath = "";
            try
            {
                loggingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LogFiles");
                if (!Directory.Exists(loggingPath))
                {
                    Directory.CreateDirectory(loggingPath);
                }
                AppSettings.LastLogFile = Path.Combine(loggingPath, "Log-" + DateTime.Today.ToString("yyyyMMdd") + ".txt");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Could not create logging folder - permissions? {0}", ex.Message);
            }
#endif
            Log.Logger = new LoggerConfiguration()
#if QA
                .WriteTo.RollingFile(Path.Combine(loggingPath, "Log-{Date}.txt"), outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.NSLog()
#endif
                .CreateLogger();

            Log.Information("AppDelegate is launched, initialising Forms.");

            if (AppConstants.UseAzure)
            {
                MobileCenter.Start(AppConstants.MobileCenter_iOS, typeof(Analytics), typeof(Crashes), typeof(Push));
            }

            //Display measuring
            Emnd.DisplayInfo.Init(new Emnd.iOS.DisplayInfo());

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            Xamarin.Forms.Forms.Init();

            // Allow crossplatform toasts
            Xamarin.Forms.DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init();

            Emnd.App.Navigation = new Emnd.AppNavigationService();

            //MVX stuff
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            Window.MakeKeyAndVisible();

            // Request Local Notification Permission for toast
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}
