/*Before editing any part of the code you must keep following ideas that i had implemented in your mind:
 1.I have assumed buttons as a element of matrix
 2.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Scientific_Calculator.DataTier;
using Scientific_Calculator.AppTier;
using Scientific_Calculator.PreTier;

namespace Scientific_Calculator
{

    public partial class MainForm : Form
    {

        Calculator Calc = new Calculator();
        Logger logger = new Logger();
        HistoryForm historyForm = new HistoryForm();

        //Constructor
        public MainForm()
        {
            InitializeComponent();
        }

        //Form Load Event
        private void frmMain_Load(object sender, EventArgs e)
        {
            //To focus TextBox initially
            txtBoxExpression.Select();
            //Guna Shadow
            guna2ShadowForm1.SetShadowForm(this);
            //For the drag control feature
            guna2DragControl1.TargetControl = this;
            radioButton1.Checked = true;
            this.siepanel.Visible = false;
        }

       


        //Method to Update expression on Text Field
        private void UpdateExpression(string ex)
        {
            //Update expression in txtBoxExpression as well as evaluationString
            CalculatorState.evaluationString += ex;
            txtBoxExpression.Text += ex;
        }

        //Method to evaluate Expression
        private void Eval()
        {
            try
            {
                //This ensures at least one button which performs some function is clicked
                if (CalculatorState.currentCommand != "")
                    Parse();
            }
            catch (Exception)
            {
                //If you want to display error message you can display from here
            }


            //Instantiating datatable
            DataTable dt = new DataTable();
            try
            {
                //Using Compute inbuilt method of datatable
                double result = Convert.ToDouble(dt.Compute(CalculatorState.evaluationString, String.Empty));
                //Displaying result
                txtBoxResult.Text = Math.Round(result, 15).ToString();
                CalculatorState.result = Math.Round(result, 15).ToString();
                updateBasesValues(result);
            }
            catch (Exception)
            {
                //On Exception clear the result field
                txtBoxResult.Text = "";
                CalculatorState.result = "";
            }

        }


        //Handling Click event on Equals button
        private void btnEquals_Click(object sender, EventArgs e)
        {
            //Swapping the values between two text fields
            SwapValues();
        }

        //Handling Click Events on Multiple Buttons
        private void HandleClick(object sender, EventArgs e)
        {
            //Type Casting
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            //Updating Expression When numeric button clicks
            UpdateExpression(b.Text);
        }

        //Handling Click events on Clear Button
        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear Both Input Fields
            txtBoxExpression.Text = "";
            txtBoxResult.Text = "";
            CalculatorState.result = "";
            CalculatorState.result = "";
            CalculatorState.countOfBracket = 0;
            //Clear evaluationString as well
            CalculatorState.evaluationString = "";
        }

        //Method to Swap Values between Two text fields
        private void SwapValues()
        {
            logger.addNewEntry(CalculatorState.evaluationString + " = " + txtBoxResult.Text);
            try
            {
            updateBasesValues(Convert.ToUInt32(txtBoxResult.Text));

            }
            catch (Exception)
            {

            }

            //Swap values in evaluation string as well
            CalculatorState.evaluationString = txtBoxResult.Text;
            txtBoxExpression.Text = txtBoxResult.Text;
            txtBoxResult.Text = "";
            CalculatorState.result= "";
            //Reset count of bracket and change sign button
            CalculatorState.countOfBracket = 0;
            //Place Ibeam at the last of string
            txtBoxExpression.Select(txtBoxExpression.Text.Length, 0);


        }

        //Method to handle Click event on BackSpace Button
        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            BackSpaceClick();
        }
        private void BackSpaceClick()
        {
            if (txtBoxExpression.Text.Length > 0)
            {
                //Temprorarily Hold Expression
                string tempExpression = txtBoxExpression.Text;

                //If removed char is ) then
                if (tempExpression[tempExpression.Length - 1] == '(')
                {
                    CalculatorState.countOfBracket = 0;
                }
                //If removed char is ) then
                if (tempExpression[tempExpression.Length - 1] == ')')
                {
                    CalculatorState.countOfBracket++;
                }
                txtBoxExpression.Text = tempExpression.Remove(tempExpression.Length - 1);
                //Set updated value in evaluationString as well
                CalculatorState.evaluationString = txtBoxExpression.Text;
                //Clear ResultTextBox
                txtBoxResult.Text = "";
                CalculatorState.result = "";
            }
            txtBoxExpression.Select(txtBoxExpression.Text.Length, 0);
            //Then evaluate again
            Eval();
        }


        //Handling TextChange Event on TextBoxExpression
        //Evaluate txtBoxExpression each time when text on it changes
        private void txtBoxExpression_TextChanged(object sender, EventArgs e)
        {
            Eval();

        }

        //Handling Click Events on brackets button
        private void btnBrackets_Click(object sender, EventArgs e)
        {
            //Regular Expression to check if string ends with any algebric opertaor
            Regex regex = new Regex(@"[+\-% */]$");
            //Regular expression to check if strikng ends with numeric digit
            Regex numericEnd = new Regex(@"\d$");

            //if Expression is empty
            if (CalculatorState.countOfBracket == 0 && txtBoxExpression.Text.Length == 0)
            {
                UpdateExpression("(");
                CalculatorState.countOfBracket++;
            }

            //If opening bracket is on last index
            else if (txtBoxExpression.Text.LastIndexOf('(') == txtBoxExpression.Text.Length - 1)
            {
                UpdateExpression("(");
                CalculatorState. countOfBracket++;
            }
            //If string ends with algebric operator
            else if (regex.IsMatch(txtBoxExpression.Text))
            {
                UpdateExpression("(");
                CalculatorState.countOfBracket++;
            }
            //
            else if (CalculatorState.countOfBracket == 0 && numericEnd.IsMatch(txtBoxExpression.Text))
            {
                UpdateExpression("*(");
                CalculatorState.countOfBracket++;

            }
            //Otherwise push closing bracket
            else
            {
                UpdateExpression(")");
                CalculatorState.countOfBracket--;
            }



        }

        //Handling Click Events on ChangeSign Button
        private void btnChangeSign_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[+\-%*/(]$");
            if (txtBoxExpression.Text.Length == 0)
            {
                UpdateExpression("(-");
                CalculatorState.countOfBracket++;
            }
            else if (regex.IsMatch(txtBoxExpression.Text))

            {
                UpdateExpression("(-");
                CalculatorState.countOfBracket++;


            }
            else if (txtBoxExpression.Text.EndsWith(")"))
            {
                UpdateExpression("*(-");
                CalculatorState.countOfBracket++;
            }
        }


        //Method of parsing inputExpression on each time where text on inputExpression is Changed
        private void Parse()
        {
            //Declaration of variable
            int startPosition, endPosition, index;

            string parsedNumber = "";

            double number, result;

            //ScreenNumber is 1 if current screen is first screen otherwise 2
            int screenNumber = CalculatorState.isSecondPage ? 2 : 1;

            //getting status
            int status = int.Parse(String.Concat(screenNumber.ToString(), CalculatorState.currentBtnIndex));

            switch (status)
            {
                /*
                Explanation Here Case 102 Means:
                1->First Screen
                0->index of row i.e 0 means first row
                2->index of column i.e 2 means third row
                */
                case 102://Square Root
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("sqrt(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where sqrt( length is 5 so
                        for (index = (startPosition + 5); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Round result
                        result = Math.Round(Calc.SquareRoot(number), 2);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;
                    }
                case 110://sin
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("sin(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where sin( length is 4 so
                        for (index = (startPosition + 4); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Check calculator is in degree mode or not
                        if (CalculatorState.isDegree) { number = Calc.DegreeToRadian(number); }
                        //Round result
                        result = Math.Round(Calc.Sin(number), 2);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 111://cos
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("cos(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where cos( length is 4 so
                        for (index = (startPosition + 4); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Check calculator is in degree mode or not
                        if (CalculatorState.isDegree) { number = Calc.DegreeToRadian(number); }

                        //Round result
                        result = Math.Round(Calc.Cos(number), 2);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 112://tan
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("tan(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where tan( length is 4 so
                        for (index = (startPosition + 4); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Check calculator is in degree mode or not
                        if (CalculatorState.isDegree) { number = Calc.DegreeToRadian(number); }

                        //Round result
                        result = Math.Round(Calc.Tan(number), 2);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 120://ln
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("ln(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where ln( length is 3 so
                        for (index = (startPosition + 3); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Round result
                        result = Math.Round(Calc.Ln(number), 4);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 121://log
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("log(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where log( length is 4 so
                        for (index = (startPosition + 4); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Round result
                        result = Math.Round(Calc.Log(number), 4);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 130://e^
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("e^(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where e^( length is 3 so
                        for (index = (startPosition + 3); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Round result
                        result = Math.Round(Calc.Exponential(number), 5);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }
                case 140://absolute
                    {
                        startPosition = CalculatorState.evaluationString.IndexOf("abs(");
                        endPosition = CalculatorState.evaluationString.IndexOf(')', startPosition);
                        //Where abs( length is 4 so
                        for (index = (startPosition + 4); index < endPosition; index++)
                        {
                            //Getting Number lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        //Parsing into float
                        number = double.Parse(parsedNumber);
                        //Round result
                        result = Calc.Absolute(number);
                        //Than replaces in evaluationString
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
                        break;

                    }              
                //Same process on three cases so
                case 131:
                case 132:
                case 241:
                    {
                        string givenNumber = "";
                        startPosition = CalculatorState.evaluationString.IndexOf("^(");
                        endPosition = CalculatorState.evaluationString.IndexOf(")", startPosition);
                        //Where ^( length is 2 so
                        for (index = (startPosition + 2); index < endPosition; index++)
                        {
                            //Getting Number Lies inside Bracket
                            parsedNumber += CalculatorState.evaluationString[index];
                        }
                        double power = double.Parse(parsedNumber);
                        //To get the actual number
                        index = startPosition - 1;

                        //Replace this with regular expression for short hand
                        while (index >= 0 && (CalculatorState.evaluationString[index] != '+' && txtBoxExpression.Text[index] != '-' && txtBoxExpression.Text[index] != '*' && txtBoxExpression.Text[index] != '/' && txtBoxExpression.Text[index] != '%' && txtBoxExpression.Text[index] != '(' && txtBoxExpression.Text[index] != ')'))
                        {
                            givenNumber += CalculatorState.evaluationString[index];
                            index--;
                        }
                        //Result
                        result = Math.Round(Calc.Power(double.Parse(givenNumber), power), 2);
                        //Replace result on txtBoxExpression
                        CalculatorState.evaluationString = CalculatorState.evaluationString.Remove(index + 1, endPosition - index).Insert(index + 1, result.ToString());

                        break;
                    }
                
                //On Default Case do nothing 

                default: { break; }

            }

        }


        //Handle Click on Buttons which are like function
        private void btnFunction_Clicked(object sender, EventArgs e)
        {
            //Type Casting
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            //To Parse details from button
            ParseFunctionText(b.Text, b.Name);

        }
        //Method to parse function text
        private void ParseFunctionText(string Text, string name)
        {
            Regex regex = new Regex(@"[+\-% */]$");


            //Declaration and Initialization of variable
            string templateString = "", command = Text;
            //Checking command Text to perform operation
            if (Text == "√") { command = "sqrt"; }
            if (Text == "e^x") { command = "e^"; }
            if (Text == "|x|") { command = "abs"; }
            if (Text == "x^2") { command = "x^"; }
            if (Text == "2^x") { command = "2^"; }


            //To save the status of currently which button is clicked
            CalculatorState.currentCommand = command;
            templateString = name;

            //To get Last Two char from templateString
            CalculatorState.currentBtnIndex = templateString.Substring(templateString.Length - 2);

            if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
            {
                //Updating Expression
                UpdateExpression(command + "(");
                //Increment countOfBracket
                CalculatorState.countOfBracket++;
            }

            else
            {
                //Updating Expression
                UpdateExpression("*" + command + "(");
                //Increment countOfBracket
                CalculatorState.countOfBracket++;
            }

        }
    
     
        //When button of fifth row and 2nd column is clicked
        private void btn41_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[+\-% */]$");

            if (!CalculatorState.isSecondPage)
            {
                if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
                {
                    UpdateExpression(Calc.PI().ToString());
                  
                }

                else
                {
                    UpdateExpression(Calc.PI().ToString());

                }
            }
            //On second page
            else
            {
                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
                ParsePowerFunctionText(b.Text, b.Name);
            }

        }
        //When button of fifth row and 3rd column is clicked
        private void btn42_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[+\-% */]$");

            if (!CalculatorState.isSecondPage)
            {

                if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
                {
                    UpdateExpression(Calc.E().ToString());
                }
                //On second page
                else
                {
                    UpdateExpression("*" + Calc.E().ToString());

                }
            }
            //On second page
            else
            {
                //Type Casting
                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
                //To Parse details from button
                ParseFunctionText(b.Text, b.Name);
            }

        }

        //When button of third row and third column is clicked
        private void btn22_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[+\-% */]$");

            if (!CalculatorState.isSecondPage)//I.e When (1/x)button is clicked
            {
                if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
                {
                    UpdateExpression("1/");
                }
                else
                {
                    UpdateExpression("*1/");
                }


            }
            //On Second Page
            else
            {
                //Type Casting
                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
                //To Parse details from button
                ParseFunctionText(b.Text, b.Name);

            }
        }
        //Handling Click events on buttons which display power of something

        private void PowerFunction_Clicked(object sender, EventArgs e)
        {
            if (!CalculatorState.isSecondPage)
            {
                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

                ParsePowerFunctionText(b.Text, b.Name);

            }
            //On Second Page
            else
            {
                //Type Casting
                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
                //To Parse details from button
                ParseFunctionText(b.Text, b.Name);

            }


        }
        //Method to parse power function text
        private void ParsePowerFunctionText(string command, string Name)
        {
            Regex regex = new Regex(@"[+\-% */()]$");

            string expression = txtBoxExpression.Text;
            //To Save the status of currently which button is clicked
            CalculatorState.currentBtnIndex = Name.Substring(Name.Length - 2);
            CalculatorState.currentCommand = command;

            //Check Whether txtBoxExpression is empty or not and it doesn't ends with /,*,-,+,%,(,)
            if (!(expression.Length == 0 || regex.IsMatch(txtBoxExpression.Text)))
            {
                if (command == "x^2")
                {
                    UpdateExpression("^(2)");
                }
                else if (command == "x^3")
                {

                    UpdateExpression("^(3)");
                }
                else
                {
                    UpdateExpression("^(");
                    CalculatorState.countOfBracket++;
                }
            }

        }
        //Click event on Radian/Degree Change Button
        private void btn01_Click(object sender, EventArgs e)
        {
            if (!CalculatorState.isDegree)
            {
                btn01.Text = "Rad";
                CalculatorState.isDegree = true;
            }
            else
            {
                btn01.Text = "Deg";
                CalculatorState.isDegree = false;
            }
        }


       private void updateBasesValues(double baseTenNumber)
        {

            int value = Convert.ToUInt16(baseTenNumber);
            labelbase2.Text = BaseConverter.FromBase10(value, 2);
            label8.Text = BaseConverter.FromBase10(value, 8);
            label10.Text = baseTenNumber+"";
            label16.Text = BaseConverter.ToHex(value);
        } 

        //Close Button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Minimize button
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Validate key press on txtBoxExpression
        private void txtBoxExpression_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SwapValues();
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                BackSpaceClick();
                e.Handled = true;
            }
            else if (e.KeyChar == 37 || e.KeyChar == 40 || e.KeyChar == 41 || e.KeyChar == 42 || e.KeyChar == 43 || e.KeyChar == 45 || e.KeyChar == 46 || e.KeyChar == 47)
            {
                e.Handled = false;
                CalculatorState.evaluationString += ((char)e.KeyChar).ToString();
            }
            else
            {
                bool status = !char.IsDigit(e.KeyChar);
                if (status) { e.Handled = true; }
                else
                {
                    CalculatorState.evaluationString += ((char)e.KeyChar).ToString();
                    e.Handled = false;
                }

            }

        }

       

        private void scientificClick(object sender, EventArgs e)
        {
            this.siepanel.Visible = true;

        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.siepanel.Visible = false;
            this.panelSoft.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.siepanel.Visible = false;
            this.panelSoft.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.siepanel.Visible = true;
            this.panelSoft.Visible = false;

        }

      
        private void buttonHistory_Click(object sender, EventArgs e)
        {
            if (historyForm.IsDisposed)
            {
                historyForm = new HistoryForm();
            }
            historyForm.Show();
            historyForm.BringToFront();
        }

        private void txtBoxResult_TextChanged(object sender, EventArgs e)
        {
            

        }
    }
}
