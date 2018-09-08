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
###### Product.cs
```
 public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
```

###### Brand.cs
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

###### ProductController.cs
```
 public class ProductController
    {
         public IActionResult AllProducts(){
          var model = _unitOfWork.Repository<Brand>().GetAll();
            //do something if you want

            return View(model);
         }
         
          public IActionResult AllProductsWithInclude(){
          var model = _unitOfWork.Repository<Brand>().GetAllInclude(b=>b.Brand);
            //do something if you want

            return View(model);
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
            return RedirectToAction("BrandIndex");
        }
        
         public async Task<IActionResult> AddProductAsync()
        {
            vif (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Something wrong");
                return View(product);
            }

            await _unitOfWork.Repository<Product>().InsertAsync(product);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> SearchByName(string name){
        var model=await _unitOfWork.Repository<Product>().FindAllAsync(x=>x.Name.Contains(name));
        
        return View(model);
        }
    }
```
