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
    public partial class DeleteForm : Form
    {
        List<Student> students;
        string form;
        public DeleteForm(ref List<Student> stud, string _form)
        {
            InitializeComponent();
            students = stud;
            form = _form;
        }

        public bool IndexChecking()
        {
            bool isNotEmpty = false;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].form == form && students[i].id.ToString() == numberTextBox.Text)
                {
                    isNotEmpty = true;
                    break;
                }
            }
            return isNotEmpty;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (IndexChecking())
            {
                int num;
                int.TryParse(numberTextBox.Text, out num);
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].id == num && students[i].form == form)
                    {
                        students.RemoveAt(i);
                        i--;
                    }
                }
                Close();
            }
            else
            {
                MessageBox.Show("Вы ввели номер несуществующий строки.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
