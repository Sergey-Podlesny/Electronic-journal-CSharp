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
    public partial class AddForm : Form
    {

        List<Student> students;
        string lesson, form;
        string[] marksString;

        public AddForm(ref List<Student> stud, string less, string _form)
        {
            InitializeComponent();
            lesson = less;
            students = stud;
            form = _form;
        }

        public bool PreviousIsDigit(char curLetter)
        {
            bool isDigit = false;
            if (markTextBox.Text.Length > 0 && char.IsDigit(markTextBox.Text[markTextBox.Text.Length - 1]))
            {
                isDigit = true;
            }
            if (markTextBox.Text.Length > 0 && markTextBox.Text[markTextBox.Text.Length - 1] == '1' && curLetter == '0')
            {
                isDigit = false;
            }
            return isDigit;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (numberTextBox.Text.Trim() != "" && surnameTextBox.Text.Trim() != "")
            {
                if (IndexChecking())
                {
                    FillList();
                    Close();
                }
                else
                {
                    MessageBox.Show("Вы ввели номер, который уже занят.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы не заполнили поля НОМЕР и ФАМИЛИЯ.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillList()
        {
            int temp;
            Student tempStudent = new Student();
            tempStudent.form = form;
            int.TryParse(numberTextBox.Text.Trim(), out temp);
            tempStudent.id = temp;
            tempStudent.surname = surnameTextBox.Text.Trim();
            marksString = markTextBox.Text.Trim().Split(' ');
            int[] tempArray;
            switch (lesson)
            {
                case "Физика":
                    if (markTextBox.Text == "")
                    {
                        tempStudent.phisMarks = null;
                    }
                    else
                    {
                        tempArray = tempStudent.phisMarks;
                        FillMarks(ref tempArray);
                        tempStudent.phisMarks = tempArray;
                    }
                    break;
                case "Математика":
                    if (markTextBox.Text == "")
                    {
                        tempStudent.mathMarks = null;
                    }
                    else
                    {
                        tempArray = tempStudent.mathMarks;
                        FillMarks(ref tempArray);
                        tempStudent.mathMarks = tempArray;
                    }
                    break;
                case "Русский язык":
                    if (markTextBox.Text == "")
                    {
                        tempStudent.langMarks = null;
                    }
                    else
                    {
                        tempArray = tempStudent.langMarks;
                        FillMarks(ref tempArray);
                        tempStudent.langMarks = tempArray;
                    }
                    break;
            }


            students.Add(tempStudent);
        }

        public bool IndexChecking()
        {
            bool isNotEmpty = true;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].form == form && students[i].id.ToString() == numberTextBox.Text)
                {
                    isNotEmpty = false;
                    break;
                }
            }
            return isNotEmpty;
        }

        private void numberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void markTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) || PreviousIsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        public void FillMarks(ref int[] marksInt)
        {
            marksInt = new int[marksString.Length];
            for (int i = 0; i < marksString.Length; i++)
            {
                int tempMark;
                int.TryParse(marksString[i], out tempMark);
                marksInt[i] = tempMark;
            }
        }
    }
}
