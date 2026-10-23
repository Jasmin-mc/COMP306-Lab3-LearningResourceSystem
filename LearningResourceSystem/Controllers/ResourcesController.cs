using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class ResourcesController : Controller
    {
        // TEMPORARY SAMPLE DATA.
        // Replace this list with Samreet's database/service integration.
        private static readonly List<LearningResourceModel> Resources = new()
        {
            new LearningResourceModel
            {
                ResourceId = 1,
                Title = "Introduction to C#",
                Topic = "Programming",
                Description = "An introduction to C# programming concepts.",
                AuthorName = "Sample Instructor",
                CreatedDate = DateTime.Today.AddDays(-2),
                
            },
            new LearningResourceModel
            {
                ResourceId = 2,
                Title = "Understanding SQL",
                Topic = "Databases",
                Description = "Learn the basics of relational databases and SQL.",
                AuthorName = "Sample Instructor",
                CreatedDate = DateTime.Today.AddDays(-1),
                
            },
            new LearningResourceModel
            {
                ResourceId = 3,
                Title = "ASP.NET Core MVC Basics",
                Topic = "Web Development",
                Description = "Learn how MVC controllers, views, and models work.",
                AuthorName = "Sample Instructor",
                CreatedDate = DateTime.Today,
                
            }
        };

        public IActionResult Index(
            string? searchTerm,
            string? author)
        {
            IEnumerable<LearningResourceModel> results = Resources;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                results = results.Where(r =>
                    r.Title.Contains(searchTerm,
                        StringComparison.OrdinalIgnoreCase) ||
                    r.Topic.Contains(searchTerm,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                results = results.Where(r =>
                    r.AuthorName.Contains(author,
                        StringComparison.OrdinalIgnoreCase));
            }

            

            ViewBag.SearchTerm = searchTerm;
            ViewBag.Author = author;
           

            return View(results.ToList());
        }

        public IActionResult Details(int id)
        {
            var resource = Resources.FirstOrDefault(r =>
                r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }        

            return View(resource);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new LearningResourceModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LearningResourceModel resource)
        {
            if (!ModelState.IsValid)
            {
                return View(resource);
            }

            // TEMPORARY: no permanent file upload or database storage.
            resource.ResourceId = Resources.Count == 0
                ? 1
                : Resources.Max(r => r.ResourceId) + 1;
            resource.CreatedDate = DateTime.Today;
            resource.AuthorName = "Demo User";

            Resources.Add(resource);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var resource = Resources.FirstOrDefault(r =>
                r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LearningResourceModel updated)
        {
            if (id != updated.ResourceId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updated);
            }

            var existing = Resources.FirstOrDefault(r =>
                r.ResourceId == id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = updated.Title;
            existing.Topic = updated.Topic;
            existing.Description = updated.Description;

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var resource = Resources.FirstOrDefault(r =>
                r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var resource = Resources.FirstOrDefault(r =>
                r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            Resources.Remove(resource);

            return RedirectToAction(nameof(Index));
        }
    }
}
