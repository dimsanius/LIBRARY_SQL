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
    public partial class DetailedUser : Form
    {
        MySqlConnection sqlCon;
        DataSet ds;
        MySqlDataAdapter da;
        MySqlConnectionStringBuilder conBuilder;
        MySqlCommand sqlCommand;
        string Student_index;

        public DetailedUser(string info)
        {
            
            InitializeComponent();
            Student_index = info;
            ds = new DataSet();
            conBuilder = new MySqlConnectionStringBuilder();
            conBuilder.Server = "localhost";
            conBuilder.UserID = "root";
            conBuilder.Password = "";
            conBuilder.Database = "knygynas";
            conBuilder.AllowZeroDateTime = true;

            sqlCon = new MySqlConnection(conBuilder.ToString());
            sqlCon.Open();
            sqlCommand = sqlCon.CreateCommand();
            sqlCommand.CommandText = String.Format("SELECT GRAFIKAS.ID, PAIMTOS_KNYGOS_AUTORIUS, PAIMTOS_KNYGOS_PAVADINIMAS, KADA_PAEME, KADA_ATIDAVE, UDK.UDK_APRASYMAS FROM `grafikas` INNER JOIN UDK on grafikas.UDK_NUMERIS_ID=udk.ID WHERE KAS_PAEME_ID={0}", Student_index);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void DetailedUser_Load(object sender, EventArgs e)
        {
            
            //Form1.ActiveForm = Form1.Deactivate;
            //Get the info of a student
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
            sqlCommand.CommandText = String.Format("SELECT VARDAS, PAVARDE, KLASE_NR, KLASE_RAIDE, KIEK_YRA_PAIMTU_KNYGU, KIEK_ATIDAVE_KNYGU, KADA_UZREGISTRUOTAS FROM mokiniai WHERE MOKINIAI.ID = {0}", Student_index);
            MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
            while(sqlReader.Read())
            {
                textBox1.Text = sqlReader.GetString(0);
                textBox2.Text = sqlReader.GetString(1);
                textBox3.Text = sqlReader.GetString(2) + sqlReader.GetString(3);
                textBox4.Text = sqlReader.GetString(4);
                textBox5.Text = sqlReader.GetString(5);
                textBox6.Text = sqlReader.GetString(6);
                
            }
            sqlCon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MAIN.Forms.AddingOfABook AddingOfABook_Form = new Forms.AddingOfABook(Student_index);
            AddingOfABook_Form.FormClosed += new FormClosedEventHandler(AddingOfABook_FormClosed);
            AddingOfABook_Form.ShowDialog();
        }

        void AddingOfABook_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();

            sqlCommand = sqlCon.CreateCommand();
            sqlCommand.CommandText = String.Format("SELECT GRAFIKAS.ID, PAIMTOS_KNYGOS_AUTORIUS, PAIMTOS_KNYGOS_PAVADINIMAS, KADA_PAEME, KADA_ATIDAVE, UDK.UDK_APRASYMAS FROM `grafikas` INNER JOIN UDK on grafikas.UDK_NUMERIS_ID=udk.ID WHERE KAS_PAEME_ID = {0}", Student_index);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            ds.Clear();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
