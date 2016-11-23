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

namespace MAIN
{
    public partial class AddStudent : Form
    {
        MySqlConnection sqlCon;
        DataSet ds;
        MySqlDataAdapter da;
        MySqlConnectionStringBuilder conBuilder;
        MySqlCommand sqlCommand;
        public AddStudent()
        {
            InitializeComponent();

            ds = new DataSet();
            conBuilder = new MySqlConnectionStringBuilder();
            conBuilder.Server = "localhost";
            conBuilder.UserID = "root";
            conBuilder.Password = "";
            conBuilder.Database = "knygynas";
            conBuilder.AllowZeroDateTime = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void AddStudent_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {


            sqlCon = new MySqlConnection(conBuilder.ToString());
            sqlCon.Open();
            sqlCommand = sqlCon.CreateCommand();
            //sqlCommand.CommandText = String.Format("SELECT VARDAS, PAVARDE FROM `mokiniai` WHERE `VARDAS`= '{0}' AND `PAVARDE` = '{1}'", textBox1.Text, textBox2.Text);
            //MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            //adapter.Fill(ds);

            if (textBox1.Text == "" || textBox2.Text == "" || Convert.ToString(comboBox1.SelectedItem) == "")
            {
                MessageBox.Show("Neteisingi duomenys!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult ArOK = MessageBox.Show(String.Format("Bus sukurtas toks mokinys: \nVardas: {0} \nPavarde: {1} \nKlases numeris: {2} \nKlases raide: {3}", textBox1.Text, textBox2.Text, numericUpDown1.Value, comboBox1.SelectedItem), "Patikrinimas", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (ArOK == DialogResult.OK)
                {
                    if(sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();

                    sqlCommand = new MySqlCommand(String.Format("INSERT INTO MOKINIAI (VARDAS, PAVARDE, KLASE_NR, KLASE_RAIDE) VALUES ('{0}', '{1}', {2}, '{3}')", textBox1.Text, textBox2.Text, numericUpDown1.Value, comboBox1.SelectedItem), sqlCon);
                    if (sqlCommand.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Mokinis pridetas");
                        this.Close();
                        
                    }
                    else
                        MessageBox.Show("Ivyko klaida", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
                
        }
       
        private void AddStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void AddStudent_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


    }
}
