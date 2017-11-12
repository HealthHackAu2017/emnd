using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(ProgressBar), typeof(Emnd.iOS.CustomProgressBar))]
namespace Emnd.iOS
{
    public class CustomProgressBar : ProgressBarRenderer
    {
        public CustomProgressBar()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            Control.ProgressTintColor = UIColor.Red;
        }

        public override void LayoutSubviews()
        {
            CGAffineTransform transform = CGAffineTransform.MakeScale(1.5f, 12.0f);
            transform.TransformSize(this.Frame.Size);
            this.Transform = transform;
        }
    }

}
