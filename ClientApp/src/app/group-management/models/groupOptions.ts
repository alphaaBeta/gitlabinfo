import { ISurvey } from './survey';

export interface IGroupOptions {
    reportTimeEnabled: boolean;
    engagementPointsEnabled: boolean;
    workDescriptionEnabled: boolean;
    workDescriptionCommentsEnabled: boolean;
    surveyEnabled: boolean;
}

export interface IGroupOptionsPost {
    groupId: number;
    surveyString: string;
    surveyId: number | null;
    reportTimeEnabled: boolean;
    engagementPointsEnabled: boolean;
    workDescriptionEnabled: boolean;
    workDescriptionCommentsEnabled: boolean;
    surveyEnabled: boolean;
}