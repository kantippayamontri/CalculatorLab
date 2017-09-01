using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        
        private int way = 0;
        private string secondoperator = "";
        private int check_start = 1;        
        

        private calculatorengine engine;




        private void resetAll()
        {
            way = 0;
            check_start = 1;
            firstOperand = "0";
            secondoperator = "";

            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            
        }

        public MainForm()
        {
            InitializeComponent();
            engine = new calculatorengine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (way == 1)
            {
                way = 0;
                secondoperator = operate;
                check_start = 2;
            }
            
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;

            
        }
        private void btnOperator_Click(object sender, EventArgs e)
        {
            way = 1;
            string secondOperand;
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }

            if (check_start == 2)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }

                secondOperand = lblDisplay.Text;
                string result = engine.calculate(secondoperator, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                        {
                    lblDisplay.Text = "Error";
                }
                        else
                        {
                    lblDisplay.Text = result;
                    firstOperand = result;
                }
                check_start = 1;
                isAfterEqual = true;
            }
            else
            {
                isAfterOperater = true;
                firstOperand = lblDisplay.Text;

            }
            operate = ((Button)sender).Text;

            isAllowBack = false;

        }

        /*private void btnOperator_Click(object sender, EventArgs e)
        {
            way = 1;
            /*if (pass_operator)
            {
                operate = ((Button)sender).Text;
                return;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                //operate = ((Button)sender).Text;
                return;
            }

            if (check_start == 2)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }

                string secondOperand = lblDisplay.Text;
                string result = engine.calculate(secondoperator, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                    firstOperand = result;
                    
                }
                check_start = 1;
                isAfterEqual = true;
            }
            else
            {
                isAfterOperater = true;
                firstOperand = lblDisplay.Text;
            }

            operate = ((Button)sender).Text;
            /*switch (operate)
            {
                case "+":
                case "-":
                case "X":
        
                case "÷":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    break;
                
                case "%":
                    // your code here
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    break;
                

            }
            isAllowBack = false;
            //pass_operator = true;
            
        }*/

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            
            string secondOperand = lblDisplay.Text;
            string result = engine.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
                firstOperand = result;
                check_start = 1;
            }
            isAfterEqual = true;
        }

        

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnonestep(object sender, EventArgs e)
        {
            string operate_onestep =((Button)sender).Text;
            string firstoperate = lblDisplay.Text;
            string ans;



            switch (operate_onestep)
            {
                case "√":
                    ans = (Math.Sqrt(Convert.ToDouble(firstoperate))).ToString();
                    if (ans.Length > 8)
                    {
                        
                        lblDisplay.Text = ans.Substring(0, 8);
                    }
                    else
                    {
                        lblDisplay.Text = ans;
                    }
                    break;
                case "1/x":
                    ans = (1.0 / Convert.ToDouble(firstoperate)).ToString();
                    if (ans.Length > 8)
                    {

                        lblDisplay.Text = ans.Substring(0, 8);
                    }
                    else
                    {
                        lblDisplay.Text = ans;
                    }
                    break;

            }
        }

        private void M_click(object sender, EventArgs e)
        {

        }
    }
}
