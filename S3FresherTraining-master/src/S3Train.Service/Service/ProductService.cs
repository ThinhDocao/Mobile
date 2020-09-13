using System;
using System.Collections.Generic;
using System.Linq;
using S3Train.Contract;
using S3Train.Domain;

namespace S3Train.Service
{
    public class ProductService : GenenicServiceBase<Product>, IProductService
    {
        public ProductService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<Product> ListAll()
        {
            return this.EntityDbSet.Where(y => y.Status == true).ToList();
        }

        public List<Product> ListAllByID(Guid productCategoryID)
        {
            return this.EntityDbSet.Where(y=>y.Status==true &&y.ProductCategoryID==productCategoryID).OrderByDescending(x => x.CreateDate).ToList();
        }

        public bool Create(Product product)
        {
            this.DbContext.Products.Add(product);
            this.DbContext.SaveChanges();
            return true;
        }
        public bool Update(Product product)
        {
            var pro = this.DbContext.Products.Find(product.Id);
            pro.Name = product.Name;
            pro.ProductCategoryID = product.ProductCategoryID;
            pro.BrandID = product.BrandID;
            pro.Code = product.Code;
            pro.MetaTitle = product.MetaTitle;
            pro.Descriptions = product.Descriptions;
            pro.Image = product.Image;
            pro.Price = product.Price;
            pro.PromotionPrice = product.PromotionPrice;
            pro.Quantity = product.Quantity;
            pro.Detail = product.Detail;
            pro.Warranty = product.Warranty;
            pro.ModifiedDate = product.ModifiedDate;
            pro.ModifiedBy = product.ModifiedBy;
            pro.MetaDescriptions = product.MetaDescriptions;
            pro.TopHot = product.TopHot;
            this.DbContext.SaveChanges();
            return true;
        }

        public void UpdateImages(Guid id,string images)
        {
            var product = this.DbContext.Products.Find(id);
            product.MoreImage = images;
            this.DbContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var product = this.DbContext.Products.Find(id);
            this.DbContext.Products.Remove(product);
            this.DbContext.SaveChanges();
            return true;
        }

        public List<Product> ListAllOrderByCreateDate()
        {
            return this.EntityDbSet.OrderByDescending(x => x.CreateDate).ToList();
        }

        public bool? ChangeStatus(Guid id)
        {
            var product = this.DbContext.Products.Find(id);
            product.Status = !product.Status;
            this.DbContext.SaveChanges();
            return product.Status;

        }



        ////////////////////// Hiếu ------------------
        ///

        public List<Product> ListTopHotALL()
        {
            return this.DbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.ProductCategoryID != null).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Product> ListTopHotTablet()
        {
            return this.DbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.ProductCategory.Name == "Tablet").OrderByDescending(x => x.CreateDate).ToList();

        }
        public List<Product> ListTopHotPhone()
        {
            return this.DbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.ProductCategory.Name == "Mobile").OrderByDescending(x => x.CreateDate).ToList();

        }
        public List<Product> ListTopHotWatch()
        {
            return this.DbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.ProductCategory.Name == "Clock").OrderByDescending(x => x.CreateDate).ToList();

        }
        public List<Product> ListTopHotLapTop()
        {
            return this.DbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.ProductCategory.Name == "Laptop").OrderByDescending(x => x.CreateDate).ToList();

        }

        public List<Product> relatedProduct(Guid id)
        {
            var product = this.DbContext.Products.Find(id);
            return this.DbContext.Products.Where(t => t.Id != id && t.BrandID == product.BrandID).Take(4).ToList();
        }
        public List<Product> detailProduct(Guid productCategoryID)
        {
            return this.DbContext.Products.Where(t => t.Id == productCategoryID).ToList();
        }
        public List<Product> SearchBrank(Guid id)
        {
            return this.DbContext.Products.Where(t => t.BrandID == id).ToList();
        }
        public List<Product> lessthan200()
        {
            return this.DbContext.Products.Where(t => t.Price < 200).ToList();
        }
        public List<Product> greaterthan1000()
        {
            return this.DbContext.Products.Where(t => t.Price > 1000).ToList();
        }
        public List<Product> between200and500()
        {
            return this.DbContext.Products.Where(t => t.Price >= 200 && t.Price < 500).ToList();
        }
        public List<Product> between500and1000()
        {
            return this.DbContext.Products.Where(t => t.Price >= 500 && t.Price <= 1000).ToList();
        }

    }
}