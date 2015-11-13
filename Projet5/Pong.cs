using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Projet5
{
    public delegate void PointEndedEventHandler(object sender, PointEndedEventArgs e);
    public delegate void GameEndedEventHandler(object sender, GameEndedEventArgs e);
    public delegate void SetEndedEventHandler(object sender, SetEndedEventArgs e);
    public delegate void MatchEndedEventHandler(object sender, MatchEndedEventArgs e);

    public class Pong
    {
        private TennisPongForm Parent { get; set; }
        public Rectangle Terrain { get; private set; }
        public Ligne Filet { get; private set; }
        public Rectangle Joueur1 { get; private set; }
        public Rectangle Joueur2 { get; private set; }
        public Cercle Balle { get; private set; }
        public Texte ScoreTexteJoueur1 { get; private set; }
        public Texte ScoreTexteJoueur2 { get; private set; }
        public Boolean ServiceJoueur1 { get; set; }
        public Boolean ServiceJoueur2 { get; set; }
        public Boolean AllerBalle { get; set; }
        public Boolean Partie { get; set; }
        public int AngleBalle { get; set; }
        public int AngleJoueur1 { get; set; }
        public int AngleJoueur2 { get; set; }
        public System.Timers.Timer TimerBalle { get; private set; }
        public Score ScoreJoueur1 { get; set; }
        public Score ScoreJoueur2 { get; set; }
        public Jeu Jeu { get; set; }
        public Set Set { get; set; }
        public Match Match { get; set; }
        public SoundPlayer SoundPlayer { get; private set; }
        public bool VibrationsJoueur1 { get; set; }
        public bool VibrationsJoueur2 { get; set; }
        public bool Sons { get; set; }
        public event PointEndedEventHandler OnPointEnded;
        public event GameEndedEventHandler OnGameEnded;
        public event SetEndedEventHandler OnSetEnded;
        public event MatchEndedEventHandler onMatchEnded;
        public int UpButtonPressedJoueur1 { get; set; }
        public int UpButtonPressedJoueur2 { get; set; }
        public int DownButtonPressedJoueur1 { get; set; }
        public int DownButtonPressedJoueur2 { get; set; }

        public Pong(TennisPongForm parent)
        {
            this.Parent = parent;
            this.ServiceJoueur1 = true;
            this.ServiceJoueur2 = false;
            this.AllerBalle = true;
            this.Partie = false;
            this.AngleBalle = 0;
            this.AngleJoueur1 = 0;
            this.AngleJoueur2 = 0;
            this.TimerBalle = new System.Timers.Timer();
            this.TimerBalle.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
            this.TimerBalle.Interval = 1;
            this.TimerBalle.Enabled = false;
            this.ScoreJoueur1 = new Score();
            this.ScoreJoueur2 = new Score();
            this.Jeu = new Jeu();
            this.Set = new Set();
            this.Match = new Match();
            this.SoundPlayer = new SoundPlayer();
            this.VibrationsJoueur1 = false;
            this.VibrationsJoueur2 = false;
            this.Sons = false;
            this.OnPointEnded += new PointEndedEventHandler(this.Pong_OnPointEnded);
            this.OnGameEnded += new GameEndedEventHandler(this.Pong_OnGameEnded);
            this.OnSetEnded += new SetEndedEventHandler(this.Pong_OnSetEnded);
            this.onMatchEnded += new MatchEndedEventHandler(Pong_onMatchEnded);
            this.UpButtonPressedJoueur1 = 0;
            this.UpButtonPressedJoueur2 = 0;
            this.DownButtonPressedJoueur1 = 0;
            this.DownButtonPressedJoueur2 = 0;
            this.CreateEnvironment();
        }

        internal void CreateEnvironment()
        {
            this.Terrain = new Rectangle(this.Parent.PictureBox1.Width - 10, this.Parent.PictureBox1.Height - 10,
                new Point(this.Parent.PictureBox1.Width / 2, this.Parent.PictureBox1.Height / 2), Color.White, new Point(0, 0), 2, Color.OrangeRed);
            this.Filet = new Ligne(this.Parent.PictureBox1.Height - 20, new Point(this.Parent.PictureBox1.Width / 2, 10),
                Color.White, new Point(this.Parent.PictureBox1.Width / 2, this.Parent.PictureBox1.Height - 10), 2);
            this.Joueur1 = new Rectangle(5, 50, new Point(15, this.Parent.PictureBox1.Height / 2), Color.White, new Point(0, 0), 1, Color.White);
            this.Joueur2 = new Rectangle(5, 50, new Point(this.Parent.PictureBox1.Width - 15, this.Parent.PictureBox1.Height / 2),
                Color.White, new Point(0, 0), 1, Color.White);
            this.Balle = new Cercle(5, new Point(this.Joueur1.Centre.X + 10, this.Joueur1.Centre.Y), Color.Yellow, 1, Color.Yellow);
            this.ScoreTexteJoueur1 = new Texte(this.ScoreJoueur1.Point.ToString() + " - " + this.Jeu.Score1.Point.ToString() + " - " +
                this.Set.Score1.Point.ToString() + " - " + this.Match.Score1.Point.ToString(), new Point(50, 25));
            this.ScoreTexteJoueur2 = new Texte(this.ScoreJoueur2.Point.ToString() + " - " + this.Jeu.Score2.Point.ToString() + " - " +
                this.Set.Score2.Point.ToString() + " - " + this.Match.Score2.Point.ToString(), new Point(this.Parent.PictureBox1.Width - 200, 25));
            this.Parent.PictureBox1.Invalidate();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (this.AllerBalle)
            {
                if (this.Balle.Centre.X < this.Parent.PictureBox1.Width - 15)
                {
                    this.Balle.Centre = new Point(this.Balle.Centre.X + this.Balle.Rayon, this.Balle.Centre.Y + this.AngleBalle);
                }
                else
                {
                    if (this.Balle.Centre.X == this.Joueur2.Centre.X)
                    {
                        if ((this.Balle.Centre.Y >= this.Joueur2.Centre.Y - (this.Joueur2.Largeur / 2)) &&
                            (this.Balle.Centre.Y <= this.Joueur2.Centre.Y + (this.Joueur2.Largeur / 2)))
                        {
                            this.AngleBalle += this.AngleJoueur2;
                            this.Balle.Centre = new Point(this.Balle.Centre.X - this.Balle.Rayon, this.Balle.Centre.Y + this.AngleBalle);
                            this.AllerBalle = false;
                            if (this.VibrationsJoueur2)
                            {
                                if (this.Parent.ManetteController.Manette2Controller.IsAlive)
                                {
                                    this.Parent.ManetteController.Manette2.Vibrate(7500, 7500);
                                    Thread.Sleep(150);
                                    this.Parent.ManetteController.Manette2.Vibrate(0, 0);
                                }
                            }
                            if (this.Sons)
                            {
                                this.SoundPlayer.SoundLocation = @"Resources/4359__NoiseCollector__PongBlipF4.wav";
                                this.SoundPlayer.Play();
                            }
                        }
                        else
                        {
                            this.ServiceJoueur1 = true;
                            this.ServiceJoueur2 = false;
                            this.AllerBalle = true;
                            this.AngleBalle = 0;
                            this.AngleJoueur1 = 0;
                            this.AngleJoueur2 = 0;
                            this.TimerBalle.Enabled = false;
                            this.TimerBalle.Stop();
                            this.Balle.Centre = new Point(this.Joueur1.Centre.X + 10, this.Joueur1.Centre.Y);
                            PointEndedEventArgs peea = new PointEndedEventArgs(1);
                            this.OnPointEnded(this, peea);
                        }
                        this.UpButtonPressedJoueur1 = 0;
                        this.DownButtonPressedJoueur1 = 0;
                    }
                }
            }
            else
            {
                if (this.Balle.Centre.X > 15)
                {
                    this.Balle.Centre = new Point(this.Balle.Centre.X - this.Balle.Rayon, this.Balle.Centre.Y + this.AngleBalle);
                }
                else
                {
                    if ((this.Balle.Centre.Y >= this.Joueur1.Centre.Y - (this.Joueur1.Largeur / 2)) &&
                        (this.Balle.Centre.Y <= this.Joueur1.Centre.Y + (this.Joueur1.Largeur / 2)))
                    {
                        this.AngleBalle += this.AngleJoueur1;
                        this.Balle.Centre = new Point(this.Balle.Centre.X + this.Balle.Rayon, this.Balle.Centre.Y + this.AngleBalle);
                        this.AllerBalle = true;
                        if (this.VibrationsJoueur1)
                        {
                            if (this.Parent.ManetteController.Manette1Controller.IsAlive)
                            {
                                this.Parent.ManetteController.Manette1.Vibrate(7500, 7500);
                                Thread.Sleep(150);
                                this.Parent.ManetteController.Manette1.Vibrate(0, 0);
                            }
                        }
                        if (this.Sons)
                        {
                            this.SoundPlayer.SoundLocation = @"Resources/4360__NoiseCollector__ponblipG_5.wav";
                            this.SoundPlayer.Play();
                        }
                    }
                    else
                    {
                        this.ServiceJoueur1 = false;
                        this.ServiceJoueur2 = true;
                        this.AllerBalle = false;
                        this.AngleBalle = 0;
                        this.AngleJoueur1 = 0;
                        this.AngleJoueur2 = 0;
                        this.TimerBalle.Enabled = false;
                        this.TimerBalle.Stop();
                        this.Balle.Centre = new Point(this.Joueur2.Centre.X - 10, this.Joueur2.Centre.Y);
                        PointEndedEventArgs peea = new PointEndedEventArgs(2);
                        this.OnPointEnded(this, peea);
                    }
                    this.UpButtonPressedJoueur2 = 0;
                    this.DownButtonPressedJoueur2 = 0;
                }
            }
            if (this.Balle.Centre.Y <= 10)
                this.AngleBalle += this.Balle.Rayon / 2;
            if (this.Balle.Centre.Y >= this.Parent.PictureBox1.Height - 10)
                this.AngleBalle -= this.Balle.Rayon / 2;
            this.Parent.PictureBox1.Invalidate();
        }

        private void Pong_OnPointEnded(object sender, PointEndedEventArgs e)
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
            this.ScoreTexteJoueur1.Texto = this.ScoreJoueur1.Point.ToString() + " - " +
                this.Jeu.Score1.Point.ToString() + " - " + this.Set.Score1.Point.ToString() + " - " +
                this.Match.Score1.Point.ToString();
            this.ScoreTexteJoueur2.Texto = this.ScoreJoueur2.Point.ToString() + " - " +
                this.Jeu.Score2.Point.ToString() + " - " + this.Set.Score2.Point.ToString() + " - " +
                this.Match.Score2.Point.ToString();
        }

        private void Pong_OnGameEnded(object sender, GameEndedEventArgs e)
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
            this.ScoreTexteJoueur1.Texto = this.ScoreJoueur1.Point.ToString() + " - " +
                this.Jeu.Score1.Point.ToString() + " - " + this.Set.Score1.Point.ToString() + " - " +
                this.Match.Score1.Point.ToString();
            this.ScoreTexteJoueur2.Texto = this.ScoreJoueur2.Point.ToString() + " - " +
                this.Jeu.Score2.Point.ToString() + " - " + this.Set.Score2.Point.ToString() + " - " +
                this.Match.Score2.Point.ToString();
        }

        private void Pong_OnSetEnded(object sender, SetEndedEventArgs e)
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
            this.ScoreTexteJoueur1.Texto = this.ScoreJoueur1.Point.ToString() + " - " +
                this.Jeu.Score1.Point.ToString() + " - " + this.Set.Score1.Point.ToString() + " - " +
                this.Match.Score1.Point.ToString();
            this.ScoreTexteJoueur2.Texto = this.ScoreJoueur2.Point.ToString() + " - " +
                this.Jeu.Score2.Point.ToString() + " - " + this.Set.Score2.Point.ToString() + " - " +
                this.Match.Score2.Point.ToString();
        }

        private void Pong_onMatchEnded(object sender, MatchEndedEventArgs e)
        {
            if (e.Match.Score1.Point > e.Match.Score2.Point)
                new WinForm("Le Joueur 1 a gagné !").ShowDialog();
            else if (e.Match.Score1.Point < e.Match.Score2.Point)
                new WinForm("Le Joueur 2 a gagné !").ShowDialog();
            else
                new WinForm("Egalite !").ShowDialog();
            this.Partie = false;
            this.TimerBalle.Stop();
            this.TimerBalle.Close();
            this.ScoreJoueur1 = new Score();
            this.ScoreJoueur2 = new Score();
            this.Jeu = new Jeu();
            this.Set = new Set();
            this.Match = new Match();
            this.CreateEnvironment();
            this.Parent.NouvellePartieToolStripMenuItem.Enabled = true;
            this.Parent.TerminerLaPartieToolStripMenuItem.Enabled = false;
            this.Parent.JoueursToolStripMenuItem.Enabled = true;
            this.Parent.DifficultéToolStripMenuItem.Enabled = true;
            if (!this.Parent.ManetteController.ManetteController.IsAlive)
            {
                this.Parent.ManetteController.ManetteController = new Thread(this.Parent.ManetteController.ManetteThread);
                this.Parent.ManetteController.ManetteController.Start();
            }
            if ((this.Parent.ClavierToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.Parent.ClavierToolStripMenuItem1.CheckState == CheckState.Checked))
                this.Parent.KeyDown -= new KeyEventHandler(this.Parent.pictureBox1_KeyDown);
            if ((this.Parent.SourisToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.Parent.SourisToolStripMenuItem1.CheckState == CheckState.Checked))
            {
                this.Parent.PictureBox1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.Parent.pictureBox1_MouseClick);
                this.Parent.PictureBox1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.Parent.pictureBox1_MouseMove);
            }
            if (this.Parent.ManetteController.Manette1Controller.IsAlive)
                this.Parent.ManetteController.Manette1Controller.Abort();
            if (this.Parent.ManetteController.Manette2Controller.IsAlive)
                this.Parent.ManetteController.Manette2Controller.Abort();
            if (this.Parent.AiController.Ai1Controller.IsAlive)
                this.Parent.AiController.Ai1Controller.Abort();
            if (this.Parent.AiController.Ai2Controller.IsAlive)
                this.Parent.AiController.Ai2Controller.Abort();
        }
    }
}