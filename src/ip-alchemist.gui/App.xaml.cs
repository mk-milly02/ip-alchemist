namespace ip_alchemist.gui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

#if WINDOWS
        window.MaximumWidth = 900;
        window.MinimumWidth = 900;
        window.MaximumHeight = 650;
        window.MinimumHeight = 650;

        //get display size
        DisplayInfo info = DeviceDisplay.Current.MainDisplayInfo;

        //center the window
        window.X = (info.Width / info.Density - window.Width) / 2;
        window.Y = (info.Height / info.Density - window.Height) / 2;
#endif

        return window;
    }
}
