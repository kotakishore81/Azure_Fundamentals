using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTangyFunction.Data
{
    public class GroceryItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
