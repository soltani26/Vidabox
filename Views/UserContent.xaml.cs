using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VidaBox.Models;
using VidaBox.ViewsModels;

namespace VidaBox.Views
{
    public partial class UserContent : UserControl, INotifyPropertyChanged
    {
        private static UserContent instance = null;
        private string _urlServer;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Movie> movies { get; set; }
        public ObservableCollection<Serie> series { get; set; }
        public string UrlServer { get => _urlServer; set => _urlServer = value; }
        private Movie _selectedMovie;
        private Serie _selectedSerie;
        public Movie selectedMovie
        {
            get { return _selectedMovie; }
            set { _selectedMovie = value; NotifyPropertyChanged("selectedMovie"); }
        }
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
        public static UserContent initInstance(string urlServer)
        {
            if (instance == null)
            {
                instance = new UserContent();
            }
            instance.UrlServer = urlServer;
            return instance;
        }

        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static UserContent getInstance()
        {
            if (instance == null)
            {
                throw new Exception("userLogin : appeler init instance avant getinstance");
            }
            return instance;
        }


        public UserContent()
        {
            InitializeComponent();

            movies = new ObservableCollection<Movie>();
            series = new ObservableCollection<Serie>();
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


        // GESTION DES EVENEMENTS ///

        /// <summary>
        /// Permet d'effectuer une recherche de contenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            UserContentViewModel.updateContentList(txtBox_Search.Text);
        }

        /// <summary>
        /// Permet d'appeler la fonction de recherche de contenu à partir de la touche entrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBox_Search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UserContentViewModel.updateContentList(txtBox_Search.Text);
        }

        /// <summary>
        /// Permet de déconnecter l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(UserLogin.getInstance());

            //clear des champs apres deconnexion
            TextBox txtMail = (TextBox)UserLogin.getInstance().FindName("txtmail");
            txtMail.Text = string.Empty;
            PasswordBox txtPassword = (PasswordBox)UserLogin.getInstance().FindName("txtPassword");
            txtPassword.Password = string.Empty;
        }

        /// <summary>
        /// Permet d'adapter les informations détaillés du film sélectionné
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
        /// Permet de rediriger l'utilisateur vers la liste de ses films
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MesFilms_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Navigator.Navigate(UserMesFilms.getInstance());
        }

        /// <summary>
        /// Permet de rediriger l'utilisateur vers la liste de ses séries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serie_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Navigator.Navigate(UserMesSeries.getInstance());
        }

        /// <summary>
        /// Permet d'ajouter le film sélectionné dans la bibliothèque de l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjoutFilm_Click(object sender, RoutedEventArgs e)
        {
            //SI UN FILM EST SELECTIONNE
            if (selectedMovie != null)
            {
                string LoggedId_User = UserLogin.getInstance().LoggedId_User.ToString();
                string Id_Films = selectedMovie.Id_Films.ToString();

                //SI JE SUIS BIEN CONNECTE
                if (LoggedId_User != "-1")
                {
                    Dictionary<string, string> datas = new Dictionary<string, string>();
                    datas.Add("Id_User", LoggedId_User);
                    datas.Add("Id_Films", Id_Films);
                    ApiHelper.ApiPost(MainWindow.getInstance().urlApi + "api/Utilisateur_Films/Add", datas);
                }
            }
        }

        
    }
}
