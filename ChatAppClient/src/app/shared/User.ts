import { USER_STATUS } from './UserStatus';

export class User {
    public id: string;
    public firstName: string;
    public lastName: string;
    public emailAddress: string;
    public userStatus: USER_STATUS;
    public userStatusString: string;
    public bytePhoto: number;
    public photoUrl: any;
}
