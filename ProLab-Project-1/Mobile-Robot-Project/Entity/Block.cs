using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity.Enums;

namespace GezginRobotProjesi.Entity
{
    public class Block
    {
        public Coordinate Position {get; set;}
        public BlockType Type {get; set;}
        public bool IsMoveble {get; set;}
        public bool IsVisible {get; set;}

        public Block(Coordinate position, BlockType blockType, bool isMoveble, bool isVisible) {
            this.Position = position;
            this.Type = blockType;
            this.IsMoveble = isMoveble;
            this.IsVisible = isVisible;
        }

        public Block(Coordinate position, BlockType type, bool isMoveble) {
            this.Position = position;
            this.Type = type;
            this.IsMoveble = isMoveble;
            this.IsVisible = false;
        }

        public Block(Coordinate position, int type, bool isMoveble){
            this.Position = position;
            this.Type = (BlockType)type;
            this.IsMoveble = isMoveble;
        }
    }
}