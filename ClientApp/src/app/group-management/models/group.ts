import { IGroupOptions } from './groupOptions';

export interface IGroup {
    id: string;
    web_url: string;
    name: string;
    path: string;
    description: string;
    parent_id: string;
    isOwner: boolean;
    options: IGroupOptions;
}
