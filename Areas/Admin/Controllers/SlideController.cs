using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaA.DAL;
using ProniaA.Models;
using ProniaA.Utilities.Enums;
using ProniaA.Utilities.Extensions;





namespace ProniaA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController: Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

            if (!ProniaA.Utilities.Extensions.Validator.ValidateType(slide.Photo, "image/")) 
            {
                ModelState.AddModelError("Photo", "Siz uygun formatda file elave etmirsiz");
                return View();
            }
            if (slide.Photo.ValidateSize(FileSize.KB,20)) 
            {
                ModelState.AddModelError("Photo", "File olcusu 2MB-dan boyuk ola bilmez");
                return View();
            }



            //string path = "C:\\Users\\user\\Desktop\\APA201\\ProniaA\\wwwroot\\assets\\images\\website-images\\" + fileName;
            //string path = _env.WebRootPath + "assets"+"images"+"website-images" + fileName;


            //string fileName = String.Concat(Guid.NewGuid().ToString(), slide.Photo.FileName);
            //string path = Path.Combine(_env.WebRootPath, "assets", "images", "website-images", fileName);
            //FileStream fileStrem = new(path, FileMode.Create);
            //await slide.Photo.CopyToAsync(fileStrem);
            //fileStrem.Close();


            slide.Image = await slide.Photo.CreateFile(_env.WebRootPath, "assets", "images", "website-images");

            //return Content(slide.Photo.FileName + " " + slide.Photo.ContentType + " " + slide.Photo.Length);
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
