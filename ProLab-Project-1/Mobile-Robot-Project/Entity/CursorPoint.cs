using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezginRobotProjesi.Entity
{
    public class CursorPoint
    {
        public int Left {get; set;}
        public int Top {get; set;}

        public CursorPoint(int left, int top){
            this.Left = left;
            this.Top = top;
        }

    }
}