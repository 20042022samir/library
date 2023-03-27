using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Base
{
    internal abstract class Basee
    {
        public string Name { get; set; }

        public string AuthorName { get; set; }

        public int PageCount { get; set; }

        public string ExtraInfo { get; set; }

        public int ID { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsUptated { get; set; }
    }
}
