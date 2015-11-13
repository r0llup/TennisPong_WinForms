using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projet5
{
    public class Texte : Figure
    {
        public string Texto { get; set; }
        public Font Police { get; set; }
        public SolidBrush Brush { get; set; }

        public Texte()
            : this("Hello World !")
        {
        }

        public Texte(string texte)
            : this(texte, new Point(150, 150))
        {
        }

        public Texte(string texte, Point centre)
            : this(texte, centre, Color.Black)
        {
        }

        public Texte(string texte, Point centre, Color couleur)
            : this(texte, centre, couleur, new Font("Arial", 16))
        {
        }

        public Texte(string texte, Point centre, Color couleur, Font police)
            : this(texte, centre, couleur, police, new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
        {
        }

        public Texte(string texte, Point centre, Color couleur, Font police, SolidBrush brush)
            : base(centre, couleur, new Point(0, 0), 1)
        {
            this.Texto = texte;
            this.Police = police;
            this.Brush = brush;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawString(this.Texto, this.Police, this.Brush, this.Centre);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}