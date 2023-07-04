using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreAndFood.ViewComponents
{
	public class FoodGetList:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			FoodRepository foodRepository = new FoodRepository();
			var foodList = foodRepository.TList();
			return View(foodList);
		}
	}
}
