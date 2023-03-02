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
    public partial class frm_newPurchaseOrder : Form
    {
        frm_transactionHistory transactionHistoryForm;

        public frm_newPurchaseOrder(frm_transactionHistory hubCallingForm)
        {
            transactionHistoryForm = hubCallingForm;
            InitializeComponent();
        }


        //For code readability and convenience, assign the simple variable names role and username from frm_hub 
        string role = "";
        string username = "";

        private void frm_newPurchaseOrder_VisibleChanged(object sender, EventArgs e)
        {
            //Set details for which user is logged in accordingly
            username = frm_hub.username;
            role = frm_hub.role;

            FillInformation(); //Change the text boxes to show the details accordingly
        }

        private void FillInformation() //Auto-fill fields of a new purchase order form with necessary details
        {
            //Insert time and date of purchase
            DateTime today = DateTime.Today;
            txt_date.Text = DateTime.Now.ToString("d/M/yyyy");
            txt_time.Text = DateTime.Now.ToString("HH:mm:ss tt");

            txt_user.Text = username;
            txt_subject.Text = frm_transactionHistory.subject;
            txt_stage.Text = frm_transactionHistory.stage;
            lbl_allocatedsubbudget.Text = frm_transactionHistory.allocatedSubBudget.ToString();
            lbl_currentSubBudgetLeft.Text = frm_transactionHistory.currentSubBudgetLeft.ToString();
            lbl_currentGlobalUnspent.Text = frm_transactionHistory.currentGlobalUnspent.ToString();
            lbl_subLeftover.Text = frm_transactionHistory.currentSubBudgetLeft.ToString();
            lbl_newGlobalUnspent.Text = frm_transactionHistory.currentGlobalUnspent.ToString();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            //As the form has no borderstyle, emulate a custom close button. If the 'X' button is clicked, close the form.
            Close();
        }

        int decimalDigits = 0; //How many decimal places included
        bool decimalPoint = false; //If a decimal place exists
        private void txt_cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow decimal (float) numbers and up to 2 decimal points after it
            //This is achieved by checking if a dot is being entered and if so, if the index of a '.' exists in the textbox
            //then handle the decimal point (prevent it from entering the textbox) as a decimal point already exists
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            //Changes decimal point to false only when decimal point is hit. This is used instead of an else below the above 'if' statement
            //because it which would trigger if anything except for a '.' was used
            if (e.KeyChar == '.')
            {
                decimalPoint = true;
            }

            // Verify that the pressed key isn't CTRL or any non-numeric digit

            //<= 1 rather than <= 2 because first time it runs, checks against 0 and second time will check against decimalDigits = 1
            //the || e.KeyChar here allows backspace to be used
            if (decimalDigits <= 1 || e.KeyChar == (char)Keys.Back)
            {
                //If the character is NOT a backspace (control character), digit or a '.', then handle the character (prevent it from entering the textbox) - this is NOT because of the ! before each condition
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
                else //If the character IS either a backspace, digit or '.'
                {
                    //If decimal point has been set to true (a decimal point has been entered in the control) but the entered digit is NOT the decimal point
                    //itself, it must be a decimal number so increment the number of decimal digits (this is used to check that <= 2 decimal places are entered
                    if (decimalPoint == true && e.KeyChar != '.')
                    {
                        decimalDigits++;
                    }
                }
            }
            else //If there are >= 1 decimal digit already (already reached 2 decimal places) or the key was not a backspace, then handle the character (prevent it from entering the textbox)
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Back) //If the key pressed was the backspace
            {
                //Then if decimal point is true (a decimal point has been entered) and the textbox (not including the number which is being deleted indicated by length - 1) contains a decimal point
                if (decimalPoint == true && txt_cost.Text.Substring(0, txt_cost.Text.Length - 1).Contains(".") == true)
                {
                    //Then user must be backspacing the final number (which is a decimal place) so remove a decimal digit
                    decimalDigits -= 2;
                }
                //If the textbox is empty, handle the backspace
                else if (txt_cost.Text == "")
                {
                    e.Handled = true;
                }
                //Because otherwise take last character must have the decimal point in it
                else if (txt_cost.Text.Substring(0, txt_cost.Text.Length - 1).Contains(".") == false)
                {
                    //The decimal point must have been removed so set the text box as no longer having a decimal point, and now there are no deimal places
                    decimalPoint = false;
                    decimalDigits = 0;
                }
            }
        }

        private void txt_cost_TextChanged(object sender, EventArgs e) //Each time the user changes the cost
        {
            GetCurrentSubBudgetSpent(); //Calculate the current amount spent in all purchases which are allocated to the same subject and budget as this purchase

            if (txt_cost.Text != "" && txt_cost.Text != ".") // Ensure that the new budget global budget is in a form which can be converted in a float(for calculations)
            {
                if (txt_cost.TextLength < 29) //Ensure not at a length that intercepts with interface
                {
                    //Calculate the new subbudget left by subtracting the amount of money already spent in purchases from the allocated subject budget, then subtracting the cost for the new item
                    float currentLeftover = float.Parse(lbl_allocatedsubbudget.Text) - subBudgetSpent - float.Parse(txt_cost.Text);
                    lbl_subLeftover.Text = currentLeftover.ToString();

                    //Calculate the new global unspent amount by subtracting the amount of money already spent in purchases from the total global budget, then subtracting the cost for the new item
                    float currentGlobalUnspent = frm_transactionHistory.currentGlobalUnspent;
                    lbl_newGlobalUnspent.Text = (currentGlobalUnspent - float.Parse(txt_cost.Text)).ToString();
                }
                else //Notify user the budget is too long
                {
                    MessageBox.Show("Cost must be shorter than 29 characters in length.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txt_cost.Text = "";
                }
            }
            else //Thus the cost must be empty so substitute 0/remove the new cost in the calculations
            {
                float currentLeftover = float.Parse(lbl_allocatedsubbudget.Text) - subBudgetSpent;
                lbl_subLeftover.Text = currentLeftover.ToString();

                float currentGlobalUnspent = frm_transactionHistory.currentGlobalUnspent;
                lbl_newGlobalUnspent.Text = currentGlobalUnspent.ToString();
            }
        }

        public void setSudoUser()
        {
            txt_user.Text = frm_sudoForNewOrder.currentUser;
        }

        private void btn_createPurchaseOrder_Click(object sender, EventArgs e)
        {
            if (txt_stage.Text != "" && txt_subject.Text != "" && txt_itemname.Text != "" && txt_description.Text != "" && txt_cost.Text != "") //Ensure all of the fields are complete
            {
                if (float.Parse(lbl_subLeftover.Text) >= 0) //There must be a positive amount avaliable in the subject for other purchase orders after this order (otherwise subject would be in debt)
                {
                    if (float.Parse(lbl_newGlobalUnspent.Text) >= 0) //This would logically follow the previous condition, but as an extra precaution ensure global unspent remains positive after the purchase is added
                        //(Otherwise the entire system would be in debt as the global budget would be negative)
                    {
                        //Establish connection with SQLite database file
                        SQLiteConnection sqlConnection = new SQLiteConnection();
                        sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

                        //Instantiate a new SQL command object
                        SQLiteCommand sqlCommand = new SQLiteCommand();

                        //Customise the SQL command arguments associated with it
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.Text;

                        sqlCommand.CommandText = $"INSERT into TransactionHistory (stage,subject,itemName,user,description,date,time,cost,active) Values('{txt_stage.Text}','{txt_subject.Text}','{txt_itemname.Text}','{txt_user.Text}','{txt_description.Text}','{txt_date.Text}','{txt_time.Text}','{txt_cost.Text}','True')";

                        //Associated command with the event of that button specifically

                        //Open a connection with the database
                        sqlConnection.Open();
                        //Execute the command
                        sqlCommand.ExecuteNonQuery();
                        //Close connection with the database
                        sqlConnection.Close();

                        //Perform updates to the interface and calculations with the new item
                        UpdateSubject(); //Change data for subject and stage allocated to purchase in the Subject Overview table
                        transactionHistoryForm.ReadSubjectOverview(); //Update datagridview with new item
                        transactionHistoryForm.UpdateGlobalUnspent(); //Re-calculate
                        transactionHistoryForm.StageCheck();

                        //Then the form back to the transaction history form
                        Close();
                    }
                    else
                    {
                        //Notify user of issue
                        MessageBox.Show("Total cost of purchase orders must be within global budget. (Global unspent funds cannot be negative)", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    //Notify user of issue
                    MessageBox.Show("Purchase order cost must be within allocated subject budget. (Subject budget remaining funds cannot be negative)", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
                }
            }
            else
            {
                //Notify user of issue
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void UpdateSubject()
        {
            GetCurrentSubBudgetSpent(); //Do calculations for subBudgetSpent column

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a SELECT statement (SQLite query) which updates the actual calculations of the subject and stage allocated to the purchase
            sqlCommand.CommandText = $"UPDATE SubjectOverview SET subBudgetSpent='{subBudgetSpent.ToString()}', subBudgetLeft='{lbl_subLeftover.Text}' WHERE subjectName='" + txt_subject.Text + "' AND stage='" + txt_stage.Text + "'";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

        }

        float subBudgetSpent; //Amount of money spent by that subject
        private void GetCurrentSubBudgetSpent()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) - select all purchases allocated to the subject and stage
            string commandText = "SELECT * FROM TransactionHistory WHERE subject='" + txt_subject.Text + "' AND stage='" + txt_stage.Text + "'";

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

            subBudgetSpent = 0; //Reset for each calculation

            for (int i = 0; i < datatable.Rows.Count; i++) //For each row in the datatable of the subject and stage's purchases, add the cost of the item to a total of the amount spent
            {
                subBudgetSpent += float.Parse(datatable.Rows[i]["cost"].ToString());
            }

        }

        private void btn_sudo_Click(object sender, EventArgs e) //Ensure user has admin permission if sudo of which user is creating the purchase order is attempted
        {
            if (role == "Principal" || role == "Head Teacher")
            {
                frm_sudoForNewOrder sudoForm = new frm_sudoForNewOrder(this);
                sudoForm.ShowDialog();
            }
            else //Otherwise deny access
            {
                MessageBox.Show("No permission to sudo as other users. \nPlease contact your system administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txt_itemname_TextChanged(object sender, EventArgs e) //Ensure not at a length that intercepts with interface
        {
            if (txt_itemname.TextLength > 39)
            {
                //Notify user of issue
                MessageBox.Show("Item name must be less than 39 characters in length.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txt_itemname.Text = "";
            }
        }

        private void txt_description_TextChanged(object sender, EventArgs e) //Ensure not at a length that intercepts with interface
        {
            if (txt_description.TextLength > 100)
            {
                //Notify user of issue
                MessageBox.Show("Description must be less than 100 characters in length.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txt_description.Text = "";
            }
        }
    }
}
