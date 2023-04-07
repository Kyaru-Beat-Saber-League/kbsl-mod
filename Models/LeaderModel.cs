namespace KBSL_MOD.Models
{
    public class LeaderModel
    {
        public int AccLeft { get; set; }
        public int AccRight { get; set; }
        public double Accuracy { get; set; }
        public int BadCut { get; set; }
        public int BaseScore { get; set; }
        public int BombCut { get; set; }
        public int MissedNote { get; set; }
        public int ModifiedScore { get; set; }
        public int Pause { get; set; }
        public int PlayCount { get; set; }
        public string SongDifficulty { get; set; }
        public string SongHash { get; set; }
        public string SongModeType { get; set; }
        public int WallsHit { get; set; }
    }
}