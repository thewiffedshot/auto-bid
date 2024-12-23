import { CarImageModel } from "./car-image-model";

export interface CarOfferModel {
    id?: string | null;
    make: CarOfferMake | null;
    model: string | null;
    year: number | null;
    price: number | null;
    odometer: string | null;
    odometerInMiles?: boolean;
    isAutomatic?: boolean;
    images?: CarImageModel[];
    description?: string;
    ownerUsername: string | null;
    carImagesToAdd?: CarImageModel[];
    carImagesToDelete?: string[];
}

export const initialCarOfferModel: CarOfferModel = {
    id: null,
    make: null,
    model: null,
    year: null,
    price: null,
    odometer: null,
    odometerInMiles: false,
    isAutomatic: false,
    images: [],
    description: '',
    ownerUsername: null,
    carImagesToAdd: [],
    carImagesToDelete: [],
};

export enum CarOfferMake {
    Ford = 'Ford',
    Chevrolet = 'Chevrolet',
    Toyota = 'Toyota',
    Honda = 'Honda',
    Nissan = 'Nissan',
    BMW = 'BMW',
    Mercedes = 'Mercedes',
    Audi = 'Audi',
    Volkswagen = 'Volkswagen',
    Hyundai = 'Hyundai',
    Kia = 'Kia',
    Subaru = 'Subaru',
    Mazda = 'Mazda',
    Jeep = 'Jeep',
    Volvo = 'Volvo',
    Porsche = 'Porsche',
    Lexus = 'Lexus',
    Tesla = 'Tesla',
    LandRover = 'LandRover',
    Jaguar = 'Jaguar',
    Ferrari = 'Ferrari',
    Maserati = 'Maserati',
    Bentley = 'Bentley',
    RollsRoyce = 'RollsRoyce',
    Lamborghini = 'Lamborghini',
    Bugatti = 'Bugatti',
    McLaren = 'McLaren',
    AstonMartin = 'AstonMartin',
    AlfaRomeo = 'AlfaRomeo',
    Fiat = 'Fiat',
    Mini = 'Mini',
    Smart = 'Smart',
    Saab = 'Saab',
    Saturn = 'Saturn',
    Pontiac = 'Pontiac',
    Oldsmobile = 'Oldsmobile',
    Mercury = 'Mercury',
    Lincoln = 'Lincoln',
    Hummer = 'Hummer',
    GMC = 'GMC',
    Dodge = 'Dodge',
    Chrysler = 'Chrysler',
    Cadillac = 'Cadillac',
    Buick = 'Buick',
    Acura = 'Acura',
    Infiniti = 'Infiniti',
    Scion = 'Scion',
    Suzuki = 'Suzuki',
    Mitsubishi = 'Mitsubishi',
    Isuzu = 'Isuzu',
    Daewoo = 'Daewoo',
    Geo = 'Geo',
    Plymouth = 'Plymouth',
    Eagle = 'Eagle',
    DeLorean = 'DeLorean',
    Lancia = 'Lancia',
    Peugeot = 'Peugeot',
    Citroen = 'Citroen',
    Renault = 'Renault',
    Opel = 'Opel',
    Seat = 'Seat',
    Skoda = 'Skoda',
    Dacia = 'Dacia',
    Alpina = 'Alpina',
    Datsun = 'Datsun',
    MG = 'MG',
    Rover = 'Rover',
    Lada = 'Lada',
    Moskvitch = 'Moskvitch',
    ZAZ = 'ZAZ',
    UAZ = 'UAZ',
    GAZ = 'GAZ',
    VAZ = 'VAZ',
    ZIL = 'ZIL',
    Kamaz = 'Kamaz',
    MAZ = 'MAZ',
    KRAZ = 'KRAZ',
    BelAZ = 'BelAZ',
}
