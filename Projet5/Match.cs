using System.Collections.Generic;

namespace Projet5
{
    public class Match
    {
        public LinkedList<Set> ListeSet { get; private set; }
        public Score Score1 { get; private set; }
        public Score Score2 { get; private set; }

        public Match()
        {
            this.ListeSet = new LinkedList<Set>();
            this.Score1 = new Score();
            this.Score2 = new Score();
        }

        public Match(Match match)
        {
            this.ListeSet = new LinkedList<Set>(match.ListeSet);
            this.Score1 = new Score(match.Score1);
            this.Score2 = new Score(match.Score2);
        }
    }
}