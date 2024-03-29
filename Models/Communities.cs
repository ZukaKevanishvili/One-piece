using Reddit.Models;

namespace Reddit.Entities
{
    public class Communities  
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Users { get; set; } 
    }
}
