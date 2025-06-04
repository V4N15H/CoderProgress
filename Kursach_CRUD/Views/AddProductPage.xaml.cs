using Kursach_CRUD.Models;
using Kursach_CRUD.Services;

namespace Kursach_CRUD.Views;

public partial class AddProductPage : ContentPage
{
	public AddProductPage()
	{
		InitializeComponent();
	}

    private async void OnAddClicked(object sender, EventArgs e)
    {
        // Считываем поля
        var name = NameEntry.Text;
        var category = CategoryPicker.SelectedItem?.ToString();
        var quantityText = QuantityEntry.Text;
        var priceText = PriceEntry.Text;
        var serialNumbersRaw = SerialNumbersEditor.Text;

        // Базовая проверка
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(category) ||
            string.IsNullOrWhiteSpace(quantityText) ||
            string.IsNullOrWhiteSpace(priceText) ||
            string.IsNullOrWhiteSpace(serialNumbersRaw))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
            return;
        }

        if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
        {
            await DisplayAlert("Ошибка", "Неверное количество", "OK");
            return;
        }

        if (!decimal.TryParse(priceText, out decimal price) || price < 0)
        {
            await DisplayAlert("Ошибка", "Неверная цена", "OK");
            return;
        }

        // Разделяем введённые серийные номера по строкам
        var serialNumbers = serialNumbersRaw
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

        if (serialNumbers.Count != quantity)
        {
            await DisplayAlert("Ошибка", $"Количество серийных номеров ({serialNumbers.Count}) должно совпадать с указанным количеством ({quantity})", "OK");
            return;
        }

        // Сохраняем каждое изделие как отдельную запись
        foreach (var serial in serialNumbers)
        {
            var product = new Product
            {
                Name = name,
                Category = category,
                Quantity = 1, // каждая запись — 1 штука
                Price = price,
                SerialNumber = serial
            };

            await App.Database.AddProductAsync(product);
        }

        await DisplayAlert("Успех", "Комплектующие добавлены", "OK");
        await Navigation.PopAsync();
    }

}