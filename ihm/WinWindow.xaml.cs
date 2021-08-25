using System.Windows;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Logique d'interaction pour WinWindow.xaml
    /// Interface représentant la fenetre de victoire
    /// </summary>
    public partial class WinWindow : Window
    {
        //L'ecran de jeu qui a instancier cette fenetre
        private readonly GameWindow gameWindow = null;
        private readonly GameWindowBot gameWindowBot = null;

        /// <summary>
        /// Constucteur prenant en parametre un booleen pour savoir qui a gagné et l'écran de jeu qui l'a instancier
        /// </summary>
        /// <param name="rougeWin">true si les rouges ont gagné, false si c'est les jaunes</param>
        /// <param name="gameWindow">L'ecran de jeu qui a instancier cette fenetre</param>
        public WinWindow(bool rougeWin,GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
            InitializeComponent();
            if (rougeWin)
            {
                textColor.Content = "Les Rouges ont gagnés !";
            }else
            {
                textColor.Content = "Les Jaunes ont gagnés !";
            }
        }

        /// <summary>
        /// Constucteur prenant en parametre un booleen pour savoir qui a gagné et l'écran de jeu qui l'a instancier
        /// </summary>
        /// <param name="rougeWin">true si les rouges ont gagné, false si c'est les jaunes</param>
        /// <param name="gameWindow">L'ecran de jeu qui a instancier cette fenetre</param>
        public WinWindow(bool rougeWin, GameWindowBot gameWindowBot)
        {
            this.gameWindowBot = gameWindowBot;
            InitializeComponent();
            if (rougeWin)
            {
                textColor.Content = "Les Rouges ont gagnés !";
            }
            else
            {
                textColor.Content = "Les Jaunes ont gagnés !";
            }
        }

        /// <summary>
        /// Methode permettant de quitter cette fenetre et l'ecran de jeu.
        /// Elle nous ramene à l'ecran d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quitter(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            if (gameWindow != null)
            {
                gameWindow.Close();
            }
            else
            {
                gameWindowBot.Close();
            }
            this.Close();
        }
    }
}
