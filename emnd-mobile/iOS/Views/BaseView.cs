using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using MvvmCross.Binding.iOS.Views;
using System.Collections.Generic;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.Threading.Tasks;
using Serilog;
using MvvmCross.Core.ViewModels;

namespace Emnd.iOS
{
    public class BaseView<TViewModel> : MvxViewController where TViewModel : class
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = (IMvxViewModel)value; }
        }

        public BaseView()
        {
        }

        public BaseView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            //    EdgesForExtendedLayout = UIRectEdge.None;
            //    View.BackgroundColor = UIColor.White;

            base.ViewDidLoad();

            // Dismiss the keyboard on background tap
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false;
            View.AddGestureRecognizer(g);
        }

        protected virtual void DismissKeyboard(UITextField textfield = null)
        {
            View.EndEditing(true);
            if (textfield != null)
            {
                if (textfield.CanResignFirstResponder) textfield.ResignFirstResponder();
            }
        }

        protected virtual void SetNavigationBarTitle(string title, bool usetitlecase = false)
        {
            Log.Information($"Setting navigation title {title}");
            try
            {
                if (!string.IsNullOrEmpty(title))
                {
                    NavigationController.NavigationBar.SetBackgroundImage(null, UIBarMetrics.Default);
                    this.NavigationItem.Title = usetitlecase ? System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title) : title;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SetNavigationBarTitle " + ex.StackTrace);
            }
        }
    }
}
