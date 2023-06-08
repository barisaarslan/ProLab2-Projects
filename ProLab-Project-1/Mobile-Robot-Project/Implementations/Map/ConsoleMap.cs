using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace GezginRobotProjesi.Implementations.Map
{
    public class ConsoleMap : GameMap
    {
        private List<List<CursorPoint>> cursorPoints {get; set;}
        public ConsoleMap(List<List<Block>> map) : base(map){
            cursorPoints = new List<List<CursorPoint>>();
        }
        public override void Draw(List<Coordinate> visited, Coordinate robotPosition)
        {
            cursorPoints = new List<List<CursorPoint>>();
            Console.Clear();
            Console.ResetColor();
            int height = this.Playground.Count;
            int width = height > 0 ? this.Playground[0].Count : 0;
            Console.WriteLine(string.Format("Başlangıç Noktası: ({0},{1})", this.StartingPosition.X, this.StartingPosition.Y));
            Console.WriteLine(string.Format("Bitiş Noktası: ({0},{1})", this.EndingPosition.X, this.EndingPosition.Y));
            for(int i=0; i<height; i++) {
                List<CursorPoint> columnCursorPoints = new List<CursorPoint>();
                for(int j=0; j<width; j++) {
                    columnCursorPoints.Add(new CursorPoint(Console.CursorLeft, Console.CursorTop));
                    SetBackgroundColor(this.Playground[i][j], visited, robotPosition);
                    Console.Write(string.Format(" {0} ", ((int)this.Playground[i][j].Type)));
                }
                cursorPoints.Add(columnCursorPoints);
                Console.ResetColor();
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public override void UpdateBlock(Coordinate position, bool isRobot)
        {
            CursorPoint currentPoint = new CursorPoint(Console.CursorLeft, Console.CursorTop);
            CursorPoint targetPoint = cursorPoints[position.X][position.Y];
            Console.SetCursorPosition(targetPoint.Left, targetPoint.Top);
            Console.ResetColor();
            List<Coordinate> visitedBlocks = new List<Coordinate>();
            Coordinate robotPosition = new Coordinate(-1, -1);
            if(isRobot){
                robotPosition = position;
            }else{
                visitedBlocks.Add(position);
            }
            SetBackgroundColor(Playground[position.X][position.Y], visitedBlocks, robotPosition);
            Console.Write(string.Format(" {0} ",(int)Playground[position.X][position.Y].Type));
            Console.ResetColor();
            Console.SetCursorPosition(currentPoint.Left, currentPoint.Top);
        }

        private void SetBackgroundColor(Block currentBlock, List<Coordinate> visitedBlocks, Coordinate robotPosition){
            if(currentBlock.Type == BlockType.Path){
                Console.BackgroundColor = currentBlock.IsVisible ? ConsoleColor.Green : ConsoleColor.DarkGreen;
            }else{
                if(currentBlock.IsMoveble){
                    Console.BackgroundColor = ConsoleColor.Red;
                }else{
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
            }
            if(currentBlock.Position.IsEqual(this.StartingPosition) || currentBlock.Position.IsEqual(this.EndingPosition)){
                Console.BackgroundColor = ConsoleColor.Yellow;
            }

            if(visitedBlocks.Any(c => c.X == currentBlock.Position.X && c.Y == currentBlock.Position.Y)){
                Console.BackgroundColor = ConsoleColor.White;
            }

            if(currentBlock.Position.IsEqual(robotPosition)){
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
        }

        public override void UpdateBlocks(List<Coordinate> blocks, List<Coordinate> visitedBlocks)
        {
            CursorPoint currentPoint = new CursorPoint(Console.CursorLeft, Console.CursorTop);
            Coordinate robotPosition = new Coordinate(-1, -1);
            foreach(Coordinate block in blocks){
                CursorPoint targetPoint = cursorPoints[block.X][block.Y];
                Console.SetCursorPosition(targetPoint.Left, targetPoint.Top);
                SetBackgroundColor(Playground[block.X][block.Y], visitedBlocks, robotPosition);
                Console.Write(string.Format(" {0} ",(int)Playground[block.X][block.Y].Type));
                Console.ResetColor();
            }
            Console.SetCursorPosition(currentPoint.Left, currentPoint.Top);
        }

        public override void DrawShortestPath(List<Coordinate> visited, List<Coordinate> shortestPath)
        {
            Console.Clear();
            Console.ResetColor();
            Coordinate robotPosition = EndingPosition;
            int height = this.Playground.Count;
            int width = height > 0 ? this.Playground[0].Count : 0;
            Console.WriteLine(string.Format("Başlangıç Noktası: ({0},{1})", this.StartingPosition.X, this.StartingPosition.Y));
            Console.WriteLine(string.Format("Bitiş Noktası: ({0},{1})", this.EndingPosition.X, this.EndingPosition.Y));
            for(int i=0; i<height; i++) {
                for(int j=0; j<width; j++) {
                    SetBackgroundColor(this.Playground[i][j], visited, robotPosition);
                    if(shortestPath.Any(p => p.X == i && p.Y == j)){
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(string.Format(" {0} ", ((int)this.Playground[i][j].Type)));
                }
                Console.ResetColor();
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}