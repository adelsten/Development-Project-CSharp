using Sparcpoint.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparcpoint.Abstract
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        int AddProduct(Product product);
    }
}
