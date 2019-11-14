import { IProject } from './project';

export interface IProjectRequest {
    project: IProject;
    member_emails: string[];
    parent_group_id: number;
}