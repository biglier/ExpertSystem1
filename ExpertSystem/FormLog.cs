using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertSystem
{
    public partial class FormLog : Form
    {
        string s = "Data Source=SLAVA-PC\\SQLEXPRESS;Database=ExpSys;Trusted_Connection = True";
        SqlConnection connectStr = new SqlConnection();
        public FormLog()
        {
            InitializeComponent();
            connectStr.ConnectionString =s;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable user = new DataTable();
            string sql = string.Format("Select Password,Admin from  [User] where NickName='{0}'", NameBox.Text);
            connectStr.Open();
            using (SqlCommand cmd = new SqlCommand(sql,connectStr))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                user.Load(dr);
                dr.Close();
            }
            connectStr.Close();
            try
            {
                if (user.Rows[0][0].ToString() == PassBox.Text)
                {
                    if (user.Rows[0][1].ToString() =="True")
                    {
                        FormMain fm = new FormMain();
                        this.Hide();
                        fm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Користувач не є експертом-адміністратором");
                    }                  
                }
                else
                {
                    MessageBox.Show("Перевірте правильність введених данних");
                }
            }
            catch
            {
                MessageBox.Show("Перевірте правильність введених данних");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegistration form = new FormRegistration();
            this.Hide();
            form.ShowDialog();
        }
    }
}

