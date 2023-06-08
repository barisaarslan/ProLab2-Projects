using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;

namespace GezginRobotProjesi.Interfaces
{
    public interface IPlayerRobotFactory
    {
        public PlayerRobot CreateInstance(ProblemType problemType, Coordinate position);
    }
}