using System.Drawing;

namespace Projet5
{
    public interface IDrawable
    {
        void Draw(Graphics g);
        Point Centre { get; set; }
        Color Couleur { get; set; }
        Point Position { get; set; }
        float Epaisseur { get; set; }
    }
}