using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs.Address
{
    public class ReadAddressDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ADDRESS_CITY_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.ADDRESS_CITY_TOO_LONG)]
        public string City { get; set; }

        [Required(ErrorMessage = Messages.ADDRESS_DISTRICT_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.ADDRESS_DISTRICT_TOO_LONG)]
        public string District { get; set; }

        [Required(ErrorMessage = Messages.ADDRESS_STREET_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.ADDRESS_STREET_TOO_LONG)]
        public string Street { get; set; }

        [Required(ErrorMessage = Messages.ADDRESS_NUMBER_REQUIRED)]
        [Range(0, 10000, ErrorMessage = Messages.ADDRESS_NUMBER_OUTOFRANGE)]
        public int Number { get; set; }

        [Required(ErrorMessage = Messages.ADDRESS_POSTALCODE_REQUIRED)]
        public int PostalCode { get; set; }




    }
}
