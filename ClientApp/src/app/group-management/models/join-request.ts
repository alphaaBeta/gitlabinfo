import { IUser } from '../../account/user';
import { IGroup } from './group';

export interface IJoinRequest {
    id: number;
    requestee: IUser;
    requestedGroup: IGroup;
}