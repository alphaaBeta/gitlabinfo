import { IProject } from './project';
import { IUser } from '../../models/user';

export interface IProjectRequestPut {
    name?: string;
    description?: string;
    member_emails?: string[];
    parent_group_id?: number;
}

export interface IProjectRequestGet {
    id: number;
    name?: string;
    description?: string;
    members?: IUser[] | string[];
    parent_group_id?: number;
}