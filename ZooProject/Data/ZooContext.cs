using Microsoft.EntityFrameworkCore;
using ZooProject.Models;

namespace ZooProject.Data
{
    public class ZooContext : DbContext
    {
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public ZooContext(DbContextOptions<ZooContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new Animal { AnimalId = 1, Name = "Lion", Age = 14, CategoryId = 1, Description = "Simba, The lion king", ImgSource = "~/images/lion.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 2, Name = "Dolphin", Age = 5, CategoryId = 1, Description = "The Dolphin is...", ImgSource = "~/images/dolphin.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 3, Name = "Chicken", Age = 3,  CategoryId = 2, Description = "The Chicken is...", ImgSource = "~/images/chicken.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 4, Name = "Eagle", Age = 10, CategoryId = 2, Description = "The Eagle is the", ImgSource = "~/images/eagle.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 5, Name = "Lizard", Age = 25,  CategoryId = 3, Description = "The Lizard is...", ImgSource = "~/images/lizard.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 6, Name = "Snake", Age = 1,  CategoryId = 3, Description = "The Snake is...", ImgSource = "~/images/snake.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 7, Name = "Gold Fish", Age = 2,  CategoryId = 4, Description = "The Goldfish is...", ImgSource = "~/images/goldfish.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 8, Name = "Octopus", Age = 15,  CategoryId = 4, Description = "The Octopus is...", ImgSource = "~/images/octopus.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 9, Name = "Wolf", Age = 5, CategoryId = 1, Description = "The Wolf is...", ImgSource = "~/images/wolf.jpg", Comments = new List<Comment>() },
                new Animal { AnimalId = 10, Name = "Kangaroo", Age = 4, CategoryId = 1, Description = "The Kangaroo is...", ImgSource = "~/images/kangaroo.jpg", Comments = new List<Comment>() }

                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Mammals" }, 
                new Category { CategoryId = 2, CategoryName = "Aves" }, 
                new Category { CategoryId = 3, CategoryName = "Reptiles" },
                new Category { CategoryId = 4, CategoryName = "Fish" }
                );
            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, AnimalId = 1, CommentString = "Wow, this is the king!" },
                new Comment { CommentId = 2, AnimalId = 1, CommentString = "Damn, this lion is ugly" },
                new Comment { CommentId = 3, AnimalId = 2, CommentString = "It is such a cute animal!" },
                new Comment { CommentId = 4, AnimalId = 3, CommentString = "KOO-KOORIKOO!!!" },
                new Comment { CommentId = 5, AnimalId = 4, CommentString = "He flies so high, wow..." }
                );
        }
    }
}
