using Kursach_CRUD.Models;
using Kursach_CRUD.Services;

namespace Kursach_CRUD.Views;

public partial class EditProductPage : ContentPage
{
    private List<Product> _productsToEdit;

    public EditProductPage(GroupedProduct groupedProduct)
    {
        InitializeComponent();

        // Загружаем из базы все экземпляры по Name + Category + Price
        LoadProductGroupAsync(groupedProduct);
    }

    private async void LoadProductGroupAsync(GroupedProduct groupedProduct)
    {
        _productsToEdit = await App.Database.GetProductsByNameCategoryPriceAsync(
            groupedProduct.Name, groupedProduct.Category, groupedProduct.Price);

        // Заполняем поля значениями
        NameEntry.Text = groupedProduct.Name;
        CategoryEntry.Text = groupedProduct.Category;
        PriceEntry.Text = groupedProduct.Price.ToString();
        SerialsEditor.Text = string.Join(", ", groupedProduct.SerialNumbers);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_productsToEdit == null || !_productsToEdit.Any()) return;

        string newName = NameEntry.Text?.Trim();
        string newCategory = CategoryEntry.Text?.Trim();
        bool parsed = decimal.TryParse(PriceEntry.Text, out decimal newPrice);
        string[] newSerials = SerialsEditor.Text?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newCategory) || !parsed || newSerials.Length != _productsToEdit.Count)
        {
            await DisplayAlert("Ошибка", "Проверьте правильность заполнения всех полей. Кол-во серийников должно совпадать с количеством товаров.", "OK");
            return;
        }

        for (int i = 0; i < _productsToEdit.Count; i++)
        {
            var product = _productsToEdit[i];
            product.Name = newName;
            product.Category = newCategory;
            product.Price = newPrice;
            product.SerialNumber = newSerials[i];

            await App.Database.UpdateProductAsync(product);
        }

        await DisplayAlert("Готово", "Товары успешно обновлены", "OK");
        await Navigation.PopAsync();
    }
}