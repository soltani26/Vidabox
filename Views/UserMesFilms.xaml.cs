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
    /// Logique d'interaction pour UserMesFilms.xaml
    /// </summary>
    public partial class UserMesFilms : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static UserMesFilms instance = null;
        public ObservableCollection<Movie> movies { get; set; }
        private Movie _selectedMovie;
        private string _urlServer;
        public string UrlServer { get => _urlServer; set => _urlServer = value; }
        public Movie selectedMovie
        {
            get { return _selectedMovie; }
            set { _selectedMovie = value; NotifyPropertyChanged("selectedMovie"); }
        }


        /// <summary>
        /// Initialisation de l'objet UserLogin avec l'url du serveur
        /// </summary>
        /// <param name="urlServer"></param>
        /// <returns>L'instance de UserLogin</returns>
        public static UserMesFilms initInstance(string urlServer)
        {
            if (instance == null)
            {
                instance = new UserMesFilms();
            }
            instance.UrlServer = urlServer;
            return instance;
        }


        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static UserMesFilms getInstance()
        {

            if (instance == null)
            {
                throw new Exception("userLogin : appeler init instance avant getinstance");
            }

            instance.getAllFilmsFromUser();

            return instance;
        }
 

        /// <summary>
        /// Permet de récupérer l'ensemble des films de la bibiothèque de l'utilisateur
        /// </summary>
        public void getAllFilmsFromUser()
        {
            instance.movies.Clear();

            //REQUETE POUR RECUPERER TOUT LES FILM AJOUTER PAR L'UTILISATEUR CONNECTE
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("Id_User", UserLogin.getInstance().LoggedId_User.ToString());
            string json = ApiHelper.ApiGet(MainWindow.getInstance().urlApi + "api/Utilisateur_Films/Get", datas);

            var result = JsonConvert.DeserializeObject<VidaboxSearch.RootObject>(json);

            if (result != null)
            {
                for (int i = 0; i < result.films.Count; i++)
                {
                    instance.movies.Add(new Movie(
                        result.films[i].Id_Films,
                        result.films[i].Titre,
                        result.films[i].synopsis,
                        result.films[i].dateSortie,
                        result.films[i].duree,
                        result.films[i].Url_Affiche,
                        result.films[i].url_BandeAnnoce,
                        result.films[i].Id_genre));
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

        
        public UserMesFilms()
        {
            InitializeComponent();
            movies = new ObservableCollection<Movie>();
        }


        // GESTION DES EVENEMENTS //

        /// <summary>
        /// Redirige l'utilisateur vers sa bibiothèque de série
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serie_PreviewMouseDown(object sender, MouseButtonEventArgs e)
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
        private void BtnRetireFilm_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("Id_User", UserLogin.getInstance().LoggedId_User.ToString());
            datas.Add("Id_Films", selectedMovie.Id_Films.ToString());

            string json = ApiHelper.ApiDelete(MainWindow.getInstance().urlApi + "api/Utilisateur_Films/Delete", datas);

            movies.Clear();
            selectedMovie = null;

            instance.getAllFilmsFromUser();
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
            if (index >= 0 && index < movies.Count)
            {
                selectedMovie = movies[myListView.SelectedIndex];
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
