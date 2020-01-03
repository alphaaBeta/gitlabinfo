export interface ISurveyAnswer {
    surveyId: number;
    projectId: number;
    multiselectAnswers: IMultiselectAnswer[];
    textAnswers: ITextAnswer[];
}

export interface IMultiselectAnswer {
    questionId: number;
    answer: IMultiselectAnswerAnswer;
}

export interface IMultiselectAnswerAnswer {
    choices: IMultiselectAnswerChoice[];
}
interface IMultiselectAnswerChoice {
    key: string;
    value: string;
}

export interface ITextAnswer {
    questionId: number;
    text: string;
}