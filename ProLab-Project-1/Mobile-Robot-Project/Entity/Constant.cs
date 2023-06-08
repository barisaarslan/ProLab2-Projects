using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity.Enums;

namespace GezginRobotProjesi.Entity
{
    public static class Constant
    {
        public static List<string> MapUrls = new List<string> {
            "http://bilgisayar.kocaeli.edu.tr/prolab2/url1.txt",
            "http://bilgisayar.kocaeli.edu.tr/prolab2/url2.txt"
        };

        public static MapSize MinimumSize = new MapSize(10, 10);
        public static MapSize MaximumSize = new MapSize(100, 100);
    }
}