using Kursach_CRUD.Models;

namespace Kursach_CRUD.Views;

public partial class ProductListPage : ContentPage
{
	public ProductListPage()
	{
		InitializeComponent();
	}


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var products = await App.Database.GetAllProductsAsync();

        // Группируем
        var grouped = products
            .GroupBy(p => new { p.Name, p.Category, p.Price })
            .Select(g => new GroupedProduct
            {
                Name = g.Key.Name,
                Category = g.Key.Category,
                Price = g.Key.Price,
                SerialNumbers = g.Select(p => p.SerialNumber).ToList()
            })
            .ToList();

        ProductCollection.ItemsSource = grouped;
    }

    private void OnToggleSerialsClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is GroupedProduct product)
        {
            product.IsExpanded = !product.IsExpanded;
        }
    }

    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var groupedProduct = button?.BindingContext as GroupedProduct;

        if (groupedProduct == null) return;

        bool confirm = await DisplayAlert(
            "Удаление",
            $"Удалить все экземпляры \"{groupedProduct.Name}\"?",
            "Да", "Нет");

        if (confirm)
        {
            var allMatching = await App.Database.GetProductsByNameCategoryPriceAsync(
                groupedProduct.Name, groupedProduct.Category, groupedProduct.Price);

            foreach (var item in allMatching)
            {
                await App.Database.DeleteProductAsync(item);
            }

            await DisplayAlert("Успех", "Удалено", "OK");
            var products = await App.Database.GetAllProductsAsync();

            var grouped = products
                .GroupBy(p => new { p.Name, p.Category, p.Price })
                .Select(g => new GroupedProduct
                {
                    Name = g.Key.Name,
                    Category = g.Key.Category,
                    Price = g.Key.Price,
                    SerialNumbers = g.Select(p => p.SerialNumber).ToList()
                })
                .ToList();

            ProductCollection.ItemsSource = grouped;
        }

    }

    private async void OnEditProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var groupedProduct = button?.BindingContext as GroupedProduct;

        if (groupedProduct != null)
        {
            await Navigation.PushAsync(new EditProductPage(groupedProduct));
        }
    }


}