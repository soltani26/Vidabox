using System.Windows.Controls;

namespace VidaBox.Models
{
    /// <summary>
    /// Permet de naviguer entre les pages de l'application
    /// </summary>
    class Navigator
    {

        public static void Navigate(UserControl userControl)
        {
            MainWindow.getInstance().DataContext = userControl;
        }
    }
}
