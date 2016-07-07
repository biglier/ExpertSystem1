using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using ExpertTest;

namespace ExpertTest
{
    public partial class FormLog : Form
    {
        ExpDataContext MyContext = new ExpDataContext();
        public FormLog()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                var user = (from u in MyContext.Users
                           where u.NickName == NameBox.Text
                           && u.Password == PassBox.Text  
                           select u).Single();
                    UserSession.userId = user.Id;
                FormMain fm = new FormMain();
                this.Hide();
                fm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Ви ввели некоректнідані або вас забанили");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegistration form = new FormRegistration();
            this.Hide();
            form.ShowDialog();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

