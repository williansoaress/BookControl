using System.Collections;
using System.Collections.Generic;

namespace BookControl.Business.Models
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        /* EF Relations */
        public IEnumerable<Book> Books { get; set; }
    }
}
