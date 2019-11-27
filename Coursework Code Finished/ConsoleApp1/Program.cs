using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;



namespace CaveRouteApp
{
    class Program
    {
        public DataSort childcaveHandler = new DataSort();

        static void Main(string[] args)
        {

            int c = 0;

            //load text file from BAt
            string filepath = args[0];
            //string filepath = @"D:\Users\grant\Desktop\Artificial Intelligence\AI Coursework] latest\input7";
            string filename = System.IO.Path.GetFileName(filepath);
            filepath += ".cav";
            //;
             

            //create object for sorting and storing data. 
            DataSort Data = new DataSort();
            Cave startCave;
            Cave endCave;
            string[] lines = File.ReadAllLines(filepath);
            //string[] lines = File.ReadAllLines(filepath);

            Data.Connections = new List<string>();
            List<Cave> Caves = new List<Cave>();
            Data.sortData(lines);
            //creates a variable which holds number of caves
            Data.NoofCaves = Convert.ToInt16(Data.sortedList[0]) * 2;

            //create an array which contains all the coordinates of caves, with the length defined by the number of coordinatee values.
            Data.caveCoords = new string[Data.NoofCaves];
            //Creates a list of coordinates
            Data.CreateCoordList();

            //creating a list of connections
            Data.CreateConnectionList();

            //create a list of caves 
            for (int i = 0; i < Data.caveCoords.Length; i += 2)
            {
                Caves.Add(new Cave(Convert.ToInt16(Data.caveCoords[i]), Convert.ToInt16(Data.caveCoords[i + 1]), c + 1));
                c++;
            }

            //creates the connections between caves
            for (int i = 0; i < Caves.Count; i++)
            {
                for (int d = 0; d < Caves.Count; d++)
                {
                    if (Data.Connections[(i * Caves.Count) + d] == "1")
                    {
                        Caves[d].Connections.Add(Caves[i]);                        
                    }
                }
            }

            startCave = Caves[0];
            endCave = Caves[Caves.Count - 1];

            //calculate distance to end node for each cave.
            for (int i = 0; i < Caves.Count; i++)
            {
                // calculates how far each cave is to the end cave.
                Caves[i].DistancetoGoalCave = Data.nodeToNode(endCave, Caves[i]);
                //Console.WriteLine("Cave: " + Caves[i].CaveNum + "Distance to Goal: " + Caves[i].DistancetoGoalCave);

            }

 
            //Run algorithm here..
          
            List<Cave> pathToCave = new List<Cave>();

            AStarSearch(startCave, endCave, Caves, filename);
                      
        }
 

        public static void InsertChild(ref List<Cave> openList, ref List<Cave> ChildCaves, ref List<Cave> closedList)
        {
            for (int g = 0; g < ChildCaves.Count; g++)
            {
                if (!openList.Contains(ChildCaves[g]) && !closedList.Contains(ChildCaves[g]))
                    openList.Add(ChildCaves[g]);
            }
        }

        public static void AStarSearch(Cave startCave, Cave endCave, List<Cave> Caves, string path)
        {
            DataSort data = new DataSort();          
            List<Cave> openList = new List<Cave>();
            List<Cave> closedList = new List<Cave>();

            bool noPath = false;
            Cave caveCurrent = new Cave();
            //start the search by adding the first cave
            openList.Add(startCave);
            //make the current cave the start cave
            caveCurrent = startCave;


            while (openList.Count > 0 && caveCurrent != endCave)
            {
                openList.Remove(caveCurrent);
                closedList.Add(caveCurrent);
                List<Cave> childList = new List<Cave>();
                //calculate distances for child caves for current cave
                childList =  data.CalculateChildCaves(caveCurrent);
                //add the child caves of the current cave to the open list (to be checked)
                InsertChild(ref openList, ref childList, ref closedList);

                
                if (openList.Count > 0) 
                    //order caves by DistancetogoalCave + distance to current cave and make that = currentcave.
                    //This ensures best path taken.
                { caveCurrent = openList.OrderBy(c => c.DistancetoGoalCave + c.DistanceToCurrentCave).ElementAt(0); }
                else
                {                    
                    noPath = true;
                    break;
                }

            }

            List<Cave> BestPath = new List<Cave>();
            if (noPath == false)
            {
                //while the current cave is not equal to end cave, add the current cave to the best path and then make current cave the closest child cave.
                caveCurrent = endCave;
                while (caveCurrent != startCave)
                {
                    BestPath.Add(caveCurrent);
                    caveCurrent = caveCurrent.ClosestChildCave;
                }

                //completes the list by adding the start cave
                BestPath.Add(startCave);
                //gets the best path by reversing the list.

                BestPath.Reverse();

                using (StreamWriter writer = File.CreateText(path + ".csn"))
                {
                    foreach (Cave cave in BestPath)
                    {
                        writer.Write(cave.CaveNum + " ");
                    }
                    
                }
              
            }
            else
            {
                //if no path found , write 0 to file.
                using (StreamWriter writer = File.CreateText(path + ".csn"))
                {
                    writer.Write("0");
                }
                Console.WriteLine("No path found");

            }
        

        }



    }
}



