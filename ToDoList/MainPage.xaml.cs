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

        public MainPage()
        {
            this.InitializeComponent();
            //CreateFileProject();
        }

        async void CreateFileProject(String name)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync(name + ".txt",
                    Windows.Storage.CreationCollisionOption.OpenIfExists);
        }

        async void WriteOnFile(string data)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(data + ".txt");
            string tmp = await ReadOnFileAsync(data);
            if (tmp != null)
                data = data + "\n" + tmp;
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(data, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
            await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);
            //await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");
        }

        private async System.Threading.Tasks.Task<string> ReadOnFileAsync(string name)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(name + ".txt");
            var buffer = await Windows.Storage.FileIO.ReadBufferAsync(sampleFile);
            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                string text = dataReader.ReadString(buffer.Length);
                return (text);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateFileProject("Toto");
            WriteOnFile("Toto");
        }

        private void TextBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

        }

        private async void SeeProject_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageChooseProject));

            try
            {
                string text = await ReadOnFileAsync("Toto");
                ResultTextBlock.Text = text;
            }
            catch (System.IO.FileNotFoundException)
            {
                ResultTextBlock.Text = "Vous n'avez pas de projets !";
            }
        }

        private void DeleteProjectClik(object sender, RoutedEventArgs e)
        {

        }
    }
}
