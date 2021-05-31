import {ClassNames} from 'constants/classNames';
import {CommentBoxComponentParameters} from 'models/parameters/components/commentBoxComponentParameters';
import {CommentViewModel} from 'models/viewModels/commentViewModel';
import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';
import {EventTypes} from 'constants/eventTypes';
import {InnerComponentSelectors} from 'constants/innerComponentSelectors';

export class CommentBoxComponent extends ComponentBase {
    private readonly _parameters: CommentBoxComponentParameters;

    constructor(parameters: CommentBoxComponentParameters) {
        super();
        this._parameters = parameters;
    }

    public onDone = (_data: CommentViewModel) => void 0;

    public buttonCancelJQuery = () => this.commentBoxJQuery().find(InnerComponentSelectors.buttonCancel);
    public buttonCommentJQuery = () => this.commentBoxJQuery().find(InnerComponentSelectors.buttonComment);
    public commentBoxJQuery = () => jQuery(ComponentSelectors.commentBox);
    public textAreaJQuery = () => this.commentBoxJQuery().find(InnerComponentSelectors.textArea);

    init(): void {
        this.attachEventHandlers();
        this.enableButtons(false);
    }

    attachEventHandlers(): void {
        const component = this;

        this.buttonCancelJQuery().off().on(EventTypes.click,function (): void {
            component.enableButtons(false);
            component.textAreaJQuery().val(String.empty);
        });

        this.buttonCommentJQuery().off().on(EventTypes.click,function (): void {
            if (component.textAreaJQuery().val()) {
                component.post();
                component.enableButtons(false);
                component.textAreaJQuery().val(String.empty);
            }
        });

        this.textAreaJQuery().off().on(EventTypes.input,function (): void {
            component.enableButtons(component.textAreaJQuery().val() !== String.empty);
        });
    }

    public enableButtons(isEnabled: boolean): void {
        if (isEnabled) {
            this.buttonCancelJQuery().removeClass(ClassNames.disabled);
            this.buttonCommentJQuery().removeClass(ClassNames.disabled);
        } else {
            this.buttonCancelJQuery().addClass(ClassNames.disabled);
            this.buttonCommentJQuery().addClass(ClassNames.disabled);
        }
    }

    public post(): void {
        const component = this;
        const parameters = component._parameters;
        
        const data: CommentViewModel = {
            id: parameters.model.id,
            text: component.encodeToHtml(component.textAreaJQuery())
        };

        this.ajax({
            url: parameters.apiUrl,
            data: data
        }).done(function (data: CommentViewModel): void {
            component.onDone(data);
        });
    }
}