using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;

namespace GezginRobotProjesi.Helpers
{
    public static class ConsoleHelper
    {
        public static void ClearLast2Lines(){
            CursorPoint currentPoint = new CursorPoint(Console.CursorLeft, Console.CursorTop);
            ClearLast2Lines(currentPoint);
        }

        public static void ClearLast2Lines(CursorPoint startingPoint){
            Console.SetCursorPosition(0, startingPoint.Top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, startingPoint.Top - 1);
            Console.Write(new string(' ', Console.WindowWidth));
        }

    }
}