import {AjaxClient} from 'ajax/ajaxClient';

export abstract class ComponentBase {
    private _isLocked: boolean;

    public abstract init(): void;
    public abstract attachEventHandlers(): void;

    public isLocked = () => this._isLocked === true;
    public lock = (isLocked: boolean = true) => this._isLocked = isLocked;

    protected ajax(settings: JQuery.AjaxSettings): JQuery.jqXHR {
        return AjaxClient.post(settings);
    }

    protected encodeToHtml(node: JQuery): string {
        const text = node.val() as string;
        return node.text(text).html();
    }
}