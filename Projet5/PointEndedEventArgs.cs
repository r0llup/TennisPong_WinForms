using System;

namespace Projet5
{
    public class PointEndedEventArgs : EventArgs
    {
        public int Joueur { get; private set; }

        public PointEndedEventArgs(int joueur)
        {
            this.Joueur = joueur;
        }
    }
}