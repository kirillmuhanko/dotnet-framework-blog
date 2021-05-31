import {CommentComponentParameters} from 'models/parameters/components/commentComponentParameters';

export interface CommentLoaderComponentParameters {
    apiUrl: string;
    commentParameters: CommentComponentParameters;
    id: number;
    pageSize: number;
}