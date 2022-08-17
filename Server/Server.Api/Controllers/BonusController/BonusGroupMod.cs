using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Controllers.BonusController
{
    public class BonusGroupMod
    {
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public decimal[] jine = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}
