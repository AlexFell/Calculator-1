using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {

            inputCapacity.SelectedIndex = inputCapacity.FindString("10");
            outputCapacity.SelectedIndex = outputCapacity.FindString("10");
        }

        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cursorPosition = inputBox.SelectionStart;
            int lenghtDifference = button.Text.Length;
            inputBox.Text = inputBox.Text.Insert(cursorPosition, button.Text);
            inputBox.SelectionStart = cursorPosition + lenghtDifference;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            inputBox.Text = "";
            outputBox.Text = "";
        }

        private void Erase_Click(object sender, EventArgs e)
        {
            if (inputBox.Text != "")
            {
                var cursorPosition = inputBox.SelectionStart;
                if (cursorPosition != 0)
                {
                    var previousSymbol = cursorPosition - 1;
                    inputBox.Text = inputBox.Text.Remove(previousSymbol, 1);
                    inputBox.SelectionStart = cursorPosition - 1;
                }
            }
        }

        private void InputCapacity_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control item in addictionalButtons.Controls)
            {
                item.Enabled = false;
            }
            foreach (Control item in mainButtons.Controls)
            {
                item.Enabled = false;
            }

            num0.Enabled = true;
            num1.Enabled = true;
            delimiter.Enabled = true;
            eraseButton.Enabled = true;

            switch (inputCapacity.SelectedIndex)
            {
                case 1:
                    {
                        foreach (Control item in mainButtons.Controls)
                        {
                            item.Enabled = true;
                        }
                        num8.Enabled = false;
                        num9.Enabled = false;
                    }
                    break;

                case 2:
                    {
                        foreach (Control item in mainButtons.Controls)
                        {
                            item.Enabled = true;
                        }
                    }
                    break;

                case 3:
                    {
                        foreach (Control item in addictionalButtons.Controls)
                        {
                            item.Enabled = true;
                        }
                        foreach (Control item in mainButtons.Controls)
                        {
                            item.Enabled = true;
                        }
                    }
                    break;
            }
        }

        private void Equally_Click(object sender, EventArgs e)
        {
            string toCalculate = "(" + inputBox.Text + ")";
            StringCalculation calculation = new StringCalculation();
            //calculation.Calculation
        }

        private void convert_Click(object sender, EventArgs e)
        {

        }

        
    }
}
