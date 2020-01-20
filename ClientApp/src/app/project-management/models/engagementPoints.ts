import { IUser } from '../../models/user';

export interface IEngagementPointsPut {
    projectId: number;
    receivingUser: IUser;
    points: number;
    bonus: boolean;
    comment: string;
}

export interface IEngagementPointsGet {
    awardingUser: IUser;
    receivingUser: IUser;
    points: number;
    receivingDate: Date;
}
