using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;

namespace GezginRobotProjesi.Helpers
{
    public static class Bfs {
        private static int[] row = {-1, 0, 0, 1};
        private static int[] column = {0, -1, 1, 0};

        private static bool isSafeToGo(Coordinate nextPosition, List<List<bool>> map){
            int height = map.Count;
            int width = height > 0 ? map[0].Count : 0;
            return nextPosition.X >= 0 && nextPosition.Y >= 0 && nextPosition.X < height && nextPosition.Y < width && map[nextPosition.X][nextPosition.Y];
        }

        private static void findPath(CoordinateNode node, List<Coordinate> shortestRoute){
            if(node != null){
                findPath(node.ParentNode, shortestRoute);
                shortestRoute.Add(node.Position);
            }
        }

        public static List<Coordinate> FindShortestRoute(List<List<bool>> map, Coordinate source, Coordinate destination){
            List<Coordinate> shortestRoute = new List<Coordinate>();
            List<List<bool>> visited = new List<List<bool>>();
            int height = map.Count;
            int width = height > 0 ? map[0].Count : 0;
            for(int i=0; i<height; i++){
                List<bool> depth = new List<bool>();
                for(int j=0; j<width; j++){
                    depth.Add(false);
                }
                visited.Add(depth);
            }
            visited[source.X][source.Y] = true;
            Queue<CoordinateNode> queue = new Queue<CoordinateNode>();
            CoordinateNode firstNode = new CoordinateNode(source, null);
            queue.Enqueue(firstNode);

            while(queue.Any()){
                CoordinateNode head = queue.Dequeue();
                if(head.Position.IsEqual(destination)){
                    findPath(head, shortestRoute);
                    return shortestRoute;
                }
                for(int i=0; i<4; i++){
                    Coordinate nextPosition = new Coordinate(head.Position.X + row[i], head.Position.Y + column[i]);
                    if(isSafeToGo(nextPosition, map)){
                        CoordinateNode nextNode = new CoordinateNode(nextPosition, head);
                        if(!visited[nextPosition.X][nextPosition.Y]){
                            queue.Enqueue(nextNode);
                            visited[nextPosition.X][nextPosition.Y] = true;
                        }
                    }
                }
            }
            return shortestRoute;
        }

    }
}