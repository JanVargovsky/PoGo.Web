using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PoGo.Web.Models.MapModels
{
    public class MapRegistrationModel
    {
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = "Město")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Display(Name = "Uživatelské jméno na Discordu")]
        public string DiscordUsername { get; set; }
    }
}
