namespace Projet5
{
    public class Jeu
    {
        public Score Score1 { get; private set; }
        public Score Score2 { get; private set; }

        public Jeu()
        {
            this.Score1 = new Score();
            this.Score2 = new Score();
        }

        public Jeu(Jeu jeu)
        {
            this.Score1 = new Score(jeu.Score1);
            this.Score2 = new Score(jeu.Score2);
        }
    }
}