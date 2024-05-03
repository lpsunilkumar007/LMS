using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.ViewModel.Company
{
   public class TestTockenVerficationViewModel
    {
        [Required(ErrorMessage = "Test tocken is required")]      
        public string TestTocken { get; set; }
    }
}
