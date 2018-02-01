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
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync(name + ".txt",
                    Windows.Storage.CreationCollisionOption.OpenIfExists);
        }

        async public void WriteOnFile(string name, string data)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(name + ".txt");
            string tmp = await ReadOnFileAsync(name);
            if (tmp != null)
                tmp = tmp + "\n" + data;
            else
                tmp = data;
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(tmp, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
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
                return (text);
            }
        }
    }
}
