using System.Collections.Generic;
using System.Data;
using Dapper;
using nhlstendencafe.Models;

namespace nhlstendencafe.Repositories
{
    public class ProductRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string sql = "SELECT * FROM Product";

            using var connection = GetConnection();
            var products = connection.Query<Product>(sql);
            return products;
        }

        public Product GetProductById(int productId)
        {
            string sql = "SELECT * FROM Product WHERE ProductId = @ProductId";

            using var connection = GetConnection();
            var product = connection.QuerySingleOrDefault<Product>(sql, new { ProductId = productId });
            return product;
        }

        public int AddProduct(Product product)
        {
            string sql = @"INSERT INTO Product (Name, Price, CategoryId)
                           VALUES (@Name, @Price, @CategoryId)";

            using var connection = GetConnection();
            return connection.Execute(sql, product);
        }

        public int UpdateProduct(Product product)
        {
            string sql = @"UPDATE Product SET 
                       Name = @Name, 
                       Price = @Price, 
                       CategoryId = @CategoryId 
                   WHERE ProductId = @ProductId";

            Console.WriteLine(sql); // Debugging: Output the SQL command to the console

            var parameters = new
            {
                product.Name,
                product.Price,
                product.CategoryId,
                product.ProductId
            };

            using var connection = GetConnection();
            return connection.Execute(sql, parameters);
        }


        public void DeleteProduct(int productId)
        {
            string sql = "DELETE FROM Product WHERE ProductId = @ProductId";

            using var connection = GetConnection();
            connection.Execute(sql, new { ProductId = productId });
        }
        
        public IEnumerable<Product> GetProductWithCategory()
        {
            string sql = @"    SELECT * 
                            FROM Product as P
                                JOIN Category as C ON P.CategoryId = C.CategoryId 
                            ORDER BY C.Name, P.Name";
            
            using var connection = GetConnection();
            var productsWithCategory = connection.Query<Product, Category, Product>(
                sql, 
                map: (product, category) =>
                {
                    product.Category = category;
                    return product;
                }, 
                splitOn: "CategoryId"
            );
            return productsWithCategory;
        }
    }
}