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

            if (PlayerTurn)
            {

                //Find row and column of the clicked cell/button
                int row = Grid.GetRow(button);          
                int column = Grid.GetColumn(button);

                int index = column + (row * 3);         //Calculate the index for the Results array
                Results[index] = MarkType.Cross;        //If it's the Player's turn update the cell with a cross
                button.Content = "X";                   //Update the button content
                PlayerTurn = false;                     //Computer to play next
            }
            if (!PlayerTurn)
            {
                ComputerPlay();
            }

        }

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
    }
}
