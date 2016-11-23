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
    public partial class UDKManagementForm : Form
    {
        DataSet ds;
        MySqlConnectionStringBuilder conBuilder;
        MySqlConnection sqlCon;
        MySqlCommand sqlCommand;
        public UDKManagementForm()
        {
            InitializeComponent();
            ds = new DataSet();
            conBuilder = new MySqlConnectionStringBuilder();
            conBuilder.Server = "localhost";
            conBuilder.UserID = "root";
            conBuilder.Password = "";
            conBuilder.Database = "knygynas";
            conBuilder.AllowZeroDateTime = true;

            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
            sqlCommand.CommandText = String.Format("SELECT * FROM UDK");
            MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                ds.Tables[0].Columns.Add();

            }
            sqlCon.Close();
        }

        private void UDKManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
