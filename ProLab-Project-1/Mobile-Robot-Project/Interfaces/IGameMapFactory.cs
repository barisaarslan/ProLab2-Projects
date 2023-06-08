using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;

namespace GezginRobotProjesi.Interfaces
{
    public interface IGameMapFactory
    {
        public GameMap CreateMap(List<List<Block>> map);
    }
}