using System;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Plugin.Toasts;
using Serilog;
using Xamarin.Forms;

namespace Emnd
{
    public interface IAppNavigationService
    {
        void ToastMessage(string title, string message);
        void ShowAlert(string message, string title = "SHOWmndME");
        void ShowTodoInfo(string message, string phase = "Todo");
        void ShowError(string usermessage, string loggedmessage = null);
        void ShowAPIError(string usermessage, string loggedmessage = null);
        void Logout();
        void ShowHomePage();
    }

    public partial class AppNavigationService : IAppNavigationService
    {
        public void ToastMessage(string title, string message)
        {
            var notificator = Xamarin.Forms.DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = title,
                Description = message
            };
            //options.AndroidOptions.SmallDrawableIcon = Resource.Drawable.rqlogo;

            Device.BeginInvokeOnMainThread(() =>
            {
                notificator.Notify(options);
            });
        }

        public void Logout()
        {
            AppSettings.LogoutUser();
        }

        public void ShowError(string usermessage, string loggedmessage = null)
        {
            if (loggedmessage == null)
            {
                loggedmessage = usermessage;
            }
            Log.Error(loggedmessage);
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    UserDialogs.Instance.Alert(usermessage, "Error");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            });
        }

        public void ShowAPIError(string usermessage, string loggedmessage = null)
        {
            if (loggedmessage == null)
            {
                loggedmessage = usermessage;
            }
            Log.Error(loggedmessage);
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                        UserDialogs.Instance.Alert(usermessage, "API Error");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            });
        }

        public void ShowAlert(string message, string title = "SHOWmndME")
        {
            Log.Information($"ShowAlert: {title} {message}");
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    UserDialogs.Instance.Alert(message, title);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            });

        }

        public void ShowTodoInfo(string message, string title = "Todo")
        {
            Log.Information($"Todo info: {title} {message}");
            Device.BeginInvokeOnMainThread(() =>
            {
                try       
                {
                    UserDialogs.Instance.Alert(message, title);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            });

        }

        public void ShowHomePage()
        {
            Plugin.Share.CrossShare.Current.OpenBrowser("http://www.emnd.com.au");
        }

    }
}
