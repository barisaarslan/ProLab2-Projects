using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Helpers;

namespace GezginRobotProjesi.Helpers
{
    public static class MazeHelper
    {
        /// <summary>
        /// Metin olarak alınan harita matrisini 2d blok objesine çevirir ve engel bloklarının bazılarını rastgele bir şekilde yürünebilir hale getirir.
        /// </summary>
        /// <param name="mapAsString"></param>
        /// <returns></returns>
        public static List<List<Block>> SetMap(string mapAsString){
            List<List<Block>> map = new List<List<Block>>();
            string[] mapLines = mapAsString.Split('\n');
            int mapHeight = mapLines.Length;
            for(int i=0; i<mapHeight; i++){
                char[] mapDepth = mapLines[i].ToCharArray();
                int mapWidth = mapDepth.Length;
                List<Block> column = new List<Block>();
                for(int j=0; j<mapWidth; j++){
                    Coordinate position = new Coordinate(i, j);
                    BlockType type = GetBlockType(mapDepth[j]);
                    Block block = new Block(position, type, type == BlockType.Path, false);
                    column.Add(block);
                }
                map.Add(column);
            }
            return RandomizeObstacle(map);
        }

        /// <summary>
        /// Verilen karakteri ilgili blok tipine çevirir bilinen bir blok tipi değil ise yürünebilir yol olarak kabul eder.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static BlockType GetBlockType(char c){
            if(c == '0'){
                return BlockType.Path;
            }
            if(c == '1'){
                return BlockType.Basic;
            }
            if(c == '2'){
                return BlockType.Intermediary;
            }
            if(c == '3'){
                return BlockType.Advanced;
            }
            return BlockType.Path;
        }

        /// <summary>
        /// Verilen tüm haritadaki engel bloklarını alır ve ilgili engel blok grubunun en az 1 en fazla 3 tanesini yürünebilir blok haline getirir.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private static List<List<Block>> RandomizeObstacle(List<List<Block>> map){
            List<Block> obstacles = GetObstacles(map, BlockType.Basic);
            List<Coordinate> visited = new List<Coordinate>();
            int obstacleLen = obstacles.Count;
            for(int i=0; i<obstacleLen; i++) {
                if(visited.Any(v => v.X == obstacles[i].Position.X && v.Y == obstacles[i].Position.Y)){
                    continue;
                }
                Random r = new Random();
                int movebleBlockCount = r.Next(1, 4);
                
                List<Coordinate> gonnaBeVisited = GetObstacleGroup(obstacles[i]);
                visited.AddRange(gonnaBeVisited);
                List<Coordinate> turnToMoveble = gonnaBeVisited.OrderBy(x => r.Next()).Take(movebleBlockCount).ToList();
                for(int j=0; j<movebleBlockCount; j++){
                    map[turnToMoveble[j].X][turnToMoveble[j].Y].IsMoveble = true;
                }
            }
            return map;
        }

        /// <summary>
        /// Verilen engel bloğunu başlangıç bloğu kabul ederek engel blok grubunun coordinatlarını döner. 
        /// </summary>
        /// <param name="currentBlock"></param>
        /// <returns></returns>
        private static List<Coordinate> GetObstacleGroup(Block currentBlock){
            List<Coordinate> result = new List<Coordinate>();
            int currentX = currentBlock.Position.X;
            int currentY = currentBlock.Position.Y;
            int maxX = 0;
            int maxY = 0;
            if(currentBlock.Type == BlockType.Advanced){
                maxX = currentX + 3;
                maxY = currentY + 3;
            }
            if(currentBlock.Type == BlockType.Intermediary){
                maxX = currentX + 2;
                maxY = currentY + 2;
            }
            List<Block> obstacleGroup = new List<Block>();
            for(int x = currentX; x < maxX; x++) {
                for(int y = currentY; y < maxY; y++) {
                    result.Add(new Coordinate(x, y));
                }
            }
            return result;
        }

        /// <summary>
        /// Haritadaki 2 veya 3 tipindeki blokları 1d array olarak döner
        /// </summary>
        /// <param name="map">Tüm harita</param>
        /// <returns></returns>
        public static List<Block> GetObstacles(List<List<Block>> map, BlockType type) {
            List<Block> obstacles = new List<Block>();
            int mapHeight = map.Count;
            for(int i=0; i<mapHeight; i++) {
                List<Block> obstacleColumn = map[i].Where(x => x.IsMoveble == false && x.Type != type && x.Type != BlockType.Path && x.Type != BlockType.Unvisited).ToList();
                obstacles.AddRange(obstacleColumn);
            }
            return obstacles;
        }
    }
}