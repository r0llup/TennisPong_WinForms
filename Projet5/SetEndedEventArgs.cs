using System;

namespace Projet5
{
    public class SetEndedEventArgs : EventArgs
    {
        public int Joueur { get; private set; }
        public Set Set { get; private set; }

        public SetEndedEventArgs(int joueur, Set set)
        {
            this.Joueur = joueur;
            this.Set = new Set(set);
        }
    }
}