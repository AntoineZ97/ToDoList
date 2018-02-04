using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class FileGestion
    {
        async public void CreateFileProject(String name)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                Windows.Storage.StorageFile sampleFile =
                    await storageFolder.CreateFileAsync(name + ".txt",
                        Windows.Storage.CreationCollisionOption.OpenIfExists);
            }
            catch (System.IO.FileNotFoundException)
            {
                return;
            }
        }

        async public void CreateNewFileProject(String name)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync(name + ".txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);
        }

        async public void WriteOnFile(string name, string data)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(name + ".txt");
                string tmp = await ReadOnFileAsync(name);
                if (tmp != null)
                    tmp = tmp + "\n" + data;
                else
                    tmp = data;
                var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(tmp, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
                await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);
            }
            catch (System.ArgumentException)
            {
                return;
            }
        }

        async public void WriteOnFile(string name, string data, bool ok)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(name + ".txt");
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(data, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
            await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);
        }

        public async System.Threading.Tasks.Task<string> ReadOnFileAsync(string name)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(name + ".txt");
            var buffer = await Windows.Storage.FileIO.ReadBufferAsync(sampleFile);
            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                string text = dataReader.ReadString(buffer.Length);
                Task.WaitAll();
                return (text);
            }
        }

        public async void RemoveTaskAsync(string name)
        {
            string tmp = await ReadOnFileAsync("NameProject");
            if (tmp != null)
            {
                List<string> listProject = tmp.Split('\n').ToList();
                string newFile = "";
                foreach (var value in listProject)
                {
                    if (value.ToString().Contains(name) == false)
                    {
                        newFile += (value + "\n");
                    }
                }
                WriteOnFile("NameProject", newFile, true);
            }
        }

        public async void RemoveTaskProjectAsync(string name, string project)
        {
            string tmp = await ReadOnFileAsync(project);
            if (tmp != null)
            {
                List<string> listProject = tmp.Split('\n').ToList();
                string newFile = "";
                foreach (var value in listProject)
                {
                    if (value.ToString().Contains("Task:"))
                    {
                        string valueTmp = value.Substring("Task:".Length);
                        string[] all = valueTmp.Split(',');
                        if (all[0].Equals(name) == false)
                        {
                            newFile += (value + "\n");
                        }
                    }
                }
                WriteOnFile(project, newFile, true);
            }
        }

        public async void ChangeTaskAsync(string nameTask, string nameProject, int newType)
        {
            string tmp = await ReadOnFileAsync(nameProject);
            if (tmp != null)
            {
                List<string> listProject = tmp.Split('\n').ToList();
                string newFile = "";
                foreach (var value in listProject)
                {
                    if (value.ToString().Contains("Task:"))
                    {
                        string valueTmp = value.Substring("Task:".Length);
                        string[] all = valueTmp.Split(',');
                        if (all[0].Equals(nameTask))
                        {
                            string newValue = "Task:" + all[0] + ',' + newType.ToString();
                            newFile += (newValue + "\n");
                        }
                        else
                            newFile += (value + "\n");                      
                    }
                    else
                        newFile += (value + "\n");  
                }
                WriteOnFile(nameProject, newFile, true);
            }
        }
    }
}
