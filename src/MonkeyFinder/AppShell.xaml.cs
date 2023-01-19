using Microsoft.Maui.Controls;
using MonkeyFinder.View;

namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MonkeyDetailsPage), typeof(MonkeyDetailsPage));
	}
}
