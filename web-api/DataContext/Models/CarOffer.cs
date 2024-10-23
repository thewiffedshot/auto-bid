using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;
using WebApi.Interfaces.Models;

namespace WebApi.DataContext.Models
{
    public class CarOffer : IEntity<CarOfferModel>
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public CarMake Make { get; set; }
        [Required]
        public required string Model { get; set; }
        [Required]
        public uint Year { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public required string Odometer { get; set; }
        [Required]
        public bool OdometerInMiles { get; set; }
        [Required]
        public bool IsAutomatic { get; set; }
        [Required, MinLength(1)]
        public required List<CarImage> Images { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public required User Owner { get; set; }

        public CarOfferModel ToModel()
        {
            return new CarOfferModel
            {
                Make = Make.ToString(),
                Model = Model,
                Year = Year,
                Price = Price,
                Odometer = Odometer,
                OdometerInMiles = OdometerInMiles,
                IsAutomatic = IsAutomatic,
                Images = Images.Select(i => i.ToModel()).ToList(),
                Description = Description,
                Owner = Owner.ToModel()
            };
        }
    }

    public enum CarMake {
        Ford,
        Chevrolet,
        Toyota,
        Honda,
        Nissan,
        BMW,
        Mercedes,
        Audi,
        Volkswagen,
        Hyundai,
        Kia,
        Subaru,
        Mazda,
        Jeep,
        Volvo,
        Porsche,
        Lexus,
        Tesla,
        LandRover,
        Jaguar,
        Ferrari,
        Maserati,
        Bentley,
        RollsRoyce,
        Lamborghini,
        Bugatti,
        McLaren,
        AstonMartin,
        AlfaRomeo,
        Fiat,
        Mini,
        Smart,
        Saab,
        Saturn,
        Pontiac,
        Oldsmobile,
        Mercury,
        Lincoln,
        Hummer,
        GMC,
        Dodge,
        Chrysler,
        Cadillac,
        Buick,
        Acura,
        Infiniti,
        Scion,
        Suzuki,
        Mitsubishi,
        Isuzu,
        Daewoo,
        Geo,
        Plymouth,
        Eagle,
        DeLorean,
        Lancia,
        Peugeot,
        Citroen,
        Renault,
        Opel,
        Seat,
        Skoda,
        Dacia,
        Alpina,
        Datsun,
        MG,
        Rover,
        Lada,
        Moskvitch,
        ZAZ,
        UAZ,
        GAZ,
        VAZ,
        ZIL,
        Kamaz,
        MAZ,
        KRAZ,
        BelAZ
    }
}