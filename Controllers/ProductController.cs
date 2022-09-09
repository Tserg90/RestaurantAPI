using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestaurantAPI.Model;

namespace RestaurantAPI.Controllers
{
    public class Menu : ControllerBase
    {
        protected ProductStoregeContext storage;
        public Menu(ProductStoregeContext context)
        {
            storage = context;
        }
        public enum ResultState
        {
            Success,
            Failed
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class ProductController : Menu
    {
        public ProductController(ProductStoregeContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("/getmenu")]
        public IReadOnlyCollection<ProductForUI> GetMenu ()
        {
            return storage.GetMenuProducts();
        }

        [HttpGet]
        [Route("/getproducts")]
        public IReadOnlyCollection<ProductInDb> GetProducts( Sort sort,  string categoryName )
        {
            return storage.GetProducts( sort, categoryName );
        }

        [HttpGet]
        [Route("/getproductforname")]
        public IReadOnlyCollection<ProductForUI> GetProductForName(string nameProduct)
        {
            return storage.GetPoductForName( nameProduct );
        }

        [HttpGet]
        [Route("/getproductforid")]
        public IReadOnlyCollection<ProductInDb> GetProductForId(int idProduct)
        {
            return storage.GetPoductForId( idProduct );
        }

        [HttpPost]
        [Route("/addprduct")]
        public ResultState AddProduct( string name, string description, string name_category, int price, string image )
        {
            byte[] image_convert = null;
            if (image != null)
                image_convert = Encoding.UTF32.GetBytes(image);
            storage.AddProduct(name, description, name_category, price, image_convert);
            return ResultState.Success;
        }

        [HttpPost]
        [Route("/updateprduct")]
        public ResultState UpdateProduct(int id, string name, string description, string name_category, int price, string image)
        {
            byte[] image_convert = null;
            if (image != null)
                image_convert = Encoding.UTF32.GetBytes(image);
            storage.UpdateProduct(id, name, description, name_category, price, image_convert);
            return ResultState.Success;
        }

        [HttpDelete]
        [Route("/removeproduct")]
        public ResultState RemoveProduct(int id)
        {
            storage.RemoveProduct(id);
            return ResultState.Success;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Menu
    {
        public CategoryController(ProductStoregeContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("/getallcategory")]
        public IReadOnlyCollection<CategoryForUI> GetAllCategory()
        {
            return storage.GetAllCategory();
        }

        [HttpPost]
        [Route("/addcategory")]
        public ResultState AddCategory( string category, string image )
        {
            byte[] image_convert = null;
            if (image != null)
                image_convert = Encoding.UTF32.GetBytes(image);
            storage.AddCategory(category, image_convert);
            return ResultState.Success;
        }

        [HttpPost]
        [Route("/renamecategory")]
        public ResultState RenameCategory(string namecategory, string new_namecategory)
        {
            storage.RenameCategory(namecategory, new_namecategory);
            return ResultState.Success;
        }

        [HttpDelete]
        [Route("/removecategory")]
        public ResultState RemoveCategoru(string name)
        {
            storage.RemoveCategory(name);
            return ResultState.Success;
        }
    }
}
