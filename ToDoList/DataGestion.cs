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
        private string Description = "Aucune";
        private List<Tuple<string, int>> Activities = new List<Tuple<string, int>>();
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

        public void SetActivities(string data)
        {
            this.Activities.Clear();
            List<string> name = data.Split('\n').ToList();
            this.data = data;
            foreach (var value in name)
            {
                if (value.ToString().Contains("Task:"))
                {
                    string tmp2 = value.Substring("Task:".Length);
                    string[] all = tmp2.Split(',');
                    string task = all[0];
                    string type = all[1];
                    int typeFinal = Int32.Parse(type);
                    Tuple<string, int> tupleTmp = new Tuple<string, int>(task, typeFinal);
                    Activities.Add(tupleTmp);            
                }
            }
        }


        public List<Tuple<string, int>> GetActivities()
        {
            return (this.Activities);
        }

        public void SetDescription(string data)
        {
            List<string> name = data.Split('\n').ToList();
            this.data = data;
            foreach (var value in name)
            {
                if (value.ToString().Contains("Description:"))
                {
                    Description = "Description :\n";
                    Description += value.Substring("Description:".Length);
                    break;
                }
                else
                    Description = "Aucune";
            }
        }

        public void SetDateCreation(string data)
        {
            List<string> name = data.Split('\n').ToList();
            this.data = data;
            foreach (var value in name)
            {
                if (value.ToString().Contains("Creation:"))
                {
                    creation = "Crée le : ";
                    creation += value.Substring("Creation:".Length);
                    break;
                }
                else
                    creation = "Inconnu";
            }
        }

        public string GetDateCreation()
        {
            return (creation);
        }

        public string GetDescription()
        {
            return (this.Description);
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
