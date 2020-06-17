import { CHAT_PRIVACY } from './CHAT_PRIVACY';
import { ChatMessage } from './Message';

export class Chat {
    public id: string;
    public name: string;
    public password: string;
    public chatPrivacy: CHAT_PRIVACY;
    public chatPrivacyString: string;
    public picture: any;
    public pictureUrl: any;
    public messageStorage: ChatMessage[];
}
