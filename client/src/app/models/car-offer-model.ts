import { CarImageModel } from "./car-image-model";
import { UserModel } from "./user-model";

export interface CarOfferModel {
    id?: string;
    make: string;
    model: string;
    year: number;
    price: number;
    odometer: string;
    odometerInMiles?: boolean;
    isAutomatic?: boolean;
    images?: CarImageModel[];
    description?: string;
    ownerUsername: string;
}
