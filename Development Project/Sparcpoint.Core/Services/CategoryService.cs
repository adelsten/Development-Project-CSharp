using Sparcpoint.Abstract;
using Sparcpoint.Models;
using Sparcpoint.SqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Sparcpoint.Services
{
    public class CategoryService : ICategoryService
    {
        private ISqlExecutor _sqlExecutor;

        public CategoryService(ISqlExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public int AddCategory(Category category)
        {
            var categoryId = _sqlExecutor.Execute<int>((conn, trans) =>
            {
                var command = conn.CreateCommand();
                command.Transaction = trans;
                command.CommandText = "CreateCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Name", category.Name));
                command.Parameters.Add(new SqlParameter("@Description", category.Description));
                return (int)command.ExecuteScalar();
            });

            foreach (var attribute in category.Attributes)
            {
                _sqlExecutor.Execute((conn, trans) =>
                {
                    var command = conn.CreateCommand();
                    command.Transaction = trans;
                    command.CommandText = "CreateProductAttribute";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Instanceid", categoryId));
                    command.Parameters.Add(new SqlParameter("@Key", attribute.Key));
                    command.Parameters.Add(new SqlParameter("@Value", attribute.Value));
                    return command.ExecuteNonQuery();

                });
            }

            return categoryId;
        }

        public IEnumerable<Category> GetCategories()
        {
            var products = _sqlExecutor.Execute<List<Category>>((conn, trans) =>
            {
                var results = new List<Category>();
                var command = conn.CreateCommand();
                command.Transaction = trans;
                command.CommandText = "SELECT InstanceId, [Name], [Description], CreatedTimestamp FROM [Instances].[Categories]";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var category = new Category()
                        {
                            InstanceId = (int)dataReader.GetValue(0),
                            Name = (string)dataReader.GetValue(1),
                            Description = (string)dataReader.GetValue(2),
                            CreatedTimestamp = (DateTime)dataReader.GetValue(3),
                        };
                        results.Add(category);
                    }
                }
                return results;
            });
            return products;
        }
    }
}
