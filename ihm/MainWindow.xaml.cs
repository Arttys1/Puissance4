using Puissance4Upgrade.metier.IA;
using Puissance4Upgrade.stockage;
using System.Windows;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// Interface représentant la fenetre d'acceuil
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Tree tree = new Tree();
            tree.Build(2);
            int count = tree.Count();
            tree.Notation(Etat.ROUGE);
            int note = tree.Value;
        }

        /// <summary>
        /// Methode lançant une nouvelle partie
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Nouvelle_Partie(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Methode permettant de quitter le jeu
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Quitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Methode permettant de charger une partie anciennement sauvegarder
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Charger_Partie(object sender, RoutedEventArgs e)
        {
            try
            {
                GameWindow gameWindow = new GameWindow(JeuDAO.Get().Charger(DAOType.JVJ));
                gameWindow.Show();
                gameWindow.Charger();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Problème de chargement");
            }
        }

        /// <summary>
        /// Methode permettant de lancer une nouvelle partie contre un bot
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindowBot gameWindowBot = new GameWindowBot();
            gameWindowBot.Show();
            this.Close();
        }
    }
}

 
