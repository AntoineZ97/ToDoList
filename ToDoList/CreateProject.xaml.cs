using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoList
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CreateProject : Page
    {
        public CreateProject()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileGestion file = new FileGestion();
            file.CreateFileProject(nameInput.Text);
            file.WriteOnFile("NameProject", nameInput.Text);
            DateTime thisDay = DateTime.Today;
            string name = "Name:" + nameInput.Text;
            string creation = "Creation:" + thisDay.ToString("g");
            string modification = "Modification:" + thisDay.ToString("g");
            string description = "Description:" + descriptionInput.Text;
            string final = name + "\n" + description + "\n" + creation + "\n" + modification;
            file.WriteOnFile(nameInput.Text, final);
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
