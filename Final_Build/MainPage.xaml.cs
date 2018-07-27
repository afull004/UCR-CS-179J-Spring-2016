using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Adafruit_LEDBackpack;
using Windows.Devices.Input;
using Windows.Devices.Gpio;
using IoTHelpers;
using IoTHelpers.Gpio;
using IoTHelpers.Boards;
using IoTHelpers.Gpio.Modules;
using IoTHelpers.Gpio.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
//Found libraries for Windows 10 IOT 7-seg from: https://github.com/sobek85/Adafruit_LEDBackpack
namespace Forth_Time_Now
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    

    public sealed partial class MainPage : Page
    {
        private readonly PushButton button_go;
        private readonly PushButton button_reset;
        private readonly PushButton button_up;
        private readonly PushButton button_down;

        public AlphaNumericFourCharacters alpha;
        public AlphaNumericFourCharacters beta;
        public AlphaNumericFourCharacters gamma;
        public AlphaNumericFourCharacters delta;
        

        char textboxCount = '0';

        int A = 0;
        int B;
        char C = '1';
        char C1 = '0', C2 = '0', C3 = '0', C4 = '0', C5 = '0', C6 = '0', C7 = '0', C8 = '0', C9 = '0',
            C10 = '0', C11 = '0', C12 = '0', C13 = '0', C14 = '0', C15 = '0', C16 = '0';
        int Array_Size;
        int Counter = 1;
        int blockCount = 0;

        private GpioPin GO_BUTTON;
        private GpioPin RESET_BUTTON;
        private GpioPin UP_BUTTON;
        private GpioPin DOWN_BUTTON;

        Windows.UI.Xaml.Controls.TextBox txt = new Windows.UI.Xaml.Controls.TextBox();
        TextBlock[] arrayOfTextBlocks;

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            Array_Size = Convert.ToInt32(textBoxArray.Text);
            if (Array_Size < 16)
            {
                Array_Size++;
            }
            textBoxArray.Text = Convert.ToString(Array_Size);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Array_Size = Convert.ToInt32(textBoxArray.Text);
            if (Array_Size > 0)
            {
                Array_Size--;
            }
            textBoxArray.Text = Convert.ToString(Array_Size);
        }

        TextBox[] arrayOfTextBoxes;

        public MainPage()
        {
            this.InitializeComponent();

            textBlock1.Text = " ";
            textBoxArrayHeader.Text = "Please enter the size of array_1";
            textBoxArray.Text = "0";
            textBox0.Opacity = 0.4;
            textBox21.Opacity = 0.4;
            textBox20.Opacity = 0.4;
            textBox19.Opacity = 0.4;
            textBox18.Opacity = 0.4;
            textBox17.Opacity = 0.4;
            TextBox[] arrayOfTextBoxes2 = { textBox0, textBox1, textBox2, textBox3, textBox4,
                textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11,
                textBox12, textBox13, textBox14, textBox15, textBox16,
                textBox17, textBox18, textBox19, textBox20, textBox21, };
            TextBlock[] arrayOfTextBlocks2 = { textBlock0, textBlock1, textBlock2, textBlock3, textBlock4,
                textBlock5, textBlock6, textBlock7, textBlock8, textBlock9, textBlock10, textBlock11,
                textBlock12, textBlock13, textBlock14, textBlock15, textBlock16,
                textBlock17, textBlock18, textBlock19, textBlock20, textBlock21, };

            arrayOfTextBoxes = arrayOfTextBoxes2;
            arrayOfTextBlocks = arrayOfTextBlocks2;

            
            string tempBox;
            string tempBlock;
            for (int i = 0; i < 22; i++)
            {
                tempBox = "textBox" + i.ToString();
                tempBlock = "textBlock" + i.ToString();

            }
            for (int i = 0; i < 22; i++)
            {
                if (i == 0 || i > 16)
                {
                    arrayOfTextBlocks[i].Text = "This is out of the array's memory range";
                }
                else
                {
                    arrayOfTextBlocks[i].Text = " ";
                }

            }

            textBlockError.Text = " ";

            Unloaded += MainPage_Unloaded;

            var gpio = GpioController.GetDefault();

            alpha = new AlphaNumericFourCharacters(0x70);
            beta = new AlphaNumericFourCharacters(0x71);
            gamma = new AlphaNumericFourCharacters(0x72);
            delta = new AlphaNumericFourCharacters(0x73);

            alpha.ClearDisplay();
            beta.ClearDisplay();
            gamma.ClearDisplay();
            delta.ClearDisplay();

            alpha.SetBlinkRate(0);
            beta.SetBlinkRate(0);
            gamma.SetBlinkRate(0);
            delta.SetBlinkRate(0);

            alpha.SetBrightness(1);
            beta.SetBrightness(1);
            gamma.SetBrightness(1);
            delta.SetBrightness(1);
            
            
            GO_BUTTON = gpio.OpenPin(pinNumber: 12);
            GO_BUTTON.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            if (GO_BUTTON.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                GO_BUTTON.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                GO_BUTTON.SetDriveMode(GpioPinDriveMode.Input);
            GO_BUTTON.ValueChanged += GO_BUTTON_ValueChanged;

            RESET_BUTTON = gpio.OpenPin(pinNumber: 26);
            RESET_BUTTON.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            if (RESET_BUTTON.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                RESET_BUTTON.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                RESET_BUTTON.SetDriveMode(GpioPinDriveMode.Input);
            RESET_BUTTON.ValueChanged += RESET_BUTTON_ValueChanged;

            UP_BUTTON = gpio.OpenPin(pinNumber: 19);
            UP_BUTTON.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            if (UP_BUTTON.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                UP_BUTTON.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                UP_BUTTON.SetDriveMode(GpioPinDriveMode.Input);
            UP_BUTTON.ValueChanged += UP_BUTTON_ValueChanged;

            DOWN_BUTTON = gpio.OpenPin(pinNumber: 13);
            DOWN_BUTTON.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            if (DOWN_BUTTON.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                DOWN_BUTTON.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                DOWN_BUTTON.SetDriveMode(GpioPinDriveMode.Input);
            DOWN_BUTTON.ValueChanged += DOWN_BUTTON_ValueChanged;
            

            alpha.WriteCharacters(C1, C2, C3, C4);
            beta.WriteCharacters(C5, C6, C7, C8);
            gamma.WriteCharacters(C9, C10, C11, C12);
            delta.WriteCharacters(C13, C14, C15, C16);

            alpha.WriteDisplay();
            beta.WriteDisplay();
            gamma.WriteDisplay();
            delta.WriteDisplay();
            //}
            //button_go.Pressed += ButtonPressed;
            //button_go.Released += ButtonReleased;

        }
        
        private void ButtonPressed(object sender, EventArgs e)
        {
            var alpha = new AlphaNumericFourCharacters(0x70);
            var beta = new AlphaNumericFourCharacters(0x71);
            var gamma = new AlphaNumericFourCharacters(0x72);
            var delta = new AlphaNumericFourCharacters(0x73);
            
            alpha.ClearDisplay();
            beta.ClearDisplay();
            gamma.ClearDisplay();
            delta.ClearDisplay();

            alpha.SetBlinkRate(0);
            beta.SetBlinkRate(0);
            gamma.SetBlinkRate(0);
            delta.SetBlinkRate(0);

            alpha.SetBrightness(1);
            beta.SetBrightness(1);
            gamma.SetBrightness(1);
            delta.SetBrightness(1);


            alpha.WriteCharacters('1', '2', '3', '4');
            beta.WriteCharacters('9', '8', '7', '6');
            gamma.WriteCharacters('0', '3', '6', '9');
            delta.WriteCharacters('2', '5', '8', '7');

            alpha.WriteDisplay();
            beta.WriteDisplay();
            gamma.WriteDisplay();
            delta.WriteDisplay();
            
        }

        private void ButtonReleased(object sender, EventArgs e)
        {
            var alpha = new AlphaNumericFourCharacters(0x70);
            var beta = new AlphaNumericFourCharacters(0x71);
            var gamma = new AlphaNumericFourCharacters(0x72);
            var delta = new AlphaNumericFourCharacters(0x73);

            alpha.ClearDisplay();
            beta.ClearDisplay();
            gamma.ClearDisplay();
            delta.ClearDisplay();

            alpha.SetBlinkRate(0);
            beta.SetBlinkRate(0);
            gamma.SetBlinkRate(0);
            delta.SetBlinkRate(0);

            alpha.SetBrightness(1);
            beta.SetBrightness(1);
            gamma.SetBrightness(1);
            delta.SetBrightness(1);

            alpha.WriteCharacters('0', '0', '0', '0');
            beta.WriteCharacters('0', '0', '0', '0');
            gamma.WriteCharacters('0', '0', '0', '0');
            delta.WriteCharacters('0', '0', '0', '0');

            alpha.WriteDisplay();
            beta.WriteDisplay();
            gamma.WriteDisplay();
            delta.WriteDisplay();
        }
        private void MainPage_Unloaded(object sender, object args)
        {
            //button_go.Pressed -= ButtonPressed;
            //button_go.Released -= ButtonReleased;
            GO_BUTTON.ValueChanged -= GO_BUTTON_ValueChanged;
            GO_BUTTON.Dispose();
            RESET_BUTTON.ValueChanged -= RESET_BUTTON_ValueChanged;
            RESET_BUTTON.Dispose();
            UP_BUTTON.ValueChanged -= UP_BUTTON_ValueChanged;
            UP_BUTTON.Dispose();
            DOWN_BUTTON.ValueChanged -= DOWN_BUTTON_ValueChanged;
            DOWN_BUTTON.Dispose();

            //button_go.Dispose();
        }

        private void GO_BUTTON_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            
            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    Array_Size = Convert.ToInt32(textBoxArray.Text);
                    if (Counter + Array_Size > 17)
                    {
                        textBlockError.Text = "You do not have room in memory to create more arrays";
                    }
                    else
                    {

                        if (Array_Size == 0)
                        {
                            textBlockError.Text = "You cannot create an array of size 0";
                        }
                        else
                        {

                            textboxCount++;

                            for (int i = Counter; i < Counter + Array_Size && 18 > Counter + Array_Size; i++)
                            {
                                arrayOfTextBoxes[i].Background = new SolidColorBrush(Colors.Crimson);
                                arrayOfTextBlocks[i].Text = "     array_" + this.C.ToString();
                                switch (i)
                                {
                                    case 1:
                                        C1 = C;
                                        break;
                                    case 2:
                                        C2 = C;
                                        break;
                                    case 3:
                                        C3 = C;
                                        break;
                                    case 4:
                                        C4 = C;
                                        break;
                                    case 5:
                                        C5 = C;
                                        break;
                                    case 6:
                                        C6 = C;
                                        break;
                                    case 7:
                                        C7 = C;
                                        break;
                                    case 8:
                                        C8 = C;
                                        break;
                                    case 9:
                                        C9 = C;
                                        break;
                                    case 10:
                                        C10 = C;
                                        break;
                                    case 11:
                                        C11 = C;
                                        break;
                                    case 12:
                                        C12 = C;
                                        break;
                                    case 13:
                                        C13 = C;
                                        break;
                                    case 14:
                                        C14 = C;
                                        break;
                                    case 15:
                                        C15 = C;
                                        break;
                                    case 16:
                                        C16 = C;
                                        break;
                                }
                            }

                            alpha.WriteCharacters(C1, C2, C3, C4);
                            beta.WriteCharacters(C5, C6, C7, C8);
                            gamma.WriteCharacters(C9, C10, C11, C12);
                            delta.WriteCharacters(C13, C14, C15, C16);

                            alpha.WriteDisplay();
                            beta.WriteDisplay();
                            gamma.WriteDisplay();
                            delta.WriteDisplay();

                            
                            Counter = Counter + Array_Size;
                            C++;
                            textBlockError.Text = " ";
                            textBoxArrayHeader.Text = "Please enter the size of array_" + this.C.ToString();
                            Array_Size = 0;
                            textBoxArray.Text = Array_Size.ToString();
                            textBlockError.Text = " ";
                        }
                    }
                }
                else
                {

                }
            });
            
        }

        private void RESET_BUTTON_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            
            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    Counter = 1;
                    C = '1';
                    for (int i = 0; i < 22; i++)
                    {
                        arrayOfTextBoxes[i].Background = new SolidColorBrush(Colors.White);
                        if (i == 0 || i > 16)
                        {
                            arrayOfTextBlocks[i].Text = "This is out of the array's memory range";
                        }
                        else
                        {
                            arrayOfTextBlocks[i].Text = " ";

                        }
                    }

                    C1 = C2 = C3 = C4 = C5 = C6 = C7 = C8 = C9 = C10 = C11 = C12 = C13 = C14 = C15 = C16 = '0';

                    alpha.ClearDisplay();
                    beta.ClearDisplay();
                    gamma.ClearDisplay();
                    delta.ClearDisplay();

                    alpha.WriteCharacters(C1, C2, C3, C4);
                    beta.WriteCharacters(C5, C6, C7, C8);
                    gamma.WriteCharacters(C9, C10, C11, C12);
                    delta.WriteCharacters(C13, C14, C15, C16);

                    alpha.WriteDisplay();
                    beta.WriteDisplay();
                    gamma.WriteDisplay();
                    delta.WriteDisplay();

                    textBoxArrayHeader.Text = "Please enter the size of array_1";
                    Array_Size = 0;
                    textBoxArray.Text = Array_Size.ToString();
                    textBlockError.Text = " ";
                }
                else
                {

                }
            });

        }

        private void UP_BUTTON_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {

            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    Array_Size = Convert.ToInt32(textBoxArray.Text);
                    if (Array_Size < 9)
                    {
                        Array_Size++;
                    }
                    textBoxArray.Text = Convert.ToString(Array_Size);
                }
                else
                {

                }
            });

        }

        private void DOWN_BUTTON_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {

            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    Array_Size = Convert.ToInt32(textBoxArray.Text);
                    if (Array_Size > 0)
                    {
                        Array_Size--;
                    }

                    textBoxArray.Text = Convert.ToString(Array_Size);
                }
                else
                {

                }
            });

        }

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            
        }
                
        //FOR DYNAMICALLY CREATING BOXES AND BLOCKS
        public Windows.UI.Xaml.Controls.TextBlock AddNewTextBlock()
        {
            Windows.UI.Xaml.Controls.TextBlock txtBlock = new Windows.UI.Xaml.Controls.TextBlock();
            txtBlock.Name = "txtBlock" + this.blockCount.ToString();
            txtBlock.Height = 28;
            txtBlock.Width = 300;
            this.Grid1.Children.Add(txtBlock);
            txtBlock.VerticalAlignment = VerticalAlignment.Bottom;
            txtBlock.Margin = new Windows.UI.Xaml.Thickness { Bottom = A * 30, Left = 300 };
            txtBlock.HorizontalAlignment = HorizontalAlignment.Left;
            //txtBlock.Margin = new Windows.UI.Xaml.Thickness;
            txtBlock.Text = "Array_" + this.C.ToString();
            blockCount = blockCount + 1;
            return txtBlock;
        }
        public Windows.UI.Xaml.Controls.TextBox AddNewTextBox()
        {
            Windows.UI.Xaml.Controls.TextBox txt = new Windows.UI.Xaml.Controls.TextBox();
            txt.Name = "txt_" + this.A.ToString();
            txt.IsReadOnly = true;
            txt.Height = 28;
            txt.Width = 200;
            this.Grid1.Children.Add(txt);
            txt.VerticalAlignment = VerticalAlignment.Bottom;//A * 28;
            txt.Margin = new Windows.UI.Xaml.Thickness { Bottom = A * 30 };
            txt.HorizontalAlignment = HorizontalAlignment.Left; //15;
            B = A * 4;
            txt.Text = "Memory Address: " + this.B.ToString();
            if (A <= 1 || A >= 21)
            {
                txt.Opacity = 0.4;
            }
            else
            {
                txt.Background = new SolidColorBrush(Colors.Crimson);
            }
            A = A + 1;
            if (txt.Name == "txt_3")
            {
                txt.Text = "HAHAHA";
            }
            return txt;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBlock2_Copy10_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
