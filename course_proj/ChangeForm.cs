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
    public partial class ChangeForm : Form
    {
        List<Student> students;
        int index, curId;
        string lesson, form;
        public ChangeForm(ref List<Student> stud, int num, string _lesson, string _form)
        {
            InitializeComponent();
            students = stud;
            lesson = _lesson;
            form = _form;
            index = GetIndex(num);
            curId = num;
            FillBoxes();
        }

        public void FillBoxes()
        {
            numberTextBox.Text = students[index].id.ToString();
            surnameTextBox.Text = students[index].surname;
            switch (lesson)
            {
                case "Физика":
                    SetTableMarks(students[index].phisMarks);
                    break;
                case "Математика":
                    SetTableMarks(students[index].mathMarks);
                    break;
                case "Русский язык":
                    SetTableMarks(students[index].langMarks);
                    break;
            }
            markTextBox.Text = markTextBox.Text.Trim();
        }

        public void FillList()
        {
            int temp;
            int.TryParse(numberTextBox.Text.Trim(), out temp);
            students[index].id = temp;
            students[index].surname = surnameTextBox.Text.Trim();
            int[] tempMarks = new int[markTextBox.Text.Trim().Split(' ').Length];
            SetListMarks(ref tempMarks);
            switch (lesson)
            {
                case "Физика":
                    if (markTextBox.Text.Trim().Split(' ')[0] == "")
                    {
                        students[index].phisMarks = null;
                    }
                    else
                    {
                        students[index].phisMarks = tempMarks;
                    }
                    break;
                case "Математика":
                    if (markTextBox.Text.Trim().Split(' ')[0] == "")
                    {
                        students[index].mathMarks = null;
                    }
                    else
                    {
                        students[index].mathMarks = tempMarks;
                    }
                    break;
                case "Русский язык":
                    if (markTextBox.Text.Trim().Split(' ')[0] == "")
                    {
                        students[index].langMarks = null;
                    }
                    else
                    {
                        students[index].langMarks = tempMarks;
                    }
                    break;
            }
        }



        public int GetIndex(int num)
        {
            int temp = -1;
            for(int i = 0; i < students.Count; i++)
            {
                if(students[i].id == num && students[i].form == form)
                {
                    temp = i;
                    break;
                }
            }
            return temp;
        }


        public void SetTableMarks(int[] marks)
        {
            markTextBox.Text = "";
            for(int i = 0; i < marks?.Length; i++)
            {
                markTextBox.Text += marks[i].ToString() + " ";
            }
        }


        public void SetListMarks(ref int[] marks)
        {
            for(int i = 0; i < markTextBox.Text.Trim().Split(' ').Length; i++)
            {
                int.TryParse(markTextBox.Text.Trim().Split(' ')[i], out marks[i]);
            }
        }

        public bool PreviousIsDigit(char curLetter)
        {
            bool isDigit = false;
            if(markTextBox.Text.Length > 0 && char.IsDigit(markTextBox.Text[markTextBox.Text.Length - 1]))
            {
                isDigit = true;
            }
            if(markTextBox.Text.Length > 0 && markTextBox.Text[markTextBox.Text.Length - 1] == '1' && curLetter == '0')
            {
                isDigit = false;
            }
            return isDigit;
        }


        public bool IndexChecking()
        {
            bool isNotEmpty = true;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].form == form && students[i].id.ToString() == numberTextBox.Text && students[i].id != curId)
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
            if ((!char.IsDigit(e.KeyChar) || PreviousIsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 32 )
            {
                e.Handled = true;
            }
            
        }

        private void changeButton_Click(object sender, EventArgs e)
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
    }
}
