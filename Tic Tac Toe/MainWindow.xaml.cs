using System.Windows;


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

        #endregion
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        private void NewGame()
        {
            //Create an array of 9 cells
            Results = new MarkType[9];

            //Make all cells markers Free
            for (int i = 0; i < Results.Length; i++)
                Results[i] = MarkType.Free;
        }
    }
}
