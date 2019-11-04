using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VidaBox.Models;

namespace VidaBox.Views
{
    /// <summary>
    /// Page affichant la page de connexion
    /// </summary>
    public partial class UserLogin : UserControl
    {
        private static UserLogin instance = null;
        public int LoggedId_User = -1;
        public string UrlServer { get; private set; }

        public UserLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialisation de l'objet UserLogin avec l'url du serveur
        /// </summary>
        /// <param name="urlServer"></param>
        /// <returns>L'instance de UserLogin</returns>
        public static UserLogin initInstance(string urlServer)
        {
            if (instance == null)
            {
                instance = new UserLogin();
            }
            instance.UrlServer = urlServer;
            return instance;
        }

        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static UserLogin getInstance()
        {

            if (instance == null)
            {
                throw new Exception("userLogin : appeler init instance avant getinstance");
            }
            return instance;
        }

        
       // GESTION DES EVENEMENTS //

        /// <summary>
        /// Permet de se connecter à sa bibiothèque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            LoggedId_User = -1;

            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("email", txtmail.Text);
            datas.Add("motPasse", txtPassword.Password);

            // Appeler l'api pour vérifier les données de connexion
            var resultJson = ApiHelper.ApiGet(UrlServer + "api/Users", datas);
            Users userConnect = JsonConvert.DeserializeObject<Users>(resultJson);

            if (userConnect != null)
            {
                LoggedId_User = userConnect.Id_user;
                Navigator.Navigate(UserContent.getInstance());
            }
            else
                MessageBox.Show("l'email et/ou le mot de passe sont incorrect");
                
        }

        /// <summary>
        /// Permet de rediriger l'utilisateur vers la page d'inscription
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationCompte_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(UserInscription.getInstance());
        }

        /// <summary>
        /// Permet d'appeler la fonction qui redirige vers la page d'inscription en appuyant sur entrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Connexion_Click(null, null);
        }
    }
}
