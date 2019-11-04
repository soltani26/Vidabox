using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VidaBox
{
    /// <summary>
    /// Logique d'interaction pour VidaBoxHome.xaml
    /// </summary>
    public partial class VidaBoxHome : Page
    {

        int randomed;

        public VidaBoxHome()
        {
            InitializeComponent();
        }


        //    private void Page_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        NouvellePartie();
        //    }


        //    private void BtnValider_Click(object sender, RoutedEventArgs e)
        //    {
        //        int pickedNum = PickANumber();

        //        if (pickedNum != randomed)
        //        {
        //            pickedNum = tryAgain(pickedNum);
        //        }
        //        else
        //        {
        //            YouWin();
        //        }


        //    }

        //    private void BtnNouvellePartie_Click(object sender, RoutedEventArgs e)
        //    {
        //        NouvellePartie();
        //    }

        //    void YouWin()
        //    {
        //        textBlockInfo.Text = "Bravo, tu as gagné";
        //    }

        //    int PickANumber()
        //    {
        //        string picked = textBoxEssai.Text;

        //        int pickedNum;
        //        bool isNumeric = int.TryParse(picked, out pickedNum);

        //        if (isNumeric == false)
        //        {
        //            textBlockInfo.Text = "Oups ! ce n'est pas un nombre valide";
        //        }
        //        else
        //        {
        //            textBlockInfo.Text = string.Empty;
        //        }
        //        return pickedNum;
        //    }

        //    void NouvellePartie()
        //    {
        //        randomed = GenereNombreAleatoire();
        //        textBlockInfo.Text = "Vous débutez une nouvelle partie";
        //        textBoxEssai.Text = String.Empty;
        //    }

        //    private int tryAgain(int pickedNum)
        //    {
        //        if (pickedNum > randomed)
        //        {
        //            textBlockInfo.Text = "C'est moins";
        //        }
        //        else
        //        {
        //            textBlockInfo.Text = "C'est plus";
        //        }
        //        return pickedNum;
        //    }

        //    private int GenereNombreAleatoire()
        //    {
        //        return new Random().Next(1, 21);
        //    }


        //}
    }
}
