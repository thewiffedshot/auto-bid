export class UserModel {
    id?: string;
    firstName!: string;
    lastName?: string;
    email!: string;
    username!: string;
}

export const initialUserModel: UserModel = {
    firstName: '',
    email: '',
    username: '',
};