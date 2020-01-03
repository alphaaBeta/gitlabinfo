export interface ISurvey {
    surveyId: number;
    name: string;
    multiselectQuestions: IMultiselectQuestion[];
    textQuestions: ITextQuestion[];
}

export interface IMultiselectQuestion {
    id: number;
    text: string;
    type: QuestionType;
    options: IMultiselectQuestionOptions;
}
export interface ITextQuestion {
    id: number;
    text: string;
    type: QuestionType;
    options: object;
}

interface IMultiselectQuestionOptions {
    choices: string[];
}

enum QuestionType {
    multiselect,
    text
}
