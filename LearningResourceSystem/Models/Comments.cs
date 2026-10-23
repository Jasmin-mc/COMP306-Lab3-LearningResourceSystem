using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class Comments  
    {

        public string entityId { get; set; } = "";
        public string commentId { get; set; } = "";
        public string content { get; set; } = "";
        public string username { get; set; } = "";

    }
}
