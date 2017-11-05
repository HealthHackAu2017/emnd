using System;
using Xamarin.Forms;
namespace Emnd
{
    public class LineEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create<LineEntry, Color>(p => p.BorderColor, Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        new public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<LineEntry, double>(p => p.FontSize, Font.Default.FontSize);

        new public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        new public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<LineEntry, Color>(p => p.PlaceholderColor, Color.Default);

        new public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
    }
}
