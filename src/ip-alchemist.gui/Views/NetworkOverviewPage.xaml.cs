using ip_alchemist.gui.ViewModels;

namespace ip_alchemist.gui.Views;

public partial class NetworkOverviewPage : ContentPage
{
	public NetworkOverviewPage()
	{
		InitializeComponent();
		BindingContext = new NetworkOverviewViewModel();
	}
}