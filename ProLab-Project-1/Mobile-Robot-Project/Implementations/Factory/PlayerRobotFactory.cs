using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Implementations.Robot;
using GezginRobotProjesi.Interfaces;

namespace GezginRobotProjesi.Implementations.Factory
{
    public class PlayerRobotFactory : IPlayerRobotFactory
    {
        public PlayerRobot CreateInstance(ProblemType problemType, Coordinate position)
        {
            if(problemType == ProblemType.Problem1){
                return new WallFollowerRobot(WallFollowerRule.RightHand, position);
            }
            return new WallFollowerRobot(WallFollowerRule.LeftHand, position);
        }
    }
}