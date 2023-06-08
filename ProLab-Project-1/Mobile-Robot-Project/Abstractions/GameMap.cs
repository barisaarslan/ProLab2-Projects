using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace GezginRobotProjesi.Abstractions
{
    public abstract class GameMap
    {
        public List<List<Block>> Playground {get; set;}
        public Coordinate StartingPosition {get; set;}
        public Coordinate EndingPosition {get; set;}

        public GameMap(List<List<Block>> map){
            this.Playground = map;
            this.StartingPosition = new Coordinate(0, 0);
            this.EndingPosition = new Coordinate(0, 0);
            int height = map.Count;
            int width = height > 0 ? map[0].Count : 0;
            for(int i=0; i<width; i++){
                if(map[1][i].Type == BlockType.Path){
                    this.StartingPosition = new Coordinate(0, i);
                    LabyrinthHelper.SetBlockAsPath(map, this.StartingPosition);
                    break;
                }
            }
            for(int j=width-1; j>-1; j--){
                if(map[height-2][j].Type == BlockType.Path){
                    this.EndingPosition = new Coordinate(height-1, j);
                    LabyrinthHelper.SetBlockAsPath(map, this.EndingPosition);
                    break;
                }
            }
        }

        public abstract void Draw(List<Coordinate> visited, Coordinate robotPosition);
        public abstract void DrawShortestPath(List<Coordinate> visited, List<Coordinate> shortestPath);
        public abstract void UpdateBlock(Coordinate playerPosition, bool isRobot);
        public abstract void UpdateBlocks(List<Coordinate> blocks, List<Coordinate> visitedBlocks);

        public bool IsGameOver(Coordinate robotPosition){
            return EndingPosition.IsEqual(robotPosition);
        }

        protected bool CanMove(Coordinate position){
            int height = Playground.Count;
            int width = height > 0 ? Playground[0].Count : 0;
            if(position.X >= 0 && position.Y >= 0){
                return position.X < height && position.Y < width && Playground[position.X][position.Y].IsMoveble; 
            }
            return false;
        }

        public List<Coordinate> GetAccesiblePaths(Coordinate playerCoordinate){
            List<Coordinate> result = new List<Coordinate>();
            Coordinate up = new Coordinate(playerCoordinate.X - 1, playerCoordinate.Y);
            if(CanMove(up)){
                result.Add(up);
            }
            Coordinate bottom = new Coordinate(playerCoordinate.X + 1, playerCoordinate.Y);
            if(CanMove(bottom)){
                result.Add(bottom);
            }
            Coordinate left = new Coordinate(playerCoordinate.X, playerCoordinate.Y - 1);
            if(CanMove(left)){
                result.Add(left);
            }
            Coordinate right = new Coordinate(playerCoordinate.X, playerCoordinate.Y + 1);
            if(CanMove(right)){
                result.Add(right);
            }
            MakeBlocksVisible(result);
            return result;
        }

        private void MakeBlocksVisible(List<Coordinate> visibleBlocks){
            foreach(var block in visibleBlocks){
                Playground[block.X][block.Y].IsVisible = true;
            }
        }

    }
}