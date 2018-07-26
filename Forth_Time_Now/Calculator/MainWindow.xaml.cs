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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double currentNum = 0;
        double temp1;
        double total = 0;
        double temp2;
        bool answered = false;
        int func;
        bool firstEq = true;
        bool userInput = false;
        bool pressedEqual = false;

        private void clear()
        {
            currentNum = 0;
            temp1 = 0;
            total = 0;
            temp2 = 0;
            answered = false;
            func = 0;
            firstEq = true;
            this.Display.Text = "";
            this.Equation.Text = "";
        }

        private void TotalIt()
        {
            if(func == 0)
            {
                temp1 = currentNum;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    temp2 = currentNum;
                }
                this.Display.Text = "";
                this.Display.Text += total;
                answered = true;
            }
            else if (func == 1)
            {
                temp1 = currentNum;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    temp2 = currentNum;
                }
                this.Display.Text = "";
                total += temp2;
                this.Display.Text += total;
                answered = true;
            }
            else if(func == 2)
            {
                temp1 = currentNum;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    temp2 = currentNum;
                }
                this.Display.Text = "";
                total -= temp2;
                this.Display.Text += total;
                answered = true;
            }
            else if(func == 3)
            {
                temp1 = currentNum;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    temp2 = currentNum;
                }
                this.Display.Text = "";
                total *= temp2;
                this.Display.Text += total;
                answered = true;
            }
            else if(func == 4)
            {
                temp1 = currentNum;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    temp2 = currentNum;
                }
                this.Display.Text = "";
                total /= temp2;
                this.Display.Text += total;
                answered = true;
            }
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Zero.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            if(pressedEqual == true)
            {
                clear();
            }
            this.Display.Text += One.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            if (pressedEqual == true)
            {
                clear();
            }
            this.Display.Text += Two.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Three.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Four.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Five.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Six.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Seven.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Eight.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            if (answered == true)
            {
                this.Display.Text = "";
            }
            this.Display.Text += Nine.Content;
            answered = false;
            userInput = true;
            pressedEqual = false;
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (firstEq == true)
            {
                func = 0;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    total = currentNum;
                }
                firstEq = false;
                answered = true;
            }
            else
            {
                TotalIt();
                func = 0;
            }
            this.Equation.Text = "";
            pressedEqual = true;
            userInput = true;
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            this.Display.Text += ".";
        }


        private void Negative_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text != "")
            {
                currentNum = Convert.ToDouble(this.Display.Text);
                currentNum = currentNum * -1;
                this.Display.Text = Convert.ToString(currentNum);
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (firstEq == true)
            {
                func = 1;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    total = currentNum;
                }
                    
                if (userInput == true)
                {
                    this.Equation.Text += " " + currentNum + " +";
                    userInput = false;
                    firstEq = false;
                    answered = true;
                }
                else
                {
                    if (Equation.Text != "")
                    {
                        this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                        this.Equation.Text = Equation.Text + "+";
                    }
                }
            }
            else
            {
                if (userInput == true)
                {
                    TotalIt();
                    func = 1;
                    this.Equation.Text += " " + currentNum + " +";
                    userInput = false;
                }
                else
                {
                    func = 1;
                    this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                    this.Equation.Text = Equation.Text + "+";
                }
            }
         
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (firstEq == true)
            {
                func = 2;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    total = currentNum;
                }
                if (userInput == true)
                {
                    this.Equation.Text += " " + currentNum + " -";
                    userInput = false;
                    firstEq = false;
                    answered = true;
                }
                else
                {
                    if (Equation.Text != "")
                    {
                        this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                        this.Equation.Text = Equation.Text + "-";
                    }
                }
            }
            else
            {
                if (userInput == true)
                {
                    TotalIt();
                    func = 2;
                    this.Equation.Text += " " + currentNum + " -";
                    userInput = false;
                }
                else
            {
                    func = 2;
                    this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                    this.Equation.Text = Equation.Text + "-";
                }
            }
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            if (firstEq == true)
            {
                func = 3;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    total = currentNum;
                }
                if (userInput == true)
                {
                    this.Equation.Text += " " + currentNum + " *";
                    userInput = false;
                    firstEq = false;
                    answered = true;
                }
                else
                {
                    if (Equation.Text != "")
                    {
                        this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                        this.Equation.Text = Equation.Text + "*";
                    }
                }
            }
            else
            {
                if (userInput == true)
                {
                    TotalIt();
                    func = 3;
                    this.Equation.Text += " " + currentNum + " *";
                    userInput = false;
                }
                else
                {
                    func = 3;
                    this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                    this.Equation.Text = Equation.Text + "*";
                }
            }
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            if (firstEq == true)
            {
                func = 4;
                if (Display.Text != "")
                {
                    currentNum = Convert.ToDouble(this.Display.Text);
                    total = currentNum;
                }
                if (userInput == true)
                {
                    this.Equation.Text += " " + currentNum + " /";
                    userInput = false;
                    firstEq = false;
                    answered = true;
                }
                else
                {
                    if (Equation.Text != "")
                    {
                        this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                        this.Equation.Text = Equation.Text + "/";
                    }
                }
            }
            else
            {
                if (userInput == true)
                {
                    TotalIt();
                    func = 4;
                    this.Equation.Text += " " + currentNum + " /";
                    userInput = false;
                }
                else
                {
                    func = 4;
                    this.Equation.Text = Equation.Text.Substring(0, Equation.Text.Length - 1);
                    this.Equation.Text = Equation.Text + "/";
                }
            }
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (answered == false)
            {
                this.Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            currentNum = 0;
            this.Display.Text = "";
        }
    }
}
