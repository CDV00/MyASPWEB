using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Detail { get; set; }
        public int Number { get; set; }
        public float Price { get; set; }
        public float PriceSale { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public string Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Updated_by { get; set; }
        public int Status { get; set; }
    }
}
