using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VidaBox.Models;


namespace VidaBox.Views
{
    /// <summary>
    /// Logique d'interaction pour UserInscription.xaml
    /// </summary>
    public partial class UserInscription : UserControl
    {
        private static UserInscription instance = null;
        private string _urlServer;
        public string UrlServer { get => _urlServer; set => _urlServer = value; }

        /// <summary>
        /// Initialisation de l'objet UserLogin avec l'url du serveur
        /// </summary>
        /// <param name="urlServer"></param>
        /// <returns>L'instance de UserLogin</returns>
        public static UserInscription initInstance(string urlServer)
        {
            if (instance == null)
            {
                instance = new UserInscription();
            }
            instance.UrlServer = urlServer;
            return instance;
        }


        /// <summary>
        /// Récuoère l'instance de l'objet courant
        /// </summary>
        /// <returns></returns>
        public static UserInscription getInstance()
        {
            if (instance == null)
            {
                throw new Exception("userLogin : appeler init instance avant getinstance");
            }
            return instance;
        }


        /// <summary>
        /// initialisation de la base de donnée
        /// </summary>
        public UserInscription()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Valide l'inscription en cliquant sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnInscription_Click(object sender, RoutedEventArgs e)
        {
            var nom = InputNom.Text;
            var prenom = InputPrenom.Text;
            var DateNaissance = InputDateNaissance.Text;
            var mail = InputEmail.Text;
            var mdp = InputMdp.Password;
            var mdpConfirm = InputMdpConfirm.Password;
            
            // Regex verification
            String regexNom = @"^[a-zA-Z]+[ \-']?[[a-zéêâôî]+[ \-ç']?]*[a-z]+$";
            String regexPrenom = @"^[a-zA-Z]+[ \-']?[[a-zéêâôî]+[ \-ç']?]*[a-z]+$";
            String regexDate = @"^([0-3][0-9])\/([0-9]{2,2})\/([0-9]{4,4})$";
            String regexEmail = @"^[^W][.a-zA-Z0-9_]+(.[a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*.[a-zA-Z]{2,4}$";


            // Si l'un des champs n'est pas rempli, alors afficher un message à l'utilisateur lui indiquant que tous les champs doivent être rempli
            if (nom == string.Empty || prenom == string.Empty || DateNaissance == string.Empty || mail == string.Empty || mdp == string.Empty || mdpConfirm == string.Empty)
            {
                InfoUtilisateur.Text = "Tous les champs sont obligatoires, Merci de les remplir";
            }
            else
            {
                // Vérification des données entrées par l'utlisateur
                var erreurMail = afficheErreur(verificationChamp(regexEmail, mail), InfoMail);
                var erreurNom = afficheErreur(verificationChamp(regexNom, nom), InfoNom);
                var erreurPrenom = afficheErreur(verificationChamp(regexPrenom, prenom), InfoPrenom);
                var erreurDate = afficheErreur(verificationChamp(regexDate, DateNaissance), InfoDate);

                if (!mdp.Equals(mdpConfirm))
                    InfoMdp.Text = "Les mots de passe doivent être identiques";
                else
                    InfoMdp.Text = string.Empty;

                // si les données sont correctes
                if (erreurMail && erreurNom && erreurPrenom && erreurDate && mdp.Equals(mdpConfirm))
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();

                   string connectionString = @"Data Source=sana;Initial Catalog=Vidaboxnew; User ID=sa;Password=""VidaboxAfip2019?;""";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {

                        conn.Open();
                        string rq = "INSERT INTO Utilisateur  (nom, prenom, dateNaissance, email, motPasse)VALUES('" + nom + "','" + prenom + "','" + DateNaissance + "','" + mail + "','" + mdp + "')"
                            ;
                        SqlCommand cmd = new SqlCommand(rq, conn);
                       // cmd.Parameters.Add("@Id_User", SqlDbType.VarChar);
                        //cmd.Parameters["@Id_User"].Value = id_user;

                        cmd.Parameters.Add("@nom", SqlDbType.VarChar);
                        cmd.Parameters["@nom"].Value = nom;
                        cmd.Parameters.Add("@prenom", SqlDbType.Text);
                        cmd.Parameters["@prenom"].Value = prenom;
                        cmd.Parameters.Add("@dateNaissance", SqlDbType.DateTime);
                        cmd.Parameters["@dateNaissance"].Value = DateNaissance;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar);
                        cmd.Parameters["@email"].Value = mail;
                        cmd.Parameters.Add("@motPasse", SqlDbType.VarChar);
                        cmd.Parameters["@motPasse"].Value = mdp;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Votre compte à ete bien crée");
                        await Task.Delay(3000);
                        Navigator.Navigate(UserLogin.getInstance());
                        conn.Close();
                        

                    }
                }
            }
        }

        /// <summary>
        /// affiche les erreurs 
        /// </summary>
        /// <param name="check"></param>
        /// <param name="info"></param>
        public static bool afficheErreur(bool check, TextBlock info)
        {
            if (!check)
            {
                if (info.Name.Equals("InfoNom") || info.Name.Equals("InfoPrenom"))
                {
                    info.Text = "le nom doit comporter 2 caractères minimum et ne doit pas contenir de caractères spéciaux";
                }
                else if (info.Name.Equals("InfoDate"))
                {
                    info.Text = "le format de la date attendue est JJ/MM/AAAA";
                }
                else if (info.Name.Equals("InfoMail"))
                {
                    info.Text = "votre adresse email doit correspondre au format dupont@gmail.com par exemple";
                }
                return false;
            }
            else
            {
                info.Text = string.Empty;
                return true;
            }
        }
        
        /// <summary>
        /// vérification du format de chaque champs avec une regex
        /// </summary>
        /// <param name="regExp"></param>
        /// <param name="valeurChamp"></param>
        /// <returns></returns>
        public static bool verificationChamp(String regExp, String valeurChamp)
        {
            bool checkValeurChamp = Rx(regExp, valeurChamp);
            return checkValeurChamp;
        }
        
        /// <summary>
        /// Renvoie un boolean si la valeur du champs correspond au format attendu
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="chaine"></param>
        /// <returns></returns>
        public static bool Rx (string regex, string chaine)
        {
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = reg.Matches(chaine);

            if(matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        

    }
}
