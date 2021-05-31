import {ClassNames} from 'constants/classNames';
import {CommentComponentParameters} from 'models/parameters/components/commentComponentParameters';
import {CommentComponent} from 'components/commentComponent';
import {CommentLoaderComponentParameters} from 'models/parameters/components/commentLoaderComponentParameters';
import {CommentViewModel} from 'models/viewModels/commentViewModel';
import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';

export class CommentLoaderComponent extends ComponentBase {
    private readonly _parameters: CommentLoaderComponentParameters;
    private _pageNumber: number = 1;
    private _pageSize: number = 1;

    constructor(parameters: CommentLoaderComponentParameters) {
        super();
        this._parameters = parameters;
        this._pageSize = parameters.pageSize;
    }

    public onLoadFail = (_data) => void 0;
    public commentEditorJQuery = () => jQuery(ComponentSelectors.commentEditor);
    public commentJQuery = () => jQuery(ComponentSelectors.comment);
    public commentLoaderJQuery = () => jQuery(ComponentSelectors.commentLoader);
    public spinnerJQuery = () => jQuery(ComponentSelectors.spinner);

    public init(): void {
    }

    attachEventHandlers(): void {
    }

    public addComment(model: CommentViewModel, prepend: boolean = false): CommentComponent {
        let html: string;
        let newCommentJQuery: JQuery;
        const commentLoader = this.commentLoaderJQuery();

        if (model.isEditable) {
            html = this.commentEditorJQuery().html();
        } else {
            html = this.commentJQuery().html();
        }

        if (prepend) {
            commentLoader.prepend(html);
            newCommentJQuery = commentLoader.children().first();
        } else {
            commentLoader.append(html);
            newCommentJQuery = commentLoader.children().last();
        }

        let instance = this.buildComponent(model);
        instance.commentJQuery = () => newCommentJQuery;
        instance.init();
        return instance;
    }

    public addComments(commentList: Array<CommentViewModel>): void {
        const component = this;
        commentList.forEach(function (item): void {
            component.addComment(item);
        });
    }

    private buildComponent(model: CommentViewModel): CommentComponent {
        const component = this;
        const parameters = component._parameters.commentParameters;

        const data = {
            deleteApiUrl: parameters.deleteApiUrl,
            model: model,
            reportUrl: parameters.reportUrl,
            updateApiUrl: parameters.updateApiUrl
        } as CommentComponentParameters;

        return new CommentComponent(data);
    }

    public loadComments(count: number = this._pageSize): void {
        const component = this;
        const parameters = component._parameters;

        if (component.isLocked()) {
            return;
        }

        const promise = this.post(this._pageNumber, count);

        promise.done(function (list: Array<CommentViewModel>): void {
            component.addComments(list);
            component._pageNumber++;

            if (list?.length < parameters.pageSize) {
                component.spinnerJQuery().addClass(ClassNames.displayNone);
                component.lock();
            } else {
                component.lock(false);
            }
        });

        promise.fail(function (data): void {
            component.onLoadFail(data);
            component.lock(false);
        });

        component.lock();
    }

    private post(page: number, pageSize: number): JQuery.jqXHR {
        const component = this;
        const parameters = component._parameters;

        return this.ajax({
            url: parameters.apiUrl,
            data: {
                id: parameters.id,
                page,
                pageSize
            }
        });
    }
}