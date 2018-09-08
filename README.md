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

## StartUp.cs
'''

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
'''
