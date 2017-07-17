using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [Range(8, 100)]
        public int Age { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
