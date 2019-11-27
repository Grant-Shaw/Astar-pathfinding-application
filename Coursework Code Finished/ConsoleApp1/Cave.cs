using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveRouteApp
{
    public class Cave
    {

        public List<Cave> Connections = new List<Cave>();
        

        private double xCoord;
        private double yCoord;
        private double caveNum;      
        private double distancetoCurrentCave;
        private double shortestDistanceToA;
        private Cave closestChildCave;
        

      
        public double DistancetoGoalCave
        {
            get { return shortestDistanceToA; }
            set { shortestDistanceToA = value; }
        }

        public Cave ClosestChildCave
        {
            get { return closestChildCave; }
            set {closestChildCave = value ; }

        }
     
        public double DistanceToCurrentCave
        {

            get { return distancetoCurrentCave; }
            set { distancetoCurrentCave = value; }
        }

     
        public double XCoord
        {
            get { return xCoord; }
            set { xCoord = value; }
        }
        public double YCoord
        {
            get { return yCoord; }
            set { yCoord = value; }
        }

        public double CaveNum
        {
            get { return caveNum; }
            set { caveNum = value; }
        }


        public void CalcCost(double g, double h)
        {
            DistancetoGoalCave = g + h;
   

        }

 
        public Cave(double x, double y, double id)
        {
            XCoord = x;
            YCoord = y;
            CaveNum = id;
            
   
        }

        public Cave()
        {


        }

        //for testing purposes
        public override string ToString()
        {
            return "Cave " + CaveNum + " Coordinates are  X:" + +XCoord + " Y:" + YCoord;
                
        }

    }
}
