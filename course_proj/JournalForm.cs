using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public partial class JournalForm : Form
    {
        List<Student> students = new List<Student>();
        public JournalForm()
        {
            InitializeComponent();
            students = MyFile.GetStudentsDataFromFile();
        }

        
        public void FillTable()
        {
            for(int i = 0, j = 0; i < students.Count; i++)
            {
                if(students[i].form == formComboBox.Text)
                {
                    FillRow(students[i], j);
                    double averMark = AverMarkCounting(students[i]);
                    dataGridView.Rows[j].Cells[22].Value = (averMark  == 0) ? "" : averMark.ToString();
                    j++;
                }
            }

        }

        public void FillRow(Student student, int numbLine)
        {
            dataGridView.Rows.Add();
            dataGridView.Rows[numbLine].Cells[0].Value = student.id.ToString();
            dataGridView.Rows[numbLine].Cells[1].Value = student.surname;

            switch (lessonComboBox.Text)
            {
                case "Физика":
                    FillMarks(student.phisMarks, numbLine);
                    break;
                case "Математика":
                    FillMarks(student.mathMarks, numbLine);
                    break;
                case "Русский язык":
                    FillMarks(student.langMarks, numbLine);
                    break;
            }

        }

        public void FillMarks(int[] marks, int numbLine)
        {
            for (int i = 0; i < marks?.Length && i < 20; i++)
            {
                dataGridView.Rows[numbLine].Cells[i + 2].Value = marks[i];
            }
        }

        public double AverMarkCounting(Student student)
        {
            double averMark = 0;
            switch (lessonComboBox.Text)
            {
                case "Физика":
                    if (student.phisMarks != null)
                    {
                        averMark = (double)student.phisMarks.Sum() / student.phisMarks.Length;
                    }
                    break;
                case "Математика":
                    if (student.mathMarks != null)
                    {
                        averMark = (double)student.mathMarks.Sum() / student.mathMarks.Length;
                    }
                    break;
                case "Русский язык":
                    if (student.langMarks != null)
                    {
                        averMark = (double)student.langMarks.Sum() / student.langMarks.Length;
                    }
                    break;
            }
            return averMark;

        }

        public bool IndexChecking()
        {
            bool isNotEmpty = false;
            for(int i = 0; i < students.Count; i++)
            {
                if(students[i].form == formComboBox.Text && students[i].id.ToString() == numOfChangeRowTextBox.Text)
                {
                    isNotEmpty = true;
                    break;
                }
            }
            return isNotEmpty;
        }



        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (lessonComboBox.Text != "Предмет" && formComboBox.Text != "Класс")
            {
                DeleteForm deleteForm = new DeleteForm(ref students, formComboBox.Text);
                deleteForm.ShowDialog();
                dataGridView.Rows.Clear();
                students.Sort(new Comparison<Student>((Student x, Student y) => x.id.CompareTo(y.id)));
                FillTable();
            }
            else
            {
                MessageBox.Show("Вы не выбрали класс и предмет.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (lessonComboBox.Text != "Предмет" && formComboBox.Text != "Класс")
            {
                AddForm addForm = new AddForm(ref students, lessonComboBox.Text, formComboBox.Text);
                addForm.ShowDialog();
                dataGridView.Rows.Clear();
                students.Sort(new Comparison<Student>((Student x, Student y) => x.id.CompareTo(y.id)));
                FillTable();
            }
            else
            {
                MessageBox.Show("Вы не выбрали класс и предмет.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            if (lessonComboBox.Text != "Предмет" && formComboBox.Text != "Класс")
            {
                if (IndexChecking())
                {
                    int num;
                    int.TryParse(numOfChangeRowTextBox.Text, out num);
                    ChangeForm changeForm = new ChangeForm(ref students, num, lessonComboBox.Text, formComboBox.Text);
                    changeForm.ShowDialog();
                    dataGridView.Rows.Clear();
                    students.Sort(new Comparison<Student>((Student x, Student y) => x.id.CompareTo(y.id)));
                    FillTable();
                }
                else
                {
                    MessageBox.Show("Вы ввели номер несуществующей строки.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали класс и предмет.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            numOfChangeRowTextBox.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (lessonComboBox.Text != "Предмет" && formComboBox.Text != "Класс")
            {

                dataGridView.Rows.Clear();
                students.Sort(new Comparison<Student>((Student x, Student y) => x.id.CompareTo(y.id)));
                FillTable();
            }
            else
            {
                MessageBox.Show("Вы не выбрали класс и предмет.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            MyFile.SetStudentsDataToFile(students);
        }

        private void numOfChangeRowTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
