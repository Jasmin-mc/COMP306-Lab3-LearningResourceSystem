using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            //TEMPORARY PLACEHOLDER
            //REPLACE WITH CATEGORIES FROM SAMREET

            var categories = new List<string>
            {
                "Programming",
                "Databses",
                "Web Development",
                "Cyber Security"
            };

            return View(categories);
        }
    }
}
