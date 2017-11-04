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
    [Register ("SurveyEntryView")]
    partial class SurveyEntryView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider ComparisonSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MyLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider MySlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ParticipantIdEntry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ParticipantNameEntry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SendButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider SleepSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField WeightEntry { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ComparisonSlider != null) {
                ComparisonSlider.Dispose ();
                ComparisonSlider = null;
            }

            if (MainView != null) {
                MainView.Dispose ();
                MainView = null;
            }

            if (MyLabel != null) {
                MyLabel.Dispose ();
                MyLabel = null;
            }

            if (MySlider != null) {
                MySlider.Dispose ();
                MySlider = null;
            }

            if (ParticipantIdEntry != null) {
                ParticipantIdEntry.Dispose ();
                ParticipantIdEntry = null;
            }

            if (ParticipantNameEntry != null) {
                ParticipantNameEntry.Dispose ();
                ParticipantNameEntry = null;
            }

            if (SendButton != null) {
                SendButton.Dispose ();
                SendButton = null;
            }

            if (SleepSlider != null) {
                SleepSlider.Dispose ();
                SleepSlider = null;
            }

            if (WeightEntry != null) {
                WeightEntry.Dispose ();
                WeightEntry = null;
            }
        }
    }
}