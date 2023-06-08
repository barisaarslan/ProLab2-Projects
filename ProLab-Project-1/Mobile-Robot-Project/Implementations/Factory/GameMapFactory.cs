using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Implementations.Map;
using GezginRobotProjesi.Interfaces;

namespace GezginRobotProjesi.Implementations.Factory
{
    public class GameMapFactory : IGameMapFactory
    {
        public GameMap CreateMap(List<List<Block>> map)
        {
            return new ConsoleMap(map);
        }
    }
}