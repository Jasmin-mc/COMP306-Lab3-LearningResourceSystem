using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class LearningResourceModel
    {
        
        public int ResourceId { get; set; }

        public string Title { get; set; } = "";

        public string Topic { get; set; } = "";

        public string Description { get; set; } = "";

        public string AuthorName { get; set; } = "";

        public DateTime CreatedDate { get; set; } = DateTime.Today;
    
    }
}
