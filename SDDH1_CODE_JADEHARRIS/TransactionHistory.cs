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
    public partial class frm_transactionHistory : Form
    {

        public frm_transactionHistory()
        {
            InitializeComponent();
            //Hide/collapse all submenus to start
            CollapseSubmenus();
        }


        //For code readability and convenience, assign the simple variable names role and username from frm_hub 
        string role = "";
        string username = "";

        //Use visiblechanged event so it triggers EACH TIME form appears instead of shown event (only first time)
        int change = 0;
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

            if (frm_settings.anySubject == false) //If the anySubject setting is OFF (thus users can only use the subject and stage they are allocated)
            {
                //Disable the comboboxes (as user cannot change which subject and stage is selected)
                cmb_subject.Enabled = false; 
                cmb_stage.Enabled = false;

                if (change % 2 == 0) //Basically just only calls FillSubject when change is even, so will only complete the combo boxes with the users allocated subject when the form is opening
                {
                    FillSubject();
                }
            }
            else if (cmb_subject.Text != "") //Otherwise, if the form is becoming visible and a subject has been selected
            {
                //Reset the stage and subject combo boxes so user can choose again
                cmb_stage.Text = "";
                cmb_subject.Text = "";
            }
            else
            {
                //Otherwise enable both combo boxes because the anySubject setting must be ON and the user must not have selected any subject yet
                cmb_subject.Enabled = true;
                cmb_stage.Enabled = true;
            }

            change++;

            //Update calculations
            UpdateGlobalUnspent();
            StageCheck();
        }

        private void FillSubject() //As the anySubject setting must be OFF for this function to be called, auto-fill the stage and subject combo box with the user's allocated subject and stage
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";


            //Define SELECT Statement/command
            string commandText = "SELECT * FROM Users WHERE username='" + frm_hub.username + "'";

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

            //Fill data from datatable into form controls
            cmb_subject.Text = datatable.Rows[0]["subject"].ToString();
            cmb_stage.Text = datatable.Rows[0]["stage"].ToString();

        }


        public void ResetSearch()
        {
            //Refresh datagridview
            ReadSubjectOverview();
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
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Deifne a select statement (SQL query) - * means select all
            //By defining a string here, no need to rewrite
            string commandText = "SELECT  * FROM TransactionHistory";

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
            dgv_transactionHistory.DataSource = datatable;

            //Format for datagridview design
            DataGridViewAesthetics();
        }


        private void DataGridViewAesthetics()
        {
            dgv_transactionHistory.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); // Make the header of the columns bold
            DisableDataGridViewSorting();//For funtionality, disable ability to select specific columns (only rows can be selected)

            try //Try and catch statement prevent runtime error if datagridview is not populated
            {
                //Fisable column header of selected cell from appearing selected as well
                dgv_transactionHistory.EnableHeadersVisualStyles = false;
                dgv_transactionHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_transactionHistory.ColumnHeadersDefaultCellStyle.BackColor;

                //Size columns of the datagridview
                dgv_transactionHistory.Columns[0].Width = 25;
                dgv_transactionHistory.Columns[1].Width = 50;
                dgv_transactionHistory.Columns[4].Width = 200;
            }
            catch
            {

            }
            //Aesthetically indicate whether subject is enabled or disabled 
            CheckActiveItems();
        }

        private void DisableDataGridViewSorting()
        {
            //Prevent sorting using header (for aesthetics)
            for (int i = 0; i < dgv_transactionHistory.Columns.Count; i++)
            {
                dgv_transactionHistory.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CheckActiveItems()
        {
            //For each row
            for (int i = 0; i < dgv_transactionHistory.Rows.Count; ++i)
            {
                //If the item has been disabled (set to 'false')
                if (dgv_transactionHistory.Rows[i].Cells[9].Value.ToString() == "False")
                {
                    for (int j = 0; j < 10; j++)
                    {
                        //Set each of the 7 cells in that row to have the background colour of silver (to indicate disabled)
                        dgv_transactionHistory.Rows[i].Cells[j].Style.BackColor = Color.Silver;
                    }
                }
                else
                //Otherwise, if 'active' is not false (thus must be 'true' - subject is enabled)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        //Set each of the 7 cells in that row to have the background colour of white (to indicate enabled)
                        dgv_transactionHistory.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
            }
        }



        private void btn_openSubOverview_Click(object sender, EventArgs e)
        {
            ShowSubjectOverviewForm();
        }

        private void btn_clearSearch_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }


        public static float currentGlobalUnspent = 0;
        private void btn_newPurchaseOrder_Click(object sender, EventArgs e) //Create a purchase order allocated to the subject and stage in combo boxes
        {
            if (cmb_subject.Text != "" && cmb_stage.Text != "") //Providing neither combo box is empty (so both a subejct and stage is selected)
            {
                if (lbl_subjectBudgetAllocated.Text == "-") //If they have selected unsuccessfully - visible by whether subejct budget allocated is a value or '-' (dash if the subject and stage is inactive)
                {
                    //Notify user unsuccessful because the subject they wish to allocate the purchase to is inactive
                    MessageBox.Show("Must have an active subject and stage selected to allocate a purchase order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else //Thus they must have selected an active subject (successfully)
                {
                    currentGlobalUnspent = float.Parse(txt_globalBudgetUnspent.Text); //Update the public currentGlobalUnspent
                    SetSubjectAndStage(); //Update other variables and open the form
                }
            }
            else
            {
                //Notify user of error
                MessageBox.Show("Must have a subject and stage selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public static string subject;
        public static string stage;
        public static float allocatedSubBudget;
        public static float currentSubBudgetLeft;

        private void SetSubjectAndStage() //Update necessary variables and open a new purchase order
        {
            if (lbl_subjectBudgetAllocated.Text != "-" && txt_subjectBudgetLeft.Text != "-")
            {
                subject = cmb_subject.Text;
                stage = cmb_stage.Text;
                allocatedSubBudget = float.Parse(lbl_subjectBudgetAllocated.Text);
                currentSubBudgetLeft = float.Parse(txt_subjectBudgetLeft.Text);

                //Open purchase order form
                frm_newPurchaseOrder purchaseOrderForm = new frm_newPurchaseOrder(this);
                purchaseOrderForm.ShowDialog();
            }
            else
            {
                //Notify user of error
                MessageBox.Show("Must have an active subject and stage selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_delPurchaseOrder_Click(object sender, EventArgs e) //Delete the purchase order selected in the datagridview
        {
            if (dgv_transactionHistory.SelectedRows.Count < 1) //Firstly, ensure that a purchase order is selected
            {
                MessageBox.Show("Must have a purchase order selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (role == "Principal" || role == "Head Teacher") //Ensure an admin is attempting to delete the purchase
                {
                    DeletePurchase();
                }
                else
                {
                    //Otherwise, if it is not an admin users can delete their OWN purchases only.
                    if (dgv_transactionHistory.SelectedRows[0].Cells[5].Value.ToString() == username) 
                    {
                        DeletePurchase();
                    }
                    else
                    {
                        //Otherwise user must be attempting to delete a purchase they did not create so refute access
                        MessageBox.Show("No permission to delete other user's purchase orders. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void DeletePurchase() //Delete the purchase order selected currently in the data grid view
        {
            //Deter user accidentally deleting a purchase by requiring user to respond to a confirmation box
            if (MessageBox.Show($"Are you sure you wish to delete the purchase order? \n(This is irreversible)", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = new SQLiteConnection();
                sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                //Instantiate a new SQL command object
                SQLiteCommand sqlCommand = new SQLiteCommand();

                //Customise the SQL command arguments associated with it
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;

                //Define a command statement (SQL query) to delete the item (identified by its item ID)
                sqlCommand.CommandText = $"DELETE FROM TransactionHistory WHERE id='" + dgv_transactionHistory.SelectedRows[0].Cells[0].Value.ToString() + "'";

                //Open a connection with the database
                sqlConnection.Open();
                //Execute the command
                sqlCommand.ExecuteNonQuery();
                //Close connection with the database
                sqlConnection.Close();

                //Notify the user it was successful
                MessageBox.Show($"Purchase order removed.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Update the datagridview, Transation Hisotry and Subject Overview calculations after this item has been removed
                ReadSubjectOverview();

                UpdateGlobalUnspent();
                StageCheck();
                CalculateSubBudgetLeft();

                UpdateSubject(); //Update Subject Overview subBudgetSpent and subBudgetLeft
            }
        }


        public void UpdateGlobalUnspent() //Update calculations
        {
            GetGlobalBudget();

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Create SELECT statement 
            string commandText = "SELECT * FROM TransactionHistory WHERE active='True'";

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

            float globalBudgetUnspent = 0;
            float globalBudgetSpent = 0;

            for (int i = 0; i < datatable.Rows.Count; ++i)
            {
                globalBudgetSpent += float.Parse(datatable.Rows[i]["cost"].ToString());
            }

            globalBudgetUnspent = globalBudget - globalBudgetSpent;

            txt_globalBudgetUnspent.Text = globalBudgetUnspent.ToString();
        }

        float globalBudget = 0;
        private void GetGlobalBudget()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Create SELECT statement 
            string commandText = "SELECT * FROM GlobalBudget";


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

            globalBudget = float.Parse(datatable.Rows[0]["currentBudget"].ToString());
        }


        private void UpdateSubject()
        {
            GetCurrentSubBudgetSpent();
            GetSubBudgetAllocated();

            float newSubSpent = subBudgetSpent;
            float newSubLeft = subBudgetAllocatedFromDelete - subBudgetSpent;

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to change the user's password to the new version
            sqlCommand.CommandText = $"UPDATE SubjectOverview SET subBudgetSpent='{newSubSpent.ToString()}', subBudgetLeft='{newSubLeft.ToString()}' WHERE subjectName='" + cmb_subject.Text + "' AND stage='" + cmb_stage.Text + "'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();
        }

        private void GetSubBudgetAllocated()
        {
            try
            {
                //Establish connection with SQLite database file
                SQLiteConnection sqlConnection = CreateConnection();

                //Create SELECT statement
                string commandText = "SELECT * FROM SubjectOverview WHERE subjectName='" + cmb_subject.Text + "' AND stage='" + cmb_stage.Text + "'";


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

                subBudgetAllocatedFromDelete = float.Parse(datatable.Rows[0]["allocatedSubBudget"].ToString());
            }
            catch
            {

            }
        }

        float subBudgetSpent;
        float subBudgetAllocatedFromDelete;

        private void GetCurrentSubBudgetSpent()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Create SELECT statement
            string commandText = "SELECT * FROM TransactionHistory WHERE subject='" + cmb_subject.Text + "' AND stage='" + cmb_stage.Text + "'";


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

            float subSpent = 0;

            for (int i = 0; i < datatable.Rows.Count; ++i)
            {
                subSpent += float.Parse(datatable.Rows[i]["cost"].ToString());
            }

            subBudgetSpent = subSpent;
        }


        private void cmb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frm_settings.anySubject == true)
            {
                if (cmb_subject.SelectedIndex != -1) //Ensure the event is only triggered when a subject is chosen
                {
                    cmb_stage.SelectedIndex = -1; //Reset stage index (even though this will be set in this same event later) to ensure even if switching between subjects which have the same stage
                    //(so technically the stage selected index change wouldn't trigger - it does)
                    cmb_stage.Enabled = false;

                    //Establish connection with SQLite database file
                    SQLiteConnection sqlConnection = CreateConnection();

                    //define SELECT statement
                    string commandText = "SELECT * FROM SubjectOverview WHERE subjectName='" + cmb_subject.Text + "'";

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

                    if (cmb_subject.Text == "Food Technology" || cmb_subject.Text == "Industrial Technology Timber" || cmb_subject.Text == "Industrial Technology Graphics" || cmb_subject.Text == "Design and Technology" || cmb_subject.Text == "Industrial Technology Multimedia")
                    {
                        cmb_stage.SelectedIndex = -1;
                        cmb_stage.Enabled = true;
                        cmb_stage.Items.Remove("4");
                    }
                    else if (cmb_subject.Text == "Technology Mandatory")
                    {
                        cmb_stage.Items.Add("4");
                        cmb_stage.Text = datatable.Rows[0]["stage"].ToString();
                    }
                    else
                    {
                        //Fill data from datatable into form controls
                        cmb_stage.Text = datatable.Rows[0]["stage"].ToString();
                    }
                }
            }
        }

        private void cmb_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            StageCheck();
        }

        float subBudgetAllocated;

        public void StageCheck() //Once the stage has been entered as well, check if the subject and stage is active
        {
            if (cmb_stage.SelectedIndex != -1)
            {
                if (cmb_subject.Text != "" && cmb_stage.Text != "")
                {
                    try
                    {
                        //Establish connection with SQLite database file
                        SQLiteConnection sqlConnection = CreateConnection();

                        //Define SELECT Statement/command
                        string commandText = "SELECT * FROM SubjectOverview WHERE subjectName='" + cmb_subject.Text + "' AND stage='" + cmb_stage.Text + "'";

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

                        subBudgetAllocated = float.Parse(datatable.Rows[0]["allocatedsubBudget"].ToString());
                        //float subBudgetleft = float.Parse(datatable.Rows[0]["subBudgetLeft"].ToString());

                        lbl_subjectBudgetAllocated.Text = subBudgetAllocated.ToString();

                        CalculateSubBudgetLeft();
                        //txt_subjectBudgetLeft.Text = subBudgetleft.ToString();
                    }
                    catch
                    {
                        if (frm_settings.anySubject == false)
                        {
                            MessageBox.Show("Subject and stage allocated to user must be active. \n(Activate subject in subject overview, change subject allocated to user in manage users or allow any subject to be selected for purchase orders in settings)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            MessageBox.Show("Must have an active stage and subject selected. \n(Activate subject in subject overview)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                        cmb_stage.SelectedIndex = -1;
                        cmb_stage.Enabled = false;
                        cmb_subject.SelectedIndex = -1;
                        lbl_subjectBudgetAllocated.Text = "-";
                        txt_subjectBudgetLeft.Text = "-";
                        txt_globalBudgetUnspent.Text = "-";
                    }
                }
            }
        }

        private void CalculateSubBudgetLeft()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = CreateConnection();

            //Define SELECT statement
            string commandText = "SELECT * FROM TransactionHistory WHERE subject='" + cmb_subject.Text + "'";

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

            float subBudgetUnspent = 0;
            float subBudgetSpent = 0;

            for (int i = 0; i < datatable.Rows.Count; ++i)
            {
                subBudgetSpent += float.Parse(datatable.Rows[i]["cost"].ToString());
            }

            subBudgetUnspent = subBudgetAllocated - subBudgetSpent;

            txt_subjectBudgetLeft.Text = subBudgetUnspent.ToString();
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
            string commandText = "SELECT * FROM TransactionHistory WHERE stage=" + cmb_sortStage.Text;

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
            dgv_transactionHistory.DataSource = datatable;

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
                    commandText = "SELECT * FROM TransactionHistory WHERE active='True'";
                }
                else
                {
                    commandText = "SELECT * FROM TransactionHistory WHERE active='False'";
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
                dgv_transactionHistory.DataSource = datatable;

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
            string commandText = "SELECT * FROM TransactionHistory WHERE subject='" + cmb_sortSubject.Text + "'";


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

            dgv_transactionHistory.DataSource = datatable;


            //Update aesthetics to indicate if subject is enabled or disabled (even after search)
            CheckActiveItems();
        }


        private void dgv_transactionHistory_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txt_itemID.Text = dgv_transactionHistory.SelectedRows[0].Cells[0].Value.ToString();
            }
            catch
            {
            }
        }

        private void btn_calculateGlobalUnspent_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
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

        private void btn_removeAllPurchases_Click(object sender, EventArgs e)
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
        }//Collapsed code which expands the navigation bar submenus


        public static frm_hub form_hub = new frm_hub();

        private void ShowHubForm()
        {
            //Collapse all menus
            HideSubMenu();
            //Show new form
            form_hub.Show();
            //Hide this form
            Hide();
        }

        public static frm_subjectOverview subjectOverviewForm = new frm_subjectOverview();
        private void ShowSubjectOverviewForm()
        {
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
            ShowSubjectOverviewForm();
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
        } //Collapsed code for navigation bar button events

        private void cmb_subject_KeyPress(object sender, KeyPressEventArgs e) //Ensure that only valid information enters the system (user must choose one of the subjects)
        {
            e.Handled = true; //'Handle' typing so no text is entered
            MessageBox.Show("Cannot type. Select a subject using drop down.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Notify the user
        }

        private void cmb_stage_KeyPress(object sender, KeyPressEventArgs e) //Ensure that only valid information enters the system (user must choose one of the stages)
        {
            e.Handled = true; //'Handle' typing so no text is entered
            MessageBox.Show("Cannot type. Select a stage using drop down.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Notify the user
        }
    }
}
