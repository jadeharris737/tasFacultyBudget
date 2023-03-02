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
    public partial class frm_settings : Form
    {
        public frm_settings()
        {
            InitializeComponent();

            //Hide/collapse all submenus to start
            CollapseSubmenus();
        }

        //For code readability and convenience, assign the simple variable names role and username from frm_hub 
        string role = "";
        string username = "";

        //Variables used to store and track settings
        public static bool closeToHub = false;
        public static bool anySubject;

        //Use visiblechanged event so it triggers EACH TIME form appears instead of shown event (only first time)
        private void frm_subjectOverview_VisibleChanged(object sender, EventArgs e)
        {
            //Set details for which user is logged in at the top of the side-nav based off the (public) variables set in hub
            //Change the text boxes to show the details accordingly
            username = frm_hub.username;
            role = frm_hub.role;
            lbl_username.Text = username;
            lbl_role.Text = role;

            SetSettings(); //Set the settings based on the setting stored in the database (so the setting is set each time you log in)
            ChangeButtons(); //Change the appearance of the buttons based off their setting
        }

        private void SetSettings()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) to select all settings
            string commandText = "SELECT * FROM Settings";

            //Instantiate a new DataTable object (to store the data from the database)
            var datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);
            //Close a connection with the database
            sqlConnection.Close();

            //Set the anySubject global variable to the bool variable stored in the database (in this way, it allows the setitng to be accessed in the program)
            anySubject = bool.Parse(datatable.Rows[0]["Toggle"].ToString());
        }

        private void ChangeButtons() //Set the appearance of the setting's buttons to correspond to their setting in the databsae
        {
            if (anySubject == true) //Make the anySubject setting appear ON
            {
                btn_anySubject.Text = "ON";
                btn_anySubject.BackColor = Color.PaleGreen;
                btn_anySubject.ForeColor = Color.Black;
            }
            else //Make the anySubject setting appear OFF
            {
                btn_anySubject.Text = "OFF";
                btn_anySubject.BackColor = Color.FromArgb(255, 128, 128);
                btn_anySubject.ForeColor = Color.White;
            }
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



        private void btn_closeToHub_Click(object sender, EventArgs e) //Toggle the closeToHub setting button and set the appearance of the closeToHub to correspond
        {
            if (closeToHub == true) //If the setting is currently set to ON, then a user clicking the button would want the setting to be turned off
            {
                btn_closeToHub.Text = "OFF"; //Make the setting appear OFF
                closeToHub = false;
                btn_closeToHub.BackColor = Color.FromArgb(255, 128, 128);
                btn_closeToHub.ForeColor = Color.White;


                MessageBox.Show("Exit system immediately when the 'X' close button is pressed.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user
            }
            else
            {
                btn_closeToHub.Text = "ON"; //Make the setting appear ON
                closeToHub = true;
                btn_closeToHub.BackColor = Color.PaleGreen;
                btn_closeToHub.ForeColor = Color.Black;


                MessageBox.Show("Return to hub before exiting program (use the hub 'X' button to completely exit program).", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user
            }
        }

        private void btn_anySubject_Click(object sender, EventArgs e) //Allow ADMINS ONLY to toggle the anySubject setting button and set the appearance of the closeToHub to correspond
        {
            if (role == "Principal" || role == "Head Teacher") //As this is an admin setting, ensure only admin's can toggle the button
            {
                if (anySubject == true) //If the setting is currently set to ON, then a user clicking the button would want the setting to be turned off
                {
                    btn_anySubject.Text = "OFF"; //Make the setting appear OFF
                    anySubject = false;
                    btn_anySubject.BackColor = Color.FromArgb(255, 128, 128);
                    btn_anySubject.ForeColor = Color.White;

                    AnySubjectFalse(); //Change the setting to off in the database so that the setting is remembered for the next log-in time

                    MessageBox.Show("Users can only make purchases from their allocated subject and stage.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user
                }
                else
                {
                    btn_anySubject.Text = "ON"; //Make the setting appear ON
                    anySubject = true;
                    btn_anySubject.BackColor = Color.PaleGreen;
                    btn_anySubject.ForeColor = Color.Black;

                    AnySubjectTrue(); //Change the setting to n in the database so that the setting is remembered for the next log-in time


                    MessageBox.Show("Users can allocate purchases to any subjects that are active.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information); //Notify user
                }
            }
            else
            {
                MessageBox.Show("No permission to change this admin setting. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop); //Refuse access to the user
            }
        }

        private void AnySubjectTrue() //Save anySubject setting as ON
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to change the setting to False (off)
            sqlCommand.CommandText = $"UPDATE Settings SET Toggle='true' WHERE Setting='AnySubject'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close the connection with the database
            sqlConnection.Close();
        }

        private void AnySubjectFalse() //Save anySubject setting as OFF
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to change the setting to False (off)
            sqlCommand.CommandText = $"UPDATE Settings SET Toggle='false' WHERE Setting='AnySubject'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close the connection with the database
            sqlConnection.Close();

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

        private void btn_profile_Click(object sender, EventArgs e)
        {
            ShowProfileForm();
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

        private void btn_settingLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        } //Collapsed code for navigation bar button events

    }
}
