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
        // ��������� ����
        var name = NameEntry.Text;
        var category = CategoryPicker.SelectedItem?.ToString();
        var quantityText = QuantityEntry.Text;
        var priceText = PriceEntry.Text;
        var serialNumbersRaw = SerialNumbersEditor.Text;

        // ������� ��������
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(category) ||
            string.IsNullOrWhiteSpace(quantityText) ||
            string.IsNullOrWhiteSpace(priceText) ||
            string.IsNullOrWhiteSpace(serialNumbersRaw))
        {
            await DisplayAlert("������", "����������, ��������� ��� ����", "OK");
            return;
        }

        if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
        {
            await DisplayAlert("������", "�������� ����������", "OK");
            return;
        }

        if (!decimal.TryParse(priceText, out decimal price) || price < 0)
        {
            await DisplayAlert("������", "�������� ����", "OK");
            return;
        }

        // ��������� �������� �������� ������ �� �������
        var serialNumbers = serialNumbersRaw
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

        if (serialNumbers.Count != quantity)
        {
            await DisplayAlert("������", $"���������� �������� ������� ({serialNumbers.Count}) ������ ��������� � ��������� ����������� ({quantity})", "OK");
            return;
        }

        // ��������� ������ ������� ��� ��������� ������
        foreach (var serial in serialNumbers)
        {
            var product = new Product
            {
                Name = name,
                Category = category,
                Quantity = 1, // ������ ������ � 1 �����
                Price = price,
                SerialNumber = serial
            };

            await App.Database.AddProductAsync(product);
        }

        await DisplayAlert("�����", "������������� ���������", "OK");
        await Navigation.PopAsync();
    }

}