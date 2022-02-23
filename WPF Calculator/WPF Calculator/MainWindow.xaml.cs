using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum eOperation
        {
            e_None,
            e_Add,
            e_Sub,
            e_Div,
            e_Mul
        }

        private eOperation m_previousOperation = eOperation.e_None; 

        public MainWindow()
        {
            InitializeComponent();
        }
        private void PerformCalculation(eOperation operationToPerfom)
        {
            List<double> inputtedNumbers = new List<double>();
            switch (operationToPerfom)
            {
                case eOperation.e_None:
                    break;
                case eOperation.e_Add:
                    inputtedNumbers = textDisplay.Text.Split(" + ").Select(double.Parse).ToList();
                    textDisplay.Text = (inputtedNumbers[0] + inputtedNumbers[1]).ToString();
                    break;
                case eOperation.e_Sub:
                    inputtedNumbers = textDisplay.Text.Split(" - ").Select(double.Parse).ToList();
                    textDisplay.Text = (inputtedNumbers[0] - inputtedNumbers[1]).ToString();
                    break;
                case eOperation.e_Div:
                    try
                    {
                        inputtedNumbers = textDisplay.Text.Split(" ÷ ").Select(double.Parse).ToList();
                        if (inputtedNumbers[1] == (double)0)
                        {
                            throw new DivideByZeroException(); // because apparently it is infinity otherwise... Weird C#
                        }
                        textDisplay.Text = (inputtedNumbers[0] / inputtedNumbers[1]).ToString();
                    }
                    catch (DivideByZeroException)
                    {
                        textDisplay.Text = "ERROR: CAN'T DIVIDE BY ZERO";
                    }
                    break;
                case eOperation.e_Mul:
                    inputtedNumbers = textDisplay.Text.Split(" x ").Select(double.Parse).ToList();
                    textDisplay.Text = (inputtedNumbers[0] * inputtedNumbers[1]).ToString();
                    break;
                default:
                    break;
            }
            m_previousOperation = eOperation.e_None;
        }


        private void onNumberButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (textDisplay.Text == "0")
            {
                textDisplay.Text = b.Content.ToString();
            }
            else
            {
                textDisplay.Text += b.Content.ToString();
            }
        }

        private void btnMul_Click(object sender, RoutedEventArgs e)
        {
            if(m_previousOperation != eOperation.e_None)
            {
                PerformCalculation(m_previousOperation);
            }

            m_previousOperation = eOperation.e_Mul;
            textDisplay.Text += " x ";
        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            if (m_previousOperation != eOperation.e_None)
            {
                PerformCalculation(m_previousOperation);
            }

            m_previousOperation = eOperation.e_Div;
            textDisplay.Text += " ÷ ";
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            if (m_previousOperation != eOperation.e_None)
            {
                PerformCalculation(m_previousOperation);
            }

            m_previousOperation = eOperation.e_Sub;
            textDisplay.Text += " - ";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (m_previousOperation != eOperation.e_None)
            {
                PerformCalculation(m_previousOperation);
            }

            m_previousOperation = eOperation.e_Add;
            textDisplay.Text += " + ";

        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation(m_previousOperation);
            m_previousOperation = eOperation.e_None;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            textDisplay.Text = "";
            m_previousOperation = eOperation.e_None;

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            m_previousOperation = eOperation.e_None;
            if (textDisplay.Text.Length > 0)
            {
                textDisplay.Text = textDisplay.Text.Remove(textDisplay.Text.Length - 1, 1);
            }
        }
    }
}
