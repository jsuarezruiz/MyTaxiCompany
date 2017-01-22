namespace MyTaxiCompany02.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init(GlobalSetting.BingMapsAPIKey);

            LoadApplication(new MyTaxiCompany02.App());
        }
    }
}
