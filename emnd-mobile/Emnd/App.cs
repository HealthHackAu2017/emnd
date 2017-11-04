using Acr.UserDialogs;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Xamarin.Forms.Xaml;
using Splat;

namespace Emnd
{
    public class App: MvvmCross.Core.ViewModels.MvxApplication
    {
        public static App Instance;
        public static IAppNavigationService Navigation { get; set; }
        public IMvxNavigationService _nav;
        public static bool IsForeground;

        public override void Initialize()
        {
            Instance = this;
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            RegisterNavigationServiceAppStart<TabRootViewModel>();

            Locator.CurrentMutable.Register(() => new Cache(), typeof(ICache));
            Locator.CurrentMutable.Register(() => new NetworkService(), typeof(INetworkService));
        }

        void SetupIOC()
        {
        }

    }
}
