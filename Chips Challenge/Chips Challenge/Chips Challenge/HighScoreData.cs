using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chips_Challenge
{
    class HighScoreData
    {
        private int newScore;
        private string gameName;
        private int[] scores;
        private string[] scoresStr;
        private string[] names;

        public HighScoreData(string gameName)
        {
            this.gameName = gameName;
            this.newScore = 0;
            scores = new int[5];
            scoresStr = new string[5];
            names = new string[5];
            readFile();
        }
        //public HighScoreData(string gameName, int newScore)
        //{
        //    this.gameName = gameName;
        //    this.newScore = newScore;
        //    scores = new int[5];
        //    scoresStr = new string[5];
        //    names = new string[5];
        //    readFile();
          
        //}
      
        private void readFile()
        {

            string filePath = gameName + ".ini";            
                    
            if (File.Exists( filePath ))         
                {             
                    StreamReader file = null;               
                    try             
                    {                    
                        file = new StreamReader( filePath );
                        string nameLine;
                        string scoreLine;
                        string gm = file.ReadLine();


                        for (int i = 0; i < 5; i++)
                        {
                            nameLine = file.ReadLine();
                            scoreLine =file.ReadLine();
                            names[i] = nameLine.Substring(6, nameLine.Length - 6);
                            scoreLine = scoreLine.Substring(8, scoreLine.Length - 8);                        
                            scoresStr[i] = scoreLine;
                            scores[i] = Convert.ToInt32(scoreLine);
                        }
                        file.Close();

               
                    }                
                    finally               
                    {                               
                        if (file != null)                        
                            file.Close();                
                     }            
               } 
        }
        public bool checkScore_HighIsGood(int newscore){
            int insertPosition = 6;
            for (int i = 4; i >= 0; i--)
            {
                if (scores[i] < newscore)
                {
                    insertPosition = i;
                }

            }
            if (insertPosition < 5)
            {
                return true;
            }
            return false;
        }
        public void insertHighScore_HighIsGood(int newscore, string newname)
        {
            int insertPosition = 6;
            for (int i = 4; i >= 0; i--)
            {
                if (scores[i] < newscore)
                {
                    insertPosition = i;
                }

            }
            if (insertPosition < 5)
            {
                for (int j = 4; j > insertPosition; j--)
                {
                    scores[j] = scores[j - 1];
                    names[j] = names[j - 1];

                }
                scores[insertPosition] = newscore;
                names[insertPosition] = newname;
            }
           
        }
       
        public string[] getNames()
        {
            return names;

        }
        public int[] getScores()
        {
            return scores;

        }
        public string[] getScoresStr()
        {
            return scoresStr;

        }
     public void writeFile(){
    //    FileStream fs=new FileStream(gameName + "ini", FileMode.Open, FileAccess.Write, FileShare.Write);
       
        StreamWriter sw = new StreamWriter(gameName + ".ini", false);
        sw.WriteLine("[" + gameName + "]");
        for (int i = 1; i < 6; i++)
        {
            sw.WriteLine("name" + i + "=" + names[i - 1]);
            sw.WriteLine("hscore" + i + "=" + scores[i - 1]);
        }
        sw.Close();

        }
    }
}
