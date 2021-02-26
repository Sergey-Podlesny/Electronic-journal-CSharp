using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{
    [Serializable]
    public class Student
    {
        public int id { get; set; }
        public string surname { get; set; }
        public string form { get; set; }
        public int[] phisMarks { get; set; }
        public int[] mathMarks { get; set; }
        public int[] langMarks { get; set; }
        public Student() { }
        public Student(int _id, string _surname, int[] _phisMarks, int[] _mathMarks, int[] _langMarks)
        {
            id = _id;
            surname = _surname;
            phisMarks = _phisMarks;
            mathMarks = _mathMarks;
            langMarks = _langMarks;
        }
    }
}
