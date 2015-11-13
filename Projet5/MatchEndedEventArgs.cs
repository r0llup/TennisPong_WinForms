using System;

namespace Projet5
{
    public class MatchEndedEventArgs : EventArgs
    {
        public Match Match { get; private set; }

        public MatchEndedEventArgs(Match match)
        {
            this.Match = new Match(match);
        }
    }
}