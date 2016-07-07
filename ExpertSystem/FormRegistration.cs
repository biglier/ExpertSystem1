using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertSystem
{
    public partial class FormRegistration : Form
    {
        ExpDataContext MyContext = new ExpDataContext();

        string s = "Data Source=SLAVA-PC\\SQLEXPRESS;Database=ExpSys;Trusted_Connection = True";
        SqlConnection connectStr = new SqlConnection();
        public FormRegistration()
        {
            InitializeComponent();
            connectStr.ConnectionString = s;
        }

        private void Regbutton_Click(object sender, EventArgs e)
        {
            if (PassBox.Text== PassBox2.Text)
            {
                User newUser = new User()
                {
                    NickName = NameBox.Text,
                    Password = PassBox.Text
                };
                try
                {
                    MyContext.Users.InsertOnSubmit(newUser);
                    MyContext.SubmitChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                ExpertStatistic newUserStatistics = new ExpertStatistic()
                {
                    Count = 0,
                    UserId = newUser.Id
                };
            }
          
            /* 
            DataTable user = new DataTable();
            string sql = string
         
            .Format("Select NickName from  [User] where NickName='{0}'", NameBox.Text);
            connectStr.Open();
            using (SqlCommand cmd = new SqlCommand(sql, connectStr))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                user.Load(dr);
                dr.Close();
            }
            connectStr.Close();
            try
            {
              string  inf = string.Format(user.Rows[0][0].ToString()+"Логін зайнято");
                    MessageBox.Show(user.Rows[0][0].ToString());
            }
            catch
            {
                if (PassBox.Text == PassBox2.Text && NameBox.Text!="" && PassBox.Text!="")
                {
                    sql = string.Format("Insert Into [User]"+ "(NickName, Password,Admin,Ban) Values(@NickName, @Password,@Admin,@Ban)");
                    try
                    {
                        connectStr.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, connectStr))
                        {
                            cmd.Parameters.AddWithValue("@NickName", NameBox.Text);
                            cmd.Parameters.AddWithValue("@Password", PassBox.Text);
                            cmd.Parameters.AddWithValue("@Admin", false);
                            cmd.Parameters.AddWithValue("@Ban", false);
                            cmd.ExecuteNonQuery();
                        }
                        connectStr.Close();
                        MessageBox.Show("Ok");
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }                    
                }
                else
                {
                    MessageBox.Show("Перевірте інформацію");
                }
            }
        }*/
        }
    }
}
