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
    [Register ("SurveySectionView")]
    partial class SurveySectionView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DoneButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView QuestionTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SectionInfoLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SkipButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DoneButton != null) {
                DoneButton.Dispose ();
                DoneButton = null;
            }

            if (MainView != null) {
                MainView.Dispose ();
                MainView = null;
            }

            if (QuestionTable != null) {
                QuestionTable.Dispose ();
                QuestionTable = null;
            }

            if (SectionInfoLabel != null) {
                SectionInfoLabel.Dispose ();
                SectionInfoLabel = null;
            }

            if (SkipButton != null) {
                SkipButton.Dispose ();
                SkipButton = null;
            }
        }
    }
}