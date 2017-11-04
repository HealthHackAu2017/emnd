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
    [Register ("SurveyListView")]
    partial class SurveyListView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SurveyHeaderLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SurveyHeaderLabel != null) {
                SurveyHeaderLabel.Dispose ();
                SurveyHeaderLabel = null;
            }
        }
    }
}