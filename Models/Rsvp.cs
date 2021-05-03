using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Bbq.Models
{
    public class Rsvp
    {
        [Key]
        public int RsvpId { get;set; }

        //Foreign Key
        public int UserId { get;set; }

        public int BbqEventId { get;set; }

        public string Dish { get;set; }
        
        public int TotalGuests { get;set; }

        //Nav Props
        public User Guest { get;set; }

        public BbqEvent Group { get;set; }
    }
}