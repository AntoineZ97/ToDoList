using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class ProjectControl : Page
    {
        private string nameProject;
        private string content;
        DataGestion dataGestion = null;
        FileGestion fileGestion = new FileGestion();
        object paramConstant;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            var parameters = (ProjetParam)e.Parameter;
            paramConstant = e.Parameter;
            nameProject = parameters.Name;
            content = parameters.Data;
            dataGestion = new DataGestion();
            dataGestion.SetDateCreation(content);
            dataGestion.SetDescription(content);
            this.Title.Text = nameProject;
            this.Description.Text = dataGestion.GetDescription();
            this.DateDebut.Text = dataGestion.GetDateCreation();
            dataGestion.SetActivities(content);
            CreateButtonTask();
            // parameters.Name
            // parameters.Text
            // ...
        }

        public ProjectControl()
        {
            this.InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Delete_ProjectAsync(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = true;
                fileGestion.RemoveTaskAsync(nameProject);
            }
        }

        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
            }
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Add_Task(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                AddTaskPop.IsOpen = true;
            }
        }

        private void CheckTask(object sender, RoutedEventArgs e)
        {
            if (NameTask.Text != "" && NameTask.Text != "Nom")
            {
                string tmp = "Task:" + NameTask.Text + ",1";
                fileGestion.WriteOnFile(nameProject, tmp);
                if (AddTaskPop.IsOpen)
                {
                    NameTask.Text = "";
                    AddTaskPop.IsOpen = false;
                    Reload();
                }
            }
            else
            {
                NameTask.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
            }
        }

        private void CreateButtonTask()
        {
            List<Tuple<string, int>> taskList = dataGestion.GetActivities();
            foreach (Tuple<string,int> value in taskList)
            {
                if (value.Item2 == 1)
                {
                    Button newBtn = new Button()
                    {
                        Name = value.Item1.ToString(),
                        Content = value.Item1.ToString(),
                        Tag = value.Item1.ToString(),
                        Background = new SolidColorBrush(Windows.UI.Colors.Cyan)

                    };
                    newBtn.Click += new RoutedEventHandler(Button_TaskUse);
                    this.Doing.Children.Add(newBtn);
                }
                else if (value.Item2 == 2)
                {
                    Button newBtn = new Button()
                    {
                        Name = value.Item1.ToString(),
                        Content = value.Item1.ToString(),
                        Tag = value.Item1.ToString(),
                        Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow)

                    };
                    newBtn.Click += new RoutedEventHandler(Button_TaskEnd);
                    this.Ending.Children.Add(newBtn);
                }
            }
        }

        private void Button_TaskUse(object sender, RoutedEventArgs e)
        {
            string tmp = (sender as Button).Tag.ToString();
            fileGestion.ChangeTaskAsync(tmp, nameProject, 2);
            AddTaskPop.IsOpen = false;
            Reload();
        }

        private void Button_TaskEnd(object sender, RoutedEventArgs e)
        {
            AddTaskPop.IsOpen = false;
            string tmp = (sender as Button).Tag.ToString();
            fileGestion.RemoveTaskProjectAsync(tmp, nameProject);
            Reload();
        }

        private void CancelTask(object sender, RoutedEventArgs e)
        {
            AddTaskPop.IsOpen = false;
        }


        private async void Reload()
        {
            this.Doing.Children.Clear();
            this.Ending.Children.Clear();
            string tmp = await fileGestion.ReadOnFileAsync(nameProject);
            
            dataGestion.SetActivities(tmp);
            CreateButtonTask();
        }
    }

}
