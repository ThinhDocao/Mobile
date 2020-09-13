using S3Train.Domain;

namespace S3Train.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var categories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Id = Guid.NewGuid(),

                    Name = "Category One",
                    MetaTitle="//",
                    ParentID=1,
                    DisplayOrder=1,
                    SeoTitle="asdas",
                    CreateDate=DateTime.Now

                },
                new ProductCategory
                {
                    Id = Guid.NewGuid(),

                    Name = "Category Two",
                    MetaTitle="//",
                    ParentID=2,
                    DisplayOrder=2,
                    SeoTitle="asdas",
                    CreateDate=DateTime.Now
                },
                
            };
            categories.ForEach(x => context.ProductCategorys.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = true,

                    ProductCategoryID = categories.Single(x => x.Name.Equals("Category One", StringComparison.OrdinalIgnoreCase)).Id,
                    Image = "http://placehold.it/700x400",
                    Name = "Product One",
                    Price = 24.99m,
                    Descriptions = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet numquam aspernatur!",
                    Quantity = 0
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = true,

                    ProductCategoryID = categories.Single(x => x.Name.Equals("Category One", StringComparison.OrdinalIgnoreCase)).Id,
                    Image = "http://placehold.it/700x400",
                    Name = "Product Two",
                    Price = 13m,
                    Descriptions = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet numquam aspernatur!",
                    Quantity = 5
                },   
            };
            products.ForEach(x => context.Products.AddOrUpdate(p => p.Name, x));
            context.SaveChanges();

            var productAds = new List<ProductAdvertisement>
            {
                new ProductAdvertisement
                {
                    Id = Guid.NewGuid(),

                    EventUrl = "controller/action/1",
                    EventUrlCaption = string.Empty,
                    ImagePath = "http://placehold.it/900x350",
                    Title = "Lorem ipsum",
                    Description = string.Empty,
                    AdType = ProductAdvertisementType.SliderBanner,
                    Status=true
                },
                new ProductAdvertisement
                {
                    Id = Guid.NewGuid(),

                    EventUrl = "controller/action/2",
                    EventUrlCaption = string.Empty,
                    ImagePath = "http://placehold.it/900x350",
                    Title = "Lorem ipsum",
                    Description = string.Empty,
                    AdType = ProductAdvertisementType.SliderBanner,
                    Status=true
                },
                new ProductAdvertisement
                {
                    Id = Guid.NewGuid(),

                    EventUrl = "controller/action/3",
                    EventUrlCaption = string.Empty,
                    ImagePath = "http://placehold.it/900x350",
                    Title = "Lorem ipsum",
                    Description = string.Empty,
                    AdType = ProductAdvertisementType.SliderBanner,
                    Status=true
                },
                new ProductAdvertisement
                {
                    Id = Guid.NewGuid(),

                    EventUrl = "controller/action/1",
                    EventUrlCaption = string.Empty,
                    ImagePath = "http://placehold.it/900x350",
                    Title = "Lorem ipsum",
                    Description = string.Empty,
                    AdType = ProductAdvertisementType.MidHorRectangleBanner,
                    Status=true
                },
                new ProductAdvertisement
                {
                    Id = Guid.NewGuid(),

                    EventUrl = "controller/action/1",
                    EventUrlCaption = string.Empty,
                    ImagePath = "http://placehold.it/900x350",
                    Title = "Lorem ipsum",
                    Description = string.Empty,
                    AdType = ProductAdvertisementType.MidHorRectangleBanner,
                    Status=true
                },
            };
            productAds.ForEach(x => context.ProductAdvertisements.AddOrUpdate(p => p.Title, x));
            context.SaveChanges();
        }
    }
}
