using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brands.Count() > 0)
            {
                if (_dbcontext.Brands.Count() == 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbcontext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

           var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var Categorys = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);
            if (Categorys.Count() > 0)
            {
                if (_dbcontext.Categories.Count() == 0)
                {
                    foreach (var category in Categorys)
                    {
                        _dbcontext.Set<ProductCategory>().Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (products.Count() > 0)
            {
                if (_dbcontext.Products.Count() == 0)
                {
                    foreach (var product in products)
                    {
                        _dbcontext.Set<Product>().Add(product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

        }
    }
}
