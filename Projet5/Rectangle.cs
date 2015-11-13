using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projet5
{
    public class Rectangle : Figure
    {
        public int Longueur { get; set; }
        public int Largeur { get; set; }
        public Color CouleurFond { get; set; }

        public Rectangle()
            : this(100, 50, new Point(100, 100))
        {
        }

        public Rectangle(int longueur, int largeur, Point centre) :
            this(longueur, largeur, centre, Color.Black)
        {
        }

        public Rectangle(int longueur, int largeur, Point centre, Color couleur) :
            this(longueur, largeur, centre, couleur, new Point(0, 0))
        {
        }

        public Rectangle(int longueur, int largeur, Point centre, Color couleur, Point position) :
            this(longueur, largeur, centre, couleur, position, 1)
        {
        }

        public Rectangle(int longueur, int largeur, Point centre, Color couleur, Point position, float epaisseur) :
            this(longueur, largeur, centre, couleur, position, epaisseur, Color.Black)
        {
        }

        public Rectangle(int longueur, int largeur, Point centre, Color couleur, Point position, float epaisseur, Color couleurFond)
            : base(centre, couleur, position, epaisseur)
        {
            this.Longueur = longueur;
            this.Largeur = largeur;
            this.CouleurFond = couleurFond;
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(this.Couleur, this.Epaisseur), this.Centre.X - this.Longueur / 2, this.Centre.Y - this.Largeur / 2, this.Longueur, this.Largeur);
            g.FillRectangle(new SolidBrush(this.CouleurFond), this.Centre.X - this.Longueur / 2, this.Centre.Y - this.Largeur / 2, this.Longueur, this.Largeur);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}