using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class DataGestion
    {
        private string nameProject = "None";
        private string creation = "None";
        private string lastModification = "None";
        private List<Tuple<string, int>> Activities;
        private string data = "None";

        public void SetNameProject(string data)
        {
            List<string> name = data.Split('\n').ToList();
            this.data = data;
            foreach (var value in name)
            {
                if (value.ToString().Contains("Name:"))
                {
                    nameProject = value.Substring("Name:".Length);
                    break;
                }
                else
                    nameProject = "Toto";
            }
        }
        public string GetData()
        {
            return (this.data);
        }
        public string GetNameProject()
        {
            return (nameProject);
        }
    }
}
