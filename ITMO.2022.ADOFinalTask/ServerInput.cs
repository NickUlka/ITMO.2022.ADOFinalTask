using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ITMO._2022.ADOFinalTask
{
    public partial class ServerInput : Form
    {
        public bool Inputsuccess = false;
        public ServerInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  на моем сервере логин - user; пароль - 12345
            string conString = $"Persist Security Info=False; User ID={textBox2.Text}; Password={textBox3.Text}; Initial Catalog=Northwind;Data Source={textBox1.Text}";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Open();
                    Inputsuccess = true;
                    ServerName.txt = textBox1.Text;
                    ServerName.login = textBox2.Text;
                    ServerName.password = textBox3.Text;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Не удалось подключиться к серверу  c базой данных Northwind \n Ошибка: {ex.Message}");
                }
                finally
                {
                    Close(); 
                }
            }
        }
    }
    static class ServerName
    {
        public static string txt;
        public static string login;
        public static string password;
    }

}
