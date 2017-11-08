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
    [Register ("MenuView")]
    partial class MenuView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AboutButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DiaryButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ProfileButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ResearchButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AboutButton != null) {
                AboutButton.Dispose ();
                AboutButton = null;
            }

            if (DiaryButton != null) {
                DiaryButton.Dispose ();
                DiaryButton = null;
            }

            if (ProfileButton != null) {
                ProfileButton.Dispose ();
                ProfileButton = null;
            }

            if (ResearchButton != null) {
                ResearchButton.Dispose ();
                ResearchButton = null;
            }
        }
    }
}