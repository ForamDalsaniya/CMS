using System.ComponentModel.DataAnnotations;

namespace CabManagementSystem.Models
{
    public class Cab
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string DriverId {get;set;}
        [Required]
        public string CabModel {get;set;}
        public bool AC { get;set;}
        public string numberPlate { get;set;}
        public string? PhotoPath { get; set; }
        public string City { get; set; }

        public bool? isRegister { get; set; }
        public DateTime? registerDate { get; set; }

        public string? registerAddress { get; set; }
        public string? registerBy { get; set; }

    }

}
