using Microsoft.Maui.Controls;
using MonkeyFinder.ViewModel;

namespace MonkeyFinder.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Button_Clicked(object sender, System.EventArgs e)
    {
		Shell.Current.DisplayAlert("Test", "Click", "ok");
    }
}

