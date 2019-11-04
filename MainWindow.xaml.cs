using System.Windows;
using VidaBox.Models;
using VidaBox.Views;

namespace VidaBox
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // url de l'api
        public string urlApi = @"http://localhost:53264/";

        private static MainWindow instance = null;

        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static MainWindow getInstance()
        {
            return instance;
        }

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            UserLogin.initInstance(urlApi);
            UserContent.initInstance(urlApi);
            UserInscription.initInstance(urlApi);
            UserMesFilms.initInstance(urlApi);
            UserMesSeries.initInstance(urlApi);

            Navigator.Navigate(UserLogin.getInstance());
        
        }

        
    }
}
