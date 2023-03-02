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
    public partial class frm_addUser : Form
    {

        public frm_addUser()
        {
            InitializeComponent();
        }

        //As the form has no borderstyle, create a custom close button. If the 'X' button is clicked, close the form.
        //Unlike the other forms which use .hide(), .close() is used because AddUser appears as a .ShowDialog
        private void btn_close_Click(object sender, EventArgs e)
        {
            //Close the add user form now the user has been created
            Close();
        }

        private void lbl_showpass_MouseEnter(object sender, EventArgs e) //Show password when user hovers (mouse enters)
        {
            txt_password.HideSelection = true; //For aeshtetics
            lbl_showpass.Text = "HIDE"; //Indicate that the password can he hidden 
            txt_password.UseSystemPasswordChar = false; //Change the textbox to use plain text (show the password)
        }

        private void lbl_showpass_MouseLeave(object sender, EventArgs e) //Censor password when user stops hovering (mouse exists)
        {
            lbl_showpass.Text = "SHOW"; //Indicate that the password can be shown
            txt_password.UseSystemPasswordChar = true; //Change the textbox to use password characters (censor the password)
        }

        private void cmb_role_KeyPress(object sender, KeyPressEventArgs e) //Ensure that only valid information enters the system (user must choose one of the 4 roles)
        {
            e.Handled = true; //'Handle' typing so no text is entered
            MessageBox.Show("Cannot type. Select a role using drop down.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Notify the user
        }

        private void cmb_subject_KeyPress(object sender, KeyPressEventArgs e) //Ensure that only valid information enters the system (user must choose a subject)
        {
            e.Handled = true; //'Handle' typing so no text is entered
            MessageBox.Show("Cannot type. Select a subject using drop down.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Notify the user
        }


        private void btn_generate_Click(object sender, EventArgs e) //Generate a password if the user selects the button
        {
            GeneratePassword();
        }

        private void GeneratePassword() //Randomly generate a password
        {
            string passwordGenerated = ""; //Reset the password string to empty (prevent the password just being added on to)
            Random randomIndex = new Random(); //Initialize random number function to create a random index (which is used to choose a random character from the allowed characters)

            string allowedCharacters = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-"; //These are the characters the password can be created from
            char[] passwordCharacters = new char[12]; //Create a character array for the password that is 12 characters long

            for (int i = 0; i < 12; i++) //For 12 characters
            {
                passwordCharacters[i] = allowedCharacters[randomIndex.Next(0, allowedCharacters.Length)]; //For each character in the password array (1-12), choose a random number between 0 and the length of the allowed characters.
                //NOTE: Because Random.Next goes from min-value to max-value, allowedCharacters.Length does not need - 1 (because it does not include the complete length)
            }

            foreach (var character in passwordCharacters) //As generating the password involved breaking the password into a 12 character array then assigning each character, piece together the password in a complete string
            {
                passwordGenerated += character; //Add each character into a string version of the password generated
            }

            txt_password.Text = passwordGenerated; //Insert the randomly generated password string into the textbox
        }


        private void btn_addUser_Click(object sender, EventArgs e) //When the button is clicked to create a user, ensure that the fields are complete
        {
            //Ensure that all of the fieds are complete and notify the user if one is empty
            if (txt_username.Text == "" && txt_password.Text == "" && cmb_role.Text == "")
            {
                MessageBox.Show("No username or password entered or role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_password.Text == "" && cmb_role.Text == "")
            {
                MessageBox.Show("No password entered or role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_username.Text == "" && txt_password.Text == "")
            {
                MessageBox.Show("No username or password entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_username.Text == "" && cmb_role.Text == "")
            {
                MessageBox.Show("No username entered or role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_username.Text == "")
            {
                MessageBox.Show("No username entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_password.Text == "")
            {
                MessageBox.Show("No password entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmb_role.Text == "")
            {
                MessageBox.Show("No role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_username.Text == "Admin") //As the user Admin (with the password: Password) is provided in the application's documentation which is avaliable for any user to read, increase security by disabling the username for a normal user.
            {
                MessageBox.Show("Cannot create user named 'Admin'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                CheckUsernameUnique(); //Set if the username is unique (with the usernameUnique boolean)

                if (usernameUnique == true) //If the username is unique
                {
                    if (txt_password.TextLength < 6) //Ensure password is secure (greater thaan 6 characters) before it becomes data entered into the system
                    {
                        //Notify user of issue
                        MessageBox.Show("Password must be greater than 6 characters for security.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else //Else here means the password must be greater 6 chaaracters
                    {
                        //Establish connection with SQLite database file
                        SQLiteConnection sqlConnection = new SQLiteConnection();
                        sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                        //Instantiate a new SQL command object
                        SQLiteCommand sqlCommand = new SQLiteCommand();

                        //Customise the SQL command arguments associated with it
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.Text;

                        //Deifne a command statement (SQLite query) to insert a new user into the Users table
                        sqlCommand.CommandText = $"INSERT into Users (username,password,role, subject, stage, new) Values('{txt_username.Text}','{txt_password.Text}','{cmb_role.Text}','{cmb_subject.Text}','{cmb_stage.Text}','True')";

                      //Open a connection with the database
                        sqlConnection.Open();
                        //Execute the command
                        sqlCommand.ExecuteNonQuery();
                        //Close connection with the database
                        sqlConnection.Close();  

                        //Notify the user it was successful
                        MessageBox.Show("User added.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Close the add user form now the user has been created
                        Close();
                    }
                }
                else
                {
                    //Else means that usernameUnique must be false, so notify the user that the username of the new user must be different
                    MessageBox.Show("Must be a unique username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        bool usernameUnique = true; //Create a bool used to track if the username is unique
        private void CheckUsernameUnique() //Check if the username attempted for the new user has already been used
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Deifne a select statement (SQLite query) - * means select all
            string commandText = "SELECT * FROM Users WHERE username='" + txt_username.Text + "'";

            //Instantiate a new DataTable object (to store the data)
            var datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Populate datatable with the query result
            myDataAdapter.Fill(datatable);
            //Close connection with the database
            sqlConnection.Close();

            //Reset the number of usernames not unique to 0 (as a new username is created)
            int usernamesNotUnique = 0;

            for (int i = 0; i < datatable.Rows.Count; i++) //For each row (as the query returns any rows where the username is the same)
            {
                if (txt_username.Text == datatable.Rows[i]["username"].ToString())
                {
                    usernamesNotUnique++; //Add the row as a userame that was not unqiue
                }
            }

            if (usernamesNotUnique > 0) //Therefore, if all/any of the usernames did match
            {
                usernameUnique = false; //Store that the username was not unique (thus a user cannot be created)
            }
            else //Otherwise, no existing users must have had the same username
            {
                usernameUnique = true; //So a new user can be created
            }
        }

        private void cmb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStageDependingOnSubject();
        }

        private void FillStageDependingOnSubject() //If a subject with two stages is selected, then the stage combo box should be populated and enabled so that the user can specify which subject they are addressing
        {
            if (cmb_subject.SelectedIndex != -1) //As this event triggers whenever the subject index is changed (even if that means it is empty), avoid errors by ensuring a subject has been chosen
            {
                cmb_stage.SelectedIndex = -1; //To avoid issue where subjects with the same stage would not trigger the change event, change stage event anyhow
                cmb_stage.Enabled = false; //Disable stage combobox

                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = new SQLiteConnection();
                sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                //Deifne a select statement (SQLite query) - * means select all
                string commandText = "SELECT * FROM SubjectOverview WHERE subjectName='" + cmb_subject.Text + "'";

                //Instantiate a new DataTable object (to store the data)
                var datatable = new DataTable();

                //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

                //Open a connection with the database
                sqlConnection.Open();
                //Populate datatable with the query result
                myDataAdapter.Fill(datatable);
                //Close connection with the database
                sqlConnection.Close();

                //If any of the subjects are chosen that have two stages (these are only stage 5 and 6)
                if (cmb_subject.Text == "Food Technology" || cmb_subject.Text == "Industrial Technology Timber" || cmb_subject.Text == "Industrial Technology Graphics" || cmb_subject.Text == "Design and Technology" || cmb_subject.Text == "Industrial Technology Multimedia")
                {
                    //Set the stage index to nothing (so that the user must choose a stage)
                    cmb_stage.SelectedIndex = -1;
                    //Enable the stage combobox (so user can choose a stage)
                    cmb_stage.Enabled = true;
                    //Remove the stage 4 item (as these subjects only have stage 5 and 6)
                    cmb_stage.Items.Remove("4");
                }
                else if (cmb_subject.Text == "Technology Mandatory")
                {
                    //Add the stage 4 item as Technology Mandatory is the only stage 4 subject (by adding the stage 4 item, the cmb_stage_SelectedIndexChanged event will trigger)
                    cmb_stage.Items.Add("4");
                    //Fill the subject's associated stage into the combo box
                    cmb_stage.Text = datatable.Rows[0]["stage"].ToString();
                }
                else
                {
                    //Fill the subject's associated stage into the combo box
                    cmb_stage.Text = datatable.Rows[0]["stage"].ToString();
                }
            }
        }

        private void cmb_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIfSubjectandStageActive();
        }

        private void CheckIfSubjectandStageActive() //Check if the subject and stage is active
        {
            if (cmb_stage.SelectedIndex != -1) //As this event triggers whenever the stage index is changed (even if that means it is empty), avoid errors by ensuring a stage has be chosen
            {
                try //Utilise the try and catch statement (as an error will occur when trying to convert to float) to test whether the stage is active or inactive
                {
                    //Establish connection with SQLite database file
                    SQLiteConnection sqlConnection = new SQLiteConnection();
                    sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                    //Define a SELECT statement (SQLite query) - * means select all
                    string commandText = "SELECT * FROM SubjectOverview WHERE subjectName='" + cmb_subject.Text + "' AND stage='" + cmb_stage.Text + "'";

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

                    //If this fails and throws an error (because a datatable with no rows cannot be converted to a float), then subject must be inactive
                    float subBudgetAllocated = float.Parse(datatable.Rows[0]["allocatedsubBudget"].ToString());

                }
                catch
                {
                    //Advise user to have an active subject selected
                    MessageBox.Show("Advised to have a currently active stage and subject selected to maximise the any subject setting. \n(Activate subject in subject overview)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
    }
}
