import { CHAT_PRIVACY } from './ChatPrivacy';
import { User } from './User';

export class ChatWithUsers {
    public id: string;
    public name: string;
    public password: string;
    public chatPrivacy: CHAT_PRIVACY;
    public picture: any;
    public users: User[];
    public pictureUrl: any;
}
