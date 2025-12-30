using ProniaA.Models;

namespace ProniaA.ViewModels
{
    public class DetailVM
    {
        public Product Product { get; set; } = new Product();
        public List<Product> RelatedProducts { get; set; } = new List<Product>();


    }
}
