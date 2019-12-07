import { IUser } from '../../models/user';

export interface IEngagementPointsPut {
    projectId: number;
    receivingUser: IUser;
    points: number;
}

export interface IEngagementPointsGet {
    awardingUser: IUser;
    receivingUser: IUser;
    points: number;
    receivingDate: Date;
}
