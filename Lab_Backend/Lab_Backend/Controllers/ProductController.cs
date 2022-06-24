using Lab_Backend.Data.Services;
using Lab_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _he;

        public ProductController(IProductService service, IWebHostEnvironment he)
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
            var products = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", products);
        }

        //GET: Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetDropdownsValues();
            ViewBag.ProductBrands = new SelectList(productDropdownsData.ProductBrands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Create
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (image != null)
            {
                //Save image to wwwroot/Images
                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                string path = Path.Combine(_he.WebRootPath + "/Images/", fileName);
                await image.CopyToAsync(new FileStream(path, FileMode.Create));
                product.Image = "/Images/" + fileName;
            }
            else
                product.Image = "/Images/noimage.jpg";

            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var productDropdownsData = await _service.GetDropdownsValues();
            ViewBag.ProductBrands = new SelectList(productDropdownsData.ProductBrands, "Id", "Name");

            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return View("NotFound");

            return View(product);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile image)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (image != null)
            {
                //Save image to wwwroot/images
                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                string path = Path.Combine(_he.WebRootPath + "/Images/", fileName);
                await image.CopyToAsync(new FileStream(path, FileMode.Create));
                product.Image = "/Images/" + fileName;
            }
            else
                product.Image = "/Images/noimage.jpg";

            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Details
        public async Task<IActionResult> Details(int id)
        {
            var productDropdownsData = await _service.GetDropdownsValues();
            ViewBag.ProductBrands = new SelectList(productDropdownsData.ProductBrands, "Id", "Name");

            var productDetails = await _service.GetByIdAsync(id);
            return View(productDetails);
        }

        //GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var productDropdownsData = await _service.GetDropdownsValues();
            ViewBag.ProductBrands = new SelectList(productDropdownsData.ProductBrands, "Id", "Name");

            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return View("NotFound");

            return View(product);
        }

        //POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
