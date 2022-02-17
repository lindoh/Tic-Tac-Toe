using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Will store the current mark type for each cell
        /// </summary>
        MarkType[] Results;

        /// <summary>
        /// True when the Player is playing and False when computer plays
        /// </summary>
        private bool PlayerTurn;

        /// <summary>
        /// True when the Game is over
        /// </summary>
        private bool GameOver;

        private Button Btn;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            //Initialize important components 
            InitializeComponent();
            NewGame();
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// To initialise the private members immediately at the start of the game
        /// </summary>
        private void NewGame()
        {
            //Create an array of 9 cells
            Results = new MarkType[9];

            //Make all cells markers Free
            for (int i = 0; i < Results.Length; i++)
                Results[i] = MarkType.Free;

            //Make sure the Player starts the game
            PlayerTurn = true;

            //True if the Game has ended and false otherwise
            GameOver = false;

            //Clear the cells content
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

          
        }

        #endregion

        /// <summary>
        /// To Update a clicked cell/button with an appropriate marker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;     //Cast the button to the sender

            //If Game ended start a new Game
            if(GameOver)
            {
                NewGame();
            }

            if (!GameOver && PlayerTurn)
            {
                //Find row and column of the clicked cell/button
                int row = Grid.GetRow(button);          
                int column = Grid.GetColumn(button);
                int index = column + (row * 3);         //Calculate the index for the Results array

                //If the cell is not Free return
                if (Results[index] != MarkType.Free)
                    return;

                Results[index] = MarkType.Cross;        //If it's the Player's turn update the cell with a cross
                button.Content = "X";                   //Update the button content
                PlayerTurn = false;                     //Computer to play next

                CheckForWinner();                       //Check if the player won already
            }

            if (!GameOver && !PlayerTurn)
            {
                ComputerPlay();                         //It's is the computer's turn to play
                CheckForWinner();                       //Check if the computer won already
            }

        }

        #region Supporting Methods
        /// <summary>
        /// To manage the computer play
        /// </summary>
        private void ComputerPlay()
        {
            bool ValidMove = false;     //True when a Free cell has been found
            Button button;

            //Create a random number for the computer's move
            Random rand = new();
            int num = rand.Next(0, 9);

            while (!ValidMove)
            {
                //If the cell is Free
                if (Results[num] == MarkType.Free)
                {
                    button = FindButton(num);
                    Results[num] = MarkType.Nought;     //Update the results array
                    button.Content = "0";               //Display a Nought on the computer's cell
                    button.Foreground = Brushes.Red;
                    ValidMove = true;                   //Exit the loop, a free a cell has been found and updated
                    PlayerTurn = true;                      //Player to play next
                }
                else
                {
                    num = rand.Next(0, 9);
                } 
            }
        }

        /// <summary>
        /// To find the corresponding button using a random number generated
        /// </summary>
        /// <param name="index">The random number which corresponds to the cell numbers (0 - 8)</param>
        /// <returns></returns>
        private Button FindButton(int index)
        {
            //An Array of all the buttons, the computer will use this array for the random play
            Button[] button = { Button0_0, Button0_1, Button0_2, Button1_0, Button1_1, Button1_2, Button2_0, Button2_1, Button2_2 };

            //Declare and initialize row and column
            int row = 0;
            int column = 0;

            //Find the column value using the given random number (index)
            //The column value can be assumed using the position of the index
            if (index <= 2)
                row = 0;
            else if (index > 2 && index <= 5)
                row = 1;
            else
                row = 2;

            column = index - (3 * row);                 //Calculate the row from the column and index values
            string s = $"Button{row}_{column}";         //Create a string equavalent for the buttons

            //Find and assign the button
            for (int i = 0; i < button.Length; i++) 
            {
                if (button[i].Name.ToString() == s)
                {
                    Btn = button[i];
                    break;
                }
            }
            return Btn;     //Return the right button picked by the computer
        }

        /// <summary>
        /// Find out who won the game and highlight the winning cells
        /// And Restart the game on the next click
        /// </summary>
        private void CheckForWinner()
        {
            //-------------------------------------Check the Horizontal combinations---------------------------------
            if ((Results[0] != MarkType.Free) && ((Results[0] & Results[1] & Results[2]) == Results[0]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            else if ((Results[3] != MarkType.Free) && ((Results[3] & Results[4] & Results[5]) == Results[3]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            else if ((Results[6] != MarkType.Free) && ((Results[6] & Results[7] & Results[8]) == Results[6]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //-------------------------------------Check the Vertical combinations---------------------------------
            if ((Results[0] != MarkType.Free) && ((Results[0] & Results[3] & Results[6]) == Results[0]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            else if ((Results[1] != MarkType.Free) && ((Results[1] & Results[4] & Results[7]) == Results[1]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            else if ((Results[2] != MarkType.Free) && ((Results[2] & Results[5] & Results[8]) == Results[2]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //-------------------------------------Check the Diagonal combinations---------------------------------
            if ((Results[0] != MarkType.Free) && ((Results[0] & Results[4] & Results[8]) == Results[0]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            else if ((Results[2] != MarkType.Free) && ((Results[2] & Results[4] & Results[6]) == Results[2]))
            {
                GameOver = true;         //Game has ended

                //Highlight the winning cells
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }

            //-------------------------------------If There is no winner---------------------------------
            bool NoWinner = false;

            //Search for Free cells
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] == MarkType.Free)
                    return;

                //If there is no free cell, it means there is no winner
                if (i + 1>= Results.Length)
                    NoWinner = true;
            }

            //If the Game has not ended, no free cells, and there is no winner, Highlight the whole board with orange
            if (!GameOver && NoWinner)
            {
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    GameOver = true;
                    button.Background = Brushes.Orange;
                });
            }
        }

        #endregion
    }
}
