import { CHAT_PRIVACY } from './ChatPrivacy';

export class Chat {
    public id: string;
    public name: string;
    public password: string;
    public chatPrivacy: CHAT_PRIVACY;
    public chatPrivacyString: string;
    public picture: any;
    public pictureUrl: any;
}
