using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projet5
{
    public class Cercle : Figure
    {
        public int Rayon { get; set; }
        public Color CouleurFond { get; set; }

        public Cercle()
            : this(25, new Point(100, 100))
        {
        }

        public Cercle(int rayon, Point centre)
            : this(rayon, centre, Color.Black)
        {
        }

        public Cercle(int rayon, Point centre, Color couleur) :
            this(rayon, centre, couleur, 1)
        {
        }

        public Cercle(int rayon, Point centre, Color couleur, float epaisseurTrait) :
            this(rayon, centre, couleur, epaisseurTrait, Color.Black)
        {
        }

        public Cercle(int rayon, Point centre, Color couleur, float epaisseurTrait, Color couleurFond) :
            base(centre, couleur, new Point(0, 0), epaisseurTrait)
        {
            this.Rayon = rayon;
            this.CouleurFond = couleurFond;
        }

        public override void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(this.Couleur, this.Epaisseur),
                this.Centre.X - this.Rayon, this.Centre.Y - this.Rayon, 2 * this.Rayon, 2 * this.Rayon);
            g.FillEllipse(new SolidBrush(this.CouleurFond),
                this.Centre.X - this.Rayon, this.Centre.Y - this.Rayon, 2 * this.Rayon, 2 * this.Rayon);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}