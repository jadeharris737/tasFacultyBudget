using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite; // Use library 

namespace SDDH1_CODE_JADEHARRIS
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            GetLoginDetails(); //When the user attempts to log-in, populate the datagridview with their information to use in checking their details
            CheckLoginDetails(); //Check the login detail of the user against this populated datagridview. If their password matches then check if the user is new
            //and allow them to enter to the system accordingly
        }

        private void GetLoginDetails()
        {
            //Establish connection
            SQLiteConnection sqlConnection = new SQLiteConnection();
            //Direct connection to link with database file
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";
            //Select all of the rows where the usernamne (should only be one or none) matches the user attempting to log in (by using the username in txt_username.Text)
            string commandText = "SELECT * FROM Users WHERE username='" + txt_username.Text + "'";

            //Create a new data table to store the rows found
            DataTable datatable = new DataTable();
            //Create a new data adapter using the connection to the database and command (find rows which match the username)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open the connection to the database
            sqlConnection.Open();
            //Populate datatable with results of the command text
            myDataAdapter.Fill(datatable);
            //Take data from datatable and place it into the dataGridView by setting the dataGridView's source to the datatable
            dgv_userLoginDetails.DataSource = datatable;
            //Close the connection after retrieval
            sqlConnection.Close();
        }

        bool newUser;
        private void CheckIfNew()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) - * means select all
            string commandText = "SELECT * FROM Users WHERE username='" + txt_username.Text + "'";

            //Instantiate a new DataTable object (to store the data from the database)
            var datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);
            //Close connection with the database
            sqlConnection.Close();

            //Determine whether the 'new' column for the user is True or False. If it is true then the user is new and should be shown the welcome screen, otherwise they can
            //progress straight to the hub form
            newUser = bool.Parse(datatable.Rows[0]["new"].ToString());

            if (newUser == true)
            {
                //Show the hub form now as user has successfully logged in
                frm_welcomeUser welcomeUserForm = new frm_welcomeUser();
                welcomeUserForm.ShowDialog();
            }
            else
            {
                frm_hub hubForm = new frm_hub();
                hubForm.Show();
            }
        }


        private void CheckLoginDetails()
        {
            //If a user exists with that username (dataGridView would be populated with 1 row where the User=username)
            if (dgv_userLoginDetails.Rows.Count > 1)
            {
                //If the password the user has entered matches the password associated with that user (password stored in the fourth column therefore has a cell ID of 4)
                if (txt_password.Text == dgv_userLoginDetails.Rows[0].Cells[5].Value.ToString())
                {
                    //Set variables used in the hub form based off which user logged in here
                    SetHubFormsUserVariables();
                    //Hide this form
                    Hide();

                    CheckIfNew();
                }
                //Otherwise, if no password has been entered notify user
                else if (txt_password.Text == "")
                {
                    MessageBox.Show("No password entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Otherwise if the user has entered a password and it does not match the one associated with the user, then the password must be incorrect
                else
                {
                    MessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //For a more refined program, error messages personalised to the situation are placed here
            //These show only if no user has been found
            else if (txt_username.Text == "" && txt_password.Text == "")
            {
                MessageBox.Show("No password or username entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_username.Text == "")
            {
                MessageBox.Show("No username entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //This must be repeated (an identical error message can be seen above in the conditional if a user was found) because if a password has been entered should be checked twice
            else if (txt_password.Text == "")
            {
                MessageBox.Show("No password entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No user exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetHubFormsUserVariables()
        {
            //Set variables used in the hub form based off which user logged in here
            frm_hub.username = txt_username.Text;
            //Set role
            frm_hub.role = dgv_userLoginDetails.Rows[0].Cells[2].Value.ToString();

            frm_subjectOverview.username = txt_username.Text;
            //Set role
            frm_subjectOverview.role = dgv_userLoginDetails.Rows[0].Cells[2].Value.ToString();
        }


        private void lbl_showpass_MouseEnter(object sender, EventArgs e) //Show password when user hovers
        {
            lbl_showpass.Text = "HIDE";
            txt_password.UseSystemPasswordChar = false;
        }

        private void lbl_showpass_MouseLeave(object sender, EventArgs e) //Censor password when user stops hovering
        {
            lbl_showpass.Text = "SHOW";
            txt_password.UseSystemPasswordChar = true;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            //As the form has no borderstyle, create a custom close button. If the 'X' button is clicked, exit the system.
            System.Environment.Exit(1);
        }
    }
}
