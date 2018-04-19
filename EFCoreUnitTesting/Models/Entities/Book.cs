using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreUnitTesting.Models.Entities
{
    [Table("Book")]
    public class Book
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [InverseProperty("Book")]
        public List<BookAuthor> BookAuthors { get; set; }
    }
}