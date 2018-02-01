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
    public sealed partial class PageChooseProject : Page
    {
        FileGestion fileGestion = new FileGestion();
        DataGestion dataGestion = new DataGestion();

        public PageChooseProject()
        {
            this.InitializeComponent();
            CreateButtonAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Button_ProjectAsync(object sender, RoutedEventArgs e)
        {
            string tmp = (sender as Button).Tag.ToString();
            string data = await fileGestion.ReadOnFileAsync(tmp);
            var parameters = new ProjetParam();
            dataGestion.SetNameProject(data);
            parameters.Name = dataGestion.GetNameProject();
            parameters.Data = dataGestion.GetData();
            this.Frame.Navigate(typeof(ProjectControl) ,parameters);
        }
        private async System.Threading.Tasks.Task CreateButtonAsync()
        {
            string tmp = await fileGestion.ReadOnFileAsync("NameProject");
            if (tmp != null)
            {
                List<string> name = tmp.Split('\n').ToList();
                foreach (var projet in name)
                {
                    if (projet != "" && projet != " ")
                    {
                        Button newBtn = new Button()
                        {
                            Name = projet.ToString(),
                            Content = projet.ToString(),
                            Tag = projet.ToString()
                        };
                        newBtn.Click += new RoutedEventHandler(Button_ProjectAsync);
                        this.ProjectList.Children.Add(newBtn);
                    }
                }

            }
            
        }
    }
}
