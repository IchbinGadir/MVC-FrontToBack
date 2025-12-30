using ProniaA.Models;


namespace ProniaA.ViewModels
{
    public class HomeVM
    {
        public List<Slide> Slides { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Shop> Shops { get; set; }
    }
}
