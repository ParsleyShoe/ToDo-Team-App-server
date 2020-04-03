using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDo_Team_App.Models;

namespace ToDo_Team_App.Models {
    public class ToDo {

        [Required]
        public int Id { get; set; }
        [StringLength(140)]
        [Required]
        public string Description { get; set; }
        public bool IsDone { get; set; }
        [StringLength(25)]
        [Required]
        public string Category { get; set; }
        public int UserId { get; set; }
        public int AssignedUserId { get; set; }
        [StringLength(10)] // allows for format of MM-DD-YYYY
        [Required]
        public string DueDate { get; set; }
        [StringLength(10)] // allows for longest value of "Accepted"
        [Required]
        public string Status { get; set; } = "Default";

        public virtual User User { get; set; }
        public virtual User AssignedUser { get; set; }

    }
}
