using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{
    public class users
    {
        //[Key]
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string hobby { get; set; }

    }
}
