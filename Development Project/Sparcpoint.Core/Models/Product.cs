using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sparcpoint.Models
{
    public class Product
    {
        public int InstanceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductImageUris { get; set; }
        public string SKU { get; set; }
        public List<Category> Categories { get; set; }
        public List<Attribute> Attributes { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
