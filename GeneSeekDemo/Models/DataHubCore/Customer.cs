using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneSeekDemo.Models.DataHubCore
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        // public decimal Price { get; set; }
    }
}
