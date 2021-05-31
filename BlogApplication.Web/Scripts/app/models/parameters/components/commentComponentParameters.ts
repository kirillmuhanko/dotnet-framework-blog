import {CommentViewModel} from 'models/viewModels/commentViewModel';

export interface CommentComponentParameters {
    deleteApiUrl: string;
    model: CommentViewModel;
    reportUrl: string;
    updateApiUrl: string;
}