using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductCategoryService : GenenicServiceBase<ProductCategory>, IProductCategoryService
    {
        public ProductCategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<ProductCategory> ListAll()
        {
            return this.EntityDbSet.Where(x =>x.Status==true).ToList();
        }

        public List<ProductCategory> ListAllOrderByCreateDate()
        {
            return this.EntityDbSet.OrderByDescending(x => x.CreateDate).ToList();
        }


        public ProductCategory ViewDetail(Guid id)
        {
            return this.EntityDbSet.Find(id);
        }

        public bool Create(ProductCategory productCategory)
        {
            this.DbContext.ProductCategorys.Add(productCategory);
            this.DbContext.SaveChanges();
            return true;
        }

        public bool? ChangeStatus(Guid id)
        {
            var productCategory = this.DbContext.ProductCategorys.Find(id);
            productCategory.Status = !productCategory.Status;
            this.DbContext.SaveChanges();
            return productCategory.Status;

        }

        public bool Update(ProductCategory productCategory)
        {
            var pro = this.DbContext.ProductCategorys.Find(productCategory.Id);
            pro.Name = productCategory.Name;
            pro.ModifiedDate = productCategory.ModifiedDate;
            pro.ModifiedBy = productCategory.ModifiedBy;
            pro.Image = productCategory.Image;
            this.DbContext.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var product = this.DbContext.ProductCategorys.Find(id);
            this.DbContext.ProductCategorys.Remove(product);
            this.DbContext.SaveChanges();
            return true;
        }

    }
}
