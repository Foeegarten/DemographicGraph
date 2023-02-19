using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demographic.FileOperations
{
    public class FileManager
    {
        private static FileManager instance;

        private FileManager()
        {
        }

        public static FileManager Instance { get { return instance ?? (instance = new FileManager()); } }

        public string FileName1
        {
            get; set;
        }
        public string FileName2
        {
            get; set;
        }
        public  List<(int ,double )> InitialAgeRead()
        {
            List<(int, double)> list = new List<(int, double)>();
            string[] lines = File.ReadAllLines(FileName1);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                int age = int.Parse(line[0]);
                
                double count = double.Parse(line[1].Replace('.', ','));
                list.Add((age, count));
            }
            return list;
        }
        public List<DeathRules> DeathRulesRead()
        {
            List<DeathRules> list = new List<DeathRules>();

            string[] lines = File.ReadAllLines(FileName2);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');

                DeathRules deathRules = new DeathRules(int.Parse(line[0]), int.Parse(line[1]), double.Parse(line[2].Replace('.',',')), double.Parse(line[3].Replace('.', ',')));
                list.Add(deathRules);
            }
            return list;
        }
    }
}
