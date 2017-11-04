// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Emnd.iOS
{
    [Register ("BodyView")]
    partial class BodyView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MainView != null) {
                MainView.Dispose ();
                MainView = null;
            }
        }
    }
}