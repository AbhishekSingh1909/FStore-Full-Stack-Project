using fStore.Business;
using fStore.Core;

namespace fStore.WEBAPI;

public class SeedingData
{
    private static Guid adminGuid = Guid.NewGuid();
    private static Guid customerGuid1 = Guid.NewGuid();
    private static Guid customerGuid2 = Guid.NewGuid();
    private static Guid customerGuid3 = Guid.NewGuid();
    private static Guid customerGuid4 = Guid.NewGuid();
    private static Guid customerGuid5 = Guid.NewGuid();

    public static List<User> GetUser()
    {
        PasswordService.HashPassword("admin123", out string adminhashedPassword, out byte[] adminsalt);
        PasswordService.HashPassword("12345", out string customerhashedPassword, out byte[] usersalt);

        return new List<User>
        {
           new User
           {
            Id =adminGuid,
            Name = "Admin",
            Email = "admin@mail.com",
            Password = adminhashedPassword,
            Avatar = "https://i.imgur.com/LDOO4Qs.jpg",
            Role = Role.Admin,
            Salt = adminsalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           },

           new User
           {
            Id = customerGuid1,
            Name = "Jane Doe",
            Email = "jane@mail.com",
            Password = customerhashedPassword,
            Avatar = "https://i.imgur.com/DTfowdu.jpg",
            Role = Role.Customer,
            Salt = usersalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           },
           new User
           {
            Id = customerGuid2,
            Name = "kiara",
            Email = "kiara@mail.com",
            Password = customerhashedPassword,
            Avatar = "https://picsum.photos/800",
            Role = Role.Customer,
            Salt = usersalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           },
           new User
           {
            Id = customerGuid3,
            Name = "Sara James",
            Email = "sara@mail.com",
            Password = customerhashedPassword,
            Avatar = "https://api.escuelajs.co/api/v1/files/8c21.jpg",
            Role = Role.Customer,
            Salt = usersalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           },
           new User
           {
            Id = customerGuid4,
            Name = "Maria",
            Email = "maria@mail.com",
            Password = customerhashedPassword,
            Avatar = "https://api.lorem.space/image/face?w=640&h=480&r=867",
            Role = Role.Customer,
            Salt = usersalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           },
           new User
           {
            Id = customerGuid5,
            Name = "John Smith",
            Email = "smith@mail.com",
            Password = customerhashedPassword,
            Avatar = "https://api.escuelajs.co/api/v1/files/4637.jpg",
            Role = Role.Customer,
            Salt = usersalt,
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
           }
        };
    }

    public static List<Address> GetAddresses()
    {
        return new List<Address>
        {
             new Address
             {
                Id = Guid.NewGuid(),
                HouseNumber = "A 1",
                Street = "Peltokatu",
                City = "Oulu",
                Country = "Finland",
                PostCode = "50610",
                UserId = customerGuid1,
                CreatedAt= DateTime.Now,
               UpdatedAt = DateTime.Now
              },
              new Address
             {
                Id = Guid.NewGuid(),
                HouseNumber = "B 17",
                Street = "Peltokatu",
                City = "Oulu",
                Country = "Finland",
                PostCode = "50610",
                UserId = customerGuid2,
                CreatedAt= DateTime.Now,
               UpdatedAt = DateTime.Now
              },
              new Address
             {
                Id = Guid.NewGuid(),
                HouseNumber = "A 7",
                Street = "Laamannintie",
                City = "Oulu",
                Country = "Finland",
                PostCode = "90650",
                UserId = customerGuid3,
                CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
              },
              new Address
             {
                Id = Guid.NewGuid(),
                HouseNumber = "8 B",
                Street = "Laamannintie",
                City = "Oulu",
                Country = "Finland",
                PostCode = "90650",
                UserId = customerGuid4,
                CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
              },
              new Address
             {
                Id = Guid.NewGuid(),
                HouseNumber = "3 A",
                Street = "Laamannintie",
                City = "Oulu",
                Country = "Finland",
                PostCode = "90650",
                UserId = customerGuid5,
                CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
              }
        };
    }
    private static Guid categoryGuid1 = Guid.NewGuid();
    private static Guid categoryGuid2 = Guid.NewGuid();
    private static Guid categoryGuid3 = Guid.NewGuid();
    private static Guid categoryGuid4 = Guid.NewGuid();
    private static Guid categoryGuid5 = Guid.NewGuid();
    private static Guid categoryGuid6 = Guid.NewGuid();
    private static Guid categoryGuid7 = Guid.NewGuid();
    private static Guid categoryGuid8 = Guid.NewGuid();
    private static Guid categoryGuid9 = Guid.NewGuid();
    private static Guid categoryGuid10 = Guid.NewGuid();
    public static List<Category> GetCategories()
    {
        return new List<Category>
       {
         new Category
            {
                Id = categoryGuid1,
                Name = "Clothes",
                ImageUrl = "https://i.imgur.com/QkIa5tT.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid2,
                Name = "Electronics",
                ImageUrl = "https://i.imgur.com/ZANVnHE.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid3,
                Name = "Furniture",
                ImageUrl = "https://i.imgur.com/Qphac99.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid4,
                Name = "Books",
                ImageUrl = "https://api.lorem.space/image/book?w=150&h=220",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid5,
                Name = "Sports",
                ImageUrl = "https://i.imgur.com/BG8J0Fj.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
             new Category
            {
                Id = categoryGuid6,
                Name = "Home & Kitchen",
                ImageUrl = "https://i.imgur.com/QkIa5tT.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid7,
                Name = "Health",
                ImageUrl = "https://i.imgur.com/Qphac99.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid8,
                Name = "Shoes",
                ImageUrl = "https://i.imgur.com/ZANVnHE.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid9,
                Name = "Beauty",
                ImageUrl = "https://i.imgur.com/BG8J0Fj.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Category
            {
                Id = categoryGuid10,
                Name = "Toys",
                ImageUrl = "https://i.imgur.com/Qphac99.jpeg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },

       };
    }

    private static Product product1 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextInt64(10, 200) * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product2 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product3 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product4 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product5 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product6 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product7 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product8 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product9 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product10 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product11 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product12 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product13 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product14 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product15 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product16 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid5,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product17 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product18 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product19 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product20 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product21 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product22 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid5,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product23 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product24 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product25 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product26 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product27 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product28 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product29 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product30 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product31 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product32 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product33 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product35 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product36 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product37 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product38 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now

    };
    private static Product product39 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product40 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product41 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product42 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product43 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product44 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product45 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid10,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product46 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid9,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product47 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid7,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product48 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid6,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product49 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid6,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product50 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid7,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product51 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid8,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product52 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid9,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product53 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid10,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product54 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid1,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product55 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product56 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product57 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product58 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid6,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product59 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid7,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product60 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid8,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product61 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product62 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid7,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product63 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid4,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product64 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid8,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product65 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid6,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product67 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid2,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product68 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid8,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product69 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid3,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product70 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid8,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product71 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid5,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };
    private static Product product72 = new Product
    {
        Id = Guid.NewGuid(),
        Title = GetRandomProductTitle(),
        Description = GetRandomProductDescription(),
        Inventory = new Random().Next(1, 100),
        Price = Math.Round((decimal)new Random().NextDouble() * 100, 2),
        CategoryId = categoryGuid5,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };

    // Add logic to generate random or realistic product titles
    private static string GetRandomProductTitle()
    {

        string[] productTitles = { "Laptop", "Smartphone", "TV", "Coffee Maker", "Running Shoes", "Bookshelf", "Headphones", "Digital Camera", "Backpack", "Gaming Console" };
        return productTitles[new Random().Next(productTitles.Length)];
    }

    // Add logic to generate random or realistic English product descriptions
    private static string GetRandomProductDescription()
    {

        string[] productDescriptions = {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
            "Sed ac odio eu orci luctus iaculis.",
            "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.",
            "Praesent dapibus justo ut felis dignissim, a tristique velit varius.",
            "Quisque vestibulum neque nec efficitur tincidunt.",
            "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.",
            "Integer ut velit sit amet velit luctus varius.",
            "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.",
            "Fusce eu libero eget arcu fermentum hendrerit id ut elit.",
            "Proin vel tortor vel augue accumsan interdum id in justo."
        };
        return productDescriptions[new Random().Next(productDescriptions.Length)];
    }
    public static List<Product> GetProducts()
    {
        return new List<Product> {
            product1,product2,product3,product4,product5,product6,product7,
            product8,product9,product10,product11,product12,product13,product14,
            product15,product16,product17,product18,product19,product20,product21,
            product22,product23,product24,product25,product26,product27,product28,
            product29,product30,product31,product32,product33,product35,product36,
            product37,product38,product39,product40,product41,product42,product43,
            product44,product45,product46,product47,product48,product49,product50,
            product51,product52,product53,product54,product55,product56,product57,
            product58,product59,product60,product61,product62,product63,product64,
            product65,product67,product68,product69,product70,product71,product72
        };
    }

    // Add logic to generate random  product images
    private static string GetRandomImages()
    {
        string[] Images =
        {
        "https://placeimg.com/640/480/any",
        "https://i.imgur.com/QkIa5tT.jpeg",
        "https://placeimg.com/640/480/any",
        "https://i.imgur.com/QkIa5tT.jpeg",
        "https://i.imgur.com/WwKucXb.jpeg",
        "https://i.imgur.com/JQRGIc2.jpeg",
        "https://i.imgur.com/kKc9A5p.jpeg",
        "https://i.imgur.com/NLn4e7S.jpeg",
        "https://i.imgur.com/J6MinJn.jpeg",
        "https://i.imgur.com/mWwek7p.jpeg"
        };
        return Images[new Random().Next(Images.Length)];
    }

    public static List<Image> GetImages()
    {
        List<Image> images = new List<Image>();

        foreach (var p in GetProducts())
        {
            for (int i = 0; i <= 2; i++)
            {
                Image image = new Image
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = GetRandomImages(),
                    ProductId = p.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                images.Add(image);
            }
        }
        return images;
    }
}
