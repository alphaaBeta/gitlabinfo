import { IUser } from '../../models/user';

export interface IProject {
    id?: number;
    description?: string;
    name?: string;
    path_with_namespace?: string;
    created_at?: string;
    last_activity_at?: string;
    members?: IUser[];
}
