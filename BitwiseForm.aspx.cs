using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Threading;
using System.Web.UI.WebControls;

namespace BitwiseOperations
{
    /*
     * Unsigned integer - 4 bytes. 32 bit. Holds a larger positive value. No negative value
     * Signed integer   - Holds positive and Negative numbers with the left most bit denoting
     * positive or negative
     * 
     * Compliment 
     * Left most bit indicates a negative or positive number. Example 0010 = +2 * 1010 = -2
     * 
     * 0001  +1        
     * 1110  Complement (15)
     * Add 1 to 15 (16)
     * 1111 The far left digit is 1 which will indicate a negative number (-8 +4 +2 +1 = -1)    
       
     * 0010 +2
     * 1101 Complement (13)
     * Add 1 to 13 (14)
     * 1110   (-8 +4 +2 = -2)      
     * 
     * Bitshifting - 
     * << >> shifts the bits to the left or right respectively.       
     * 
     */

    public partial class BitwiseForm : System.Web.UI.Page

    {

        // Color CurrentColour = new Color();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                i = ColourLabel.BackColor.G;
            }
            else
            {
                i += 10;
                ColourLabel.BackColor = Color.FromArgb(1, 10, i, 1);           
            }
         }

        protected void AndButton_Click(object sender, EventArgs e)
        {
            /*
             * Work out the bitwise AND of the two given numbers 
             * Example 
             * (num1 = 15)  (num2 = 15) output =  15
             * (num1 = 15)  (num2 = 19) output =  2
             *             
            */

            int num1 = int.Parse(And1TextBox.Text);
            int num2 = int.Parse(And2TextBox.Text);
            AndLabel.Text = string.Format("The result is {0}. Num1 is {1} and Num2 is {2}", num1 & num2, OddOrEven(num1), OddOrEven(num2));
        }

        private string OddOrEven(int thisNum)
        {
            /* Example
             * 
             * 23  00010111 
             *  1  00000001 
             *     00000001 == 1
             *     
             *  6  00000110 
             *  1  00000001
             *     00000000 == 0
             *     
             */

            if ((thisNum & 1) == 1)
            {
                return "Odd";
            }
            else
            {
                return "Even";
            }
        }

        protected void OrButton_Click(object sender, EventArgs e)
        {
            /*
             * Work out the bitwise OR of the two given numbers 
             * Example 
             * (num1 = 15)  (num2 = 15)  result =  15
             * (num1 = 15)  (num2 = 19)  result =  31
            */

            int num1 = int.Parse(Or1TextBox.Text);
            int num2 = int.Parse(Or2TextBox.Text);
            OrLabel.Text = string.Format("The result is {0}", num1 | num2);
        }

        protected void NotButton_Click(object sender, EventArgs e)
        {
            /*
             * Work out the bitwise NOT of a given number with a 1 byte integer
             * Uses two's complement.             
            */

            int num1 = int.Parse(NotTextBox.Text);
            NotLabel.Text = string.Format("The result is {0}", ~(num1));
        }

        protected void XorButton_Click(object sender, EventArgs e)
        {

            /*
             * One or other - TRUE. Both the same - FALSE
             * Example num1 1 = 10 ; num2 = 12 returns 6
             */
            int num1 = int.Parse(Xor1TextBox.Text);
            int num2 = int.Parse(Xor2TextBox.Text);
            XorLabel.Text = string.Format("The result is {0}", num1 ^ num2);
        }

        protected void BitshiftButton_Click(object sender, EventArgs e)
        {
            /*
             * Shift to the left            
             */
            int num1 = int.Parse(BitshiftTextBox.Text);
            int num2 = int.Parse(ShiftbyTextBox.Text);
            BitshiftLabel.Text = string.Format("The result is {0}", num1 << num2);
        }

        protected void No6Button_Click(object sender, EventArgs e)
        {
            /* 
             * Detect which is the first bit of the user entered number is set to ON (1)
             * From right to left (least to most significant bit)
             */
            int code = int.Parse(No6TextBox.Text);
            int mask = 1;
            int OnBit = 0;
            int x = 0;

            /* Bit shift 16 times
             * 
             * ex code 8  0000 0000 0000 1000
             *    mask 1  0000 0000 0000 0001
             * 
             * result of code & mask will be
             *            0000 0000 0000 0000
             *            
             * so bitshift left by 1 until the result returned is one
             * 
             * ie code 8  0000 0000 0000 1000
             *    mask 2  0000 0000 0000 0010
             *            0000 0000 0000 0000
             *            
             */
            for (int bit = 0; bit < 15; bit++)
            {

                if ((code & mask) == 0)
                {
                    mask = mask << 1;   // Bitshift left by 1 position          
                }
                else
                {
                    OnBit = bit;
                    break;
                }
            }

            // Display the first bit number that is ON and show the decimal with the bit turned off
            code = code - mask;  // Switch that bit off.  
            No6Label.Text = string.Format("Bit no {0} was set to ON but is now OFF. Decimal {1}", OnBit + 1, code);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            /*
             *  Check the status of a user entered bit and number and display the output. 
             */
            int bitNumber = int.Parse(TextBox7a.Text);
            int code = int.Parse(TextBox7b.Text);
            int mask = 1;
            int result = 0;
            // Move to the specified bit number
            mask = mask << bitNumber - 1;

            // Carry out a bitwise AND    
            result = code & mask;

            // if result is 0 then the bit was off ; otherwise the bit was on. So output the bit status
            if (result == 0)
            {
                Label7.Text = string.Format("Bit {0} is OFF", bitNumber);
            }
            else
            {
                Label7.Text = string.Format("Bit {0} is ON", bitNumber);
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            /*
             * Using OR to switch a bit on. 
             * Example using a byte for text display
             * 128 64 32 16 8 4 2 1
             *   1  0  0  0 0 0 0 0
             *
             * 128 set to on is blinking.  If 16 is the underlined setting. We can combine these using the OR.
             * 
             * Code   1000 0000
             * Mask   1001 0000  (mask 1, left by 4, then do code | mask, to get)
             *  
             *        1001 0000
             */

            int code = int.Parse(TextBox8a.Text);
            int bitNumber = int.Parse(TextBox8b.Text);
            int mask = 1;

            // Bitshift the mask to the required bit
            mask = mask << bitNumber - 1;

            // Carry out the OR, to switch the bit ON
            code = code | mask;

            // Display result
            Label8.Text = string.Format("New value of code is {0}", code);
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            /*
             * XOR to switch a bit off. Reverse of button 8
             * 
             * 144 -  1001 0000
             * 
             * Switch of bit 5
             * mask   0001 0000
             * XOR    1000 0000  which gives 128
             *        
             */
            int code = int.Parse(TextBox9a.Text);
            int bitNumber = int.Parse(TextBox9b.Text);
            int mask = 1;

            // Bitshift the mask to the required bit
            mask = mask << bitNumber - 1;

            // Carry out the XOR, to switch the bit OFF
            code = code ^ mask;

            // Display result
            Label9.Text = string.Format("New value of code is {0}", code);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            /* How to get the level of a certain primary colour.
             * 
             * Example
             * 
             * alpha 0000 0000 red 1000 1000 green 00000000 blue  11111111
             *         00              88            00             FF
             *            
             int mask = 0xFF; 
             int MyColour = 0x008800FF;
            */

            /*
             * Gets back CC33FF but in decimal (255,204,51,255)
             * Colour is held as 4 separate bytes\numbers.             
             */

            // Shift right by 1 byte (16 bits) to the RED byte.  This will return 0x88 or dec 136.
            //Label10.Text = String.Format("Blue Amount = {0}", (MyColour >> 16) & mask);

            
            

        }
    }
}
