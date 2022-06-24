using Lab_Backend.Data.Services;
using Lab_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IProductBrandService _service;
        private readonly IWebHostEnvironment _he;

        public ProductBrandController(IProductBrandService service, IWebHostEnvironment he)
        {
            _service = service;
            _he = he;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var productBrands = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = productBrands.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", productBrands);
        }

        //GET: Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Create
        public async Task<IActionResult> Create(ProductBrand productBrand, IFormFile logo)
        {
            if (!ModelState.IsValid)
                return View(productBrand);

            if (logo != null)
            {
                //Save image to wwwroot/images
                string fileName = Path.GetFileNameWithoutExtension(logo.FileName);
                string extension = Path.GetExtension(logo.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                string path = Path.Combine(_he.WebRootPath + "/Images/", fileName);
                await logo.CopyToAsync(new FileStream(path, FileMode.Create));
                productBrand.Logo = "/Images/" + fileName;
            }
            else
                productBrand.Logo = "/Images/noimage.jpg";

            await _service.AddAsync(productBrand);
            return RedirectToAction(nameof(Index));
        }

        //GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var productBrand = await _service.GetByIdAsync(id);

            if (productBrand == null)
                return View("NotFound");

            return View(productBrand);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductBrand productBrand, IFormFile logo)
        {
            if (!ModelState.IsValid)
                return View(productBrand);

            if (logo != null)
            {
                //Save image to wwwroot/Images
                string fileName = Path.GetFileNameWithoutExtension(logo.FileName);
                string extension = Path.GetExtension(logo.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                string path = Path.Combine(_he.WebRootPath + "/Images/", fileName);
                await logo.CopyToAsync(new FileStream(path, FileMode.Create));
                productBrand.Logo = "/Images/" + fileName;
            }
            else
                productBrand.Logo = "/Images/noimage.jpg";

            await _service.UpdateAsync(id, productBrand);
            return RedirectToAction(nameof(Index));
        }

        //GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var productBrand = await _service.GetByIdAsync(id);

            if (productBrand == null)
                return View("NotFound");

            return View(productBrand);
        }

        //POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var productBrand = await _service.GetByIdAsync(id);

            if (productBrand == null)
                return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
