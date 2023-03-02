using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace SDDH1_CODE_JADEHARRIS
{
    public partial class frm_welcomeUser : Form
    {

        public frm_welcomeUser()
        {
            InitializeComponent();
        }

        //As the form has no borderstyle, emulate a custom close button. If the 'X' button is clicked, close the form.
        //Unlike the other forms which use .hide(), .close() is used because AddUser appears as a .ShowDialog
        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_updateUser_Click(object sender, EventArgs e)
        {
            //Show the hub form now as user has successfully logged in
            frm_hub hub = new frm_hub();
            hub.Show();

            //Update the user in the database so that the welcome screen does not appear again 
            SetUserNotNewAnymore();

            //As the form has no borderstyle, create a custom close button. If the 'X' button is clicked, close the form.
            //Unlike the other forms which use .hide(), .close() is used because AddUser appears as a .ShowDialog
            Close();
        }

        private void SetUserNotNewAnymore()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update the users
            sqlCommand.CommandText = $"UPDATE Users SET new='False' WHERE username='" + frm_hub.username + "'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();
        }

        private void btn_openDocumentation_Click(object sender, EventArgs e)
        {
            //Open the pdf documentation file stored in the bin > Debug folder to provide the user with help
            Process.Start("systemDocumentation.pdf");
        }
    }
}
