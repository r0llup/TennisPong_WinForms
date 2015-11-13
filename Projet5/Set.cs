using System.Collections.Generic;

namespace Projet5
{
    public class Set
    {
        public LinkedList<Jeu> ListeJeu { get; private set; }
        public Score Score1 { get; private set; }
        public Score Score2 { get; private set; }

        public Set()
        {
            this.ListeJeu = new LinkedList<Jeu>();
            this.Score1 = new Score();
            this.Score2 = new Score();
        }

        public Set(LinkedList<Jeu> listeJeu, Score score1, Score score2)
        {
            this.ListeJeu = new LinkedList<Jeu>(listeJeu);
            this.Score1 = new Score(score1);
            this.Score2 = new Score(score2);
        }

        public Set(Set set)
        {
            this.ListeJeu = new LinkedList<Jeu>(set.ListeJeu);
            this.Score1 = set.Score1;
            this.Score2 = set.Score2;
        }
    }
}