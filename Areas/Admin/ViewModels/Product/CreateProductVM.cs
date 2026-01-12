using ProniaA.Models.Base;
using System.Globalization;

namespace ProniaA.Areas.Admin.ViewModels.Product
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; }

    }
}
