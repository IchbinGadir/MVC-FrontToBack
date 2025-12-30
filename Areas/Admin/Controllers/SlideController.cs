using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaA.DAL;
using ProniaA.Models;
using System.Threading.Tasks;


namespace ProniaA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController: Controller
    {
        public readonly AppDbContext _context;

        public SlideController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() 
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            return View(slides);
        }

        public IActionResult Test() 
        {
            string result= Guid.NewGuid().ToString();
            return Content(result);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slide.Photo.ContentType.Contains("image/")) 
            {
                ModelState.AddModelError("Photo", "Siz uygun formatda file elave etmirsiz");
                return View();
            }
            if (slide.Photo.Length>2*1024*1024) 
            {
                ModelState.AddModelError("Photo", "File olcusu 2MB-dan boyuk ola bilmez");
            }

            string fileName = String.Concat(Guid.NewGuid().ToString(), slide.Photo.FileName);

            string path = "C:\\Users\\user\\Desktop\\APA201\\ProniaA\\wwwroot\\assets\\images\\website-images\\" + fileName;
            FileStream fileStrem = new(path, FileMode.Create);
            await slide.Photo.CopyToAsync(fileStrem);
            fileStrem.Close();
            slide.Image = fileName;

            //return Content(slide.Photo.FileName + " " + slide.Photo.ContentType + " " + slide.Photo.Length);
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
