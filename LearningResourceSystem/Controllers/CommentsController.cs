using Amazon.Auth.AccessControlPolicy;
using LearningResourceSystem;
using LearningResourceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningResourcePortal.Controllers
{
    public class CommentsController : Controller
    {
        // TEMPORARY SAMPLE COMMENTS ONLY.
        private static readonly List<Comments> comments = new()
            {
            new Comments
            {
                commentId = "C001",
                entityId = "E001",
                content = "Sample text...",
                username = "NewUser001"
            },
            new Comments
            {
                commentId = "C002",
                entityId = "E002",
                content = "Sample text...",
                username = "NewUser002"
            }
        };

        public IActionResult Index(string entityId = "E001") { 
            ViewBag.entityId = entityId;

            var entityComments = comments
                .Where(c => c.entityId == entityId)
                .ToList();

            return View(entityComments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string entityId, string content)
        {
            // PLACEHOLDER ONLY.
            // Connect to DynamoDB and identify the authenticated author later.

            if (string.IsNullOrWhiteSpace(entityId))
            {
                return BadRequest("Please enter an entity ID.");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["CommentError"] = "Please enter a comment.";
                return RedirectToAction(nameof(Index), new { entityId });
            }

            comments.Add(new Comments
            {
                commentId = "C" + Guid.NewGuid().ToString("N"),
                    entityId = entityId,
                    content = content.Trim(),
                    username = "NewUser001"
            });

            return RedirectToAction(nameof(Index), new { entityId });
        }
    }
}