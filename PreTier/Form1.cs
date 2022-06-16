
using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Scientific_Calculator.DataTier;
using Scientific_Calculator.AppTier;
using Scientific_Calculator.PreTier;


namespace Scientific_Calculator
{

    public partial class MainForm : Form
    {
        //creating new state and pass it to calculator
        CalculatorState calculatorState;
        CalculatorWithHistory calculator;
        //creating new state and pass it to Logger
        LoggerState loggerState = new LoggerState();
        Logger logger = Logger.Instance;
        HistoryForm historyForm = new HistoryForm();

        //Constructor
        public MainForm()
        {
            InitializeComponent();
        }

        //Form Load Event
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtBoxExpression.Select();
            guna2ShadowForm1.SetShadowForm(this);
            guna2DragControl1.TargetControl = this;
            radioButton1.Checked = true;
            this.siepanel.Visible = false;

             calculatorState = new CalculatorState();
             calculator = new CalculatorWithHistory(calculatorState);
        }

       

        private void UpdateExpression(string ex)
        {
            calculator.UpdateExpression(ex);
            calculatorState.EvaluationString += ex;
            txtBoxExpression.Text += ex;
            calculatorState.Expression = txtBoxExpression.Text;
        }

        public void Eval()
        {


            try
            {
              double numirecResult=   calculator.Evaluate(txtBoxExpression.Text);
              txtBoxResult.Text = calculatorState.Result;
              updateBasesValues(numirecResult);
            }
            catch (Exception)
            {

                txtBoxResult.Text = "";
                calculator.ResetResult();
                updateBasesValues(0);
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

            txtBoxExpression.Text = "";
            txtBoxResult.Text = "";
            calculator.clearHistory(txtBoxExpression.Text);
        }

        //Method to Swap Values between Two text fields
        private void SwapValues()
        {
            logger.addNewlog(calculatorState.EvaluationString + " = " + txtBoxResult.Text);
            try
            {
             updateBasesValues(Convert.ToUInt32(txtBoxResult.Text));

            }
            catch (Exception)
            {

            }


            calculator.updateResult(txtBoxResult.Text);
            txtBoxExpression.Text = txtBoxResult.Text;
            calculator.updateResult("");
            txtBoxResult.Text = "";

            calculator.updateCountOfBracket(0);
            txtBoxExpression.Select(txtBoxExpression.Text.Length, 0);
            calculator.UpdateExpression(txtBoxExpression.Text);
            calculator.updateResult(txtBoxExpression.Text);


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

                string tempExpression = txtBoxExpression.Text;
                txtBoxExpression.Text = calculator.BackSpace(tempExpression);
                txtBoxResult.Text = "";
            }
            txtBoxExpression.Select(txtBoxExpression.Text.Length, 0);
            Eval();
        }


        private void txtBoxExpression_TextChanged(object sender, EventArgs e)
        {
            Eval();

        }

        //Handling Click Events on brackets button
        private void btnBrackets_Click(object sender, EventArgs e)
        {

            //  calculator.bracketsClick(txtBoxExpression.Text);
            //Regular Expression to check if string ends with any algebric opertaor
            Regex regex = new Regex(@"[+\-% */]$");
            //Regular expression to check if strikng ends with numeric digit
            Regex numericEnd = new Regex(@"\d$");

            //if Expression is empty
            if (calculatorState.CountOfBracket == 0 && txtBoxExpression.Text.Length == 0)
            {
                UpdateExpression("(");
                calculatorState.CountOfBracket++;
            }

            //If opening bracket is on last index
            else if (txtBoxExpression.Text.LastIndexOf('(') == txtBoxExpression.Text.Length - 1)
            {
                UpdateExpression("(");
                calculatorState.CountOfBracket++;
            }
            //If string ends with algebric operator
            else if (regex.IsMatch(txtBoxExpression.Text))
            {
                UpdateExpression("(");
                calculatorState.CountOfBracket++;
            }
            //
            else if (calculatorState.CountOfBracket == 0 && numericEnd.IsMatch(txtBoxExpression.Text))
            {
                UpdateExpression("*(");
                calculatorState.CountOfBracket++;

            }
            //Otherwise push closing bracket
            else
            {
                UpdateExpression(")");
                calculatorState.CountOfBracket--;
            }



        }

        //Handling Click Events on ChangeSign Button
        private void btnChangeSign_Click(object sender, EventArgs e)
        {
            calculator.changeSign(txtBoxExpression.Text);
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
            if (Text == "1/x") { command = "oneOver"; }





            //To save the status of currently which button is clicked
            calculatorState.CurrentCommand = command;
            templateString = name;

            //To get Last Two char from templateString
            calculatorState.CurrentBtnIndex = templateString.Substring(templateString.Length - 2);

            if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
            {
                //Updating Expression
                UpdateExpression(command + "(");
                //Increment countOfBracket
                calculator.forwCountOfBracket();

            }

            else
            {
                //Updating Expression
                UpdateExpression("*" + command + "(");
                //Increment countOfBracket
                calculator.forwCountOfBracket();
            }

        }
    
     
        //When button of fifth row and 2nd column is clicked
        private void btn41_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[+\-% */]$");

            if (!calculatorState.IsDegree)
            {
                if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
                {
                    UpdateExpression(calculator.PI().ToString());
                  
                }

                else
                {
                    UpdateExpression(calculator.PI().ToString());

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

            if (!calculatorState.IsDegree)
            {

                if (txtBoxExpression.Text.Length == 0 || regex.IsMatch(txtBoxExpression.Text))
                {
                    UpdateExpression(calculator.E().ToString());
                }
                //On second page
                else
                {
                    UpdateExpression("*" + calculator.E().ToString());

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

            if (!calculatorState.IsDegree)//I.e When (1/x)button is clicked
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
            if (!calculatorState.IsDegree)
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
            calculatorState.CurrentBtnIndex = Name.Substring(Name.Length - 2);
            calculatorState.CurrentCommand = command;

            //Check Whether txtBoxExpression is empty or not and it doesn't ends with /,*,-,+,%,(,)
            if (!(expression.Length == 0 || regex.IsMatch(txtBoxExpression.Text)))
            {
                if (command == "x^2")
                {
                    UpdateExpression("^(2)");
                }            
                else
                {
                    UpdateExpression("^(");
                    calculator.forwCountOfBracket();
                }
            }

        }
        //Click event on Radian/Degree Change Button
        private void btn01_Click(object sender, EventArgs e)
        {
            if (!calculatorState.IsDegree)
            {
                btn01.Text = "Rad";
                calculatorState.IsDegree = true;
            }
            else
            {
                btn01.Text = "Deg";
                calculatorState.IsDegree = false;
            }
        }


       private void updateBasesValues(double baseTenNumber)
        {

            int value = Convert.ToUInt16(baseTenNumber);
            labelbase2.Text = BaseConverter.FromBase10(value, 2);
            label8.Text = BaseConverter.FromBase10(value, 8);
            label10.Text = baseTenNumber+"";
            label16.Text = BaseConverter.ToHex(value);

            if (rbBinaryBase.Checked == true)
            {
                txtBoxResult.Text = labelbase2.Text;
            }
            else if (rbHexaDecimalBase.Checked == true)
            {
                txtBoxResult.Text = label16.Text;
            }
            else if (rbOctalBase.Checked == true)
            {
                txtBoxResult.Text = label8.Text;
            }
            else if (rbDecimalBase.Checked == true)
            {
                txtBoxResult.Text = baseTenNumber.ToString();
            }
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
                calculatorState.EvaluationString += ((char)e.KeyChar).ToString(); ;

    
            }
            else
            {
                bool status = !char.IsDigit(e.KeyChar);
                if (status) { e.Handled = true; }
                else
                {
                    calculatorState.EvaluationString += ((char)e.KeyChar).ToString();
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
        private void rbDecimalBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDecimalBase.Checked == true)
            {
                Eval();
            }
        }

        private void rbBinaryBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBinaryBase.Checked == true)
            {
                Eval();
                panelSoft.Visible = false;
            }
        }

        private void rbOctalBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOctalBase.Checked == true)
            {
                Eval();
                panelSoft.Visible = false;
            }
        }

        private void rbHexaDecimalBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHexaDecimalBase.Checked == true)
            {
                Eval();
                panelSoft.Visible = false;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
      
    }
}
