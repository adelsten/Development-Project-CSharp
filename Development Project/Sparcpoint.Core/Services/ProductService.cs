using Sparcpoint.Abstract;
using Sparcpoint.Models;
using Sparcpoint.SqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Sparcpoint.Services
{
    public class ProductService : IProductService
    {
        private ISqlExecutor _sqlExecutor;
        public ProductService(ISqlExecutor executor) 
        {
            _sqlExecutor = executor;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _sqlExecutor.Execute<List<Product>>((conn, trans) =>
            {
                var results = new List<Product>();
                var command = conn.CreateCommand();
                command.Transaction = trans;
                command.CommandText = "SELECT InstanceId, [Name], [Description], ProductImageUris, ValidSkus, CreatedTimestamp FROM [Instances].[Products]";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var product = new Product()
                        {
                            InstanceId = (int)dataReader.GetValue(0),
                            Name = (string)dataReader.GetValue(1),
                            Description = (string)dataReader.GetValue(2),
                            ProductImageUris = (string)dataReader.GetValue(3),
                            SKU = (string)dataReader.GetValue(4),
                            CreatedTimestamp = (DateTime)dataReader.GetValue(5),
                        };
                        results.Add(product);
                    }
                }
                return results;
            });
            return products;
        }

        public int AddProduct(Product product)
        {
            var productId = _sqlExecutor.Execute<int>((conn, trans) =>
            {
                var command = conn.CreateCommand();
                command.Transaction = trans;
                command.CommandText = "CreateProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@ProductImageUris", product.ProductImageUris));
                command.Parameters.Add(new SqlParameter("@ValidSkus", product.SKU));
                return (int)command.ExecuteScalar();
            });

            foreach(var category in product.Categories)
            {
                _sqlExecutor.Execute((conn, trans) =>
                {
                    var command = conn.CreateCommand();
                    command.Transaction = trans;
                    command.CommandText = "AddProductCategory";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@InstanceId", productId));
                    command.Parameters.Add(new SqlParameter("@CategoryInstanceId", category.InstanceId));
                    return command.ExecuteNonQuery();
                });
            }

            foreach(var attribute in product.Attributes)
            {
                _sqlExecutor.Execute((conn, trans) =>
                {
                    var command = conn.CreateCommand();
                    command.Transaction = trans;
                    command.CommandText = "CreateProductAttribute";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Instanceid", productId));
                    command.Parameters.Add(new SqlParameter("@Key", attribute.Key));
                    command.Parameters.Add(new SqlParameter("@Value", attribute.Value));
                    return command.ExecuteNonQuery();

                });
            }

            return productId;
        }
    }
}
