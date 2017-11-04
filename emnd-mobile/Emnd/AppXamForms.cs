using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//
// This page is displayed from within 'native' pages on iOS and Android
//
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Emnd
{
    public class AppXamForms : Xamarin.Forms.Application
    {

        public static AppXamForms XFormsApp { get; set; }
 
        public AppXamForms()
        {
            XFormsApp = this;
        }

        public void ShowAboutPage()
        {
            if (XFormsApp == null) {
                XFormsApp = new AppXamForms();
            }
            
            Current.MainPage = new NavigationPage(new AboutPageADA());
        }
    }
}
