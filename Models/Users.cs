namespace VidaBox.Models
{
    class Users
    {
        public int Id_user { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string dateNaissance { get; set; }
        public string email { get; set; }
        public string motPasse { get; set; }

        public Users(int id_user, string nom, string prenom, string dateNaissance, string email, string motPasse)
        {
            Id_user = id_user;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.motPasse = motPasse;
        }
    }
}
    
