using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDoList
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
        FileGestion fileGestion = new FileGestion();
        public MainPage()
        {
            this.InitializeComponent();
            fileGestion.CreateFileProject("NameProject");
            //CreateFileProject();
        }

       
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           this.Frame.Navigate(typeof(CreateProject));
        }

        private void TextBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

        }

        private  void SeeProject_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageChooseProject));
    
        }

        private void DeleteProjectClik(object sender, RoutedEventArgs e)
        {

        }
    }
}
