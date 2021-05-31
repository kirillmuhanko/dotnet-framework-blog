import {CommentBoxComponentParameters} from 'models/parameters/components/commentBoxComponentParameters';
import {CommentLoaderComponentParameters} from 'models/parameters/components/commentLoaderComponentParameters';
import {LikeComponentParameters} from 'models/parameters/components/likeComponentParameters';

export interface ArticleIndexPageParameters {
    commentBoxComponentParameters: CommentBoxComponentParameters;
    commentLoaderComponentParameters: CommentLoaderComponentParameters;
    likeComponentParameters: LikeComponentParameters;
}