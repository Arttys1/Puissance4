using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// Interface représentant l'interface de jeu
    /// </summary>
    public partial class GameWindow : Window
    {
        //Elle a un jeu qui s'occupera de relier les informations entre la couche ihm et la couche métier
        private readonly Jeu jeu;

        //Représente si la prochaine case et rouge ou jaune
        private Boolean isRouge;

        //Dictionnary contenant toutes les cases et toutes les ellipses
        //On lui passe une case, elle nous rend la case correspondant.
        private readonly Dictionary<Case, Ellipse> ellipses;

        /// <summary>
        /// Constructeur construisant un objet jeu et appelant l'autre constructeur
        /// </summary>
        public GameWindow() : this(new Jeu()){ }

        /// <summary>
        /// Constructeur prenant un jeu en parametre
        /// </summary>
        /// <param name="jeu">jeu de la fenetre</param>
        public GameWindow(Jeu jeu)
        {
            InitializeComponent();
            this.ellipses = new Dictionary<Case, Ellipse>();
            this.isRouge = true;
            this.jeu = jeu;
            AssocierCaseRond();
        }

        /// <summary>
        /// Methode permettant d'associer chaque case à son ellipse dans le dictionary
        /// </summary>
        private void AssocierCaseRond()
        {
            UIElementCollection children = grid.Children;   //Récupere tous les elements visuels.
            List<Ellipse> list = new List<Ellipse>();

            Ellipse b = new Ellipse();
            for (int j = 0; j < children.Count; j++)
            {
                if (children[j].GetType() == b.GetType())   //On ne garde que les objets de type Ellipse
                {
                    list.Add((Ellipse)children[j]);
                }
            }

            int i = 0;
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    ellipses.Add(jeu.GetCase(new Coordonnee(x, y)),list[i]); // les ajoutes au dictionary
                    i++;
                }
            }
        }

        /// <summary>
        /// Methode permettant de charger une ancienne partie.
        /// Elle ne doit être appelé que si la methode charger de la classe jeu, a déjà été appelé
        /// </summary>
        public void Charger()
        {
            Case @case;
            Ellipse ellipse;
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    @case = jeu.GetCase(new Coordonnee(x, y));
                    ellipse = GetEllipse(@case);
                    ColorEllipse(ellipse,@case.Etat);
                }
            }

            if(jeu.Etat == Etat.ROUGE)
            {
                isRouge = true;
                IndicateurCouleur.Fill = Brushes.Red;
            }
            else
            {
                isRouge = false;
                IndicateurCouleur.Fill = Brushes.Yellow;
            }
        }

        /// <summary>
        /// Methode permettant de cliquer sur une colonne pour y placer un pion
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Click_Collone(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;;
            int i = button.Name[^1] - 48;
            Case @case = jeu.MettreUnPion(i);
            Ellipse ellipse = GetEllipse(@case);
            if (ellipse != null)
            {
                ColorEllipse(ellipse, GetColor());
                Verify(jeu.VerifyWin(@case));
                SwitchCouleur();
            }
        }

        /// <summary>
        /// Methode pour colorier une case selon son etat
        /// </summary>
        /// <param name="ellipse">l'ellipse souhaité</param>
        /// <param name="etat">la couleur souhiaté</param>
        private void ColorEllipse(Ellipse ellipse, Etat etat)
        {
            SolidColorBrush brushes = null ;
            switch (etat)
            {
                case Etat.ROUGE:
                    brushes = Brushes.Red;
                    break;
                case Etat.JAUNE:
                    brushes = Brushes.Yellow;
                    break;
                case Etat.VIDE:
                    brushes = Brushes.White;
                    break;
                default:
                    break;
            }

            ellipse.Fill = brushes;
        }

        /// <summary>
        /// Methode permettant de renvoyer l'ellipse selon une case
        /// </summary>
        /// <param name="c">La case de l'ellipse</param>
        /// <returns>l'ellipse selon une case</returns>
        private Ellipse GetEllipse(Case c)
        {
            Ellipse ellipse = null;
            if (c != null)
            {
                ellipses.TryGetValue(c, out ellipse);
            }
            return ellipse;
        }

        /// <summary>
        /// Methode renvoyant la bonne couleur
        /// </summary>
        /// <returns>la bonne couleur</returns>
        private Etat GetColor()
        {
            Etat etat;
            if (isRouge)
            {
                etat = Etat.ROUGE;
            }
            else
            {
                etat = Etat.JAUNE;
            }
            return etat;
        }

        /// <summary>
        /// Methode permettant d'actualiser l'afficheur du prochain pion.
        /// et de switcher la couleur pour le prochain
        /// </summary>
        public void SwitchCouleur()
        {
            if (isRouge)
            {
                IndicateurCouleur.Fill = Brushes.Yellow;
                isRouge = false;
            }
            else
            {
                IndicateurCouleur.Fill = Brushes.Red;
                isRouge = true;
            }

        }

        /// <summary>
        /// Methode permettant de quitter le jeu
        /// Elle nous renvoie vers la MainWindow
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Quitter(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Methode permettant de sauvegarder le jeu
        /// </summary>
        /// <param name="sender">bouton qui vient d'être cliqué</param>
        /// <param name="e">event</param>
        private void Sauvegarder(object sender, RoutedEventArgs e)
        {
            if (jeu.Sauvegarde(stockage.DAOType.JVJ))
            {
                MessageBox.Show("La sauvegarde a bien été effectuée");
            }
            else
            {
                MessageBox.Show("Une erreur a eu lieu");
            }
        }

        /// <summary>
        /// Methode permettant d'afficher une fenetre si un joueur a gagné
        /// </summary>
        /// <param name="win">true si une victoire à eu lieu</param>
        private void Verify(bool win)
        {
            if (win)
            {
                WinWindow winWindow = new WinWindow(isRouge, this);
                winWindow.ShowDialog();
            }
        }
    }
}
