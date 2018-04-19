using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreUnitTesting.Models.Entities
{
    [Table("Author")]
    public class Author
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [InverseProperty("Author")]
        public List<BookAuthor> BookAuthors { get; set; }
    }
}