using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Entity;

namespace GezginRobotProjesi.Helpers
{
    public static class ListExtensions{
        public static void AddIfNotExists<T>(this List<T> current, List<T> entries) {
            foreach(T entry in entries){
                if(!current.Contains(entry)){
                    current.Add(entry);
                }
            }
        }
    }

}