using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VidaBox.Models
{
    /// <summary>
    /// Permet de réaliser les traitements des résultats fournies par notre API
    /// </summary>
    public class VidaboxSearch
    {
        public class Film
        {
            public int Id_Films { get; set; }
            public string Titre { get; set; }
            public string synopsis { get; set; }
            public string dateSortie { get; set; }
            public int duree { get; set; }
            public string Url_Affiche { get; set; }
            public string url_BandeAnnoce { get; set; }
            public int Id_genre { get; set; }
        }

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
        }
       
        public class RootObject
        {
            public int totalFilms { get; set; }
            public List<Film> films { get; set; }
            public List<Serie> series { get; set; }
        }
    }

    /// <summary>
    /// Permet de faire des appels à l'API (GET, POST, DELETE)
    /// </summary>
    public static class ApiHelper
    {

        /// <summary>
        /// Fait une requete get dans notre api locale
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <returns>le résultat sous forme de json</returns>
        public static string ApiGet(string url, Dictionary<string, string> datas)
        {
            // ajoute les paramètres à l'url
            for (int i = 0; i < datas.Count; i++)
            {
                if (i == 0)
                    url += "?";
                else
                    url += "&";

                url += datas.ElementAt(i).Key + "=" + datas.ElementAt(i).Value;
            }

            string jsonString = string.Empty;
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
            }
            catch { }

            return jsonString;
        }

       

        /// <summary>
        /// Fait une requete post dans notre api locale pour insérer des données
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        public static async void ApiPost(string url, Dictionary<string, string> datas)
        {
            using (var stringContent = new StringContent(JsonConvert.SerializeObject(datas), System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(url, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }
        

        /// <summary>
        /// Fait une requete delete dans notre api pour supprimer des données
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static string ApiDelete(string url, Dictionary<string, string> datas)
        {
            //AJOUT PARAMETRES URL
            for (int i = 0; i < datas.Count; i++)
            {
                if (i == 0)
                    url += "?";
                else
                    url += "&";

                url += datas.ElementAt(i).Key + "=" + datas.ElementAt(i).Value;
            }

            string jsonString = string.Empty;
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "DELETE";
            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
            }
            catch { }

            return jsonString;
        }
        
    }
}
