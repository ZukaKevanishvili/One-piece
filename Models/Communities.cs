using Reddit.Models;

namespace Reddit.Entities
{
    public class Communities  
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public ICollection<Post> Posts { get; set; }
        public User Owner { get; set; } 
        public ICollection<User> Users { get; set; } 
    }
}
