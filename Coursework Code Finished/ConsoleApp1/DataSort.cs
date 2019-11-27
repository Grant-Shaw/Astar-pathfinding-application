using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveRouteApp
{
    public class DataSort
    {
        public List<string> sortedList = new List<string>();
        public List<string> Connections = new List<string>();
        public string[] caveCoords;
        


        public int NoofCaves { get; set; }
        int b;

        public double euclidianDistance(double x1, double x2, double y1, double y2)
        {
            return (Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
        }

        //reads the data and puts it into an array 1 line at a time.
        public void sortData(string [] lines)
        {
            foreach (string line in lines)
            {
                string[] col = line.Split(',');

                foreach (string i in col)
                {
                    sortedList.Add(i);                  
                }
            }
        }

        //takes all coordinates and puts them in an array
        public void CreateCoordList()
        {           
            for (int i = 0; i < NoofCaves; i++)
            {
                while (b < this.caveCoords.Length)
                {
                    caveCoords[b] = sortedList[i];
                    b++;
                    i++;
                }
            }

        }

        public void CreateConnectionList()
        {
            for (int i = caveCoords.Length + 1; i < sortedList.Count; i++)
            {
                Connections.Add(sortedList[i]);
            }
        }

        public double nodeToNode(Cave a, Cave B)
        {
            return (Math.Sqrt(((a.XCoord - B.XCoord) * (a.XCoord - B.XCoord) + (a.YCoord - B.YCoord) * (a.YCoord - B.YCoord))));
        }


        //calculates how far each cave has to last cave.
        public void CalculateDistToEnd(ref List<Cave>Caves, ref Cave endCave)
        {
            DataSort data = new DataSort();

            for (int i = 0; i < Caves.Count; i++)
            {
                // calculates how far each cave is to the end cave.
                Caves[i].DistancetoGoalCave = nodeToNode(endCave, Caves[i]);
                
            }

            Console.WriteLine("Complete");

        }

        //method which calculates the distance from the current cave being checked to each of it's connected caves and adds it to list.
        public List<Cave> CalculateChildCaves(Cave currentCave)
        {
            List<Cave> path = new List<Cave>();
            List<Cave> ChildCaves = new List<Cave>();

            for (int p = 0; p < currentCave.Connections.Count; p++)
            {
                Cave childCave = currentCave.Connections[p];
                DataSort helper = new DataSort();

                double cost = helper.nodeToNode(currentCave, childCave);

                if (childCave.ClosestChildCave == null || currentCave.DistanceToCurrentCave + cost < childCave.DistanceToCurrentCave)
                {

                    childCave.DistanceToCurrentCave = currentCave.DistanceToCurrentCave + cost;
                    childCave.ClosestChildCave = currentCave;


                    //Console.WriteLine("Child cave Num: " + childCave.CaveNum);
                    //Console.WriteLine("Distance to current caves" + childCave.DistanceToCurrentCave);
                    //Console.WriteLine("closest child Cave: " + childCave.ClosestChildCave.CaveNum);
                }

                ChildCaves.Add(childCave);
            }
            return ChildCaves;
        }


    }
}
