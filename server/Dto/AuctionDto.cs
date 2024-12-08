using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.Dto
{
    public class AuctionDto
    {
       

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Sport category is required")]

        public string Sport { get; set; }
        [Required(ErrorMessage = "AuctioneerId is required")]

        public int PlayerId { get; set; }
        public int AuctioneerId { get; set; } // Foreign Key to User

        [Required(ErrorMessage = "Auction start time required")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Auction end time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Auction status is required")]
        public string Status { get; set; }

        
    }
}
