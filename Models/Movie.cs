using System.Linq;

namespace VidaBox.Models
{
    public class Movie
    {
        public int Id_Films { get; set; }
        public string Titre { get; set; }
        public string synopsis { get; set; }
        public string dateSortie { get; set; }
        public int duree { get; set; }
        public string Url_Affiche { get; set; }
        public string url_BandeAnnoce { get; set; }
        public int Id_genre { get; set; }

        public Movie(int Id_Films, string Titre, string synopsis, string dateSortie, int duree, string Url_Affiche, string url_BandeAnnoce, int Id_genre)
        {
            this.Id_Films = Id_Films;
            this.Titre = Titre;
            this.synopsis = synopsis != string.Empty ? synopsis : "Synopsis inconnue";

            if (dateSortie.Split('-').Count() == 3)
            {
                string year = dateSortie.Split('-')[0];
                string month = dateSortie.Split('-')[1];
                string day = dateSortie.Split('-')[2];
                this.dateSortie = "Date: " + day + "/" + month + "/" + year;
            }
            else
                this.dateSortie = "Date: Inconnue";
            this.dateSortie = dateSortie;

            this.Url_Affiche = "https://image.tmdb.org/t/p/original" + Url_Affiche;
            this.url_BandeAnnoce = url_BandeAnnoce;
            this.Id_genre = Id_genre;
        }
    }
}
