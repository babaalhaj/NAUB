using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NAUB.Models
{
    [MetadataType(typeof(BookMetaData))]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(50)]
        [Display(Name = "ISBN #")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        [Display(Name = "Book title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name #1")]
        [StringLength(50)]
        [Display(Name = "Author name #1")]
        public string Author1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Author name #2 (Only if applicable)")]
        public string Author2 { get; set; }

        [StringLength(50)]
        [Display(Name = "Author name #3 (Only if applicable)")]
        public string Author3 { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        [StringLength(50)]
        [Display(Name = "Publisher name")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        [Display(Name = "Year of publication")]
        public short PublicationYear { get; set; }

        [Required(ErrorMessage = "Edition is required")]
        [StringLength(50)]
        [Display(Name = "Edition")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "# Stock is required")]
        [Display(Name = "# in stock")]
        public short NumberInStock { get; set; }

        [Required(ErrorMessage = "Shelve # is required")]
        [StringLength(50)]
        [Display(Name = "Shelve #")]
        public string ShelveNumber { get; set; }
    }
}