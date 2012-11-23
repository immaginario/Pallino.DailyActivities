using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pallino.DailyActivities.WebApp.ViewModels
{
    public class CreateCustomerViewModel
    {
        [Required]
        [Display(Name="Nome")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z]{2})?[0-9]{11}$")]        
        [Display(Name = "Partita IVA")]
        public string VATNumber { get; set; }
    }
}