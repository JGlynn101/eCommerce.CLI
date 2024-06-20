using AmazonApp.ViewModels;

namespace AmazonApp.Views;

public partial class ItemView : ContentPage
{
	public ItemView()
	{
		InitializeComponent();
	}

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ItemViewModel).Add(); 
        Shell.Current.GoToAsync("//Inventory");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel();
    }
}