using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
	public class FoodListByCategory:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
		
			FoodRepository foodRepository = new FoodRepository();
			var foodList = foodRepository.List(x=>x.CategoryId==id);
			return View(foodList);
		}
	}
}
