using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VidaBox.Models;
using VidaBox.Views;

namespace VidaBox.ViewsModels
{
    public static class UserContentViewModel
    {
        /// <summary>
        /// Récupère les contenus à partir du contenu du champs de recherche
        /// </summary>
        /// <param name="txtBox_SearchText"></param>
        public static void updateContentList(string txtBox_SearchText)
        {
            string comboBoxSearchTypeValue = UserContent.getInstance().comboBoxSearchType.SelectedValue.ToString().Substring("System.Windows.Controls.ComboBoxItem: ".Count()); 

            if (comboBoxSearchTypeValue == "Films")
            {
                UserContent.getInstance().movies.Clear();

                Dictionary<string, string> datas = new Dictionary<string, string>();
                datas.Add("query", txtBox_SearchText);
                datas.Add("page", "1");
                var item = JsonConvert.DeserializeObject<VidaboxSearch.RootObject>(ApiHelper.ApiGet(MainWindow.getInstance().urlApi + "api/Film", datas));
                if (item != null)
                {
                    for (int i = 0; i < item.films.Count; i++)
                    {
                        Movie movie = new Movie(item.films[i].Id_Films, item.films[i].Titre, item.films[i].synopsis, item.films[i].dateSortie, item.films[i].duree, item.films[i].Url_Affiche, item.films[i].url_BandeAnnoce, item.films[i].Id_genre);
                        UserContent.getInstance().movies.Add(movie);
                    }
                }
                else
                {
                    MessageBox.Show("Aucun résultats n'à été trouvés...");
                }
            }
            else
            {
                //UserContent.getInstance().series.Clear();

                /*var item = ApiHelper.ApiVidaboxSearch(txtBox_SearchText, 1, MainWindow.getInstance().urlApi + "/api/Serie");
                if (item != null)
                {
                    for (int i = 0; i < item.series.Count; i++)
                    {
                        Serie serie = new Serie(item.series[i].Id_Serie, item.series[i].Titre, item.series[i].Synopsis, item.series[i].Date_sortie, item.series[i].Url_affiche, item.series[i].Url_bande_annonce, item.series[i].Id_genre, item.series[i].Id_dbmovie);
                        UserContent.getInstance().series.Add(serie);
                    }
                }
                else
                {
                    MessageBox.Show("Aucun résultats n'à été trouvés...");
                }*/
            }
        }
    }
}
