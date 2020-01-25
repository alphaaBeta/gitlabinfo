export interface IReportedTimePost {
    projectId: number;
    date: Date;
    timeInHours: number;
    description: string;
    issueId: number;
}

export interface IReportedTimeGet {
    userId: number;
    userName: string;
    projectId: number;
    date: Date;
    timeInHours: number;
    description: string;
    issueId: number;
}
