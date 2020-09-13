using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace S3Train.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
            
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<ProductAdvertisement> ProductAdvertisements { get; set; }

        public DbSet<Contact> Contacts{ get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentCategory> ContentCategorys { get; set; }
        public DbSet<ContentTag> ContentTags { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Footer> Footers{ get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuType> MenuTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Answer> answers{ get; set; }

        public DbSet<Spec> Specs { get; set; }
        public DbSet<SpecValue> SpecValues { get; set; }
        public DbSet<ProductSpecSpecValue> productSpecSpecValues { get; set; }
        public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasMany(c => c.OrderDetails).WithRequired(p => p.Product);
            modelBuilder.Entity<Product>().HasMany(c => c.ProductSpecSpecValues).WithRequired(p => p.Product);
            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Code).HasMaxLength(20);
            modelBuilder.Entity<Product>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<Product>().Property(x => x.Descriptions).HasMaxLength(500);
            modelBuilder.Entity<Product>().Property(x => x.Image).HasMaxLength(250);
            modelBuilder.Entity<Product>().Property(x => x.MoreImage);
            modelBuilder.Entity<Product>().Property(x => x.Price);
            modelBuilder.Entity<Product>().Property(x => x.PromotionPrice);
            modelBuilder.Entity<Product>().Property(x => x.IncludeVAT);
            modelBuilder.Entity<Product>().Property(x => x.Quantity);
            modelBuilder.Entity<Product>().Property(x => x.Detail);
            modelBuilder.Entity<Product>().Property(x => x.Warranty);
            modelBuilder.Entity<Product>().Property(x => x.CreateDate);
            modelBuilder.Entity<Product>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<Product>().Property(x => x.ModifiedBy).HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(x => x.Metakeywords);
            modelBuilder.Entity<Product>().Property(x => x.MetaDescriptions);
            modelBuilder.Entity<Product>().Property(x => x.Status);
            modelBuilder.Entity<Product>().Property(x => x.TopHot);
            modelBuilder.Entity<Product>().Property(x => x.ViewCount);

            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductCategory>().HasMany(c => c.Products).WithRequired(p => p.ProductCategory);
            modelBuilder.Entity<ProductCategory>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.ParentID);
            modelBuilder.Entity<ProductCategory>().Property(x => x.DisplayOrder);
            modelBuilder.Entity<ProductCategory>().Property(x => x.SeoTitle).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.CreateDate);
            modelBuilder.Entity<ProductCategory>().Property(x => x.CreateBy).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<ProductCategory>().Property(x => x.ModifiedBy);
            modelBuilder.Entity<ProductCategory>().Property(x => x.Metakeywords).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.MetaDescriptions).HasMaxLength(250);
            modelBuilder.Entity<ProductCategory>().Property(x => x.Status);
            modelBuilder.Entity<ProductCategory>().Property(x => x.Image);
            modelBuilder.Entity<ProductCategory>().Property(x => x.ShowOnHome);

            modelBuilder.Entity<ProductAdvertisement>().ToTable("ProductAdvertisement");
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.EventUrl).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.EventUrlCaption).HasMaxLength(10).IsOptional();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.Title).HasMaxLength(100).IsOptional();
            modelBuilder.Entity<ProductAdvertisement>().Property(x => x.Description).HasMaxLength(500).IsOptional();

            modelBuilder.Entity<About>().ToTable("About");
            modelBuilder.Entity<About>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<About>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<About>().Property(x => x.Descriptions).HasMaxLength(500);
            modelBuilder.Entity<About>().Property(x => x.Image).HasMaxLength(250);
            modelBuilder.Entity<About>().Property(x => x.Detail);
            modelBuilder.Entity<About>().Property(x => x.CreateDate);
            modelBuilder.Entity<About>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<About>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<About>().Property(x => x.ModifiedBy).HasMaxLength(50);
            modelBuilder.Entity<About>().Property(x => x.Metakeywords).HasMaxLength(250);
            modelBuilder.Entity<About>().Property(x => x.MetaDescriptions).HasMaxLength(250);
            modelBuilder.Entity<About>().Property(x => x.Status);

            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Contact>().Property(x => x.Content);
            modelBuilder.Entity<Contact>().Property(x => x.Status);

            modelBuilder.Entity<Content>().ToTable("Content");
            modelBuilder.Entity<Content>().HasMany(c => c.ContentTags).WithRequired(p => p.Content);
            modelBuilder.Entity<Content>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.Descriptions).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.Image).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.Detail);
            modelBuilder.Entity<Content>().Property(x => x.Warranty);
            modelBuilder.Entity<Content>().Property(x => x.CreateDate);
            modelBuilder.Entity<Content>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Content>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<Content>().Property(x => x.ModifiedBy).HasMaxLength(50);
            modelBuilder.Entity<Content>().Property(x => x.Metakeywords).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.MetaDescriptions).HasMaxLength(250);
            modelBuilder.Entity<Content>().Property(x => x.Status);
            modelBuilder.Entity<Content>().Property(x => x.TopHot);
            modelBuilder.Entity<Content>().Property(x => x.ViewCount);
            modelBuilder.Entity<Content>().Property(x => x.Tags).HasMaxLength(500);

            modelBuilder.Entity<ContentCategory>().ToTable("ContentCategory");
            modelBuilder.Entity<ContentCategory>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<ContentCategory>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<ContentCategory>().Property(x => x.ParentID);
            modelBuilder.Entity<ContentCategory>().Property(x => x.DisplayOrder);
            modelBuilder.Entity<ContentCategory>().Property(x => x.SeoTitle).HasMaxLength(250);
            modelBuilder.Entity<ContentCategory>().Property(x => x.CreateDate);
            modelBuilder.Entity<ContentCategory>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<ContentCategory>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<ContentCategory>().Property(x => x.ModifiedBy).HasMaxLength(50);
            modelBuilder.Entity<ContentCategory>().Property(x => x.Metakeywords).HasMaxLength(250);
            modelBuilder.Entity<ContentCategory>().Property(x => x.MetaDescriptions).HasMaxLength(250);
            modelBuilder.Entity<ContentCategory>().Property(x => x.Status);
            modelBuilder.Entity<ContentCategory>().Property(x => x.ShowOnHome);

            modelBuilder.Entity<ContentTag>().ToTable("ContentTag");

            modelBuilder.Entity<Feedback>().ToTable("Feedback");
            modelBuilder.Entity<Feedback>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Feedback>().Property(x => x.Phone).HasMaxLength(50);
            modelBuilder.Entity<Feedback>().Property(x => x.Email).HasMaxLength(50);
            modelBuilder.Entity<Feedback>().Property(x => x.Address).HasMaxLength(50);
            modelBuilder.Entity<Feedback>().Property(x => x.Content).HasMaxLength(250);
            modelBuilder.Entity<Feedback>().Property(x => x.CreatedDate);
            modelBuilder.Entity<Feedback>().Property(x => x.Status);

            modelBuilder.Entity<Footer>().ToTable("Footer");
            modelBuilder.Entity<Footer>().Property(x => x.Content);
            modelBuilder.Entity<Footer>().Property(x => x.Status);

            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<Menu>().Property(x => x.Text).HasMaxLength(50);
            modelBuilder.Entity<Menu>().Property(x => x.Image).HasMaxLength(50);
            modelBuilder.Entity<Menu>().Property(x => x.Link).HasMaxLength(250);
            modelBuilder.Entity<Menu>().Property(x => x.DisplayOrder);
            modelBuilder.Entity<Menu>().Property(x => x.Target).HasMaxLength(50);
            modelBuilder.Entity<Menu>().Property(x => x.Status);

            modelBuilder.Entity<MenuType>().ToTable("MenuType");
            modelBuilder.Entity<MenuType>().HasMany(c => c.Menus).WithRequired(p => p.MenuType);
            modelBuilder.Entity<MenuType>().Property(x => x.Name).HasMaxLength(50);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasMany(c => c.OrderDetails).WithRequired(p => p.Order);
            modelBuilder.Entity<Order>().Property(x => x.CreateDate);
            modelBuilder.Entity<Order>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.ShipName).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.ShipMobile).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.ShipAddress).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.ShipEmail).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.Status);

            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetail>().Property(x => x.Quantity);
            modelBuilder.Entity<OrderDetail>().Property(x => x.Price);

            modelBuilder.Entity<Slide>().ToTable("Slide");
            modelBuilder.Entity<Slide>().Property(x => x.Image).HasMaxLength(250);
            modelBuilder.Entity<Slide>().Property(x => x.DisplayOrder);
            modelBuilder.Entity<Slide>().Property(x => x.Link).HasMaxLength(250);
            modelBuilder.Entity<Slide>().Property(x => x.Description).HasMaxLength(50);
            modelBuilder.Entity<Slide>().Property(x => x.CreateDate);
            modelBuilder.Entity<Slide>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Slide>().Property(x => x.ModifiedDate);
            modelBuilder.Entity<Slide>().Property(x => x.ModifiedBy).HasMaxLength(50);
            modelBuilder.Entity<Slide>().Property(x => x.Status);

            modelBuilder.Entity<SystemConfig>().ToTable("SystemConfig");
            modelBuilder.Entity<SystemConfig>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<SystemConfig>().Property(x => x.Type).HasMaxLength(50);
            modelBuilder.Entity<SystemConfig>().Property(x => x.Value).HasMaxLength(250);
            modelBuilder.Entity<SystemConfig>().Property(x => x.Status);

            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Tag>().HasMany(c => c.ContentTags).WithRequired(p => p.Tag);
            modelBuilder.Entity<Tag>().Property(x => x.Name).HasMaxLength(50);


            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Brand>().HasMany(c => c.Products).WithRequired(p => p.Brand);
            modelBuilder.Entity<Brand>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<Brand>().Property(x => x.Logo);
            modelBuilder.Entity<Brand>().Property(x => x.CreateDate);
            modelBuilder.Entity<Brand>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Brand>().Property(x => x.ModifyDate);
            modelBuilder.Entity<Brand>().Property(x => x.ModifyBy).HasMaxLength(50);
            modelBuilder.Entity<Brand>().Property(x => x.Status);
            modelBuilder.Entity<Brand>().Property(x => x.MetaTitle).HasMaxLength(250);
            modelBuilder.Entity<Brand>().Property(x => x.DisplayOrder);



            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Answer>().Property(x => x.Content).HasMaxLength(500);
            modelBuilder.Entity<Answer>().Property(x => x.CreateDate);
            modelBuilder.Entity<Answer>().Property(x => x.CreateBy).HasMaxLength(50);


            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Question>().HasMany(c => c.Answers).WithRequired(p => p.Question);
            modelBuilder.Entity<Question>().Property(x => x.Title).HasMaxLength(250);
            modelBuilder.Entity<Question>().Property(x => x.Content).HasMaxLength(500);
            modelBuilder.Entity<Question>().Property(x => x.CreateDate);
            modelBuilder.Entity<Question>().Property(x => x.CreateBy).HasMaxLength(50);
            modelBuilder.Entity<Question>().Property(x => x.Status);





            modelBuilder.Entity<Spec>().ToTable("Spec");
            modelBuilder.Entity<Spec>().HasMany(c => c.ProductSpecSpecVaLues).WithRequired(p => p.Spec);
            modelBuilder.Entity<Spec>().Property(x => x.Name).HasMaxLength(255);

            modelBuilder.Entity<SpecValue>().ToTable("SpecValue");
            modelBuilder.Entity<SpecValue>().HasMany(c => c.ProductSpecSpecVaLues).WithRequired(p => p.SpecValue);
            modelBuilder.Entity<SpecValue>().Property(x => x.NameSpecValue).HasMaxLength(255);



            modelBuilder.Entity<ProductSpecSpecValue>().ToTable("ProductSpecSpecValue");


            modelBuilder.Entity<OrderDetailTemp>().ToTable("OrderDetailTemp");
            modelBuilder.Entity<OrderDetailTemp>().Property(x => x.UserID).HasMaxLength(255);
            modelBuilder.Entity<OrderDetailTemp>().Property(x => x.CartContent);
        }

    }
}