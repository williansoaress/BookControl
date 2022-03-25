using System;
using System.Collections.Generic;

namespace BookControl.Api.DTOs
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public IEnumerable<BookDTO> Books { get; set; }
    }
}
