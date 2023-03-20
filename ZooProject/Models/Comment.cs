using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public Animal? Animal { get; set; }
        public int AnimalId { get; set; }
        [Required]
        public string? CommentString{ get; set; }
    }
}
