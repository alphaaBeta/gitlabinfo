import { IProject } from './project';
import { IUser } from '../../models/user';

export interface IProjectRequestPut {
    name?: string;
    description?: string;
    member_emails?: string[];
    parent_group_id?: number;
}

export interface IProjectRequestGet {
    name?: string;
    description?: string;
    members?: IUser[];
    parent_group_id?: number;
}