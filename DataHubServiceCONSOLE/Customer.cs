using System;
using System.Collections.Generic;

namespace DataHubService
{
    public partial class Customer
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
