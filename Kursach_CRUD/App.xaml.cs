using Kursach_CRUD.Services;

namespace Kursach_CRUD
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "products.db3");
            Database = new DatabaseService(dbPath);

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
