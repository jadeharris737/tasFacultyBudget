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
    public partial class frm_manageUsers : Form
    {
        public frm_manageUsers()
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
            //Set details for which user is logged in at the top of the side-nav based off the (public) variables set in hub
            //Change the text boxes to show the details accordingly
            username = frm_hub.username;
            role = frm_hub.role;
            lbl_username.Text = username;
            lbl_role.Text = role;

            //Reset any search in progress
            ResetSearch();
        }

        public void ResetSearch()
        {
            //Refresh datagridview
            ReadSubjectOverview();
            cmb_sortStage.Text = "";
            cmb_sortSubject.Text = "";
        }

        private static SQLiteConnection CreateConnection() //Create connection to SQL database
        {
            //Instatiate a new object SQLiteconnection known as sqlConnection
            SQLiteConnection sqlConnection = new SQLiteConnection();
            //Set connection to a path to the database (to find database) 
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            return sqlConnection;
        }

        public void ReadSubjectOverview()
        {
            SQLiteConnection sqlConnection = CreateConnection();

            //Deifne a select statement (SQL query) - selecting all columns except password for securtity reaons and new to reduce clutter
            string commandText = "SELECT userID, username, role, subject, stage FROM Users";

            //Create the datatable
            //Instatinate a DataTable object named datatable
            DataTable datatable = new DataTable();

            //Create SQLiteDataAdapter - used to populate the data table
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            myDataAdapter.Fill(datatable);
            //Close the connection with the database
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview
            dgv_users.DataSource = datatable;

            //Format datagridview design
            DataGridViewAesthetics();
        }

        private void DataGridViewAesthetics()
        {
            dgv_users.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); // Make the header of the columns bold

            DisableDataGridViewSorting(); //For funtionality, disable ability to select specific columns so only rows can be select

            //Prevent column header of selected cell from appearing chosen as well
            dgv_users.EnableHeadersVisualStyles = false;
            dgv_users.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_users.ColumnHeadersDefaultCellStyle.BackColor;
        }

        private void DisableDataGridViewSorting()//Prevent sorting use of datagridview to sort columns for interface aesthetics
        {
            //Prevent sorting using header (for aesthetics)
            for (int i = 0; i < dgv_users.Columns.Count; i++)
            {
                dgv_users.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        string selectedID;

        private void dgv_subjectOverview_SelectionChanged(object sender, EventArgs e) //Each time a user is selected from the datagridview
        {
            if (dgv_users.SelectedRows.Count > 0) //Prevent header row being considered a user
            {
                //Creates a connection to the SQL database
                SQLiteConnection sqlConnection = CreateConnection();

                //The ID of the selected user updates accordingly
                selectedID = dgv_users.SelectedRows[0].Cells[0].Value.ToString();
                //Define SELECT Statement/command
                string commandText = "SELECT * FROM Users WHERE userId=" + selectedID;

                //Create a datatable to save data in memory
                var datatable = new DataTable();
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

                //Open a connection with the database
                sqlConnection.Open();
                //Populate datatable with the query result
                myDataAdapter.Fill(datatable);
                //Close connection with the database
                sqlConnection.Close();


                //Fill other data about the user from datatable into form controls
                txt_username.Text = datatable.Rows[0]["username"].ToString();
                txt_password.Text = datatable.Rows[0]["password"].ToString();
                cmb_role.Text = datatable.Rows[0]["role"].ToString();
                cmb_subject.Text = datatable.Rows[0]["subject"].ToString();
                cmb_stage.Text = datatable.Rows[0]["stage"].ToString();

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

        private void btn_resetPassword_Click(object sender, EventArgs e) //Change curently selected user's password to the text entered in the password box
        {
            if (txt_password.Text != "") //Providing the password is not empty
            {
                if (txt_password.Text.Count() > 6) //Ensure password is secure (greater thaan 6 characters) before it becomes data entered into the system
                {
                    if (MessageBox.Show($"Are you sure you wish to change {txt_username.Text}'s password? \n(This is irreversible unless you reset again)", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    //Deter user accidentally reseting a password or changing the wrong user's password by requiring user to respond to a confirmation box
                    {
                        //Establish connection with SQLite database file
                        SQLiteConnection sqlConnection = new SQLiteConnection();
                        sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                        //Instantiate a new SQL command object
                        SQLiteCommand sqlCommand = new SQLiteCommand();

                        //Customise the SQL command arguments associated with it
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.Text;

                        //Set userID to the ID of the currently selected row 
                        string userID = dgv_users.SelectedRows[0].Cells[0].Value.ToString();

                        //Define a command statement (SQL query) to change the user's password to the new version
                        sqlCommand.CommandText = $"UPDATE Users SET password='{txt_password.Text}' WHERE userID='" + userID + "'";

                        //Open a connection with the database
                        sqlConnection.Open();
                        //Execute the command
                        sqlCommand.ExecuteNonQuery();
                        //Close connection with the database
                        sqlConnection.Close();

                        //Notify the user it was successful
                        MessageBox.Show("Password changed.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Update the manage users datagridview with the new user
                        ReadSubjectOverview();
                    }
                }
                else
                {
                    //Notify user of issue
                    MessageBox.Show("Password must be greater than 6 characters for security.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //Notify user of issue
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_newUser_Click(object sender, EventArgs e) //Open add a user form
        {
            ShowAddUserForm(); //Allow user to intuitively and quickly add a new user (without having to use the side-nav)
        }

        private void btn_updateUser_Click(object sender, EventArgs e)
        {
            if (cmb_role.Text != "") //Providing the role detail is not empty
            {
                if (cmb_subject.Text != "") //and the user's subject detail is not empty
                {
                    if (cmb_stage.Text != "") //and the user's stage detail is not empty
                    {
                        //Deter accidentally updating the information of a user
                        if (MessageBox.Show($"Are you sure you wish to update {txt_username.Text}?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            if (txt_username.Text == username) //Ensure user cannot update the details of themselves to maintain intuitiveness of the hierachy system (for intuitiveness and consistency as users cannot delete themselves either)
                            {
                                //Notify user of the issue
                                MessageBox.Show("You cannot edit these details of your own user, log in as another user to change your current user then log back in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                UpdateUser();
                            }
                        }
                    }
                    else
                    {
                        //Notify user of the issue
                        MessageBox.Show("User must have a stage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    //Notify user of the issue
                    MessageBox.Show("User must have a subject.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                //Notify user of the issue
                MessageBox.Show("User must have a role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void UpdateUser()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update the selected user's details
            sqlCommand.CommandText = $"UPDATE Users SET role='{cmb_role.Text}', subject='{cmb_subject.Text}', stage='{cmb_stage.Text}' WHERE username='" + txt_username.Text + "'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Notify the user it was successful
            MessageBox.Show($"{txt_username.Text} updated.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Update the manage users datagridview with the changed details of the user
            ReadSubjectOverview();
        }

        private void btn_deleteUser_Click(object sender, EventArgs e) //Allow admin users to delete other users of the system
        {
            //As a layer of system protection ensure there is at least one user on the system
            if (dgv_users.Rows.Count == 1) 
            {
                MessageBox.Show("System must have at least 1 user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            //Assuming the user is deleting Admin because they are following the tutorial, then they must be in the setup stages so the extra informative dialog boxes serve to guide them through the process
            //Additionally, the Admin user is the only user that can be deleted while logged into its profile.
            else if (txt_username.Text == "Admin") 
            {
                if (MessageBox.Show($"Are you sure you wish to delete Admin and another user with the Head Teacher or Principal role exists (and recommended you have followed the tutorial thus far)? \n(This is irreversible)", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DeleteSelectedUser();

                    MessageBox.Show("Admin user deleted. Logging out now - please log in as another user you've created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart(); //Restart application because despite Admin being the only user which can be deleted while logged in to the account, the user has now been deleted so the user must log in as a different user
                }
            }
            else if (txt_username.Text == frm_hub.username) //Ensure users cannot delete themselves as this is not intuitive
            {
                MessageBox.Show("Cannot delete yourself (your own user).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                DeleteSelectedUser();
            }
        }

        private void DeleteSelectedUser() //Delete the current user selected in the datagridview
        {
            //Deter accidental deletings
            if (MessageBox.Show($"Are you sure you wish to delete {txt_username.Text}? \nThis will NOT remove any purchase orders made by them, however, you will NOT be able to SUDO as this user. \n(This is irreversible)", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = new SQLiteConnection();
                sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                //Instantiate a new SQL command object
                SQLiteCommand sqlCommand = new SQLiteCommand();

                //Customise the SQL command arguments associated with it
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;

                //Set the unique userID to the ID of the currently selected row 
                string userID = dgv_users.SelectedRows[0].Cells[0].Value.ToString();
                sqlCommand.CommandText = $"DELETE FROM Users WHERE userID='" + userID + "'";

                //Open a connection with the database
                sqlConnection.Open();
                //Execute the command
                sqlCommand.ExecuteNonQuery();
                //Close connection with the database
                sqlConnection.Close();

                //Notify the user it was successful
                MessageBox.Show($"{txt_username.Text} removed.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Update the manage users datagridview, removing the deleted user
                ReadSubjectOverview();
            }
        }

        private void cmb_sortStage_SelectedIndexChanged(object sender, EventArgs e) //Get users allocated to a stage
        {
            cmb_sortSubject.Text = ""; //Only sort by either stage or subject, not both, so clear the other sort if active

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Define a SELECT statement (SQLite query) to search for users allocated to a specific stage
            string commandText = "SELECT userID, username, role, subject, stage FROM Users WHERE stage=" + cmb_sortStage.Text;

            var datatable = new DataTable();
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);
            //Close connection with the database
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview
            dgv_users.DataSource = datatable;

        }

        private void cmb_sortSubject_SelectedIndexChanged(object sender, EventArgs e) //Get users allocated to a subject
        {
            cmb_sortStage.Text = ""; //Only sort by either stage or subject, not both, so clear the other sort if active

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Define a SELECT statement (SQLite query) to search for users allocated to a specific subject
            string commandText = "SELECT userID, username, role, subject, stage FROM Users WHERE subject='" + cmb_sortSubject.Text + "'";

            var datatable = new DataTable();
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);
            //Close connection with the database
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview
            dgv_users.DataSource = datatable;
        }

        private void cmb_subject_SelectedIndexChanged(object sender, EventArgs e) //Populate options for user subject and stages accordingly
        {
            if (cmb_subject.SelectedIndex != -1) //As this event triggers whenever the subject index is changed (even if that means it is empty), avoid errors by ensuring a subject has been chosen
            {
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
            try //Utilise the try and catch statement (as an error will occur when trying to convert to float) to avoid a runtime error when the stage is changed between which user is selected
            //in the datagridview
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

            }
        }



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


        public static frm_profile profileForm = new frm_profile();
        private void ShowProfileForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            profileForm.Show();
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

        private void ShowAddUserForm()
        {
            //Collapse all menus
            HideSubMenu();

            frm_addUserForManageUser addUserForm = new frm_addUserForManageUser(this);
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
        } //Collapsed code to show the different forms

        private void btn_home_Click(object sender, EventArgs e)
        {
            ShowHubForm();
        }

        private void btn_clearSearch_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }


        private void btn_logout_Click(object sender, EventArgs e) //Emulate an exit/logout function
        {
            Application.Restart();
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


        private void btn_subjectOverview_Click(object sender, EventArgs e)
        {
            ShowSubjectOveriewForm();
        }


        private void btn_transactionHistory_Click(object sender, EventArgs e)
        {
            ShowTransactionHistoryForm();
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            ShowProfileForm();
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
        } //Collapsed code for navigation bar button events


    }
}
