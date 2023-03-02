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

namespace SDDH1_CODE_JADEHARRIS
{
    public partial class frm_profile : Form
    {
        public frm_profile()
        {
            InitializeComponent();
            //Hide/collapse all submenus to start
            CollapseSubmenus();
        }

        //For code readability and convenience, assign the simple variable names role and username from frm_hub 
        string role = "";
        string username = "";

        //Use visiblechanged event so it triggers EACH TIME form appears instead of shown event (only first time)
        private void frm_subjectOverview_VisibleChanged(object sender, EventArgs e)
        {
            //Fill details of the 
            username = frm_hub.username;
            role = frm_hub.role;
            lbl_username.Text = username;
            lbl_role.Text = role;

            GetUserDetails(); //Get details of the user to populate the datagridview which will be used to fill the controls
            FillUserDetails(); //Fill the text with the user's information for them to view
        }


        private void GetUserDetails()
        {
            //Establish connection
            SQLiteConnection sqlConnection = new SQLiteConnection();
            //Direct connection to link with database file
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";
            //Select all of the rows where the usernamne (should only be one or none) matches the user attempting to log in (by using the username in txt_username.Text)
            string commandText = "SELECT * FROM Users WHERE username='" + username + "'";

            //Create a new data table to store the rows found
            DataTable datatable = new DataTable();
            //Create a new data adapter using the connection to the database and command (find rows which match the username)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open the connection to the database
            sqlConnection.Open();
            //Populate datatable with results of the command text
            myDataAdapter.Fill(datatable);
            //Take data from datatable and place it into the dataGridView by setting the dataGridView's source to the datatable
            dgv_userDetails.DataSource = datatable;
            //Close the connection after retrieval
            sqlConnection.Close();
        }

        private void FillUserDetails() //Display user's details
        {
            //Fill controls with the associated information
            txt_username.Text = username;
            txt_role.Text = role;
            txt_password.Text = dgv_userDetails.Rows[0].Cells[5].Value.ToString();
            txt_subject.Text = dgv_userDetails.Rows[0].Cells[3].Value.ToString();
            txt_stage.Text = dgv_userDetails.Rows[0].Cells[4].Value.ToString();
        }

        private void btn_close_Click(object sender, EventArgs e) //As the form has no borderstyle, emulate a custom close button. 
        {
            //Using setting's (publicly accessible) closeToHub variable which stores whether the setting is activated, determine whether the 'X' immediately exits the program or returns to hub first
            //(Then the 'X' button on hub will close the program)
            if (frm_settings.closeToHub == true)
            {
                Hide();
                ShowHubForm(); //Return to hub form
            }
            else
            {
                //Otherwise, if the user wants to exit the program instantly when the 'X' button is clicked, exit the application.
                System.Environment.Exit(1);
            }
        }


        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txt_password.TextLength > 29) //Ensure thatpassword is not at a length that intercepts with interface
            {
                txt_password.Text = "";
                MessageBox.Show("Password must be less than 29 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); //Notify user
            }
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


        private void btn_resetPassword_Click(object sender, EventArgs e) //If the user wants to reset their password to what is entered in the textbox by clicking this button
        {
            if (txt_password.Text != "") //Ensure a password is entered
            {
                if (txt_password.TextLength < 6) //Ensure password is secure (greater thaan 6 characters) before it becomes data entered into the system
                {
                    MessageBox.Show("Password must be greater than 6 characters for security.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Otherwise, notify user if the password is not secure enough
                }
                else //If the password is acceptable though
                {
                    ChangePassword();
                }
            }
            else
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ChangePassword()
        {
            //Prevent accidental password changes by prompting response from a dialog box
            if (MessageBox.Show("Are you sure you wish to change your password \n(this is irreversible - ensure you remember the new password)?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = new SQLiteConnection();
                sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                //Instantiate a new SQL command object
                SQLiteCommand sqlCommand = new SQLiteCommand();

                //Customise the SQL command arguments associated with it
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;

                //Define a command statement (SQL query) to update the user's password
                sqlCommand.CommandText = $"UPDATE Users SET password='{txt_password.Text}' WHERE username='" + username + "'";


                //Open a connection with the database
                sqlConnection.Open();
                //Execute the command
                sqlCommand.ExecuteNonQuery();
                //Close a connection with the database
                sqlConnection.Close();

                MessageBox.Show("Your password has been changed.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user of success
            }
        }

        private void btn_generate_Click(object sender, EventArgs e) //Generate a password if the user selects the button
        {
            GeneratePassword();
        }

        private void GeneratePassword()
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


        public static frm_hub hubForm = new frm_hub();
        private void ShowHubForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            hubForm.Show();
            //Hide this form
            Hide();
        }

        public static frm_subjectOverview subjectOverviewForm = new frm_subjectOverview();
        private void ShowSubjectOveriewForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            subjectOverviewForm.Show();
            //Hide this form
            Hide();
        }

        public static frm_transactionHistory transactionHistoryForm = new frm_transactionHistory();
        private void ShowTransactionHistoryForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            transactionHistoryForm.Show();
            //Hide this form
            Hide();
        }

        public static frm_help helpForm = new frm_help();
        private void ShowHelpForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            helpForm.Show();
            //Hide this form
            Hide();
        }

        public static frm_userHistory userHistoryForm = new frm_userHistory();
        private void ShowUserHistoryForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            userHistoryForm.Show();
            //Hide this form
            Hide();

        }

        public static frm_manageUsers manageUsersForm = new frm_manageUsers();
        private void ShowManageUsersForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            manageUsersForm.Show();
            //Hide this form
            Hide();
        }

        private void ShowAddUserForm()
        {
            //Collapse all menus
            HideSubMenu();

            frm_addUser addUserForm = new frm_addUser();
            addUserForm.ShowDialog();
        }

        public static frm_settings settingsForm = new frm_settings();
        private void ShowSettingsForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            settingsForm.Show();
            //Hide this form
            Hide();
        } //Collapsed code to show the different forms


        private void CollapseSubmenus() //Hide/collapse all submenus
        {
            //Hide submenu panels by setting visibility to false
            pnl_boardSubmenu.Visible = false;
            pnl_userSubmenu.Visible = false;
            pnl_adminSubmenu.Visible = false;
        }


        private void HideSubMenu() //Hide/collapse all submenus (only if they are already shown)
        {
            //Hide any submenu panel previously shown (so intuitively no 2 dropdowns can be active)
            if (pnl_boardSubmenu.Visible == true)
            {
                pnl_boardSubmenu.Visible = false;
            }
            if (pnl_userSubmenu.Visible == true)
            {
                pnl_userSubmenu.Visible = false;
            }
            if (pnl_adminSubmenu.Visible == true)
            {
                pnl_adminSubmenu.Visible = false;
            }
        }


        private void ShowSubMenu(Panel subMenu) //Show the panel of the dropdown submenu selected by choosing its button
        {
            //If the submenu panel clicked is currently collapsed then open the submenu
            if (subMenu.Visible == false)
            {
                //If any submenu is open, then collapse its submenu to show only its menu button
                HideSubMenu();
                //Then show submenu
                subMenu.Visible = true;
            }
            //Otherwise (if submenu is currently visible or open), collapse submenu again
            else
            {
                subMenu.Visible = false;
            }
        } //Collapsed code which expands the navigation bar submenus


        private void btn_boards_Click(object sender, EventArgs e)
        {
            //Pass in boards panel as parameter to decide whether shown or hidden
            ShowSubMenu(pnl_boardSubmenu);
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            //Pass in boards panel as parameter to decide whether shown or hidden
            ShowSubMenu(pnl_userSubmenu);
        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
            //Pass in boards panel as parameter to decide whether shown or hidden
            ShowSubMenu(pnl_adminSubmenu);
        }

        private void btn_logout_Click(object sender, EventArgs e) //Emulate an exit/logout function
        {
            Application.Restart();
        }


        private void btn_home_Click(object sender, EventArgs e)
        {
            ShowHubForm();
        }
        private void btn_subjectOverview_Click(object sender, EventArgs e)
        {
            ShowSubjectOveriewForm();
        }

        private void btn_transactionHistory_Click(object sender, EventArgs e)
        {
            ShowTransactionHistoryForm();
        }

        private void btn_userHistory_Click(object sender, EventArgs e)
        {
            ShowUserHistoryForm();
        }

        private void btn_manageUsers_Click(object sender, EventArgs e)
        {
            if (role == "Principal" || role == "Head Teacher")
            {
                ShowManageUsersForm();
            }
            else
            {
                MessageBox.Show("No permission to manage users (restricted to Principal and Head Teacher). \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_addUser_Click(object sender, EventArgs e)
        {
            if (role == "Principal" || role == "Head Teacher")
            {
                ShowHubForm();
                ShowAddUserForm();
            }
            else
            {
                MessageBox.Show("No permission to add a user (restricted to Principal and Head Teacher). \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        } //Collapsed code for navigation bar button click events
    }
}
