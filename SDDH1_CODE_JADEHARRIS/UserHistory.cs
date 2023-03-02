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
    public partial class frm_userHistory : Form
    {
        public frm_userHistory()
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

            //Change the txt_user box to indicate the datagridview is displaying the current user's hitory
            txt_user.Text = username;

            //Reset any search in progress
            ResetSearch();
        }

        public void ResetSearch()
        {
            //Refresh datagridview
            ReadSubjectOverview();

            //Clear search controls (to indicate no search active)
            cmb_sortStage.Text = "";
            cmb_sortSubject.Text = "";
            chkbx_sortActive.Checked = false;
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

            //Deifne a select statement (SQL query) - * means select all
            //By defining a string here, no need to rewrite
            string commandText = "SELECT  * FROM TransactionHistory WHERE user='" + txt_user.Text + "'";

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
            dgv_userHistory.DataSource = datatable;

            //Format datagridview design
            DataGridViewAesthetics();
        }

        private void DataGridViewAesthetics()
        {
            dgv_userHistory.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); //Make the header of the columns bold

            DisableDataGridViewSorting(); //For funtionality, disable ability to select specific columns so only rows can be selected

            //Prevent column header of selected cell from appearing chosen as well
            dgv_userHistory.EnableHeadersVisualStyles = false;
            dgv_userHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_userHistory.ColumnHeadersDefaultCellStyle.BackColor;

            try //If the datagridview is empty (say user has made no purchases), the try catch statement prevents a runtime error.
            {
                //Prevent column header of selected cell from appearing chosen as well
                dgv_userHistory.EnableHeadersVisualStyles = false;
                dgv_userHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_userHistory.ColumnHeadersDefaultCellStyle.BackColor;
                //Size columns of the datagridview
                dgv_userHistory.Columns[0].Width = 25;
                dgv_userHistory.Columns[1].Width = 50;
                dgv_userHistory.Columns[4].Width = 200;
            }
            catch
            {
            }

            //Aesthetically indicate whether item is enabled or disabled 
            CheckActiveItems();

        }

        private void DisableDataGridViewSorting() //Disable sorting ability in datagridview for aesthetics
        {
            //Prevent sorting with datagridview header (for aesthetics)
            for (int i = 0; i < dgv_userHistory.Columns.Count; i++)
            {
                dgv_userHistory.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void CheckActiveItems() //Set active items to white and inactive items to false
        {
            //For each row
            for (int i = 0; i < dgv_userHistory.Rows.Count; ++i)
            {
                //If the item has been disabled (set to 'false')
                if (dgv_userHistory.Rows[i].Cells[9].Value.ToString() == "False")
                {
                    for (int j = 0; j < 10; j++)
                    {
                        //Set each of the 10 cells in that row to have the background colour of silver (to indicate disabled)
                        dgv_userHistory.Rows[i].Cells[j].Style.BackColor = Color.Silver;
                    }
                }
                else
                //Otherwise, if 'active' is not false (thus must be 'true' - item is enabled)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        //Set each of the 10 cells in that row to have the background colour of white (to indicate enabled)
                        dgv_userHistory.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
            }
        }

        public void SetSudoUser() //This public function is used to change the user whose transaction hisotry is displayed
        {
            txt_user.Text = frm_sudoForUserHistory.currentUser;
        }

        private void btn_sudo_Click(object sender, EventArgs e) //Tool to view all users in the system and select one to display their purchase history of
        {
            if (role == "Principal" || role == "Head Teacher") //Ensure only admins can use this tool
            {
                //Display the sudo user form
                frm_sudoForUserHistory sudoForm = new frm_sudoForUserHistory(this);
                sudoForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No permission to sudo as other users. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop); //Notify user they do not have permisison
            }
        }



        bool checkboxEventResetByStage = false; //Indicate whether the CheckChanged event was triggered by the stage selection making the active box unchecked, or by the user
        private void cmb_stageSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //The datagridivew is only sorted by either stage, active status or subject at a time, so reset other searches.
            chkbx_sortActive.Checked = false;
            cmb_sortSubject.Text = "";
            //However, as this will trigger the checkbox checkedChanged event, set this bool to true so that the event is skipped
            if (chkbx_sortActive.Checked == true)
            {
                checkboxEventResetByStage = true;
            }

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Define a SELECT statement (SQLite query) to search user hisotry for purchases allocated at a stage
            string commandText = "SELECT * FROM TransactionHistory WHERE stage=" + cmb_sortStage.Text + " AND user='" + txt_user.Text + "'";

            //Instantiate a new DataTable object (to store the data from the database)
            var datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDateAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            myDateAdapter.Fill(datatable);
            //Close the connection with the database
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview
            dgv_userHistory.DataSource = datatable;

            //Update aesthetics to indicate if subject is enabled or disabled (even after search)
            CheckActiveItems();
        }

        private void chkbx_sortActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxEventResetByStage == false) //If the checkbox was not reset from stage or subject (as they change the chkbx_sortActive.Checked )
            {
                cmb_sortStage.Text = "";
                cmb_sortSubject.Text = "";

                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = CreateConnection();

                //Create SELECT statement depending on whether the user is searching for active or inactive items
                string commandText;
                if (chkbx_sortActive.Checked == true)
                {
                    commandText = "SELECT * FROM TransactionHistory WHERE active='True' AND user='" + txt_user.Text + "'";
                }
                else
                {
                    commandText = "SELECT * FROM TransactionHistory WHERE active='False' AND user='" + txt_user.Text + "'";
                }

                //Instantiate a new DataTable object (to store the data from the database)
                var datatable = new DataTable();

                //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
                SQLiteDataAdapter myDateAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

                //Open a connection with the database
                sqlConnection.Open();
                //Execute the command
                myDateAdapter.Fill(datatable);
                //Close the connection with the database
                sqlConnection.Close();

                //Take data from datatable and place into visual datagridview
                dgv_userHistory.DataSource = datatable;

                //Update aesthetics to indicate if item is enabled or disabled (even after search)
                CheckActiveItems();
            }
            checkboxEventResetByStage = false;
        }

        private void cmb_sortSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //The datagridivew is only sorted by either stage, active status or subject at a time so reset other searches
            chkbx_sortActive.Checked = false;
            cmb_sortStage.Text = "";
            //However, as this will trigger the checkbox checkedChanged event, set this bool to true so that the event is skipped
            if (chkbx_sortActive.Checked == true)
            {
                checkboxEventResetByStage = true;
            }

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Select subjects where stage is the same
            string commandText = "SELECT * FROM TransactionHistory WHERE subject='" + cmb_sortSubject.Text + "' AND user='" + txt_user.Text + "'";


            //Instantiate a new DataTable object (to store the data from the database)
            var datatable = new DataTable();

            //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
            SQLiteDataAdapter myDateAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            myDateAdapter.Fill(datatable);
            //Close the connection with the database
            sqlConnection.Close();

            dgv_userHistory.DataSource = datatable;


            //Update aesthetics to indicate if subject is enabled or disabled (even after search)
            CheckActiveItems();
        }

        private void btn_clearSearch_Click(object sender, EventArgs e) //Reset any active search
        {
            ResetSearch();
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

        private void btn_settings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        }


        private void btn_makePurchaseOrder_Click(object sender, EventArgs e)
        {
            ShowTransactionHistoryForm();
        } //Collapsed code for navigation bar button events



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
    }
}
