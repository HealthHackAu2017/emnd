using System;

using UIKit;

namespace Emnd.iOS
{
    public partial class SurveyListView : UIViewController
    {
        public SurveyListView() : base("SurveyListView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

