using System;
using System.Linq;
using System.Threading.Tasks;
using LearningResourceSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningResourceSystem
{
    public class ResourcesController : Controller
    {
        private readonly ResourcesDbContext _context;

        public ResourcesController(ResourcesDbContext context)
        {
            _context = context;
        }

        // public browsing
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(
            string? searchTerm,
            string? author)
        {
            var query = _context.LearningResources
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();

                query = query.Where(r =>
                    r.Title.Contains(term) ||
                    r.Topic.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                var authorTerm = author.Trim();

                query = query.Where(r =>
                    r.AuthorName.Contains(authorTerm));
            }

            ViewBag.SearchTerm = searchTerm;
            ViewBag.Author = author;

            var resources = await query
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();

            return View(resources);
        }

        // public display 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var resource = await _context.LearningResources
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // display creation form.
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new LearningResourceModel());
        }

        // save a new resource to AWS.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Topic,Description")]
            LearningResourceModel submitted)
        {
            submitted.ResourceId = 0;
            submitted.AuthorName = User.Identity?.Name ?? "Unknown";
            submitted.CreatedDate = DateTime.UtcNow;

            ModelState.Clear();

            if (!TryValidateModel(submitted))
            {
                return View(submitted);
            }

            _context.LearningResources.Add(submitted);
            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Details),
                new { id = submitted.ResourceId });
        }

        //  display the editing form.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _context.LearningResources
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        //save permitted field changes.
        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var existing = await _context.LearningResources
                .FirstOrDefaultAsync(r => r.ResourceId == id);

            if (existing == null)
            {
                return NotFound();
            }

            var updated = await TryUpdateModelAsync(
                existing,
                prefix: "",
                r => r.Title,
                r => r.Topic,
                r => r.Description);

            if (!updated)
            {
                return View(existing);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        // display deletion confirmation.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var resource = await _context.LearningResources
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // delete the resource from AWS.
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.LearningResources
                .FirstOrDefaultAsync(r => r.ResourceId == id);

            if (resource == null)
            {
                return NotFound();
            }

            _context.LearningResources.Remove(resource);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}