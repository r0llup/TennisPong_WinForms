namespace Projet5
{
    public class Score
    {
        public int Point { get; set; }

        public Score()
        {
            this.Point = 0;
        }

        public Score(int point)
        {
            this.Point = point;
        }

        public Score(Score score)
        {
            this.Point = score.Point;
        }
    }
}