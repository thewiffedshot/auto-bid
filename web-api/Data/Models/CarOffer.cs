using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;
using WebApi.Interfaces.Models;

namespace WebApi.Data.Models
{
    public class CarOffer : IEntity<CarOfferModel>
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public CarMake Make { get; set; }
        [Required]
        public string Model { get; set; } = default!;
        [Required]
        public uint Year { get; set; }
        [Required]
        public decimal Price { get; set; } = default!;
        [Required]
        public string Odometer { get; set; } = default!;
        [Required]
        public bool OdometerInMiles { get; set; }
        [Required]
        public bool IsAutomatic { get; set; }
        [Required, MinLength(1)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public User Owner { get; set; } = default!;

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
                Description = Description,
                OwnerUsername = Owner.Username
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