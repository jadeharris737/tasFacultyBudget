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
    public partial class frm_changeGlobalBudget : Form
    {

        public frm_changeGlobalBudget()
        {
            InitializeComponent();
        }

        private frm_subjectOverview subjectOverviewForm; //Create a form reference so that functions from the Subject Overview can be called
        public frm_changeGlobalBudget(Form callingForm)
        {
            subjectOverviewForm = callingForm as frm_subjectOverview;
            InitializeComponent();
        }

        private void frm_changeGlobalBudget_Shown(object sender, EventArgs e) //Whenever the form is shown
        {
            SetVariables(); //Fill the controls with the variables
        }

        private void SetVariables() //Fill labels with the appropraite information
        {
            CalculateAmountAlreadyAllocated();

            lbl_alreadyAllocated.Text = amountAlreadyAllocated.ToString();
            lbl_currentGlobalLeft.Text = (frm_subjectOverview.currentGlobal - amountAlreadyAllocated).ToString();
        }

        float amountAlreadyAllocated;
        private void CalculateAmountAlreadyAllocated() //Calculate how much global budget has already been spent
        {
            amountAlreadyAllocated = 0; //Reset the amount that is calcualted (so it does not continuously add to the variable)

            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Define a SELECT statement (SQLite query) - * means select all
            string commandText = "SELECT * FROM SubjectOverview WHERE active='True'";

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

            //Calculate the amount already allocated to subjects by converting and adding the amount allocated to each subject in the database
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                amountAlreadyAllocated += float.Parse(datatable.Rows[i]["allocatedsubBudget"].ToString());
            }
        }

        private void btn_changeGlobalBudget_Click(object sender, EventArgs e)
        {
            if (txt_newBudget.Text != "") //Ensure that there is a global budget entered (to prevent runtime error especially if none is entered)
            {
                if (float.Parse(lbl_newGlobalLeft.Text) > 0) //If the new global budget left is greater than 0 (not = 0 as there needs to be an amount to allocate for the system to work even just $1)
                {
                    if (MessageBox.Show("Are you sure you wish to set the global budget?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) //Prevent accidental settings of the global budget
                    {
                        SetBudget();
                    }

                }
                else
                {
                    //Otherwise, the global budget must be equal to 0 or negative in which case the user cannot assign the global budget.
                    //A message box informs them
                    MessageBox.Show("Money assigned to subjects are allocated from the global budget. (Global budget funds left for allocation cannot be negative or 0)", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
                }
            }
            else
            {
                //If the first if case was skipped, then the user must not have entered a new budget in which case an error is shown (global budget cannot be nothing)
                MessageBox.Show("No budget given to change.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetBudget()
        {
            //Establish connection with SQLite database file
            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "DataSource = TASFacultyDatabase.db";

            //Instantiate a new SQL command object
            SQLiteCommand sqlCommand = new SQLiteCommand();

            //Customise the SQL command arguments associated with it
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            //Define a command statement (SQL query) to set the current global budget to be the float entered as the new budget
            sqlCommand.CommandText = $"UPDATE GlobalBudget SET currentBudget={txt_newBudget.Text} WHERE id=1";

            //Open a connection with the database
            sqlConnection.Open();
            //Execute the command
            sqlCommand.ExecuteNonQuery();
            //Close connection with the database
            sqlConnection.Close();

            //Notify the user it was successful
            MessageBox.Show("Global budget set.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Call SetGlobalBudget to update with this new global budget on the subject overview 
            subjectOverviewForm.SetGlobalBudget();

            //Close the changeGlobalBudget ShowDialog now the global budget has been successfully set
            Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (frm_subjectOverview.setUp == true) //If this set global budget is a pop-up because no budget has been set yet, then a global budget is required for the system to work.
            {
                MessageBox.Show("Cannot exit. Must have a global budget set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); //So remove the ability for the user to exit unless a global budget is set
            }
            else
            {
                Close(); //However, as this form is also used to CHANGE (as opposed to set) the global budget, if the setUp variable is false then the user can exit this form normally and the global budget will not be changed
            }
        }


        int decimalDigits = 0;
        bool decimalPoint = false;
        private void txt_newBudget_KeyPress(object sender, KeyPressEventArgs e)
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
                if (decimalPoint == true && txt_newBudget.Text.Substring(0, txt_newBudget.Text.Length - 1).Contains(".") == true)
                {
                    //Then user must be backspacing the final number (which is a decimal place) so remove a decimal digit
                    decimalDigits -= 2;
                }
                //If the textbox is empty, handle the backspace
                else if (txt_newBudget.Text == "")
                {
                    e.Handled = true;
                }
                //Because otherwise take last character must have the decimal point in it
                else if (txt_newBudget.Text.Substring(0, txt_newBudget.Text.Length - 1).Contains(".") == false)
                {
                    //The decimal point must have been removed so set the text box as no longer having a decimal point, and now there are no deimal places
                    decimalPoint = false;
                    decimalDigits = 0;
                }
            }
        }

        private void txt_newBudget_TextChanged(object sender, EventArgs e) //Calculate 
        {
            CheckLengthandCompleteCalculations();
        }

        private void CheckLengthandCompleteCalculations() //Provide user with details necessary to set a considered global budget
        {
            if (txt_newBudget.TextLength > 29) //Ensure that the new budget is not at a length that intercepts with interface
            {
                MessageBox.Show("Global budget must be less than 29 characters in length.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); //Notify user
                txt_newBudget.Text = "";
            }
            else if (txt_newBudget.Text != "" && txt_newBudget.Text != ".") //Ensure that the new budget global budget is in a form which can be converted in a float (for calculations)
            {
                CalculateAmountAlreadyAllocated();
                //The new global budget amount avaliable for calculation is simply the new global budget the user has allocated - amount alreadu allocated to subjects
                lbl_newGlobalLeft.Text = (float.Parse(txt_newBudget.Text) - amountAlreadyAllocated).ToString();
            }
            else
            {
                {
                    CalculateAmountAlreadyAllocated();
                    //However, if the textbox is empty, instead of using the last known new global budget value just use 0
                    lbl_newGlobalLeft.Text = (0 - amountAlreadyAllocated).ToString();
                }
            }
        }

    }
}

