using System;
using System.Drawing;
using System.Threading;

namespace Projet5
{
    public class Ai
    {
        private TennisPongForm Parent { get; set; }
        public int AiSkill { get; set; }
        public Thread Ai1Controller { get; set; }
        public Thread Ai2Controller { get; set; }

        public Ai(TennisPongForm parent)
        {
            this.Parent = parent;
            this.AiSkill = 150; // ms
            this.Ai1Controller = new Thread(this.Ai1Thread);
            this.Ai1Controller.Priority = ThreadPriority.BelowNormal;
            this.Ai2Controller = new Thread(this.Ai2Thread);
            this.Ai2Controller.Priority = ThreadPriority.BelowNormal;
        }

        public void Ai1Thread()
        {
            while (true)
            {
                if (!this.Parent.PongGame.AllerBalle)
                    this.Parent.PongGame.AngleJoueur1 = new Random().Next(-30000, 30000) / 10000;
                if (this.Parent.PongGame.ServiceJoueur1)
                {
                    Thread.Sleep(new Random().Next(250, 500));
                    this.Parent.PongGame.AngleJoueur1 = new Random().Next(-30000, 30000) / 10000;
                    this.Parent.PongGame.AngleBalle += this.Parent.PongGame.AngleJoueur1;
                    this.Parent.PongGame.ServiceJoueur1 = false;
                    this.Parent.PongGame.TimerBalle.Enabled = true;
                    this.Parent.PongGame.TimerBalle.Start();
                }
                else
                {
                    if (!this.Parent.PongGame.AllerBalle)
                    {
                        if (this.Parent.PongGame.Balle.Centre.Y < this.Parent.PongGame.Joueur1.Centre.Y)
                            this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                                this.Parent.PongGame.Joueur1.Centre.Y - 25);
                        if (this.Parent.PongGame.Balle.Centre.Y > this.Parent.PongGame.Joueur1.Centre.Y)
                            this.Parent.PongGame.Joueur1.Centre = new Point(this.Parent.PongGame.Joueur1.Centre.X,
                                this.Parent.PongGame.Joueur1.Centre.Y + 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                    Thread.Sleep(this.AiSkill);
                }
            }
        }

        public void Ai2Thread()
        {
            while (true)
            {
                if (this.Parent.PongGame.AllerBalle)
                    this.Parent.PongGame.AngleJoueur2 = new Random().Next(-30000, 30000) / 10000;
                if (this.Parent.PongGame.ServiceJoueur2)
                {
                    Thread.Sleep(new Random().Next(250, 500));
                    this.Parent.PongGame.AngleJoueur2 = new Random().Next(-30000, 30000) / 10000;
                    this.Parent.PongGame.AngleBalle += this.Parent.PongGame.AngleJoueur2;
                    this.Parent.PongGame.ServiceJoueur2 = false;
                    this.Parent.PongGame.TimerBalle.Enabled = true;
                    this.Parent.PongGame.TimerBalle.Start();
                }
                else
                {
                    if (this.Parent.PongGame.AllerBalle)
                    {
                        if (this.Parent.PongGame.Balle.Centre.Y < this.Parent.PongGame.Joueur2.Centre.Y)
                            this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                                this.Parent.PongGame.Joueur2.Centre.Y - 25);
                        if (this.Parent.PongGame.Balle.Centre.Y > this.Parent.PongGame.Joueur2.Centre.Y)
                            this.Parent.PongGame.Joueur2.Centre = new Point(this.Parent.PongGame.Joueur2.Centre.X,
                                this.Parent.PongGame.Joueur2.Centre.Y + 25);
                        this.Parent.PictureBox1.Invalidate();
                    }
                    Thread.Sleep(this.AiSkill);
                }
            }
        }
    }
}