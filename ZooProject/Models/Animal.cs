using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooProject.Data;

namespace ZooProject.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [DataType(DataType.Text)]
        [Display(Name = "Animal Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter age")]
        [Display(Name = "Animal Age")]
        public int Age { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "URL")]
        public string? ImgSource { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        public Category? Category { get; set; }
        [Required(ErrorMessage = "Please enter category Id")]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        
        public void CommentOnAnimal(string comment)
        {
            if (this.Comments != null)
                this.Comments.Add(new Comment { CommentString = comment });
            else
            {
                this.Comments = new List<Comment>
                {
                    new Comment { CommentString = comment }
                };
            }

        }

    }
}
