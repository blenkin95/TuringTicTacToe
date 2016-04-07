using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoeTuring
{
    public partial class Form1 : Form
    {
        bool turn = true;//When true = x, when false = y
        int turnCount = 0; //+1 every turn , if 9 with no winner game ends
        bool againstComp = true;
      
        //static string player1, player2;

        public Form1()
        {
            InitializeComponent();
        }

        //public static void setplayernames(string n1, string n2)
        //{
        //    player1 = n1;
        //    player2 = n2;
        //}
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
                   }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Code from Chris Merritt, found on youtube:https://www.youtube.com/watch?v=p3gYVcggQOU&list=PLkGLNXkfl8gWpswazjqXLgz9EMYSNuDCh ");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender; //converts sender object as a button, which is stored in b

            if (turn)
                b.Text = "X";
            else
                b.Text = "O";

            turn = !turn; //change turn
            b.Enabled = false; //Disable button so other player can't change the value
            turnCount++;
            checkForWinner();


            if ((!turn) && (againstComp))
            {
                computer_make_move();
            }

          
        }

        private void checkForWinner()
        {
            bool thereIsAWinner = false;
            //horizontal victories
            if((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                thereIsAWinner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                thereIsAWinner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                thereIsAWinner = true;

            //vertical checks
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                thereIsAWinner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                thereIsAWinner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                thereIsAWinner = true;

            //Diagonal checks
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                thereIsAWinner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                thereIsAWinner = true;



            if (thereIsAWinner)
            {
                disableButtons();

                String winner = "";
                if (turn)
                {
                    winner = p2.Text;
                    oWinCount.Text = (Int32.Parse(oWinCount.Text) + 1).ToString();
                }
                else
                {
                    winner = p1.Text;
                    xWinCount.Text = (Int32.Parse(xWinCount.Text) + 1).ToString();
                }
                MessageBox.Show(winner + " wins!");
                newGameToolStripMenuItem.PerformClick();
            }
            else
            {

                if (turnCount == 9)
                {
                    drawCount.Text = (Int32.Parse(drawCount.Text) + 1).ToString();
                    MessageBox.Show("Draw!");
                    newGameToolStripMenuItem.PerformClick();
                }
            }
        }

        private void disableButtons()
        {
            try
                {
                foreach (Control c in Controls) //For each buton on the form, disable it
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
             }
          catch { } //KEEP THIS IN



        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;

            foreach (Control c in Controls) //For each buton on the form, disable it
                {
                    try
                    {
                        Button b = (Button)c; //convert to button
                        b.Enabled = turn;
                        b.Text = "";
                    }
                    catch { }
                }
            
        }

        private void button_enter(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender; //ref to button
                if (b.Enabled)
                {
                    if (turn)
                        b.Text = "X";
                    else
                        b.Text = "O";
                }

            }
            catch { }
         
        }


        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text = "";
            }
        }

        private void resetCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xWinCount.Text = "0";
            drawCount.Text = "0";
            xWinCount.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        //    Form f2 = new Form2();
        //    f2.ShowDialog();
        //    p1.Text = player1;
        //    p2.Text = player2;
        }

        private void p1_TextChanged(object sender, EventArgs e)
        {

        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text == "Player2")
                againstComp = true;
            else
                againstComp = false;

        }






        //AI moves
        private void computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = look_for_win_or_block("O"); //look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if

            move.PerformClick();
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }






        //CHATBOT FUNCTION
        //structure of code from https://www.youtube.com/watch?v=rBEQz5ic2tY



        private void enterChat_Click(object sender, EventArgs e)
        {
                   
            //ARRAY VALUES ASSIGNMENT
            //1 - 100 && 900 -999 Myoran
            // 101 - 200 && 899 - 800 Liam
            //201 - 300 && 799 - 700 Tom R
            // 301 - 400 && 699 - 600 Tom B

           // bool shutdown = false;
            bool foundResponse = false;
            string inputValue;
            string outputValue = "";

            inputValue = userInputText.Text.ToLower(); //converts response to lowercase


            //generic answers focus more on this
            if (inputValue.Contains('?'))
            {
                outputValue = responseArray(outputValue, 998);
                foundResponse = true;
            }
            //else if statments


            //specific answers focus less on this
            switch(inputValue)
            {
                case "hello":
                    outputValue = responseArray(outputValue, 1);
                    foundResponse = true;
                    break;

                case "how are you?":
                    outputValue = responseArray(outputValue, 2);
                    foundResponse = true;
                    break;

                case "good move":
                    outputValue = responseArray(outputValue, 3);
                    foundResponse = true;
                    break;
            }
            
    

            if (foundResponse)
            {
                viewChat.Text = outputValue;   
            }
            else if (inputValue == "")
            {
                //The most neutral generic responses
            }
            else
            {
                outputValue = responseArray(outputValue, 999);
                viewChat.Text = outputValue;  
                //if no response found, neutral response given
            }

        }

        //holds the reponse of the array
        private string responseArray(string outputValue, int num)
        {
            Random rnd = new Random();

            switch(num)
            {
                case 1: //hello
                    string[] helloResponse = { "Hey", "'Sup", "Hello", "Howdy", "Welcome" };
                    outputValue = helloResponse[rnd.Next(0, helloResponse.Length)];
                    break;  

                case 2:
                    string[] howareyouResponse = { "Fine", "Good thanks", "ok, you?", "not too bad", "great" };
                    outputValue = howareyouResponse[rnd.Next(0, howareyouResponse.Length)];
                    break;

                case 3:
                    string[] goodmoveResponse = { "thanks", "I know"};
                    outputValue = goodmoveResponse[rnd.Next(0, goodmoveResponse.Length)];
                    break;

                case 998:
                    string[] questionResponse = {"I don't like questions", "lets focus on the game", "meh", "I dont care"} ;
                    outputValue = questionResponse[rnd.Next(0, questionResponse.Length)];
                    break;

                case 999:
                    string[] neutralResponse = { "cool", "wow", "ok then", "ha", "nice one"};
                    outputValue = neutralResponse[rnd.Next(0, neutralResponse.Length)];
                    break;

            }



            return outputValue;
        }

        //hold possible user input

        //searches array

        private void userInputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void viewChat_Click(object sender, EventArgs e)
        {

        }

        


    }

}
