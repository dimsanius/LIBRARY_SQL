using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MAIN.Forms
{
    public partial class AddingOfABook : Form
    {
        MySqlConnection sqlCon;
        MySqlCommand sqlCommand;
        MySqlConnectionStringBuilder conBuilder;
        string Student_index;
        public AddingOfABook(string Student_index_ref)
        {
            InitializeComponent();
            Student_index = Student_index_ref;
            conBuilder = new MySqlConnectionStringBuilder();
            conBuilder.Server = "localhost";
            conBuilder.UserID = "root";
            conBuilder.Password = "";
            conBuilder.Database = "knygynas";
            conBuilder.AllowZeroDateTime = true;
            sqlCon = new MySqlConnection(conBuilder.ToString());
           
        }

        private void AddingOfABook_Load(object sender, EventArgs e)
        {
            //Adds items to Combobox1
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
            sqlCommand = new MySqlCommand("SELECT UDK_APRASYMAS FROM UDK", sqlCon);
            MySqlDataReader data_reader = sqlCommand.ExecuteReader();
            if(data_reader.HasRows)
            {
                while(data_reader.Read())
                {
                    comboBox1.Items.Add(data_reader.GetString(0));
                }
            }
            sqlCon.Close();
            //end adding items to combobox 1
        }

        private void button1_Click(object sender, EventArgs e)
        {       

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Neteisingi duomenys!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult ArOK = MessageBox.Show(String.Format("Bus prideta tokia knyga: \nKnygos autorius: {0} \nKnygos Pavadinimas: {1} \n Knygos UDK: {2}", textBox1.Text, textBox2.Text, comboBox1.SelectedItem), "Patikrinimas", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (ArOK == DialogResult.OK)
                {
                    //Checks UDK ID
                    if (sqlCon.State != ConnectionState.Open)
                        sqlCon.Open();
                    sqlCommand = new MySqlCommand(String.Format("SELECT ID FROM UDK WHERE UDK_APRASYMAS = '{0}'", comboBox1.SelectedItem), sqlCon);
                    int tempID=0; 
                    MySqlDataReader data_reader = sqlCommand.ExecuteReader();
                    if (data_reader.HasRows)
                    {
                        while (data_reader.Read())
                        {
                            tempID = Convert.ToInt16(data_reader.GetString(0));
                        }
                    }
                    sqlCon.Close();

                    //Inserts values

                    if (sqlCon.State != ConnectionState.Open)
                        sqlCon.Open();
                    sqlCommand = new MySqlCommand(String.Format("INSERT INTO GRAFIKAS (PAIMTOS_KNYGOS_AUTORIUS, PAIMTOS_KNYGOS_PAVADINIMAS, KAS_PAEME_ID, UDK_NUMERIS_ID) VALUES ('{0}', '{1}', {2}, {3})", textBox1.Text, textBox2.Text, Student_index, tempID), sqlCon);
                    if (sqlCommand.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Knyga prideta");
                        this.Close();

                    }
                    else
                        MessageBox.Show("Ivyko klaida", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddingOfABook_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
