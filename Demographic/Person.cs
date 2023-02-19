using Demographic.FileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demographic
{
    public class Person
    {

        public Person(Gender gender, int yearOfBirth)
        {
            Gender = gender;
            YearOfBirth = yearOfBirth;
            Alive = true;
        }
         public event Action<Person> Death;

        public event Action<Person> ChildBirth;
        public Gender Gender { get;  }
        public int YearOfBirth { get; }
        public bool Alive { get; set; }
        public int YearOfDeath { get; set; }
        const double posibilityB = 0.151;
        static List<DeathRules> deathRules = FileManager.Instance.DeathRulesRead();
        public void NewYear(int year)
        {
            int age = year - YearOfBirth;
            double possibilityD=GetDeathProbability(year);
            if (ProbabilityCalculator.IsEventHappened(possibilityD))
            {
                Alive = false;
                Death?.Invoke(this);
            }
            if (Alive == true&&age>=18&&age<=45&&Gender==Gender.Female)
                if (ProbabilityCalculator.IsEventHappened(posibilityB))
                {
                    ChildBirth?.Invoke(BirthChild(year));
                }
        }
        private Person BirthChild(int year)
        {
            Person child;
            if (ProbabilityCalculator.IsEventHappened(0.55))
                child = new Person(Gender.Female, year);
            else
                child = new Person(Gender.Male, year);
            return child;


        }
        public double GetDeathProbability(int year)
        {
            int age = year-YearOfBirth;
            
            DeathRules dr= deathRules.FirstOrDefault(t=>t.StartAge<=age&&t.EndAge>=age);
           
            return Gender == Gender.Male ? dr.PossibilityM : dr.PosibilityF;
        }
        
      //сделать нью ер на смерть
      // сделать вероятность рождения
      // 

    }
}
