using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Bbq.Validations;

namespace Bbq.Models
{
    public class BbqEvent
    {
        [Key]
        public int BbqEventId { get;set; }

        [Required(ErrorMessage="Event name is required")]
        [MinLength(3,ErrorMessage="Event name must be at least 3 characters")]
        public string EventName { get;set; }

        [Required(ErrorMessage="Address is required")]
        [MinLength(3,ErrorMessage="Address must be at least 3 characters")]
        public string Address { get;set; }

        [Required]
        [Future]
        public DateTime Date { get;set; }


        //Foreign Key
        public int UserId { get;set; }

        //One to Many - a bbq can only have on planner
        public User Planner { get;set; }

        //Many to Many - a bbq can have many guests
        public List<Rsvp> GuestList { get;set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}