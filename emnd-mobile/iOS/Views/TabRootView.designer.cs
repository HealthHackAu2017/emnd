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
    [Register ("TabRootView")]
    partial class TabRootView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBar TabRootTabBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (TabRootTabBar != null) {
                TabRootTabBar.Dispose ();
                TabRootTabBar = null;
            }
        }
    }
}