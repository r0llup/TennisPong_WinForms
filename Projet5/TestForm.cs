using System;
using System.Windows.Forms;

namespace Projet5
{
    public partial class Form2 : Form
    {
        public Score ScoreJoueur1 { get; private set; }
        public Score ScoreJoueur2 { get; private set; }
        public Jeu Jeu { get; private set; }
        public Set Set { get; private set; }
        public Match Match { get; private set; }
        public event PointEndedEventHandler OnPointEnded;
        public event GameEndedEventHandler OnGameEnded;
        public event SetEndedEventHandler OnSetEnded;
        public event MatchEndedEventHandler onMatchEnded;

        public Form2()
        {
            InitializeComponent();
            this.ScoreJoueur1 = new Score();
            this.ScoreJoueur2 = new Score();
            this.Jeu = new Jeu();
            this.Set = new Set();
            this.Match = new Match();
            this.OnPointEnded += new PointEndedEventHandler(this.Form2_OnPointEnded);
            this.OnGameEnded += new GameEndedEventHandler(this.Form2_OnGameEnded);
            this.OnSetEnded += new SetEndedEventHandler(this.Form2_OnSetEnded);
            this.onMatchEnded += new MatchEndedEventHandler(this.Form2_onMatchEnded);
            this.scoreJoueur1ToolBox.Text = this.ScoreJoueur1.Point.ToString();
            this.jeuJoueur1ToolBox.Text = this.Jeu.Score1.Point.ToString();
            this.setJoueur1ToolBox.Text = this.Set.Score1.Point.ToString();
            this.matchJoueur1ToolBox.Text = this.Match.Score1.Point.ToString();
            this.scoreJoueur2ToolBox.Text = this.ScoreJoueur2.Point.ToString();
            this.jeuJoueur2ToolBox.Text = this.Jeu.Score2.Point.ToString();
            this.setJoueur2ToolBox.Text = this.Set.Score2.Point.ToString();
            this.matchJoueur2ToolBox.Text = this.Match.Score2.Point.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointEndedEventArgs peea = new PointEndedEventArgs(1);
            this.OnPointEnded(this, peea);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointEndedEventArgs peea = new PointEndedEventArgs(2);
            this.OnPointEnded(this, peea);
        }

        private void Form2_OnPointEnded(object sender, PointEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                this.ScoreJoueur1.Point++;
                switch (this.ScoreJoueur1.Point)
                {
                    case 0:
                        this.Jeu.Score1.Point = 0;
                        break;
                    case 1:
                        this.Jeu.Score1.Point = 15;
                        break;
                    case 2:
                        this.Jeu.Score1.Point = 30;
                        break;
                    case 3:
                        this.Jeu.Score1.Point = 40;
                        break;
                    case 4:
                        this.ScoreJoueur1 = new Score();
                        GameEndedEventArgs geea = new GameEndedEventArgs(1, this.Jeu);
                        this.OnGameEnded(this, geea);
                        this.Jeu.Score1.Point = 0;
                        break;
                }
            }
            else
            {
                this.ScoreJoueur2.Point++;
                switch (this.ScoreJoueur2.Point)
                {
                    case 0:
                        this.Jeu.Score2.Point = 0;
                        break;
                    case 1:
                        this.Jeu.Score2.Point = 15;
                        break;
                    case 2:
                        this.Jeu.Score2.Point = 30;
                        break;
                    case 3:
                        this.Jeu.Score2.Point = 40;
                        break;
                    case 4:
                        this.ScoreJoueur2 = new Score();
                        GameEndedEventArgs geea = new GameEndedEventArgs(2, this.Jeu);
                        this.OnGameEnded(this, geea);
                        this.Jeu.Score2.Point = 0;
                        break;
                }
            }
            this.scoreJoueur1ToolBox.Text = this.ScoreJoueur1.Point.ToString();
            this.jeuJoueur1ToolBox.Text = this.Jeu.Score1.Point.ToString();
            this.scoreJoueur2ToolBox.Text = this.ScoreJoueur2.Point.ToString();
            this.jeuJoueur2ToolBox.Text = this.Jeu.Score2.Point.ToString();
        }

        private void Form2_OnGameEnded(object sender, GameEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                if ((this.Set.Score1.Point == 6) && (this.Set.Score1.Point - 2 >= this.Set.Score2.Point))
                {
                    SetEndedEventArgs seea = new SetEndedEventArgs(1, this.Set);
                    this.OnSetEnded(this, seea);
                    this.Set.Score1.Point = 0;
                }
                else
                {
                    this.Set.ListeJeu.AddLast(new Jeu(e.Jeu));
                    if (e.Jeu.Score1.Point > e.Jeu.Score2.Point)
                        this.Set.Score1.Point++;
                    else if (e.Jeu.Score1.Point < e.Jeu.Score2.Point)
                        this.Set.Score2.Point++;
                    else
                        this.Set.Score1.Point++;
                }
            }
            else
            {
                if ((this.Set.Score2.Point == 6) && (this.Set.Score2.Point - 2 >= this.Set.Score1.Point))
                {
                    SetEndedEventArgs seea = new SetEndedEventArgs(2, this.Set);
                    this.OnSetEnded(this, seea);
                    this.Set.Score2.Point = 0;
                }
                else
                {
                    this.Set.ListeJeu.AddLast(new Jeu(e.Jeu));
                    if (e.Jeu.Score1.Point > e.Jeu.Score2.Point)
                        this.Set.Score1.Point++;
                    else if (e.Jeu.Score1.Point < e.Jeu.Score2.Point)
                        this.Set.Score2.Point++;
                    else
                        this.Set.Score2.Point++;
                }
            }
            this.setJoueur1ToolBox.Text = this.Set.Score1.Point.ToString();
            this.setJoueur2ToolBox.Text = this.Set.Score2.Point.ToString();
        }

        private void Form2_OnSetEnded(object sender, SetEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                if (this.Match.Score1.Point == 3)
                {
                    MatchEndedEventArgs meea = new MatchEndedEventArgs(this.Match);
                    this.onMatchEnded(this, meea);
                    this.Match.Score1.Point = 0;
                }
                else
                {
                    this.Match.ListeSet.AddLast(new Set(e.Set));
                    if (e.Set.Score1.Point > e.Set.Score2.Point)
                        this.Match.Score1.Point++;
                    else if (e.Set.Score1.Point < e.Set.Score2.Point)
                        this.Match.Score2.Point++;
                    else
                        this.Match.Score1.Point++;
                }
            }
            else
            {
                if (this.Match.Score2.Point == 3)
                {
                    MatchEndedEventArgs meea = new MatchEndedEventArgs(this.Match);
                    this.onMatchEnded(this, meea);
                    this.Match.Score2.Point = 0;
                }
                else
                {
                    this.Match.ListeSet.AddLast(new Set(e.Set));
                    if (e.Set.Score1.Point > e.Set.Score2.Point)
                        this.Match.Score1.Point++;
                    else if (e.Set.Score1.Point < e.Set.Score2.Point)
                        this.Match.Score2.Point++;
                    else
                        this.Match.Score2.Point++;
                }
            }
            this.matchJoueur1ToolBox.Text = this.Match.Score1.Point.ToString();
            this.matchJoueur2ToolBox.Text = this.Match.Score2.Point.ToString();
        }

        void Form2_onMatchEnded(object sender, MatchEndedEventArgs e)
        {
            if (e.Match.Score1.Point > e.Match.Score2.Point)
                new WinForm("Le Joueur 1 a gagné !").ShowDialog();
            else if (e.Match.Score1.Point < e.Match.Score2.Point)
                new WinForm("Le Joueur 2 a gagné !").ShowDialog();
            else
                new WinForm("Egalite !").ShowDialog();
        }
    }
}