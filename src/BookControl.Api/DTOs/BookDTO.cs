using System;

namespace BookControl.Api.DTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
