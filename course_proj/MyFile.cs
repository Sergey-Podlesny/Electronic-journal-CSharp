using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace kursach
{
    static class MyFile
    {
        static string file;
        static MyFile()
        {
            file = "students.txt";
        }


        public static void SetStudentsDataToFile(List<Student> students)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, students);
            }
        }

        public static List<Student> GetStudentsDataFromFile()
        {
            List<Student> students = new List<Student>();
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                students = (List<Student>)formatter.Deserialize(fs);
            }
            return students;
        }
    }
}
