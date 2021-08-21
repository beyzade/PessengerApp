using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PessengerApp.Models
{
    public class Pessenger
    {
        [Required]
        public Status Status { get; set; }

        [Required]
        public Country Country { get; set; }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Surname { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Document Type")]
        [Required]
        public DocumentType DocumentType { get; set; }

        [Display(Name = "Document No")]
        [Required, StringLength(20)]
        public string DocumentNo { get; set; }

        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime IssueDate { get; set; }
    }
}
