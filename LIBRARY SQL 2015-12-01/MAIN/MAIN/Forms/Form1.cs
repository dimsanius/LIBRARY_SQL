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
    public partial class Form1 : Form
    {

        MySqlConnection sqlCon;
        DataSet ds;
        MySqlDataAdapter dataAdapter;
        MySqlConnectionStringBuilder conBuilder;
        MySqlCommand sqlCommand;
        int paimtos_Knygos = 0;

        
        public Form1()
        {
            InitializeComponent();
            conBuilder = new MySqlConnectionStringBuilder();
            conBuilder.Server = "localhost";
            conBuilder.UserID = "root";
            conBuilder.Password = "";
            conBuilder.Database = "knygynas";
            conBuilder.AllowZeroDateTime = true;

            sqlCon = new MySqlConnection(conBuilder.ToString());
            sqlCon.Open();
            sqlCommand = sqlCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MOKINIAI";
            dataAdapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            sqlCon.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            //int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            DetailedUser DetailedUserForm = new DetailedUser(dataGridView1.Rows[rowindex].Cells[0].Value.ToString());
            //Debug
            MessageBox.Show(dataGridView1.Rows[rowindex].Cells[0].Value.ToString());
             
            DetailedUserForm.FormClosed += new FormClosedEventHandler(DetailedUser_FormClosed);
            DetailedUserForm.ShowDialog();
        }

        private void DetailedUser_FormClosed(object sender, FormClosedEventArgs e)
        {
              int rowindex = dataGridView1.CurrentCell.RowIndex;
            //SELECT COUNT(grafikas.id) FROM grafikas WHERE KAS_PAEME_ID = 1
              if (sqlCon.State != ConnectionState.Open)
                  sqlCon.Open();
            //MessageBox.Show(Convert.ToString(sqlCon.State));
            //sqlCommand = sqlCon.CreateCommand();
            //sqlCommand.CommandText = String.Format("SELECT COUNT(grafikas.id) FROM grafikas WHERE KAS_PAEME_ID = {0}", Convert.ToInt16(dataGridView1.CurrentCell.RowIndex));
            //dataAdapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            //ds.Clear();
            //dataAdapter.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
            //dataAdapter.Fill(paimtos_Knygos);
              sqlCommand = new MySqlCommand(String.Format("SELECT COUNT(grafikas.id) FROM grafikas WHERE KAS_PAEME_ID = {0}", Convert.ToString(dataGridView1.Rows[rowindex].Cells[0].Value)),sqlCon);
              MySqlDataReader dataReader = sqlCommand.ExecuteReader();
               while(dataReader.Read())
                   paimtos_Knygos = Convert.ToInt16(dataReader.GetString(0));
              MessageBox.Show(paimtos_Knygos.ToString());
              sqlCon.Close();

              if (sqlCon.State != ConnectionState.Open)
                  sqlCon.Open();

              sqlCommand = new MySqlCommand(String.Format("UPDATE mokiniai SET KIEK_YRA_PAIMTU_KNYGU = {0} WHERE id={1}", paimtos_Knygos, dataGridView1.Rows[rowindex].Cells[0].Value), sqlCon);
              sqlCommand.ExecuteNonQuery();

            //Main table refresh after maintaining info
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();

            sqlCommand = sqlCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MOKINIAI";
            dataAdapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            ds.Clear();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void pridetiMokiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudent AddStudentForm = new AddStudent();
            AddStudentForm.FormClosed += new FormClosedEventHandler(AddStudent_FormClosed);
            AddStudentForm.ShowDialog();
        }

        void AddStudent_FormClosed(object sender, FormClosedEventArgs e)
        {

            //Main table refresh after adding students
            if(sqlCon.State != ConnectionState.Open)
            sqlCon.Open();

            sqlCommand = sqlCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MOKINIAI";
            dataAdapter = new MySqlDataAdapter(sqlCommand.CommandText, sqlCon);
            ds.Clear();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void redaguotiUDKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
