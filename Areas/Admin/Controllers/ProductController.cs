using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaA.Areas.Admin.ViewModels.Product;
using ProniaA.DAL;
using ProniaA.Models;
using ProniaA.Models.Base;

namespace ProniaA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController: Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<GetProductVM> productVMs=
                await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages.Where(pi=>pi.IsPrimary==true))
                .Select(p=>new GetProductVM 
                {
                    Id=p.Id,
                    Name =p.Name,
                    Price=p.Price,
                    CategoryName=p.Category.Name,
                    Image= p.ProductImages[0].Image
                })
                .ToListAsync();
            return View(productVMs);
        }

        public async Task<IActionResult> Create() 
        {
            CreateProductVM createProductVM = new CreateProductVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(createProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM createProductVM) 
        {
            createProductVM.Categories = await _context.Categories.ToListAsync();
            if (!ModelState.IsValid) 
            {

                return View(createProductVM);
            }

            bool result = await _context.Categories.AnyAsync(c => c.Id == createProductVM.CategoryId);
            if (!result) 
            {
                ModelState.AddModelError(nameof(createProductVM.CategoryId),"Category not found");
                return View(createProductVM);
            }

            Product product = new Product
            {
                Name = createProductVM.Name,
                Price = createProductVM.Price,
                Description = createProductVM.Description,
                CategoryId = createProductVM.CategoryId.Value,
                SKU = createProductVM.SKU
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) 
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) 
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            UpdateProductVM updateProductVM = new UpdateProductVM
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                SKU = product.SKU,
                Categories = await _context.Categories.ToListAsync(),
                CategoryId = product.CategoryId
            };
            return View(updateProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM updateProductVM)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            updateProductVM.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) 
            {
                return View(updateProductVM);
            }

            bool result = await _context.Categories.AnyAsync(c => c.Id == updateProductVM.CategoryId);

            if (!result)
            {
                ModelState.AddModelError(nameof(Category.Id), "Category not found");
                return View(result);
            }
            product.Name = updateProductVM.Name;
            product.Price = updateProductVM.Price;
            product.Description = updateProductVM.Description;
            product.SKU = updateProductVM.SKU;
            product.CategoryId = updateProductVM.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            GetProductVM getProductVM = new GetProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                Image = product.ProductImages.FirstOrDefault()?.Image
            };

            return View(getProductVM);
        }
    }
}
