using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VidaBox.Models;

namespace VidaBox.Views
{
    /// <summary>
    /// Logique d'interaction pour UserMesSeries.xaml
    /// </summary>
    public partial class UserMesSeries : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static UserMesSeries instance = null;
        public ObservableCollection<Serie> series { get; set; }
        private Serie _selectedSerie;
        private string _urlServer;
        public string UrlServer { get => _urlServer; set => _urlServer = value; }
        public Serie selectedSerie
        {
            get { return _selectedSerie; }
            set { _selectedSerie = value; NotifyPropertyChanged("selectedSerie"); }
        }

        /// <summary>
        /// Initialisation de l'objet UserLogin avec l'url du serveur
        /// </summary>
        /// <param name="urlServer"></param>
        /// <returns>L'instance de UserLogin</returns>
        public static UserMesSeries initInstance(string urlServer)
        {
            if (instance == null)
            {
                instance = new UserMesSeries();
            }
            instance.UrlServer = urlServer;
            return instance;
        }


        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static UserMesSeries getInstance()
        {

            if (instance == null)
            {
                throw new Exception("userLogin : appeler init instance avant getinstance");
            }
            return instance;
        }



        /// <summary>
        /// Permet de récupérer l'ensemble des films de la bibiothèque de l'utilisateur
        /// </summary>
        public void getAllSeriesFromUser()
        {
            instance.series.Clear();

            //REQUETE POUR RECUPERER TOUT LES FILM AJOUTER PAR L'UTILISATEUR CONNECTE
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("Id_User", UserLogin.getInstance().LoggedId_User.ToString());
            string json = ApiHelper.ApiGet(MainWindow.getInstance().urlApi + "api/Utilisateur_Serie/Get", datas);

            var result = JsonConvert.DeserializeObject<VidaboxSearch.RootObject>(json);

            if (result != null)
            {
                for (int i = 0; i < result.films.Count; i++)
                {
                    instance.series.Add(new Serie(
                        result.series[i].Id_Serie,
                        result.series[i].Titre,
                        result.series[i].Synopsis,
                        result.series[i].Date_sortie,
                        result.series[i].Url_affiche,
                        result.series[i].Url_bande_annonce,
                        result.series[i].Id_genre,
                        result.series[i].Id_dbmovie)

                     );
                }
            }
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


        public UserMesSeries()
        {
            InitializeComponent();
            series = new ObservableCollection<Serie>();
        }


        // GESTION DES EVENEMENTS //

        /// <summary>
        /// Redirige l'utilisateur vers sa bibiothèque de série
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Film_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Navigator.Navigate(UserMesSeries.getInstance());
        }

        /// <summary>
        /// Permet de déconnecter l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(UserLogin.getInstance());
        }

        /// <summary>
        /// Permet de retirer le film sélectionné de la bibiothèque de l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRetireSerie_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("Id_User", UserLogin.getInstance().LoggedId_User.ToString());
            datas.Add("Id_Serie", selectedSerie.Id_Serie.ToString());

            string json = ApiHelper.ApiDelete(MainWindow.getInstance().urlApi + "api/Utilisateur_Serie/Delete", datas);

            series.Clear();
            selectedSerie = null;

            instance.getAllSeriesFromUser();
        }

        /// <summary>
        /// Permet de mettre à jour les informations détaillé du film sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            movieDetails.Visibility = Visibility.Collapsed;
            int index = myListView.SelectedIndex;
            if (index >= 0 && index < series.Count)
            {
                selectedSerie = series[myListView.SelectedIndex];
                movieDetails.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Permet de rediriger l'utilisateur vers la page de recherche de contenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recherche_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Navigator.Navigate(UserContent.getInstance());
        }


    }
}

