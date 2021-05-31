import {LikeViewModel} from 'models/viewModels/likeViewModel';

export interface LikeComponentParameters {
    apiUrl: string;
    isLocked: boolean;
    model: LikeViewModel;
}