using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;

namespace GezginRobotProjesi.Helpers
{
    public static class LabyrinthHelper
    {
        public static List<List<Block>> SetMap(int height, int width) {
            Random r = new Random();
            List<List<Block>> map = SetBasicMap(height, width);
            Coordinate startingPoint = GetStartingPointForLabyrinthCreation(height, width);
            SetBlockAsPath(map, startingPoint);
            List<Block> walls = GetSurroundingWalls(map, startingPoint);
            SetBlocksToMap(map, walls);
            CreateLabyrinth(map);
            SetAllUnvisitedBlockAsBasic(map);
            return map;
        }

        private static List<List<Block>> SetBasicMap(int height, int width){
            List<List<Block>> map = new List<List<Block>>();
            for(int i=0; i<height; i++){
                List<Block> row = new List<Block>();
                for(int j=0; j<width; j++){
                    Coordinate position = new Coordinate(i, j);
                    BlockType type = BlockType.Unvisited;
                    Block block = new Block(position, type, false, false);
                    row.Add(block);
                }
                map.Add(row);
            }
            return map;
        }

        public static void SetBlockAsPath(List<List<Block>> map, Coordinate position){
            map[position.X][position.Y].Type = BlockType.Path;
            map[position.X][position.Y].IsMoveble = true;
        }

        private static List<Block> GetSurroundingWalls(List<List<Block>> map, Coordinate position){
            int height = map.Count;
            int width = height > 0 ? map[0].Count : 0;
            List<Block> walls = new List<Block>();
            if(position.X != 0 && map[position.X - 1][position.Y].Type != BlockType.Path){
                walls.Add(new Block(new Coordinate(position.X - 1, position.Y), BlockType.Basic, false, false));
            }
            if(position.X != height - 1 && map[position.X + 1][position.Y].Type != BlockType.Path){
                walls.Add(new Block(new Coordinate(position.X + 1, position.Y), BlockType.Basic, false, false));
            }
            if(position.Y != 0 && map[position.X][position.Y - 1].Type != BlockType.Path){
                walls.Add(new Block(new Coordinate(position.X, position.Y - 1), BlockType.Basic, false, false));
            }
            if(position.Y != width - 1 && map[position.X][position.Y + 1].Type != BlockType.Path){
                walls.Add(new Block(new Coordinate(position.X, position.Y + 1), BlockType.Basic, false, false));
            }
            return walls;
        }

        private static void SetBlocksToMap(List<List<Block>> map, List<Block> blocks){
            int blockLen = blocks.Count;
            for(int i=0; i<blockLen; i++){
                Block block = blocks[i];
                Coordinate position = block.Position;
                map[position.X][position.Y] = block;
            }
        }

        private static void CreateLabyrinth(List<List<Block>> map){
            int height = map.Count;
            int width = height > 0 ? map[0].Count : 0;
            Random r = new Random();
            List<Block> walls = MazeHelper.GetObstacles(map, BlockType.Unvisited).Where(x => x.Type == BlockType.Basic).ToList();
            while(walls.Any()){
                Block randomWall = walls[r.Next(0, walls.Count)];
                if(randomWall.Position.Y != 0){
                    if(map[randomWall.Position.X][randomWall.Position.Y - 1].Type == BlockType.Unvisited && map[randomWall.Position.X][randomWall.Position.Y + 1].Type == BlockType.Path){
                        int surroundingPaths = CountSurroundingPaths(map, randomWall.Position);
                        if(surroundingPaths < 2){
                            SetBlockAsPath(map, randomWall.Position);
                            List<Block> newWalls = GetSurroundingWalls(map, randomWall.Position);
                            walls.AddIfNotExists<Block>(newWalls);
                        }
                        walls.Remove(randomWall);
                        continue;
                    }
                }
                if(randomWall.Position.X != 0){
                    if(map[randomWall.Position.X - 1][randomWall.Position.Y].Type == BlockType.Unvisited && map[randomWall.Position.X + 1][randomWall.Position.Y].Type == BlockType.Path){
                        int surroundingPaths = CountSurroundingPaths(map, randomWall.Position);
                        if(surroundingPaths < 2){
                            SetBlockAsPath(map, randomWall.Position);
                            List<Block> newWalls = GetSurroundingWalls(map, randomWall.Position);
                            walls.AddIfNotExists<Block>(newWalls);
                        }
                        walls.Remove(randomWall);
                        continue;
                    }
                }
                if(randomWall.Position.X != height - 1){
                    if(map[randomWall.Position.X + 1][randomWall.Position.Y].Type == BlockType.Unvisited && map[randomWall.Position.X - 1][randomWall.Position.Y].Type == BlockType.Path){
                        int surroundingPaths = CountSurroundingPaths(map, randomWall.Position);
                        if(surroundingPaths < 2){
                            SetBlockAsPath(map, randomWall.Position);
                            List<Block> newWalls = GetSurroundingWalls(map, randomWall.Position);
                            walls.AddIfNotExists<Block>(newWalls);
                        }
                        walls.Remove(randomWall);
                        continue;
                    }
                }
                if(randomWall.Position.Y != width - 1){
                    if(map[randomWall.Position.X][randomWall.Position.Y + 1].Type == BlockType.Unvisited && map[randomWall.Position.X][randomWall.Position.Y - 1].Type == BlockType.Path){
                        int surroundingPaths = CountSurroundingPaths(map, randomWall.Position);
                        if(surroundingPaths < 2){
                            SetBlockAsPath(map, randomWall.Position);
                            List<Block> newWalls = GetSurroundingWalls(map, randomWall.Position);
                            walls.AddIfNotExists<Block>(newWalls);
                        }
                        walls.Remove(randomWall);
                        continue;
                    }
                }
                walls.Remove(randomWall);
            }
        }

        private static int CountSurroundingPaths(List<List<Block>> map, Coordinate position){
            int paths = 0;
            if(map[position.X - 1][position.Y].Type == BlockType.Path){
                paths++;
            }
            if(map[position.X + 1][position.Y].Type == BlockType.Path){
                paths++;
            }
            if(map[position.X][position.Y - 1].Type == BlockType.Path){
                paths++;
            }
            if(map[position.X][position.Y + 1].Type == BlockType.Path){
                paths++;
            }
            return paths;
        }

        private static void SetAllUnvisitedBlockAsBasic(List<List<Block>> map){
            foreach(List<Block> blocks in map){
                foreach(Block unvisited in blocks.Where(x => x.Type == BlockType.Unvisited)){
                    map[unvisited.Position.X][unvisited.Position.Y].Type = BlockType.Basic;
                }
            }
        }

        private static Coordinate GetStartingPointForLabyrinthCreation(int height, int width){
            Random r = new Random();
            int xPosition = r.Next(0, height);
            int yPosition = r.Next(0, width);
            if(xPosition == 0){
                xPosition = 1;
            }
            if(xPosition == height - 1){
                xPosition = height - 2;
            }
            if(yPosition == 0){
                yPosition = 1;
            }
            if(yPosition == width - 1){
                yPosition = width - 2;
            }
            return new Coordinate(xPosition, yPosition);
        }
    }
}