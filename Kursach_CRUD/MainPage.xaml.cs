using Kursach_CRUD.Views;

namespace Kursach_CRUD
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductPage());
        }

        private async void OnViewProductsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ProductListPage());
        }

    }

}
