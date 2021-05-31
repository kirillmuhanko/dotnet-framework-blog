import {ArticleIndexPageParameters} from 'models/parameters/components/articleIndexPageParameters';
import {CommentBoxComponent} from 'components/commentBoxComponent';
import {CommentLoaderComponent} from 'components/commentLoaderComponent';
import {CommentViewModel} from 'models/viewModels/commentViewModel';
import {LikeComponent} from 'components/likeComponent';
import {ScrollComponent} from 'components/scrollComponent';

export abstract class ArticleIndexPage {
    private static _commentBoxComponent: CommentBoxComponent;
    private static _commentLoaderComponent: CommentLoaderComponent;
    private static _likeComponent: LikeComponent;
    private static _scrollComponent: ScrollComponent;

    public static init(parameters: ArticleIndexPageParameters): void {
        this._commentBoxComponent = new CommentBoxComponent(parameters.commentBoxComponentParameters);
        this._commentLoaderComponent = new CommentLoaderComponent(parameters.commentLoaderComponentParameters);
        this._likeComponent = new LikeComponent(parameters.likeComponentParameters);
        this._scrollComponent = new ScrollComponent();

        this._commentBoxComponent.init();
        this._likeComponent.init();
        this._scrollComponent.init();

        this._commentBoxComponent.onDone = function (model: CommentViewModel): void {
            if (model) {
                model.isEditable = true;
                ArticleIndexPage._commentLoaderComponent.addComment(model, true);
            }
        };

        this._commentLoaderComponent.loadComments(10);

        this._scrollComponent.onScrollBottom = function (): void {
            ArticleIndexPage._commentLoaderComponent.loadComments();
        };
    }
}