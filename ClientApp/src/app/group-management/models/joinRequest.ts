import { IUserDetailed } from '../../account/user';
import { IGroup } from './group';

export interface IJoinRequest {
    id: number;
    requestee: IUserDetailed;
    requestedGroup: IGroup;
}