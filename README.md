# Generic Repository UnitOfWork

Ther are many repository out there. From many of them meets our requirement or not. So I have created a Complete Repository For any CRUD application.

# Prerequisites

This project using MVC Core 2.1 along with visual studio
Download it for [here](https://www.microsoft.com/net/download)

# Installing

After creating Project Add DBContext class
If you don't know how to add DbContext:[How to Cteate Application DbContext](https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db)

Now flow the steps carefully

1. Add Application DbContext
2. Add IRepository.cs
3. Add Repository.cs
4. Add IUnitOfWork.cs
5. Add UnitOfWork.cs

[Repository Location](GenericRepositoryUnitOfWork/Repository)

###### StartUp.cs

```

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
```

###### Product.cs (Model)

```
 public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
```

###### Brand.cs (Model)

```
 public class Brand
    {
          public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
```

Initialize in every controller where you making Database Call

```
private readonly IUnitOfWork _unitOfWork;
```

)

###### ProductController.cs

```
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
```

##### Note:

- Repository should not contain any save method (Personal advice)
