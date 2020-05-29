import { CHAT_PRIVACY } from './ChatPrivacy';

export interface ICreateChat {
    name: string;
    chatPrivacy: CHAT_PRIVACY;
    password: string;
    picture: FormData;
    chatUsers: string[];
}
