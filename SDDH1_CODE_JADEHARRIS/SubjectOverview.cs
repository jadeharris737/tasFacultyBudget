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
    public partial class frm_subjectOverview : Form
    {
        public frm_subjectOverview()
        {
            InitializeComponent();

            //Hide/collapse all submenus to start
            CollapseSubmenus();
        }

        //For code readability and convenience, assign the simple variable names role and username from frm_hub 
        public static string role = "";
        public static string username = "";

        //Use visiblechanged event so the event triggers EACH TIME form appears instead of shown event (only first time the form appears)
        bool openingForm = true;
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
            //Reads the global budget from the database and sets what global budget is visible in the textbox
            SetGlobalBudget();

            if (openingForm == true) //As it should only be checked if the global budget has been set when the form is opening, toogle a boolean to check
            {
                CheckGlobalBudgetFirstTime();
            }
            else //openingForm must be false now, so next time the visibility is changed Subject Overview must be opening
            {
                openingForm = true;
            }

            //Update global calculations displayed in the left side-bar
            UpdateGlobalBudgetCalculations();

            //If an admin is accessing Subject Overview, then they have the security level to activate subjects so enable the ACTIVE checkbox. Otherwise disable
            if (role == "Principal" || role == "Head Teacher")
            {
                chkb_active.Enabled = true;
            }
            //Otherwise the user is not an admin, in which case they do not have permission to activate or deactivate subjects so disable checkbox
            else
            {
                chkb_active.Enabled = false;
            }
        }

        public void SetGlobalBudget() //Read the global budget stored in the database and set what global budget is visible in the textbox
        {
            try
            {
                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = CreateConnection();

                //Define a SELECT statement (SQLite query) to get entire global budget table
                string commandText = "SELECT  * FROM GlobalBudget";

                //Instantiate a new DataTable object (to store the data from the database)
                DataTable datatable = new DataTable();

                //Instantiate a new SQLiteDataAdapter which sends the command text with the sql connection (used to populate the datatable)
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

                //Open a connection with the database
                sqlConnection.Open();
                //Execute the command
                myDataAdapter.Fill(datatable);
                //Close the connection with the database
                sqlConnection.Close();


                //Take data from datatable and place into a datagridview (which is hidden - used only for coding purposes)
                dgv_globalBudget.DataSource = datatable;

                //Display the budget in the global budget allocated textbox. If this throws an error, the use of the try, catch statement means no budget is set.
                lbl_globalBudgetAllocated.Text = dgv_globalBudget.Rows[0].Cells[1].Value.ToString();
            }
            catch
            {
                //In which case, this should be indicated by a hyphen
                lbl_globalBudgetAllocated.Text = "-";
            }
        }



        public static bool setUp = false;
        private void CheckGlobalBudgetFirstTime() //Check when Subject Overview is opened if 
        {
            openingForm = false; //If this has been called because openingForm is true, then the form must now be open so openingForm becomes false
            if (lbl_globalBudgetAllocated.Text == "0") //If the global budget has not yet been set
            {
                if (role == "Head Teacher" || role == "Principal") //If the user is an admin 
                {
                    setUp = true; //Used to determine if the user can leave changeGlobalBudget or if a budget must be set (in which case a global budget must be set)
                    frm_changeGlobalBudget changeGlobalBudget = new frm_changeGlobalBudget(this);
                    changeGlobalBudget.ShowDialog();
                    setUp = false; //Now setup has been complete
                }
                else //Otherwise a user who is not an admin is trying to access a Subject Overview that does not have a global budget allocated, but they do not have permission to set the global budget
                {
                    MessageBox.Show("No global budget is set but you do not have permission to set one. \n\nThis effectively makes the program unusable so LOGGING OUT now. \n\nPlease contact your system administrator and direct them to follow FIRST TIME SETUP 'Set a Global Budget' in the application's documentation (under HELP tab)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Restart(); //In this way they need to contact a system administrator, and this program will log out in preparation

                }
            }
        }

        public void ResetSearch()
        {
            //Refresh datagridview and clear search terms
            ReadSubjectOverview();
            cmb_sortStage.Text = "";
            cmb_sortBudget.Text = "";
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
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Deifne a select statement (SQL query) - * means select all
            //By defining a string here, no need to rewrite
            string commandText = "SELECT  * FROM SubjectOverview";

            //Create the datatable
            //Instatinate a DataTable object named datatable
            DataTable datatable = new DataTable();

            //Create SQLiteDataAdapter - used to populate the data table
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

            //Open database - MUST be done 
            sqlConnection.Open();

            //Populate datatable
            myDataAdapter.Fill(datatable);

            //Close connection
            sqlConnection.Close();

            //Take data from datatable and place into visual datagridview
            dgv_subjectOverview.DataSource = datatable;

            //Format for datagridview design
            DataGridViewAesthetics();
        }

        private void DataGridViewAesthetics()
        {
            dgv_subjectOverview.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); // Make the header of the columns bold

            DisableDataGridViewSorting();//For funtionality, disable ability to select specific columns (only rows can be selected)

            //Fisable column header of selected cell from appearing selected as well
            dgv_subjectOverview.EnableHeadersVisualStyles = false;
            dgv_subjectOverview.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_subjectOverview.ColumnHeadersDefaultCellStyle.BackColor;

            //Size columns of the datagridview
            dgv_subjectOverview.Columns[0].Width = 75;
            dgv_subjectOverview.Columns[1].Width = 50;
            dgv_subjectOverview.Columns[2].Width = 250;

            //Aesthetically indicate whether subject is enabled or disabled 
            CheckActiveSubjects();
        }

        private void DisableDataGridViewSorting() //Disable sorting ability in datagridview for aesthetics
        {
            //Prevent sorting with datagridview header (for aesthetics)
            for (int i = 0; i < dgv_subjectOverview.Columns.Count; i++)
            {
                dgv_subjectOverview.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void CheckActiveSubjects()
        {
            //For each row
            for (int i = 0; i < dgv_subjectOverview.Rows.Count; ++i)
            {
                //If the subject has been disabled (set to 'false')
                if (dgv_subjectOverview.Rows[i].Cells[6].Value.ToString() == "False")
                {
                    for (int j = 0; j < 7; j++)
                    {
                        //Set each of the 7 cells in that row to have the background colour of silver (to indicate disabled)
                        dgv_subjectOverview.Rows[i].Cells[j].Style.BackColor = Color.Silver;
                    }
                }
                else
                //Otherwise, if 'active' is not false (thus must be 'true' - subject is enabled)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        //Set each of the 7 cells in that row to have the background colour of white (to indicate enabled)
                        dgv_subjectOverview.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
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


        string selectedID;
        private void dgv_subjectOverview_SelectionChanged(object sender, EventArgs e)
        {
            //If a row has been selected - prevent header row being considered a subject
            if (dgv_subjectOverview.SelectedRows.Count > 0)
            {
                //Creates a connection to the SQL database
                SQLiteConnection sqlConnection = CreateConnection();

                //The ID of the selected subject updates accordingly
                selectedID = dgv_subjectOverview.SelectedRows[0].Cells[0].Value.ToString();
                //Define SELECT Statement/command
                string commandText = "SELECT * FROM SubjectOverview WHERE subjectId=" + selectedID;

                //Create a datatable to save data in memory
                var datatable = new DataTable();
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(commandText, sqlConnection);

                //Open a connection with the database
                sqlConnection.Open();
                //Populate datatable with the query result
                myDataAdapter.Fill(datatable);
                //Close connection with the database
                sqlConnection.Close();

                //Fill data from datatable into form controls
                txt_subjectName.Text = datatable.Rows[0]["subjectName"].ToString();
                chkb_active.Checked = bool.Parse(datatable.Rows[0]["active"].ToString());

                //If the subject is currently inactive (the active cell value will be False) then disable the Change subBudget button as a sub budget does not yet exist to change
                if (dgv_subjectOverview.SelectedRows[0].Cells["active"].Value.ToString() == "False")
                {
                    btn_changeSubBudget.Enabled = false;
                }
                else //Otherwise the subject must be active (active cell must be True) so enable the ability to change the subBudget is has been allocated
                {
                    btn_changeSubBudget.Enabled = true;
                }
            }
            else
            {
                btn_changeSubBudget.Enabled = false;
            }
        }


        bool checkboxEventResetByStage = false;
        private void cmb_stageSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkbx_sortActive.Checked == true)
            {
                checkboxEventResetByStage = true;
            }
            chkbx_sortActive.Checked = false;
            cmb_sortBudget.Text = "";

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Define a SELECT statement (SQLite query) to search user hisotry for purchases allocated at a stage
            string commandText = "SELECT * FROM SubjectOverview WHERE stage=" + cmb_sortStage.Text;

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
            dgv_subjectOverview.DataSource = datatable;

            //Change aesthetics to indicate if subject is enabled or disabled (even after search)
            CheckActiveSubjects();
        }

        private void chkbx_sortActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxEventResetByStage == false) //If the checkbox was not reset from stage (as stage changes the chkbx_sortActive.Checked 
            {
                cmb_sortStage.Text = "";

                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = CreateConnection();

                //Define SELECT statement depending on whether the user is searching for active or inactive subjects
                string commandText;
                if (chkbx_sortActive.Checked == true)
                {
                    commandText = "SELECT * FROM SubjectOverview WHERE active='True'";
                }
                else
                {
                    commandText = "SELECT * FROM SubjectOverview WHERE active='False'";
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
                dgv_subjectOverview.DataSource = datatable;

                //Update aesthetics to indicate if subject is enabled or disabled (even after search)
                CheckActiveSubjects();
            }
            checkboxEventResetByStage = false;
        }



        private void cmb_sortBudget_SelectedIndexChanged(object sender, EventArgs e) //Sort datagridview by ascending and descending amount of global budget allocated. 
                                                                                     //This can be mixed with the other searches.
        {
            if (cmb_sortBudget.Text == "Lowest -> Highest")
            {
                dgv_subjectOverview.Sort(dgv_subjectOverview.Columns[3], ListSortDirection.Ascending);
            }
            else
            {
                dgv_subjectOverview.Sort(dgv_subjectOverview.Columns[3], ListSortDirection.Descending);
            }
            CheckActiveSubjects();

        }

        private void btn_clearSearch_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }


        private void btn_resetGlobalBudget_Click(object sender, EventArgs e) //If the user is an admin and selects the reset global budget button, open the form to change this. Otherwise deny access.
        {
            if (role == "Principal" || role == "Head Teacher")
            {
                currentGlobal = float.Parse(lbl_globalBudgetAllocated.Text); //Update variable for use in changeGlobalBudget calculations
                frm_changeGlobalBudget changeGlobalBudget = new frm_changeGlobalBudget(this);
                changeGlobalBudget.ShowDialog();
            }
            else
            {
                MessageBox.Show("No permission to change the global budget. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void btn_changeSubBudget_Click(object sender, EventArgs e) //If the user is an admin and selects the change subject budget button, open the form to change this. Otherwise deny access.
        {
            if (role == "Principal" || role == "Head Teacher")
            {
                SetSubBudget();
            }
            else
            {
                MessageBox.Show("No permission to change the subject budget. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void SetSubBudget() //Set or reset a subject's allocated sub budget
        {
            UpdateVariablesForSubBudgetForm(); //Update public variables that can be accessed and used in calculations of the sub budget form

            frm_changeSubBudget changeSubBudgetForm = new frm_changeSubBudget(this);
            changeSubBudgetForm.ShowDialog();
        }

        //Public variables for accessed and used in the change sub budget form
        public static int currentId = 0;
        public static float currentGlobal = 0;
        public static float currentGlobalLeft = 0;
        private void UpdateVariablesForSubBudgetForm() //Update public variables that can be accessed and used in calculations of the sub budget form
        {
            currentId = Convert.ToInt32(dgv_subjectOverview.CurrentRow.Cells[0].Value);
            currentGlobal = float.Parse(lbl_globalBudgetAllocated.Text);
            currentGlobalLeft = float.Parse(txt_globalBudgetLeft.Text);


        }

        private void lbl_globalBudgetAllocated_TextChanged(object sender, EventArgs e) //Whenever the global budget is changed, update calculations
        {
            UpdateGlobalBudgetCalculations();
        }

        public void UpdateGlobalBudgetCalculations()
        {
            //For readability and convenience, set variables fo ruse in calculations
            float calculateGlobalBudgetLeft;
            float allocatedGlobalBudget = float.Parse(lbl_globalBudgetAllocated.Text);
            float subjectAllocatedSum = 0;

            for (int i = 0; i < dgv_subjectOverview.Rows.Count; ++i) //For each row of the datagridview, if it is active (the active cell 6 is True) then add its allocated subject budget (cell 3)
            {
                if (dgv_subjectOverview.Rows[i].Cells[6].Value.ToString() == "True")
                {
                    subjectAllocatedSum += float.Parse(dgv_subjectOverview.Rows[i].Cells[3].Value.ToString());
                }
            }

            //The global budget left must then be the total allocated budget - the amount already allocated to subjects
            calculateGlobalBudgetLeft = allocatedGlobalBudget - subjectAllocatedSum;
            //Display in textbox
            txt_globalBudgetLeft.Text = calculateGlobalBudgetLeft.ToString();


            float calculateGlobalBudgetUnspent;
            float subjectSpentSum = 0;

            for (int i = 0; i < dgv_subjectOverview.Rows.Count; ++i) //For each row of the datagridview, if it is active (the active cell 6 is True) then add its subBudgetSpent (cell 4)
            {
                if (dgv_subjectOverview.Rows[i].Cells[6].Value.ToString() == "True")
                {
                    subjectSpentSum += float.Parse(dgv_subjectOverview.Rows[i].Cells[4].Value.ToString());
                }
            }

            //The global budget unspent must then be the total global budget - the amount already spent by subjects (evident in transaction history)
            calculateGlobalBudgetUnspent = allocatedGlobalBudget - subjectSpentSum;
            //Display in textbox
            txt_globalBudgetUnspent.Text = calculateGlobalBudgetUnspent.ToString();

        }

        private void chkb_active_CheckedChanged(object sender, EventArgs e) //Determine whether a subject is being activated or deactivated, and run appropriate actions to activate or deactivate when check is changed
        {
            if (chkb_active.Checked == false) //If the checkbox is changed to be unchecked and the current subject is active (cell 6 is true), then the user must be deactivating the subject
            {
                if (dgv_subjectOverview.CurrentRow.Cells[6].Value.ToString() == "True")
                {
                    DeactivateSubject();
                }
            }
            else //Otherwise, the checkbox must be being changed to checked and providing the current subject is inactive (cell 6 is false), then the user must be trying to activate the subject
            {
                if (dgv_subjectOverview.CurrentRow.Cells[6].Value.ToString() == "False")
                {
                    ActivateSubject();

                    //Update variables then open the set sub budget form
                    UpdateVariablesForSubBudgetForm(); 
                    frm_changeSubBudgetFromCheckbox changeSubBudgetForm = new frm_changeSubBudgetFromCheckbox(this);
                    changeSubBudgetForm.ShowDialog();
                }
            }
        }

        public void ActivateSubject() //Activate selected subject
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update the subject which has been activated to 0, 0, 0 by default (this will likely be set to different values unless the user allocates $0 to be the budget)
            sqlCommand.CommandText = $"UPDATE SubjectOverview SET allocatedsubBudget='0', subBudgetSpent='0',subBudgetLeft='0',active='True' WHERE subjectId={dgv_subjectOverview.CurrentRow.Cells[0].Value}";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Update appearance, calculations and data accordingly
            dgv_subjectOverview.CurrentRow.Cells[6].Value = "True";
            btn_changeSubBudget.Enabled = true;

            dgv_subjectOverview.CurrentRow.Cells[3].Value = "0";
            dgv_subjectOverview.CurrentRow.Cells[4].Value = "0";
            dgv_subjectOverview.CurrentRow.Cells[5].Value = "0";
            dgv_subjectOverview.CurrentRow.Cells[6].Value = "True";

            ActivatePurchaseOrdersOfSubject();

            for (int i = 0; i < 7; i++)
            {
                dgv_subjectOverview.CurrentRow.Cells[i].Style.BackColor = Color.White;
            }

            currentId = Convert.ToInt32(dgv_subjectOverview.CurrentRow.Cells[0].Value);
            currentGlobal = float.Parse(lbl_globalBudgetAllocated.Text);

        }
        public void DeactivateSubject()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update the fields of the subject which has been deactivated to '-' and active to 'false'
            sqlCommand.CommandText = $"UPDATE SubjectOverview SET allocatedsubBudget='-',subBudgetSpent='-',subBudgetLeft='-',active='False' WHERE subjectId={dgv_subjectOverview.CurrentRow.Cells[0].Value}";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Update appearance, calculations and data accordingly
            dgv_subjectOverview.CurrentRow.Cells[3].Value = "-";
            dgv_subjectOverview.CurrentRow.Cells[4].Value = "-";
            dgv_subjectOverview.CurrentRow.Cells[5].Value = "-";
            dgv_subjectOverview.CurrentRow.Cells[6].Value = "False";
            btn_changeSubBudget.Enabled = false;

            DeactivatePurchaseOrdersOfSubject();

            for (int i = 0; i < 7; i++)
            {
                dgv_subjectOverview.CurrentRow.Cells[i].Style.BackColor = Color.Silver;
            }

            UpdateGlobalBudgetCalculations();
        }


        private void DeactivatePurchaseOrdersOfSubject()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update all orders in transaction history allocated to that subject and stage to inactive
            sqlCommand.CommandText = $"UPDATE TransactionHistory SET active='False' WHERE subject='" + txt_subjectName.Text + "' AND stage =" + dgv_subjectOverview.SelectedRows[0].Cells[1].Value.ToString();

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Notify the user it was successful
            MessageBox.Show($"{txt_subjectName.Text} deactivated.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActivatePurchaseOrdersOfSubject()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;


            //Define a command statement (SQL query) to update all orders in transaction history allocated to that subject and stage to active
            sqlCommand.CommandText = $"UPDATE TransactionHistory SET active='True' WHERE subject='" + txt_subjectName.Text + "' AND stage =" + dgv_subjectOverview.SelectedRows[0].Cells[1].Value.ToString();

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Notify the user it was successful
            MessageBox.Show($"{txt_subjectName.Text} activated.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void UncheckBecauseOfSubBudget() //Called by change sub budget form
        {
            chkb_active.Checked = false;
        }



        private void btn_resetAll_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            if (role == "Principal" || role == "Head Teacher") //Admin-only
            {
                //Deter user accidentally resetting
                if (MessageBox.Show("Are you sure you wish to reset the global budget, subject overview and all current purchase orders?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //Establish connection with SQLite database file
                    SQLiteConnection sqlConnection = CreateConnection();

                    //Instantiate a new SQL command object
                    SQLiteCommand sqlCommand = new SQLiteCommand();

                    //Customise the SQL command arguments associated with it
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;

                    //Define a command statement (SQL query) to deactivate all subjects
                    sqlCommand.CommandText = $"UPDATE SubjectOverview SET allocatedsubBudget='-', subBudgetSpent='-', subBudgetLeft='-', active='False'";

                    //Open a connection with the database
                    sqlConnection.Open();
                    //Execute the command
                    sqlCommand.ExecuteNonQuery();

                    //Define a command statement (SQL query) to reset/set global budget to 0
                    sqlCommand.CommandText = $"UPDATE GlobalBudget SET currentBudget='0'";
                    //Execute the command
                    sqlCommand.ExecuteNonQuery();

                    //Define a command statement (SQL query) to delete all purchases from transaction history
                    sqlCommand.CommandText = $"DELETE FROM TransactionHistory";
                    //Execute the command
                    sqlCommand.ExecuteNonQuery();

                    //Close connection with the database
                    sqlConnection.Close();

                    ShowHubForm(); //Return to hub form as all details need to be reset (and also for intuitiveness)

                }
            }
            else
            {
                //Notify user they cannot access
                MessageBox.Show("No permission to reset the entire system. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_resetSubjectBudget_Click(object sender, EventArgs e)
        {
            ResetAllocationandPurchases();
        }

        private void ResetAllocationandPurchases()
        {
            if (role == "Principal" || role == "Head Teacher") //Admin-only
            {
                //Deter user accidentally resetting
                if (MessageBox.Show("Are you sure you wish to reset the subject overview AND all current purchase orders?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //Establish connection with SQLite database file
                    SQLiteConnection sqlConnection = CreateConnection();

                    //Instantiate a new SQL command object
                    SQLiteCommand sqlCommand = new SQLiteCommand();

                    //Customise the SQL command arguments associated with it
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;

                    //Define a command statement (SQL query) to deactivate all subjects
                    sqlCommand.CommandText = $"UPDATE SubjectOverview SET allocatedsubBudget='-', subBudgetSpent='-', subBudgetLeft='-', active='False'";

                    //Open a connection with the database
                    sqlConnection.Open();
                    //Execute the command
                    sqlCommand.ExecuteNonQuery();

                    //Define a command statement (SQL query) to delete all purchases from Transaction History
                    sqlCommand.CommandText = $"DELETE FROM TransactionHistory";
                    //Execute the command
                    sqlCommand.ExecuteNonQuery();

                    //Close connection with the database
                    sqlConnection.Close();

                    //Update all information necessary
                    ResetSearch();
                    SetGlobalBudget();

                    UpdateGlobalBudgetCalculations();
                    ReadSubjectOverview();

                    //Notify the user it was successful
                    MessageBox.Show("All purchases removed and subject budget allocations reset.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //Notify user they cannot access
                MessageBox.Show("No permission to reset subject budgets. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
        }


        //Expand to see code which expands submenus

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
        } //Collapsed code which expands the navigation bar submenus



        private void btn_logout_Click(object sender, EventArgs e) //Emulate an exit/logout function
        {
            Application.Restart();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            ShowHubForm();
        }


        public static float currentGlobalAllocated = 0;
        private void btn_transactionHistory_Click(object sender, EventArgs e)
        {
            ShowTransactionHistoryForm();
            currentGlobalAllocated = float.Parse(lbl_globalBudgetAllocated.Text);
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

        private void btn_settings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        }

        private void btn_calculateGlobalUnspent_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        } //Collapsed code for navigation bar button events
    }
}
