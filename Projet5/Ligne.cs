using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projet5
{
    public class Ligne : Figure
    {
        public int Longueur { get; set; }

        public Ligne()
            : this(100, new Point(100, 100))
        {
        }

        public Ligne(int longueur, Point centre)
            : this(longueur, centre, Color.Black)
        {
        }

        public Ligne(int longueur, Point centre, Color couleur) :
            this(longueur, centre, couleur, new Point(0, 0))
        {
        }

        public Ligne(int longueur, Point centre, Color couleur, Point position) :
            this(longueur, centre, couleur, position, 1)
        {
        }

        public Ligne(int longueur, Point centre, Color couleur, Point position, float epaisseur) :
            base(centre, couleur, position, epaisseur)
        {
            this.Longueur = longueur;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(new Pen(this.Couleur, this.Epaisseur), this.Centre, this.Position);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}