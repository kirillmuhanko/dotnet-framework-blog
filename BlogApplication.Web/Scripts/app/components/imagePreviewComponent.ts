import {AttributeNames} from 'constants/attributeNames';
import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';
import {EventTypes} from 'constants/eventTypes';
import {InnerComponentSelectors} from 'constants/innerComponentSelectors';

export class ImagePreviewComponent extends ComponentBase {
    private _isImageLoaded: boolean;

    constructor() {
        super();
    }

    public imageJQuery = () => this.imagePreviewJQuery().find(InnerComponentSelectors.image);
    public imagePreviewJQuery = () => jQuery(ComponentSelectors.imagePreview);
    public inputCheckBoxJQuery = () => this.imagePreviewJQuery().find(InnerComponentSelectors.inputCheckBox);
    public textJQuery = () => this.imagePreviewJQuery().find(InnerComponentSelectors.text);

    init(): void {
        this.attachEventHandlers();
    }

    attachEventHandlers(): void {
        const component = this;

        this.inputCheckBoxJQuery().off().on(EventTypes.change, function (): void {
            if((this as HTMLInputElement)?.checked) {
                component.display(false);
            } else {
                component.display(true);
            }
        });

        this.imageJQuery().off().get(0).onload = function (): void {
            component._isImageLoaded = true;
            component.display(true);
        };
    }

    public display(isDisplayed: boolean): void {
        if (isDisplayed && this._isImageLoaded) {
            this.imageJQuery().show();
            this.textJQuery().hide();
        } else {
            this.imageJQuery().hide();
            this.textJQuery().show();
        }
    }

    public setImage(image: ArrayBuffer | string): void {
        this.imageJQuery().attr(AttributeNames.src, image?.toString());
        this.display(image !== null);
    }
}