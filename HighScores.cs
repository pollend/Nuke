using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HighScores
{
    [Serializable]
    class Score : IComparable<Score>
    {
        public string Name { get; set; }
        public int UserScore { get; set; }

        public Score():
            this("", 0)
        {
        }

        public Score(string name, int score)
        {
            Name = name;
            UserScore = score;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, UserScore);
        }

        #region IComparable<Score> Members

        public int CompareTo(Score other)
        {
            return other.UserScore - UserScore;
        }

        #endregion
    }

    class HighScores
    {
        private List<Score> scores = new List<Score>();
       
        public HighScores()
        {
            
        }

        public void Add(Score score)
        {
            scores.Add(score);
            scores.Sort();
                if (scores.Count > 10)
                {
                    if (scores.Count != 11)
                    {
                        scores.RemoveAt(11);
                    }
                }
             
            
        }

        public Score[] Scores
        {
            
            get 
            {
                return scores.ToArray(); 
            }
        }
        public int LastScore()
        {
            scores.Sort();
            return Scores[scores.Count -1].UserScore;
        }
        public void WriteScores(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, scores);
            }
        }

        public void ReadScores(string filename)
        {
            if (File.Exists(filename))
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    scores = (List<Score>)formatter.Deserialize(stream);
                }
                scores.Sort();
            }
        }
    }
}
