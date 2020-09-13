using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class BrandService : GenenicServiceBase<Brand>, IBrandService
    {
        public BrandService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<Brand> ListAll()
        {
            return this.EntityDbSet.Where(y => y.Status == true).ToList();
        }

        public bool Create(Brand brand)
        {
            this.DbContext.Brands.Add(brand);
            this.DbContext.SaveChanges();
            return true;
        }
        public bool Update(Brand brand)
        {
            var pro = this.DbContext.Brands.Find(brand.Id);
            pro.Name = brand.Name;
            pro.Logo = brand.Logo;
            pro.ModifyDate = brand.ModifyDate;
            pro.ModifyBy = brand.ModifyBy;
            this.DbContext.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var product = this.DbContext.Brands.Find(id);
            this.DbContext.Brands.Remove(product);
            this.DbContext.SaveChanges();
            return true;
        }

        public List<Brand> ListAllOrderByCreateDate()
        {
            return this.EntityDbSet.OrderByDescending(x => x.CreateDate).ToList();
        }

        public bool? ChangeStatus(Guid id)
        {
            var brand = this.DbContext.Brands.Find(id);
            brand.Status = !brand.Status;
            this.DbContext.SaveChanges();
            return brand.Status;

        }
    }
}
