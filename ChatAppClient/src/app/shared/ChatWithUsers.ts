import { CHAT_PRIVACY } from './CHAT_PRIVACY';
import { User } from './User';

export class ChatWithUsers {
    public id: string;
    public name: string;
    public password: string;
    public chatPrivacy: CHAT_PRIVACY;
    public picture: any;
    public chatUsers: User[];
    public pictureUrl: any;
}
