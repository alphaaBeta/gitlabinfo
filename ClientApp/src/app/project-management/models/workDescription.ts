import { IUser } from '../../models/user';

export interface IWorkDescriptionPost {
    projectId: number;
    description: string;
}

export interface IWorkDescriptionGet {
    id: number;
    user: IUser;
    description: string;
    comments: string[];
}
