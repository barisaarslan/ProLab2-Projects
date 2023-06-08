using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezginRobotProjesi.Entity
{
    public class Response<T>
    {
        public bool IsSuccess {get; set;}
        public T Result {get; set;}
        public string ErrorMessage {get; set;} = string.Empty;
    }
}