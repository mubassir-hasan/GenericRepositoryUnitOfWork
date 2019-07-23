using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepositoryUnitOfWork.Models;
using GenericRepositoryUnitOfWork.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GenericRepositoryUnitOfWork.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult BrandIndex()
        {
            var model = _unitOfWork.Repository<Brand>().GetAllInclude(x => x.Products);
            //do something if you want

            return View(model);
        }
        
        public IActionResult AddBrand()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddBrand(Brand model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something wrong");
                return View(model);
            }

            _unitOfWork.Repository<Brand>().Insert(model);
            //save new data to database
            _unitOfWork.Commit();
            return RedirectToAction("BrandIndex");
        }

        public IActionResult AddProduct()
        {
            var model=new Product();
            //brand list for dropdown
            var dbBrand = _unitOfWork.Repository<Brand>().GetAll();

            ViewBag.BrandList = dbBrand.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Something wrong");
                return View(product);
            }

            _unitOfWork.Repository<Product>().Insert(product);
            //save asynchronously to database
            await _unitOfWork.CommitAsync();
            return RedirectToAction("ProductList");
        }


        public IActionResult ProductList()
        {
            var model = _unitOfWork.Repository<Product>().GetAllInclude(x => x.Brand);

            return View(model);
        }
    }
}