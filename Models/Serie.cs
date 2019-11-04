using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidaBox.Models
{
    public class Serie
    {
        public int Id_Serie { get; set; }
        public string Titre { get; set; }
        public string Synopsis { get; set; }
        public string Date_sortie { get; set; }
        public string Url_affiche { get; set; }
        public string Url_bande_annonce { get; set; }
        public int Id_genre { get; set; }
        public int Id_dbmovie { get; set; }


        public Serie(int Id_Serie, string Titre, string Synopsis, string Date_sortie, string Url_affiche, string Url_bande_annonce, int Id_genre, int Id_dbmovie)
        {
            this.Id_Serie = Id_Serie;
            this.Titre = Titre;
            this.Synopsis = Synopsis;
            this.Date_sortie = Date_sortie;
            this.Url_affiche = Url_affiche;
            this.Url_bande_annonce = Url_bande_annonce;
            this.Id_genre = Id_genre;
            this.Id_dbmovie = Id_dbmovie;
        }
    }
}
