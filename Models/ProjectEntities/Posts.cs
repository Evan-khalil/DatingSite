
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingSite
{
    public class Posts : MainEntity<int>
    {
        public int Id { get; set; }
        [StringLength(250, MinimumLength = 0, ErrorMessage = "Field can maximum contain 250 characters")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        [ForeignKey("Reciever")]
        public string RecieverId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Reciever { get; set; }
    }
}
