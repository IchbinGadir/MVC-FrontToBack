using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaA.DAL;
using ProniaA.ViewModels;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM homeVM = new HomeVM
        {
            Slides = await _context.Slides.OrderBy(s => s.Order).Take(2).ToListAsync(),

            Products = await _context.Products.Include(p => p.ProductImages).ToListAsync(),

            Blogs = await _context.Blogs.ToListAsync()
        };

        return View(homeVM);
    }
}
