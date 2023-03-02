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
    public partial class frm_changeSubBudgetFromCheckbox : Form
    {

        private frm_subjectOverview subjectOverviewForm = null;
        public frm_changeSubBudgetFromCheckbox(Form hubCallingForm) //Create a form reference so that functions from the Subject Overview can be called
        {
            subjectOverviewForm = hubCallingForm as frm_subjectOverview;
            InitializeComponent();

            //txt_newSubBudget.Text = "0";

            FillDetails();
            UpdateSubBudgetLeft();
        }

        private void btn_close_Click(object sender, EventArgs e) //This is the only difference to the other ChangeSubBudget form - this form automatically de-activates the subject if the form is closed
            //This is because it is only activated if the form is activated, but if no budget is allocated to it then the subject must be inactive
        {
            //Deactivate the subject selected
            subjectOverviewForm.DeactivateSubject();
            //Uncheck the checkbox for the subject so it can continue to function correctly (otherwise it would change checkbox but the subject would be deactivated from the previous function)
            subjectOverviewForm.UncheckBecauseOfSubBudget();

            //As the form has no borderstyle, emulate a custom close button. If the 'X' button is clicked, close the form.
            Close();
        }

        //Set variables for use from public variables of Subject Overview
        int id = frm_subjectOverview.currentId;
        float currentGlobal = frm_subjectOverview.currentGlobal;
        float currentGlobalLeft = frm_subjectOverview.currentGlobalLeft;


        float allocatedSubBudget;
        private void FillDetails()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) - select information for the subject
            string commandText = "SELECT * FROM SubjectOverview WHERE subjectId=" + id;

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

            //Fill data from datatable into form controls
            txt_subject.Text = datatable.Rows[0]["subjectName"].ToString();
            txt_stage.Text = datatable.Rows[0]["stage"].ToString();
            lbl_currentSubBudget.Text = datatable.Rows[0]["allocatedsubBudget"].ToString();
            allocatedSubBudget = float.Parse(datatable.Rows[0]["allocatedsubBudget"].ToString());

            GetSubjectSpent(); //Calculate how much money has currently been spent in the subject using the Transaction History table

            addGlobalBudgetInformation(); //Add details of the current global budget to the form to increase convenience of the user

        }


        float budgetLeft;
        float subAmountSpent;
        private void GetSubjectSpent()
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
            //Fill data from database into datatable
            myDataAdapter.Fill(datatable);

            for (int i = 0; i < datatable.Rows.Count; i++) //For each row in the datatable of the subject and stage's purchases, add the cost of the item to a total of the amount spent
            {
                subAmountSpent += float.Parse(datatable.Rows[i]["cost"].ToString());
            }
            //Fill this detail into the label so the user can view how much has already been spent 
            lbl_currentSubBudgetSpent.Text = subAmountSpent.ToString();

            //Set budget left to be the current amount allocated to the subject - the amount of money spent by the subject
            budgetLeft = float.Parse(lbl_currentSubBudget.Text) - subAmountSpent;
            //Fill this detail into the lavel so the user can view how much money was avaliable to be spent in purchases initially
            lbl_currentSubBudgetLeft.Text = budgetLeft.ToString();
        }

        private void addGlobalBudgetInformation()
        {
            //Fill labels with details
            lbl_currentGlobal.Text = currentGlobal.ToString();
            lbl_currentGlobalBudgetLeft.Text = currentGlobalLeft.ToString();
        }

        int decimalDigits = 0;
        bool decimalPoint = false;
        private void txt_newSubBudget_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                    if (decimalPoint == true && txt_newSubBudget.Text.Substring(0, txt_newSubBudget.Text.Length - 1).Contains(".") == true)
                    {
                        //Then user must be backspacing the final number (which is a decimal place) so remove a decimal digit
                        decimalDigits -= 2;
                    }
                    //If the textbox is empty, handle the backspace
                    else if (txt_newSubBudget.Text == "")
                    {
                        e.Handled = true;
                    }
                    //Because otherwise take last character must have the decimal point in it
                    else if (txt_newSubBudget.Text.Substring(0, txt_newSubBudget.Text.Length - 1).Contains(".") == false)
                    {
                        //The decimal point must have been removed so set the text box as no longer having a decimal point, and now there are no deimal places
                        decimalPoint = false;
                        decimalDigits = 0;
                    }
                }
            }
        }

        private void txt_newSubBudget_TextChanged(object sender, EventArgs e)
        {
            UpdateSubBudgetLeft();
        }


        private void UpdateSubBudgetLeft()  //Provide user with details necessary to set a considered subject budget
        {
            if (txt_newSubBudget.Text != "" && txt_newSubBudget.Text != ".") //Ensure that the new budget global budget is in a form which can be converted in a float (for calculations)
            {
                if (txt_newSubBudget.TextLength < 29) //Ensure that the new budget is not at a length that intercepts with interface
                {
                    //Calculate the new subbudget left by subtracting the amount of money already spent in purchases from the new subject budget the user is attempting to allocate
                    float subBudgetLeft = float.Parse(txt_newSubBudget.Text) - subAmountSpent;
                    //Display detail for user
                    lbl_newSubBudgetLeft.Text = subBudgetLeft.ToString();

                    //Calculate the new amount of funds avaliable to be allocated to subjects by 'reseting' the amount left (adding the current subBudget allocated as this is has been
                    //taken out) then subtracting the new subject budget.
                    float remaining = currentGlobalLeft - float.Parse(txt_newSubBudget.Text) + float.Parse(lbl_currentSubBudget.Text);
                    //Display detail for user
                    lbl_newGlobalBudgetLeft.Text = remaining.ToString();
                }
                else //Notify user the budget is too long
                {
                    MessageBox.Show("Subject budget must be shorter than 29 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txt_newSubBudget.Text = "";
                }
            }
            else
            {
                //If mewSubjectBudget is empty, substitute 0 into the calculations
                float remainingGlobalBudgetForAllocation = currentGlobalLeft;
                lbl_newGlobalBudgetLeft.Text = remainingGlobalBudgetForAllocation.ToString();

                float subBudgetLeft = 0 + budgetLeft;
                lbl_newSubBudgetLeft.Text = subBudgetLeft.ToString();
            }
        }

        private void btn_changesubBudget_Click(object sender, EventArgs e)
        {
            if (txt_newSubBudget.Text != "" && txt_newSubBudget.Text != ".") //Ensure the new subject budget can be converted to a float to avoid errors
            {
                if (float.Parse(lbl_newGlobalBudgetLeft.Text) >= 0) //Remove the ability to over-allocate the global budget
                {
                    if (float.Parse(lbl_newSubBudgetLeft.Text) >= 0) //Ensure that the user has not allocated an amount which cannot support all of the purchases allocated oto the subject
                    {
                        SetAndUpdateGlobalBudget();
                    }
                    else //Otherwise notify user of an issue
                    {
                        MessageBox.Show("Subject money already spent must be within budget. (Subject budget remaining funds cannot be negative)", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Subject budget must be allocated from global budget. (Global budget remaining funds cannot be negative)", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Please allocate a new budget (new budget cannot be empty).", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void SetAndUpdateGlobalBudget()
        {
            GetSubjectSpent(); //Calculate 

            float newSubBudgetLeft = float.Parse(txt_newSubBudget.Text) - subAmountSpent;

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to update the subject (and its calculations) with the new budget allocation
            sqlCommand.CommandText = $"UPDATE SubjectOverview SET allocatedsubBudget='{txt_newSubBudget.Text}',subBudgetSpent='{subAmountSpent.ToString()}',subBudgetLeft='{newSubBudgetLeft.ToString()}' WHERE subjectId=" + id;

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Update the Subject Overview interface and calculations with this new global budget
            subjectOverviewForm.ReadSubjectOverview();
            subjectOverviewForm.UpdateGlobalBudgetCalculations();

            //Close this form
            Close();
        }
    }
}
