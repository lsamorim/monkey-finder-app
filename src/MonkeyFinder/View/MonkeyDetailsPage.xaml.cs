using Microsoft.Maui.Controls;
using MonkeyFinder.ViewModel;

namespace MonkeyFinder.View;

public partial class MonkeyDetailsPage : ContentPage
{
	public MonkeyDetailsPage(MonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}