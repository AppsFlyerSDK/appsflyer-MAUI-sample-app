namespace MyMAUITest;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnVersionClicked(object sender, EventArgs e)
	{
		var ver = AppsFlyerLibImpl.GetInstance().SDKVersion();
		Console.WriteLine($"the version is {ver}");
	}

    private void OnLogEventClicked(object sender, EventArgs e)
    {
		Console.WriteLine("AppsFlyer log event...");
        AppsFlyerLibImpl.GetInstance().LogEvent("af_purchase", new Dictionary<string, string>
		{
			{"key", "value"}
		});
    }
}


