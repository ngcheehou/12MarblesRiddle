using System;
using System.Collections.Generic;
using System.Text;

namespace TwelveMarbles
{
    public class AllMarbles
    {
        // for debug
        public AllMarbles(int ano, bool isHea)
        {
             Marbles = GenerateMabrles(Marbles, ano, isHea);
        }

        public AllMarbles()
        {
            Marbles = GenerateMabrles(Marbles);
        }

        public List<Marble> Marbles { get; set; } = new List<Marble>();
        public int AnomalyMarble { get; set; }
        public bool IsHeavier { get; set; }

        public override string ToString()
        { 
             string compare = IsHeavier ? "heavier" : "lighter";
            double   Weight = IsHeavier ? 1.5 : 0.5;
            return  "Anomaly marbe is Marble " + AnomalyMarble + ", weight: " + Weight + ", " + compare + " than other marbles";
        }

        public void CheckResult(Marble marble)
        {

            bool marbeIsheavier = marble.AnomalyMsg == "Heavier";
            string anomalyMarble = "Marble " + AnomalyMarble.ToString();

            if (marble.Id == anomalyMarble && IsHeavier == marbeIsheavier)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct answer: {0} vs your answer: {1}", anomalyMarble, marble.Id);
                Console.WriteLine("Pass!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Correct answer: {0} vs your answer: {1}", anomalyMarble, marble.Id);
                Console.WriteLine("Fail!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        private List<Marble> GenerateMabrles(List<Marble> Marbles) 
        {
            Random ran = new Random();

            int anomaly = ran.Next(1, 13); //9;//ran.Next(1, 13);
            bool heavier = ran.Next(1, 3) % 2 == 0 ? true : false;
         
            AnomalyMarble = anomaly;
            IsHeavier = heavier;

            for (int i = 1; i < 13; i++)
            {
                Marble marble = new Marble();
                marble.Id = "Marble " + i.ToString();
                marble.Weight = 1;

                if (i != anomaly)
                {
                    Marbles.Add(marble);
                    continue;
                }

                marble.Anomaly = true;

                if (heavier)
                    marble.Weight += 0.5;
                else
                    marble.Weight -= 0.5;

                Marbles.Add(marble);

            }
            return Marbles;
        }

        //For debug
        private List<Marble> GenerateMabrles(List<Marble> Marbles, int anomaly, bool heavier) 
        {

            AnomalyMarble = anomaly;
            IsHeavier = heavier;
            Random ran = new Random();


            for (int i = 1; i < 13; i++)
            {
                Marble marble = new Marble();
                marble.Id = "Marble " + i.ToString();
                marble.Weight = 1;

                if (i != anomaly)
                {
                    Marbles.Add(marble);
                    continue;
                }

                marble.Anomaly = true;

                if (heavier)
                    marble.Weight += 0.5;
                else
                    marble.Weight -= 0.5;

                Marbles.Add(marble);

            }
            return Marbles;
        }
    }
}

