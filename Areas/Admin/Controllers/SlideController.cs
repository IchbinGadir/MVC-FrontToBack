using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaA.Areas.Admin.ViewModels.Slide;
using ProniaA.DAL;
using ProniaA.Models;
using ProniaA.Models.Base;
using ProniaA.Utilities.Enums;
using ProniaA.Utilities.Extensions;





namespace ProniaA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
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
            string result = Guid.NewGuid().ToString();
            return Content(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM createSlideVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createSlideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Siz uygun formatda file elave etmirsiz");
                return View();
            }
            if (createSlideVM.Photo.ValidateSize(FileSize.KB, 20))
            {
                ModelState.AddModelError("Photo", "File olcusu 2MB-dan boyuk ola bilmez");
                return View();
            }


            Slide slide = new Slide()
            {
                Title = createSlideVM.Title,
                Description = createSlideVM.Description,
                Discount = createSlideVM.Discount,
                Order = createSlideVM.Order,
                Image = await createSlideVM.Photo.CreateFile(_env.WebRootPath, "assets", "images", "website-images")
            };



            //return Content(slide.Photo.FileName + " " + slide.Photo.ContentType + " " + slide.Photo.Length);
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            System.IO.File.Delete(Path.Combine(_env.WebRootPath, "assets", "images", "website-images", slide.Image));


            slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Slides.Remove(slide);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide == null)
            {
                return NotFound();
            }
            UpdateSlideVM updateSlideVM = new UpdateSlideVM()
            {
                Title = slide.Title,
                Description = slide.Description,
                Discount = slide.Discount,
                Order = slide.Order,
                Image = slide.Image
            };
            return View(updateSlideVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVM updateSlideVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateSlideVM);
            }

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (updateSlideVM.Photo is not null)
            {
                if (!updateSlideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(updateSlideVM.Photo), "invalide type");
                    return View(updateSlideVM);
                }
                if (updateSlideVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(updateSlideVM.Photo), "invalide size");
                    return View(updateSlideVM);
                }
                string filename = await updateSlideVM.Photo.CreateFile(_env.WebRootPath, "assets", "images", "website-images");
                slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

                slide.Image = filename;
            }

            slide.Title = updateSlideVM.Title;
            slide.Description = updateSlideVM.Description;
            slide.Discount = updateSlideVM.Discount;
            slide.Order = updateSlideVM.Order;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) return NotFound();

            GetSlideVM getSlideVM = new GetSlideVM
            {
                Title = slide.Title,
                Description = slide.Description,
                Image = slide.Image
            };

            return View(getSlideVM);
        }
    }
}
