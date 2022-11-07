using System.ComponentModel.DataAnnotations;

namespace CabManagementSystem.ViewModel
{
    public class CabCreateViewModel
    {
        public int CarId { get; set; }
        public string DrivarId { get; set; }
        [Required]
        [Display(Name = "Cab Model")]
        public string CabModel { get; set; }

        [Required]
        [Display(Name ="Number Plate")]
        public string numberPlate { get; set; }
        [Required]
        public string City { get; set; }
        public bool AC { get; set; }
        public IFormFile Photo { get; set; }
    }
}
