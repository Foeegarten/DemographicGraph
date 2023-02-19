using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;
using Demographic.FileOperations;
namespace Demographic
{
    public class Engine : IEngine
    {
        List<Person> people;
        private int countPeople;
        private int yearStart;
        private int yearEnd;
        public Dictionary<int, int> dictPopulation;
        public Dictionary<int, int> dictMen;
        public Dictionary<int, int> dictWomen;
        public Engine(int countPeople, int yearStart, int yearEnd)
        {
            dictPopulation = new Dictionary<int, int>();
            dictMen = new Dictionary<int, int>();
            dictWomen= new Dictionary<int, int>();
            people = new List<Person>();
            this.countPeople = countPeople;
            this.yearStart = yearStart;
            this.yearEnd = yearEnd;
            CreatePeople();
        }
        public void CreatePeople()
        {
            
           List<(int,double)> list= FileManager.Instance.InitialAgeRead();
            foreach (var item in list)
            {
                int count = (int)(countPeople *Math.Round(item.Item2));
                for (int i = 0; i < count && i<countPeople*1000; i++)
                {
                    Gender gender = i % 2 == 0 ? Gender.Male : Gender.Female;
                    Person person = new Person(gender, yearStart - item.Item1);
                    YearTick += person.NewYear;
                    person.ChildBirth += Person_ChildBirth;
                    person.Death += Person_Death;
                    people.Add(person);
                    
                }
            }
        }

        private void Person_Death(Person obj)
        {
            YearTick-= obj.NewYear;
        }

        private void Person_ChildBirth(Person obj)
        {
            obj.ChildBirth += Person_ChildBirth;
            obj.Death += Person_Death;
            YearTick += obj.NewYear;
            people.Add(obj);
        }

        public event Action<int> YearTick;

        public void Cycle()
        {
            for (int i = yearStart; i < yearEnd; i++)
            {
                YearTick?.Invoke(i);
                int counter = people.Count(t => t.Alive);
                int counterM = people.Count(t => t.Alive && t.Gender == Gender.Male);
                int counterF = people.Count(t => t.Alive && t.Gender == Gender.Female);

                dictPopulation.Add(i, counter);
                dictMen.Add(i, counterM );
                dictWomen.Add(i, counterF );
            }
        }
        
        
    }
}
