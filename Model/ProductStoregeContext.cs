using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Controllers;

namespace RestaurantAPI.Model
{
    public enum Sort
    {
        PriceUp,
        PriceDown,
        AlfavitUp,
        AlfavitDown,
    }
    public class ProductStoregeContext : DbContext
    {
        public ProductStoregeContext(DbContextOptions<ProductStoregeContext> options)
           : base(options)
        {
        }
        public DbSet<ProductInDb> Products { get; set; }
        public DbSet<CategoryInDb> Category { get; set; }

        public IReadOnlyCollection<ProductForUI> GetMenuProducts()
        {
            return Products
                .Include(p => p.Category)
                .Select(p => new ProductForUI
                {
                    Name = p.Name,
                    Category = p.Category.Name,
                    Description = p.Description,
                    Price = p.Price,
                }).ToArray();
        }
         public IReadOnlyCollection<ProductInDb> GetProducts( Sort sort, string categoryName )
         {
            var pr = Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == categoryName);

            switch (sort)
            {
                case Sort.PriceUp:
                    return pr.OrderBy(p => p.Price).ToArray();

                case Sort.PriceDown:
                    return pr.OrderByDescending(p => p.Price).ToArray();

                case Sort.AlfavitUp:
                    return pr.OrderBy(p => p.Name).ToArray();

                case Sort.AlfavitDown:
                    return pr.OrderByDescending(p => p.Name).ToArray();

                default:
                    return Products.ToArray();

            }
        }
        public IReadOnlyCollection<ProductForUI> GetPoductForName (string productName)
        {
            return Products
                .Where(p => p.Name == productName)
                .Select(p=> new ProductForUI {
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category.Name,
                    Price = p.Price,
                }) .ToArray();
        }
        public IReadOnlyCollection<ProductInDb> GetPoductForId(int idProduct)
        {
            return Products.Where(p => p.id == idProduct).ToArray();
        }
        public void AddProduct( string name, string description, string name_category, int price, byte[] image )
        {
            Products.Add(new ProductInDb
            {
                id = Products.OrderBy(p => p.id).Last().id + 1,
                Name = name,
                Description = description,
                Category = Category.Where(c=>c.Name == name_category).Single(),
                Price = price,
                Image = image,
            });
            SaveChanges();
        }
        public void UpdateProduct( int id, string name, string description, string category, int price, byte[] image)
        {
            ProductInDb pr = Products.Where(p => p.id == id).Single();

            if (name != null)
                pr.Name = name;

            if (description != null)
                pr.Description = description;

            if (category != null)
                pr.Category = Category.Where(c => c.Name == category).Single();

            if (price != 0)
                pr.Price = price;

            pr.Image = image;
            SaveChanges();
        }
        public void RemoveProduct(int id)
        {
            Products.Remove(Products.Where(p => p.id == id).Single());
            SaveChanges();
        }
        public IReadOnlyCollection<CategoryForUI> GetAllCategory()
        {
            return Category
                .Select(c=> new CategoryForUI { 
                    Name = c.Name,
                }).ToArray();
        }
        public void AddCategory( string category, byte[] image )
        {
            Category.Add(new CategoryInDb
            {
                id = Category.OrderBy(c=>c.id).Last().id+1,
                Name = category,
                Image = image,
            });
            SaveChanges();
        }
        public void RenameCategory(string name, string new_name)
        {
            Category.Where(c => c.Name == name).Single().Name = new_name;
            SaveChanges();
        }
        public void RemoveCategory(string name)
        {
            Category.Remove(Category.Where(c => c.Name == name).Single());
            SaveChanges();
        }
    }
}
