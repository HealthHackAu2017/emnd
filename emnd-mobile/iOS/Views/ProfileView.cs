using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace Emnd.iOS
{
    [MvxFromStoryboard("ProfileView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "Account", TabName = "Account")]
    public partial class ProfileView : MvxTableViewController<ProfileViewModel>
    {
        public ProfileView(IntPtr handle) : base(handle)
        {
        }
    }
}
