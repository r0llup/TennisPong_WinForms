using System.Drawing;

namespace Projet5
{
    public abstract class Figure : IDrawable
    {
        public Point Centre { get; set; }
        public Color Couleur { get; set; }
        public Point Position { get; set; }
        public float Epaisseur { get; set; }

        public Figure(Point centre, Color couleur, Point position, float epaisseur)
        {
            this.Centre = centre;
            this.Couleur = couleur;
            this.Position = position;
            this.Epaisseur = epaisseur;
        }

        public abstract void Draw(System.Drawing.Graphics g);
    }
}