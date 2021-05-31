import {AttributeNames} from 'constants/attributeNames';
import {ClassNames} from 'constants/classNames';
import {CommentComponentParameters} from 'models/parameters/components/commentComponentParameters';
import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';
import {EventTypes} from 'constants/eventTypes';
import {InnerComponentSelectors} from 'constants/innerComponentSelectors';

export class CommentComponent extends ComponentBase {
    private readonly _parameters: CommentComponentParameters;

    constructor(parameters: CommentComponentParameters) {
        super();
        this._parameters = parameters;
    }

    public actionLinkJQuery = () => this.commentJQuery().find(InnerComponentSelectors.actionLink);
    public buttonDeleteJQuery = () => this.commentJQuery().find(InnerComponentSelectors.buttonDelete);
    public buttonSaveJQuery = () => this.commentJQuery().find(InnerComponentSelectors.buttonSave);
    public commentJQuery = () => jQuery(ComponentSelectors.comment);
    public textJQuery = () => this.commentJQuery().find(InnerComponentSelectors.text);
    public titleJQuery = () => this.commentJQuery().find(InnerComponentSelectors.title);

    init(): void {
        this.attachEventHandlers();
        this.mapProperties();
    }

    attachEventHandlers(): void {
        const component = this;

        this.buttonDeleteJQuery().off().on(EventTypes.click,function (): void {
            component.postDelete();
        });

        this.buttonSaveJQuery().off().on(EventTypes.click,function (): void {
            if (!component.buttonSaveJQuery().hasClass(ClassNames.disabled)) {
                component.postUpdate();
                component.buttonSaveJQuery().addClass(ClassNames.disabled);
            }
        });

        this.textJQuery().off().on(EventTypes.input,function (): void {
            component.buttonSaveJQuery().removeClass(ClassNames.disabled);
        });
    }

    public mapProperties(): void {
        const component = this;
        const parameters = component._parameters;

        component.titleJQuery().html(parameters.model.userName);
        component.textJQuery().html(parameters.model.text);
        const url = `${parameters.reportUrl}/${parameters.model.id}`;
        component.actionLinkJQuery().attr(AttributeNames.href, url);
    }

    public postDelete(): void {
        const component = this;
        const parameters = component._parameters;

        component.ajax({
            url: parameters.deleteApiUrl,
            data: {
                id: parameters.model.id
            }
        }).done(function (): void {
            component.commentJQuery().remove();
        });
    }

    public postUpdate(): void {
        const component = this;
        const parameters = component._parameters;

        component.ajax({
            url: parameters.updateApiUrl,
            data: {
                id: parameters.model.id,
                text: component.textJQuery().text()
            }
        });
    }
}