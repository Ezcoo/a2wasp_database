using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace A2WaspDatabase
{
    public class Player
    {
        public int id { get; set; }
        public int totalScore { get; set; }

        public int ticks { get; set; }
    }
}
