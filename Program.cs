using System;
using System.Collections.Generic;
using System.Linq;

namespace TwelveMarbles
{
    class Program
    {
        static void Main(string[] args)

        {
            //Solution: https://www.prepwithjen.com/can-you-solve-this-classic-brain-teaser.html/


            //for (int i = 1; i < 13; i++)
            //{
            // AllMarbles marbles = new AllMarbles(i, false);   //Initial 12 marbles
            AllMarbles marbles = new AllMarbles();   //Initial 12 marbles

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine(marbles.ToString());
            Console.WriteLine("Steps of proving as below: ");
            Console.WriteLine("-------------------------------------------------------------------\n");


            Marble anomalyMarble = new Marble(); 
            List<Marble> marbleLHS = new List<Marble>();
            List<Marble> marbleRHS = new List<Marble>();
            List<Marble> marbleLeftout = new List<Marble>();

            (marbleLHS, marbleRHS, marbleLeftout) = SplitMarbles(marbles);


            List<Marble> maybeHeavierMarbles = new List<Marble>();
            List<Marble> maybeLighterMarbles = new List<Marble>();
            List<Marble> normalMarbles = new List<Marble>();
            string nextStep = "";



            //Step 1
            (nextStep, maybeHeavierMarbles, maybeLighterMarbles, normalMarbles) = StepOne(marbleLHS, marbleRHS, marbleLeftout);
            bool isHeavier = false;
            Console.WriteLine();


            //Step 2
            List<Marble> oddMarbles = new List<Marble>();
            switch (nextStep)
            {
                case "StepTwoSubA":
                    (nextStep, isHeavier) = StepTwo_SubA(marbleLeftout, normalMarbles);
                    break;
                case "StepTwoSubB_BranchA":
                    (nextStep, isHeavier, oddMarbles) = StepTwoSubB_BranchA(maybeHeavierMarbles, maybeLighterMarbles, marbleLeftout);
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            //Step 3
            switch (nextStep)
            {
                case "StepThree_SubA_BranchA":
                    anomalyMarble = StepThree_SubA_BranchA(marbleLeftout[0], marbleLeftout[1], marbleLeftout[2], isHeavier);
                    break;
                case "StepThree_SubA_BranchB":
                    anomalyMarble = StepThree_SubA_BranchB(marbleLHS[0], marbleLeftout[3]);
                    break;
                case "StepThree_SubB_BranchA":
                    anomalyMarble = StepThree_SubB_BranchA(oddMarbles, isHeavier);
                    break;
                case "StepThree_SubB_BranchB":
                    anomalyMarble = StepThree_SubB_BranchB(oddMarbles[0], oddMarbles[1], normalMarbles);
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Conclusion, the anomaly marble is {0}, it is {1} than other marbles \n", anomalyMarble.Id, anomalyMarble.AnomalyMsg);


            //marbles.CheckResult(anomalyMarble);
         //   }//end of for loop

            Console.ReadLine();
        }


        #region Step 1
        static (string, List<Marble>, List<Marble>, List<Marble>) StepOne(List<Marble> marbleLHS, List<Marble> marbleRHS, List<Marble> marblesLeftOut)
        {
            Console.WriteLine("Step 1 ");
            string result = Seesaw(marbleLHS, marbleRHS);

            if (result == "L")
                return ("StepTwoSubB_BranchA", marbleLHS, marbleRHS, marblesLeftOut);
            else if (result == "R")
                return ("StepTwoSubB_BranchA", marbleRHS, marbleLHS, marblesLeftOut);
            else
                return ("StepTwoSubA", new List<Marble>(), new List<Marble>(), marbleLHS);

        }
        #endregion

        #region Step 2 Sub A
        static (string, bool) StepTwo_SubA(List<Marble> marblesLeftout, List<Marble> marblesNormal)
        {
            Console.WriteLine("Step 2 ");
            List<Marble> marbleSuspicous = new List<Marble>();
            List<Marble> marbleNormal = new List<Marble>();
            int count = 0;
            foreach (Marble mar in marblesLeftout)
            {
                if (count < 3)
                {
                    marbleSuspicous.Add(mar);
                    count++;
                    continue;
                }
            }

            count = 0;
            foreach (Marble mar in marblesNormal)
            {
                if (count < 3)
                {
                    marbleNormal.Add(mar);
                    count++;
                    continue;
                }
            }

            string seesawResult = Seesaw(marbleNormal, marbleSuspicous);

            if (seesawResult == "L")
                return ("StepThree_SubA_BranchA", false);

            else if (seesawResult == "R")
                return ("StepThree_SubA_BranchA", true);
            else
                return ("StepThree_SubA_BranchB", false);



        }
        #endregion

        #region Step 2 Sub B
        static (string, bool, List<Marble>) StepTwoSubB_BranchA(List<Marble> marblesMaybeHeavier, List<Marble> marblesMaybeLighter, List<Marble> marblesNormal)
        {
            Console.WriteLine("Step 2 ");

            List<Marble> marbleLHS = new List<Marble>();
            List<Marble> marbleRHS = new List<Marble>(); 

            List<Marble> OddMarbles = new List<Marble>();
            int count = 0;
            foreach (Marble mar in marblesMaybeLighter)
            {
                if (count < 3)
                {
                    marbleLHS.Add(mar);
                    count++;
                    continue;
                }
            }
            marbleLHS.Add(marblesMaybeHeavier[0]);
            count = 0;
            foreach (Marble mar in marblesNormal)
            {
                if (count < 3)
                {
                    marbleRHS.Add(mar);
                    count++;
                    continue;
                }
            }
            marbleRHS.Add(marblesMaybeLighter[3]); 


            string seesawResult = Seesaw(marbleLHS, marbleRHS);
            if (seesawResult == "L")
            {   
                OddMarbles.Add(marblesMaybeHeavier[0]);
                OddMarbles.Add(marblesMaybeLighter[3]);
                return ("StepThree_SubB_BranchB", true, OddMarbles);
            }
            else if (seesawResult == "R")
            {

                OddMarbles.Add(marblesMaybeLighter[0]);
                OddMarbles.Add(marblesMaybeLighter[1]);
                OddMarbles.Add(marblesMaybeLighter[2]); 
                return ("StepThree_SubB_BranchA", false, OddMarbles);
            }
            else
            {
                OddMarbles.Add(marblesMaybeHeavier[1]);
                OddMarbles.Add(marblesMaybeHeavier[2]);
                OddMarbles.Add(marblesMaybeHeavier[3]); 
                return ("StepThree_SubB_BranchA", true, OddMarbles);
            }
        }

        #endregion

        #region Step 3 SubA

        static Marble StepThree_SubA_BranchA(Marble suspiciousMarble9, Marble suspiciousMarble10, Marble suspiciousMarble11, bool isHeavier)
        { 
            Console.WriteLine("Step 3 ");

            string seesawResult = Seesaw(suspiciousMarble9, suspiciousMarble10);

            if (seesawResult == "L")
            {
                if (isHeavier)
                { 
                    suspiciousMarble9.Anomaly = true;
                    suspiciousMarble9.AnomalyMsg = "Heavier";
                    return suspiciousMarble9;
                }
                else
                { 
                    suspiciousMarble10.Anomaly = true;
                    suspiciousMarble10.AnomalyMsg = "Lighter";
                    return suspiciousMarble10;
                }

            }
            else if (seesawResult == "R")
            { 
                if (isHeavier)
                { 
                    suspiciousMarble10.Anomaly = true;
                    suspiciousMarble10.AnomalyMsg = "Heavier";
                    return suspiciousMarble10;

                }
                else
                { 
                    suspiciousMarble9.Anomaly = true;
                    suspiciousMarble9.AnomalyMsg = "Lighter";
                    return suspiciousMarble9;
                }
            }
            else
            { 
                suspiciousMarble11.Anomaly = true;
                suspiciousMarble11.AnomalyMsg = isHeavier ? "Heavier" : "Lighter";
                return suspiciousMarble11;
            }


        }

        static Marble StepThree_SubA_BranchB(Marble normalMarble, Marble anomalyMarble)
        {
            Console.WriteLine("Step 3 ");

            string seesawResult = Seesaw(normalMarble, anomalyMarble);
            if (seesawResult == "L")
            {
                anomalyMarble.Anomaly = true;
                anomalyMarble.AnomalyMsg = "Lighter";
                return anomalyMarble;
            }
            else
            {
                anomalyMarble.Anomaly = true;
                anomalyMarble.AnomalyMsg = "Heavier";
                return anomalyMarble;
            }

        }
        #endregion

        #region Step 3 SubB
        static Marble StepThree_SubB_BranchA(List<Marble> oddMarbles, bool isHeavier)
        {
            Console.WriteLine("Step 3 ");

            string seesawResult = Seesaw(oddMarbles[0], oddMarbles[1]);
            if (seesawResult == "L")
            {
                if (isHeavier)
                {
                    oddMarbles[0].Anomaly = true;
                    oddMarbles[0].AnomalyMsg = "Heavier";
                    return oddMarbles[0];
                }
                else
                {
                    oddMarbles[1].Anomaly = true;
                    oddMarbles[1].AnomalyMsg = "Lighter";
                    return oddMarbles[1];
                } 
            }
            else if (seesawResult == "R")
            {
                if (isHeavier)
                {
                    oddMarbles[1].Anomaly = true;
                    oddMarbles[1].AnomalyMsg = "Heavier";
                    return oddMarbles[1];
                }
                else
                {
                    oddMarbles[0].Anomaly = true;
                    oddMarbles[0].AnomalyMsg = "Lighter";
                    return oddMarbles[0];
                }
            }
            else//draw
            { 
                oddMarbles[2].Anomaly = true;
                oddMarbles[2].AnomalyMsg = isHeavier ? "Heavier" : "Lighter";
                return oddMarbles[2];

            } 
        }

        static Marble StepThree_SubB_BranchB(Marble maybeHeavierMarble, Marble maybeLigtherMarble, List<Marble> normalMarbles)
        {
            Console.WriteLine("Step 3 ");
            string seesawResult = Seesaw(maybeHeavierMarble, normalMarbles[0]);
            if (seesawResult == "L")
            { 
                maybeHeavierMarble.Anomaly = true;
                maybeHeavierMarble.AnomalyMsg = "Heavier";
                return maybeHeavierMarble; 
            }
            else //draw
            {
                maybeLigtherMarble.Anomaly = true;
                maybeLigtherMarble.AnomalyMsg = "Lighter";
                return maybeLigtherMarble;
            } 
        }
        #endregion

        static (List<Marble>, List<Marble>, List<Marble>) SplitMarbles(AllMarbles AllMarbles)
        {
            List<Marble> marbleLHS = new List<Marble>();
            List<Marble> marbleRHS = new List<Marble>();
            List<Marble> marbleLeftout = new List<Marble>();

            int count = 0;
            foreach (Marble mar in AllMarbles.Marbles)
            {

                if (count < 4)
                {
                    marbleLHS.Add(mar);
                    count++;
                    continue;
                }

                if (count < 8)
                {
                    marbleRHS.Add(mar);
                    count++;
                    continue;
                }

                if (count < 12)
                {
                    marbleLeftout.Add(mar);
                    count++;
                    continue;
                }
            }
            return (marbleLHS, marbleRHS, marbleLeftout);
        }

        #region Seesaw
        static string Seesaw(List<Marble> marbleLHS, List<Marble> marbleRHS)
        { 
            double weightLHS = 0.00;
            double weightRHS = 0.00;
            string LHS = string.Empty;
            string RHS = string.Empty; 

            LHS = marbleLHS.Aggregate("", (str, s) => str += s.Id + ", ");
            RHS = marbleRHS.Aggregate("", (str, s) => str += s.Id + ", ");

            Console.WriteLine("Weigh {0} against {1}", LHS, RHS);

            foreach (Marble mar in marbleLHS)
                weightLHS += mar.Weight;


            foreach (Marble mar in marbleRHS)
                weightRHS += mar.Weight;


            if (weightLHS > weightRHS)
            {
                Console.WriteLine("Seesaw result: {0} heavier than {1} ", LHS, RHS);
                return "L";
            }
            else if (weightRHS > weightLHS)
            {
                Console.WriteLine("Seesaw result: {0} heavier than {1} ", RHS, LHS);
                return "R";
            }
            else
            {
                Console.WriteLine("Seesaw result: {0} and {1} share the same weight", RHS, LHS);
                return "M";
            }

        }

        static string Seesaw(Marble marbleLHS, Marble marbleRHS)
        {
            Console.WriteLine("Weigh {0} against {1}", marbleLHS.Id, marbleRHS.Id);

            if (marbleLHS.Weight > marbleRHS.Weight)
            {
                Console.WriteLine("{0} heavier than {1}", marbleLHS.Id, marbleRHS.Id);
                return "L";
            }

            else if (marbleRHS.Weight > marbleLHS.Weight)
            {
                Console.WriteLine("{0} heavier than {1}", marbleRHS.Id, marbleLHS.Id);
                return "R";
            }
            else
            {
                Console.WriteLine("Seesaw result: {0} and {1} share the same weight", marbleRHS.Id, marbleLHS.Id);
                return "M";
            } 
        }
        #endregion 
    }
}
