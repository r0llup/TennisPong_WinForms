using System.Drawing;
using System.Threading;
using SlimDX.XInput;

namespace Projet5
{
    public class Manette
    {
        private TennisPongForm Parent { get; set; }
        public GamepadState Manette1 { get; private set; }
        public GamepadState Manette2 { get; private set; }
        public Thread ManetteController { get; set; }
        public Thread Manette1Controller { get; set; }
        public Thread Manette2Controller { get; set; }

        public Manette(TennisPongForm parent)
        {
            this.Parent = parent;
            this.Manette1 = new GamepadState(UserIndex.One);
            this.Manette2 = new GamepadState(UserIndex.Two);
            this.ManetteController = new Thread(this.ManetteThread);
            this.ManetteController.Priority = ThreadPriority.Lowest;
            this.Manette1Controller = new Thread(this.Manette1Thread);
            this.Manette1Controller.Priority = ThreadPriority.BelowNormal;
            this.Manette2Controller = new Thread(this.Manette2Thread);
            this.Manette2Controller.Priority = ThreadPriority.BelowNormal;
            this.ManetteController.Start();
        }

        public void ManetteThread()
        {
            while (true)
            {
                if (this.Manette1.Connected)
                    this.Parent.ManetteToolStripMenuItem.Enabled = true;
                else
                    this.Parent.ManetteToolStripMenuItem.Enabled = false;
                if (this.Manette2.Connected)
                    this.Parent.ManetteToolStripMenuItem1.Enabled = true;
                else
                    this.Parent.ManetteToolStripMenuItem1.Enabled = false;
            }
        }

        public void Manette1Thread()
        {
            while (this.Manette1.Connected)
            {
                this.Manette1.Update();
                if (this.Manette1.DPad.Up)
                {
                    if (this.Parent.PongGame.Joueur1.Centre.Y > 25)
                    {
                        this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                            this.Parent.PongGame.Joueur1.Centre.Y - 25);
                        if (this.Parent.PongGame.ServiceJoueur1)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette1.DPad.Down)
                {
                    if (this.Parent.PongGame.Joueur1.Centre.Y < this.Parent.PictureBox1.Height - 25)
                    {
                        this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                            this.Parent.PongGame.Joueur1.Centre.Y + 25);
                        if (this.Parent.PongGame.ServiceJoueur1)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y + 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette1.LeftStick.Position.Y > 0f)
                {
                    if (this.Parent.PongGame.Joueur1.Centre.Y > 25)
                    {
                        this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                            this.Parent.PongGame.Joueur1.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        if (this.Parent.PongGame.ServiceJoueur1)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette1.LeftStick.Position.Y < 0f)
                {
                    if (this.Parent.PongGame.Joueur1.Centre.Y < this.Parent.PictureBox1.Height - 25)
                    {
                        this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                            this.Parent.PongGame.Joueur1.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        if (this.Parent.PongGame.ServiceJoueur1)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette1.X)
                {
                    if (this.Parent.PongGame.ServiceJoueur1)
                    {
                        this.Parent.PongGame.AngleBalle += this.Parent.PongGame.AngleJoueur1;
                        this.Parent.PongGame.ServiceJoueur1 = false;
                        this.Parent.PongGame.TimerBalle.Enabled = true;
                        this.Parent.PongGame.TimerBalle.Start();
                    }
                }
                if (this.Manette1.A)
                {
                    if (this.Parent.PongGame.UpButtonPressedJoueur1 < 3)
                    {
                        this.Parent.PongGame.AngleJoueur1 += 1;
                        this.Parent.PongGame.UpButtonPressedJoueur1++;
                    }
                }
                if (this.Manette1.Y)
                {
                    if (this.Parent.PongGame.DownButtonPressedJoueur1 < 3)
                    {
                        this.Parent.PongGame.AngleJoueur1 -= 1;
                        this.Parent.PongGame.DownButtonPressedJoueur1++;
                    }
                }
                Thread.Sleep(50); /*FIXME*/
            }
        }

        public void Manette2Thread()
        {
            while (this.Manette2.Connected)
            {
                this.Manette2.Update();
                if (this.Manette2.DPad.Up)
                {
                    if (this.Parent.PongGame.Joueur2.Centre.Y > 25)
                    {
                        this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                            this.Parent.PongGame.Joueur2.Centre.Y - 25);
                        if (this.Parent.PongGame.ServiceJoueur2)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette2.DPad.Down)
                {
                    if (this.Parent.PongGame.Joueur2.Centre.Y < this.Parent.PictureBox1.Height - 25)
                    {
                        this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                            this.Parent.PongGame.Joueur2.Centre.Y + 25);
                        if (this.Parent.PongGame.ServiceJoueur2)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y + 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette2.LeftStick.Position.Y > 0f)
                {
                    if (this.Parent.PongGame.Joueur2.Centre.Y > 25)
                    {
                        this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                            this.Parent.PongGame.Joueur2.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        if (this.Parent.PongGame.ServiceJoueur2)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette2.LeftStick.Position.Y < 0f)
                {
                    if (this.Parent.PongGame.Joueur2.Centre.Y < this.Parent.PictureBox1.Height - 25)
                    {
                        this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                            this.Parent.PongGame.Joueur2.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        if (this.Parent.PongGame.ServiceJoueur2)
                            this.Parent.PongGame.Balle.Centre = new Point(this.Parent.PongGame.Balle.Centre.X,
                                this.Parent.PongGame.Balle.Centre.Y - (int)(this.Manette1.LeftStick.Position.Y * 25f));
                        this.Parent.PictureBox1.Invalidate();
                    }
                }
                if (this.Manette2.X)
                {
                    if (this.Parent.PongGame.ServiceJoueur2)
                    {
                        this.Parent.PongGame.AngleBalle += this.Parent.PongGame.AngleJoueur2;
                        this.Parent.PongGame.ServiceJoueur2 = false;
                        this.Parent.PongGame.TimerBalle.Enabled = true;
                        this.Parent.PongGame.TimerBalle.Start();
                    }
                }
                if (this.Manette2.A)
                {
                    if (this.Parent.PongGame.UpButtonPressedJoueur2 < 3)
                    {
                        this.Parent.PongGame.AngleJoueur2 += 1;
                        this.Parent.PongGame.UpButtonPressedJoueur2++;
                    }
                }
                if (this.Manette2.Y)
                {
                    if (this.Parent.PongGame.DownButtonPressedJoueur2 < 3)
                    {
                        this.Parent.PongGame.AngleJoueur2 -= 1;
                        this.Parent.PongGame.DownButtonPressedJoueur2++;
                    }
                }
                Thread.Sleep(50); /*FIXME*/
            }
        }
    }
}