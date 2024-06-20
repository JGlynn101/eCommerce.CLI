using Amazon.Library.Services;
using AmazonApp.ViewModels;

namespace AmazonApp.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView() 
	{
        InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
	}
    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.updateItem();
          
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Item");
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel).RefreshItems();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}