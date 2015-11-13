using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Projet5
{
    public partial class TennisPongForm : Form
    {
        public Pong PongGame { get; private set; }
        public Manette ManetteController { get; private set; }
        public Ai AiController { get; private set; }
        internal PictureBox PictureBox1 { get { return this.pictureBox1; } }
        internal ToolStripMenuItem NouvellePartieToolStripMenuItem { get { return this.nouvellePartieToolStripMenuItem; } }
        internal ToolStripMenuItem TerminerLaPartieToolStripMenuItem { get { return this.terminerLaPartieToolStripMenuItem; } }
        internal ToolStripMenuItem JoueursToolStripMenuItem { get { return this.joueursToolStripMenuItem; } }
        internal ToolStripMenuItem ClavierToolStripMenuItem { get { return this.clavierToolStripMenuItem; } }
        internal ToolStripMenuItem ClavierToolStripMenuItem1 { get { return this.clavierToolStripMenuItem1; } }
        internal ToolStripMenuItem SourisToolStripMenuItem { get { return this.sourisToolStripMenuItem; } }
        internal ToolStripMenuItem SourisToolStripMenuItem1 { get { return this.sourisToolStripMenuItem1; } }
        internal ToolStripMenuItem ManetteToolStripMenuItem { get { return this.manetteToolStripMenuItem; } }
        internal ToolStripMenuItem ManetteToolStripMenuItem1 { get { return this.manetteToolStripMenuItem1; } }
        internal ToolStripMenuItem DifficultéToolStripMenuItem { get { return this.difficileToolStripMenuItem; } }

        public TennisPongForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false; /*FIXME*/
            InitializeComponent();
            // Double Buffering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.PongGame = new Pong(this);
            this.ManetteController = new Manette(this);
            this.AiController = new Ai(this);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.PongGame.Terrain.Draw(e.Graphics);
            this.PongGame.Filet.Draw(e.Graphics);
            this.PongGame.Joueur1.Draw(e.Graphics);
            this.PongGame.Joueur2.Draw(e.Graphics);
            this.PongGame.Balle.Draw(e.Graphics);
            this.PongGame.ScoreTexteJoueur1.Draw(e.Graphics);
            this.PongGame.ScoreTexteJoueur2.Draw(e.Graphics);
        }

        internal void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
            {
                if ((e.Location.Y > 25) && (e.Location.Y < this.PictureBox1.Height - 25))
                {
                    this.PongGame.Joueur1.Centre = new Point(this.PongGame.Joueur1.Centre.X, e.Location.Y);
                    if (this.PongGame.ServiceJoueur1)
                        this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, e.Location.Y);
                }
            }
            if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                if ((e.Location.Y > 25) && (e.Location.Y < this.PictureBox1.Height - 25))
                {
                    this.PongGame.Joueur2.Centre = new Point(this.PongGame.Joueur2.Centre.X, e.Location.Y);
                    if (this.PongGame.ServiceJoueur2)
                        this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, e.Location.Y);
                }
            }
            this.PictureBox1.Invalidate();
        }

        internal void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.UpButtonPressedJoueur1 < 3)
                    {
                        this.PongGame.AngleJoueur1 -= 1;
                        this.PongGame.UpButtonPressedJoueur1++;
                    }
                }
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.UpButtonPressedJoueur2 < 3)
                    {
                        this.PongGame.AngleJoueur2 -= 1;
                        this.PongGame.UpButtonPressedJoueur2++;
                    }
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.ServiceJoueur1)
                    {
                        this.PongGame.AngleBalle += this.PongGame.AngleJoueur1;
                        this.PongGame.ServiceJoueur1 = false;
                        this.PongGame.TimerBalle.Enabled = true;
                        this.PongGame.TimerBalle.Start();
                    }
                }
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.ServiceJoueur2)
                    {
                        this.PongGame.AngleBalle += this.PongGame.AngleJoueur2;
                        this.PongGame.ServiceJoueur2 = false;
                        this.PongGame.TimerBalle.Enabled = true;
                        this.PongGame.TimerBalle.Start();
                    }
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.DownButtonPressedJoueur1 < 3)
                    {
                        this.PongGame.AngleJoueur1 += 1;
                        this.PongGame.DownButtonPressedJoueur1++;
                    }
                }
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                {
                    if (this.PongGame.DownButtonPressedJoueur2 < 3)
                    {
                        this.PongGame.AngleJoueur2 += 1;
                        this.PongGame.DownButtonPressedJoueur2++;
                    }
                }
            }
        }

        internal void pictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        e.Handled = true;
                        if (this.PongGame.Joueur1.Centre.Y > 25)
                        {
                            this.PongGame.Joueur1.Centre = new Point(this.PongGame.Joueur1.Centre.X, this.PongGame.Joueur1.Centre.Y - 25);
                            if (this.PongGame.ServiceJoueur1)
                                this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, this.PongGame.Balle.Centre.Y - 25);
                            this.PictureBox1.Invalidate();
                        }
                        break;
                    case Keys.Down:
                        e.Handled = true;
                        if (this.PongGame.Joueur1.Centre.Y < this.pictureBox1.Height - 25)
                        {
                            this.PongGame.Joueur1.Centre = new Point(this.PongGame.Joueur1.Centre.X, this.PongGame.Joueur1.Centre.Y + 25);
                            if (this.PongGame.ServiceJoueur1)
                                this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, this.PongGame.Balle.Centre.Y + 25);
                            this.PictureBox1.Invalidate();
                        }
                        break;
                    case Keys.Space:
                        e.Handled = true;
                        if (this.PongGame.ServiceJoueur1)
                        {
                            this.PongGame.AngleBalle += this.PongGame.AngleJoueur1;
                            this.PongGame.ServiceJoueur1 = false;
                            this.PongGame.TimerBalle.Enabled = true;
                            this.PongGame.TimerBalle.Start();
                        }
                        break;
                    case Keys.A:
                        e.Handled = true;
                        if (this.PongGame.UpButtonPressedJoueur1 < 3)
                        {
                            this.PongGame.AngleJoueur1 -= 1;
                            this.PongGame.UpButtonPressedJoueur1++;
                        }
                        break;
                    case Keys.Q:
                        e.Handled = true;
                        if (this.PongGame.DownButtonPressedJoueur1 < 3)
                        {
                            this.PongGame.AngleJoueur1 += 1;
                            this.PongGame.DownButtonPressedJoueur1++;
                        }
                        break;
                }
            }
            if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        e.Handled = true;
                        if (this.PongGame.Joueur2.Centre.Y > 25)
                        {
                            this.PongGame.Joueur2.Centre = new Point(this.PongGame.Joueur2.Centre.X, this.PongGame.Joueur2.Centre.Y - 25);
                            if (this.PongGame.ServiceJoueur2)
                                this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, this.PongGame.Balle.Centre.Y - 25);
                            this.PictureBox1.Invalidate();
                        }
                        break;
                    case Keys.Down:
                        e.Handled = true;
                        if (this.PongGame.Joueur2.Centre.Y < this.pictureBox1.Height - 25)
                        {
                            this.PongGame.Joueur2.Centre = new Point(this.PongGame.Joueur2.Centre.X, this.PongGame.Joueur2.Centre.Y + 25);
                            if (this.PongGame.ServiceJoueur2)
                                this.PongGame.Balle.Centre = new Point(this.PongGame.Balle.Centre.X, this.PongGame.Balle.Centre.Y + 25);
                            this.PictureBox1.Invalidate();
                        }
                        break;
                    case Keys.Space:
                        e.Handled = true;
                        if (this.PongGame.ServiceJoueur2)
                        {
                            this.PongGame.AngleBalle += this.PongGame.AngleJoueur2;
                            this.PongGame.ServiceJoueur2 = false;
                            this.PongGame.TimerBalle.Enabled = true;
                            this.PongGame.TimerBalle.Start();
                        }
                        break;
                    case Keys.A:
                        e.Handled = true;
                        if (this.PongGame.UpButtonPressedJoueur2 < 3)
                        {
                            this.PongGame.AngleJoueur2 -= 1;
                            this.PongGame.UpButtonPressedJoueur2++;
                        }
                        break;
                    case Keys.Q:
                        e.Handled = true;
                        if (this.PongGame.DownButtonPressedJoueur2 < 3)
                        {
                            this.PongGame.AngleJoueur2 += 1;
                            this.PongGame.DownButtonPressedJoueur2++;
                        }
                        break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ManetteController.ManetteController.IsAlive)
                this.ManetteController.ManetteController.Abort();
            if (this.ManetteController.Manette1Controller.IsAlive)
                this.ManetteController.Manette1Controller.Abort();
            if (this.ManetteController.Manette2Controller.IsAlive)
                this.ManetteController.Manette2Controller.Abort();
            if (this.AiController.Ai1Controller.IsAlive)
                this.AiController.Ai1Controller.Abort();
            if (this.AiController.Ai2Controller.IsAlive)
                this.AiController.Ai2Controller.Abort();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clavierToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.clavierToolStripMenuItem1.Enabled = false;
                if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.clavierToolStripMenuItem1.Enabled = true;
            }
        }

        private void clavierToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.clavierToolStripMenuItem.Enabled = false;
                if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.clavierToolStripMenuItem.Enabled = true;
            }
        }

        private void sourisToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.sourisToolStripMenuItem1.Enabled = false;
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.sourisToolStripMenuItem1.Enabled = true;
            }
        }

        private void sourisToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.sourisToolStripMenuItem.Enabled = false;
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.sourisToolStripMenuItem.Enabled = true;
            }
        }

        private void manetteToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.manetteToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.vibrationsManette1ToolStripMenuItem.Enabled = true;
                if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
                this.vibrationsManette1ToolStripMenuItem.Enabled = false;
        }

        private void manetteToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.manetteToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.vibrationsManette2ToolStripMenuItem.Enabled = true;
                if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.intelligenceArtificielleToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            else
                this.vibrationsManette2ToolStripMenuItem.Enabled = false;
        }

        private void intelligenceArtificielleToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.difficultéToolStripMenuItem.Enabled = true;
                if (this.clavierToolStripMenuItem.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Unchecked)
                    this.difficultéToolStripMenuItem.Enabled = false;
            }
        }

        private void intelligenceArtificielleToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.difficultéToolStripMenuItem.Enabled = true;
                if (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.clavierToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.sourisToolStripMenuItem1.CheckState = CheckState.Unchecked;
                if (this.manetteToolStripMenuItem1.CheckState == CheckState.Checked)
                    this.manetteToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            else
            {
                if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Unchecked)
                    this.difficultéToolStripMenuItem.Enabled = false;
            }
        }

        private void facileToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.facileToolStripMenuItem.CheckState == CheckState.Checked)
            {
                if (this.moyenToolStripMenuItem.CheckState == CheckState.Checked)
                    this.moyenToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.difficileToolStripMenuItem.CheckState == CheckState.Checked)
                    this.difficileToolStripMenuItem.CheckState = CheckState.Unchecked;
                this.AiController.AiSkill = 150; // ms
            }
        }

        private void moyenToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.moyenToolStripMenuItem.CheckState == CheckState.Checked)
            {
                if (this.facileToolStripMenuItem.CheckState == CheckState.Checked)
                    this.facileToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.difficileToolStripMenuItem.CheckState == CheckState.Checked)
                    this.difficileToolStripMenuItem.CheckState = CheckState.Unchecked;
                this.AiController.AiSkill = 75; // ms
            }
        }

        private void difficileToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.difficileToolStripMenuItem.CheckState == CheckState.Checked)
            {
                if (this.facileToolStripMenuItem.CheckState == CheckState.Checked)
                    this.facileToolStripMenuItem.CheckState = CheckState.Unchecked;
                if (this.moyenToolStripMenuItem.CheckState == CheckState.Checked)
                    this.moyenToolStripMenuItem.CheckState = CheckState.Unchecked;
                this.AiController.AiSkill = 38; // ms
            }
        }

        private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nouvellePartieToolStripMenuItem.Enabled = false;
            this.terminerLaPartieToolStripMenuItem.Enabled = true;
            this.joueursToolStripMenuItem.Enabled = false;
            this.difficultéToolStripMenuItem.Enabled = false;
            this.PongGame.Partie = true;
            if (new Random().Next() % 2 == 0)
            {
                this.PongGame.ServiceJoueur1 = true;
                this.PongGame.ServiceJoueur2 = false;
                this.PongGame.AllerBalle = true;
                this.PongGame.Balle.Centre = new Point(this.PongGame.Joueur1.Centre.X + 10, this.PongGame.Joueur1.Centre.Y);
            }
            else
            {
                this.PongGame.ServiceJoueur1 = false;
                this.PongGame.ServiceJoueur2 = true;
                this.PongGame.AllerBalle = false;
                this.PongGame.Balle.Centre = new Point(this.PongGame.Joueur2.Centre.X - 10, this.PongGame.Joueur2.Centre.Y);
            }
            this.PictureBox1.Invalidate();
            if (this.ManetteController.ManetteController.IsAlive)
                this.ManetteController.ManetteController.Abort();
            if ((this.clavierToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked))
                this.KeyDown += new KeyEventHandler(this.pictureBox1_KeyDown);
            if ((this.sourisToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked))
            {
                this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
                this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            }
            if (this.manetteToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.ManetteController.Manette1Controller = new Thread(this.ManetteController.Manette1Thread);
                this.ManetteController.Manette1Controller.Start();
            }
            if (this.manetteToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.ManetteController.Manette2Controller = new Thread(this.ManetteController.Manette2Thread);
                this.ManetteController.Manette2Controller.Start();
            }
            if (this.intelligenceArtificielleToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.AiController.Ai1Controller = new Thread(this.AiController.Ai1Thread);
                this.AiController.Ai1Controller.Start();
            }
            if (this.intelligenceArtificielleToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                this.AiController.Ai2Controller = new Thread(this.AiController.Ai2Thread);
                this.AiController.Ai2Controller.Start();
            }
        }

        private void terminerLaPartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nouvellePartieToolStripMenuItem.Enabled = true;
            this.terminerLaPartieToolStripMenuItem.Enabled = false;
            this.joueursToolStripMenuItem.Enabled = true;
            this.difficultéToolStripMenuItem.Enabled = true;
            this.PongGame.Partie = false;
            this.PongGame.TimerBalle.Stop();
            this.PongGame.TimerBalle.Close();
            this.PongGame.ScoreJoueur1 = new Score();
            this.PongGame.ScoreJoueur2 = new Score();
            this.PongGame.Jeu = new Jeu();
            this.PongGame.Set = new Set();
            this.PongGame.Match = new Match();
            this.PongGame.CreateEnvironment();
            if (!this.ManetteController.ManetteController.IsAlive)
            {
                this.ManetteController.ManetteController = new Thread(this.ManetteController.ManetteThread);
                this.ManetteController.ManetteController.Start();
            }
            if ((this.clavierToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.clavierToolStripMenuItem1.CheckState == CheckState.Checked))
                this.KeyDown -= new KeyEventHandler(this.pictureBox1_KeyDown);
            if ((this.sourisToolStripMenuItem.CheckState == CheckState.Checked) ||
                (this.sourisToolStripMenuItem1.CheckState == CheckState.Checked))
            {
                this.pictureBox1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
                this.pictureBox1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            }
            if (this.ManetteController.Manette1Controller.IsAlive)
                this.ManetteController.Manette1Controller.Abort();
            if (this.ManetteController.Manette2Controller.IsAlive)
                this.ManetteController.Manette2Controller.Abort();
            if (this.AiController.Ai1Controller.IsAlive)
                this.AiController.Ai1Controller.Abort();
            if (this.AiController.Ai2Controller.IsAlive)
                this.AiController.Ai2Controller.Abort();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void vibrationsManette1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.vibrationsManette1ToolStripMenuItem.CheckState == CheckState.Checked)
                this.PongGame.VibrationsJoueur1 = true;
            else
                this.PongGame.VibrationsJoueur1 = false;
        }

        private void vibrationsManette2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.vibrationsManette2ToolStripMenuItem.CheckState == CheckState.Checked)
                this.PongGame.VibrationsJoueur2 = true;
            else
                this.PongGame.VibrationsJoueur2 = false;
        }

        private void sonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sonsToolStripMenuItem.CheckState == CheckState.Checked)
                this.PongGame.Sons = true;
            else
                this.PongGame.Sons = false;
        }

        private void aProposDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}