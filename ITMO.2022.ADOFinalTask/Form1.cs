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
    public partial class Form1 : Form
    {
        private SqlConnection connection = new SqlConnection($"Persist Security Info=False;User ID={ServerName.login}; Password={ServerName.password};Initial Catalog=Northwind;Data Source={ServerName.txt}");
        private SqlDataAdapter adapter;
        private DataSet dataset = new DataSet("Northwind");
        private DataTable table = new DataTable("Customers");

    


        public Form1()
        {
            InitializeComponent();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select * from Customers", connection);
            dataset.Tables.Add(table);
            adapter.Fill(dataset.Tables["Customers"]);

            dataGridView1.DataSource = dataset.Tables["Customers"];
            SqlCommandBuilder commands = new SqlCommandBuilder(adapter);

            ((DataGridViewTextBoxColumn)dataGridView1.Columns[0]).MaxInputLength = 5;
            ToolTip tt_delete = new ToolTip();
            tt_delete.SetToolTip(button2, "Выделите строки для удаления");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CellValidating += new
        DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            this.dataGridView1.CellEndEdit += new
            DataGridViewCellEventHandler(dataGridView1_CellEndEdit);

           

            try
            {
                dataset.EndInit();
                adapter.Update(dataset.Tables["Customers"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
          
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            try
            {
                adapter.Update(dataset, "Customers");
            }
            catch (Exception ex)
            {
                dataset.Clear();
                adapter.Fill(dataset, "Customers");
                dataGridView1.DataSource = dataset.Tables["Customers"];
                MessageBox.Show($"Этот CustomerID имеет запись в таблице Orders, пожалуйста выберите другую строку для удаления. \n Ошибка: {ex.Message}", "Невозможно удалить запись",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            {
                string headerText =
                    dataGridView1.Columns[e.ColumnIndex].HeaderText;

                if (!headerText.Equals("CompanyName") && !headerText.Equals("CustomerID")) 
                    return;

                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText =
                        "Поля CompanyName и CustomerID обязательны к заполнению!";
                    e.Cancel = true;
                }
              
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            dataGridView1.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void NotDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ввод только букв");
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(NotDigit_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                TextBox digit = e.Control as TextBox;
                if (digit != null)
                    digit.KeyPress += new KeyPressEventHandler(NotDigit_KeyPress);
            }
        }
    }
}
