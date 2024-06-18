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
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}