using ip_alchemist.gui.ViewModels;

namespace ip_alchemist.gui.Views;

public partial class FLSMPage : ContentPage
{
	public FLSMPage()
	{
		InitializeComponent();
		BindingContext = new FLSMViewModel();
	}
}