using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Provide access to the System.Data.SQLite nuGet package which the project uses to read and edit the SQLite database.
using System.Data.SQLite;

namespace SDDH1_CODE_JADEHARRIS
{
    public partial class frm_sudoForNewOrder : Form
    {

        frm_newPurchaseOrder newPurchaseOrderForm;

        public frm_sudoForNewOrder(frm_newPurchaseOrder newPurchaseOrderCallingForm) //Create a form reference so that functions from New Purchase Order can be called
        {
            newPurchaseOrderForm = newPurchaseOrderCallingForm;

            InitializeComponent();
            PopulateWithUsers(); //Populate the datatgridview which users can select a user from
        }

        private void PopulateWithUsers()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) - get the username of all users
            string commandText = "SELECT username FROM Users";

            //Instantiate a new DataTable object (to store the data from the database)
            DataTable datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);
            //Close connection with the database
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview (which has been aesthetically customised to suit a user
            //selecting one other user from it.
            dgv_users.DataSource = datatable;
        }


        private void btn_close_Click(object sender, EventArgs e)
        {
            //As the form has no borderstyle, emulate a custom close button. If the 'X' button is clicked, close the form.
            Close();
        }


        public static string currentUser;
        private void dgv_users_SelectionChanged(object sender, EventArgs e) //Whenever a different user is selected to SUDO as
        {
            try
            {
                currentUser = dgv_users.SelectedRows[0].Cells[0].Value.ToString(); //Set the current user public variable to the username selected
                txt_username.Text = currentUser; //Set the username textbox to this username (so the user knows that is the currently selected user)
            }
            catch //If a selection change event is triggered accidentally as the form is closing, prevent a runtime error with the catch statement
            {
            }

        }

        private void btn_selectUser_Click(object sender, EventArgs e) //When the select user button is clicked
        {
            //Call this functiom to set the user field of a new purchase order as the user selected in this form (by reading the public currentUser variable).
            newPurchaseOrderForm.setSudoUser(); 

            MessageBox.Show($"Making purchase order as {txt_username.Text}.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user of success
            Close(); //Close this form
        }

        private void dgv_users_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
