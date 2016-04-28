using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaHive.Contract.Models
{
    public class LevelModel
    {
        public Guid Id { get; set; }

        public Guid Parent { get; set; }
        public Guid Child { get; set; }
    }
}
