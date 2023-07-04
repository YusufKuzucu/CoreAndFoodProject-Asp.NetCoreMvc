using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {
        Context c=new Context();
        FoodRepository foodRepository = new FoodRepository();

        public IActionResult Index(int page=1)
        {
            return View(foodRepository.TList("Category").ToPagedList(page,3));
        }
        [HttpGet]
        public IActionResult AddFood() 
        {
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList(); 
            ViewBag.v1=values;
            return View();
        }
        [HttpPost]
        public IActionResult AddFood(urunekle p)
        {
            Food f = new Food();
            if (p.ImageURL!=null)
            {
                var extension=Path.GetExtension(p.ImageURL.FileName);
                var newimagename=Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/resimler/",newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ImageURL = newimagename;
            }
            f.Name = p.Name;
            f.Price = p.Price;
            f.Stock = p.Stock;
            f.CategoryId = p.CategoryId;
            
            foodRepository.TAdd(f);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteFood(int id)
        {
            var deger = c.Foods.Find(id);
            foodRepository.TDelete(deger);
            return RedirectToAction("Index");  
        }
        public IActionResult FoodGet(int id)
        {
            var x=foodRepository.TGet(id);
            List<SelectListItem> values = (from y in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            Food f = new Food()
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                ImageURL = x.ImageURL,
                FoodID = x.FoodID,  
            };
            return View(f);
        }
        [HttpPost]
        public IActionResult FoodUpdate(Food f)
        {
            var x = foodRepository.TGet(f.FoodID);
            x.Name = f.Name;
            x.Price = f.Price;
            x.Stock = f.Stock;
            x.Description = f.Description;
            x.ImageURL = f.ImageURL;
            x.CategoryId = f.CategoryId;
            foodRepository.TUpdate(x);
            return RedirectToAction("Index");
        }

    }
}
