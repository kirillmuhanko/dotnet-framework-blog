import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';
import {EventTypes} from 'constants/eventTypes';
import {InnerComponentSelectors} from 'constants/innerComponentSelectors';

export class ImageUploadComponent extends ComponentBase {
    constructor() {
        super();
    }

    public onImageUploadDone = (_file: File, _fileReader: FileReader) => void 0;
    public onImageUploadFail = () => void 0;

    public imageUploadJQuery = () => jQuery(ComponentSelectors.imageUpload);
    public inputJQuery = () => this.imageUploadJQuery().find(InnerComponentSelectors.inputImageUpload);
    public textJQuery = () => this.imageUploadJQuery().find(InnerComponentSelectors.text);

    init(): void {
        this.attachEventHandlers();
    }

    attachEventHandlers(): void {
        const component = this;

        this.inputJQuery().off().on(EventTypes.change,function (): void {
            const file = component.uploadImage(<HTMLInputElement>this);
            component.setLabel(file);
        });
    }

    public setLabel(file: File): void {
        const fileName = file?.name === undefined ? String.empty : file.name;
        this.textJQuery().text(fileName);
    }

    public uploadImage(input: HTMLInputElement, component: ImageUploadComponent = this): File {
        const file = input?.files[0];

        if (file) {
            const fileReader = new FileReader();

            fileReader.onload = function (): void {
                component.onImageUploadDone(file, this);
            }

            fileReader.readAsDataURL(file);
        } else {
            component.onImageUploadFail();
        }

        return file;
    }
}