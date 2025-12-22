using Microsoft.AspNetCore.Mvc;
using ProniaA.Models;
using ProniaA.ViewModels;
using ProniaA.DAL;

namespace ProniaA.Controllers
{
    public class HomeController : Controller

    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            //List<Slide> slides = new List<Slide>
            //{
            //    new Slide
            //    {
            //        Id = 1,
            //        Title = "Summer Sale",
            //        Discount = "30% Off",
            //        Description = "Get ready for summer with our exclusive sale!",
            //        Image = "1-1-524x617.png",
            //        Order = 2
            //    },
            //    new Slide
            //    {
            //        Id = 2,
            //        Title = "Winter Collection",
            //        Discount = "25% Off",
            //        Description = "Stay warm and stylish with our winter collection.",
            //        Image = "1-2-524x617.png",
            //        Order = 3
            //    },
            //    new Slide
            //    {
            //        Id = 3,
            //        Title = "Spring Arrivals",
            //        Discount = "20% Off",
            //        Description = "Freshen up your wardrobe with our spring arrivals.",
            //        Image = "1-5-270x300.jpg",
            //        Order = 1
            //    }
            //};
            List<Product> products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Stylish Shirt",
                    Image = "1-2-270x300.jpg",
                    Price = 29.99,
                    Order = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Casual Pants",
                    Image = "1-1-270x300.jpg",
                    Price = 49.99,
                    Order = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Elegant Dress",
                    Image = "1-3-270x300.jpg",
                    Price = 79.99,
                    Order = 3
                }
            };
            //List<Blog> blogs = new List<Blog>
            //{
            //    new Blog
            //    {
            //        Admin = "Admin1",
            //        Date = "2023-10-01",
            //        Title = "Fashion Trends 2023",
            //        Text = "Discover the latest fashion trends for 2023 and stay ahead of the curve.",
            //        Image = "1-1-310x220.jpg"
            //    },
            //    new Blog
            //    {
            //        Admin = "Admin2",
            //        Date = "2023-11-15",
            //        Title = "Winter Wardrobe Essentials",
            //        Text = "Learn about the must-have items for your winter wardrobe this season.",
            //        Image = "1-2-310x220.jpg"
            //    },
            //    new Blog
            //    {
            //        Admin = "Admin3",
            //        Date = "2023-12-05",
            //        Title = "Accessorizing Tips",
            //        Text = "Enhance your outfits with these expert accessorizing tips.",
            //        Image = "1-3-310x220.jpg"
            //    }
            //};

            var a = _context.Slides.ToList();
                var b = _context.Blogs.ToList();

            HomeVM homeVM = new HomeVM()
            {
                Products = products,
                Slides = a,
                Blogs = b
            };
            return View(homeVM);
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
