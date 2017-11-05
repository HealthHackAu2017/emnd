// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Emnd.iOS
{
    [Register ("SurveyQuestionViewCell")]
    partial class SurveyQuestionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider AnswerSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MaxLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MinLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel QuestionLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnswerSlider != null) {
                AnswerSlider.Dispose ();
                AnswerSlider = null;
            }

            if (MaxLabel != null) {
                MaxLabel.Dispose ();
                MaxLabel = null;
            }

            if (MinLabel != null) {
                MinLabel.Dispose ();
                MinLabel = null;
            }

            if (QuestionLabel != null) {
                QuestionLabel.Dispose ();
                QuestionLabel = null;
            }
        }
    }
}