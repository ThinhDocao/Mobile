using System;
using System.Collections.Generic;
using S3Train.Domain;

namespace S3Train.Contract
{
    public interface IProductService : IGenenicServiceBase<Product>
    {
        List<Product> ListAllByID(Guid productCategoryID);
        List<Product> ListAll();
        bool Create(Product product);
        bool Update(Product product);
        void UpdateImages(Guid id, string images);
        bool Delete(Guid id);
        List<Product> ListAllOrderByCreateDate();
        bool? ChangeStatus(Guid id);




        //----------------- Hiếu --------------------

        List<Product> ListTopHotALL();
        List<Product> ListTopHotTablet();
        List<Product> ListTopHotPhone();
        List<Product> ListTopHotWatch();
        List<Product> ListTopHotLapTop();
        List<Product> SearchBrank(Guid id);
        List<Product> lessthan200();
        List<Product> greaterthan1000();
        List<Product> between200and500();
        List<Product> between500and1000();
        List<Product> relatedProduct(Guid id);
        List<Product> detailProduct(Guid productCategoryID);
    }
}
