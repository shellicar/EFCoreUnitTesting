using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreUnitTesting.Models.Entities
{
    [Table("BookAuthor")]
    public class BookAuthor
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Column("BookId")]
        [Required]
        public Guid BookId { get; set; }

        [Column("BookId")]
        [InverseProperty("BookAuthors")]
        public Book Book { get; set; }

        [Column("AuthorId")]
        [Required]
        public Guid AuthorId { get; set; }

        [Column("AuthorId")]
        [InverseProperty("BookAuthors")]
        public Author Author { get; set; }
    }
}