using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezginRobotProjesi.Entity
{
    public class CoordinateNode
    {
        public Coordinate Position {get; set;}
        public CoordinateNode ParentNode {get; set;}

        public CoordinateNode(Coordinate position, CoordinateNode parent){
            this.Position = position;
            this.ParentNode = parent;
        }

    }
}