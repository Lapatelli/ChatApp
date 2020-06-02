import { CHAT_PRIVACY } from './CHAT_PRIVACY';

export interface ICreateChat {
    name: string;
    chatPrivacy: CHAT_PRIVACY;
    password: string;
    picture: FormData;
    chatUsers: string[];
}
