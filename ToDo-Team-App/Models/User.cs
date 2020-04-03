using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_Team_App.Models {
    public class User {

        [Required]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Username { get; set; }
        [StringLength(64)]
        [Required]
        public string Password { get; set; }
        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(30)]
        [Required]
        public string LastName { get; set; }

    }
}
