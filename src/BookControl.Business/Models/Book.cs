using System;

namespace BookControl.Business.Models
{
    public class Book : Entity
    {
        public Guid? StudentId { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        /* EF Relations */
        public Student Student { get; set; }
    }
}
