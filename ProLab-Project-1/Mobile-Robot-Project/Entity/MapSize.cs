using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezginRobotProjesi.Entity
{
    public class MapSize
    {
        public int Height {get; set;}
        public int Width {get; set;}

        public MapSize(int height, int width){
            this.Height = height;
            this.Width = width;
        }

    }
}