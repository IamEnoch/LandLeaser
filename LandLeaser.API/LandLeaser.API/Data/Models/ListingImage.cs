using System.ComponentModel.DataAnnotations.Schema;

namespace LandLeaser.API.Data.Models
{
    public class ListingImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }


        [ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing Listing { get; set; }

    }
}
