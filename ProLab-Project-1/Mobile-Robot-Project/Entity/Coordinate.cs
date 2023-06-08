using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezginRobotProjesi.Entity
{
    public class Coordinate
    {
        public int X {get; set;}
        public int Y {get; set;}

        public Coordinate(int xPosition, int yPosition) {
            this.X = xPosition;
            this.Y = yPosition;
        }

        public bool IsEqual(Coordinate target){
            return this.X == target.X && this.Y == target.Y;
        }

        public bool IsEqual(int targetX, int targetY){
            return this.X == targetX && this.Y == targetY;
        }

    }
}