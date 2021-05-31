import {ImagePreviewComponent} from 'components/imagePreviewComponent';
import {ImageUploadComponent} from 'components/imageUploadComponent';

export abstract class ArticleEditPage {
    private static _imagePreviewComponent: ImagePreviewComponent;
    private static _imageUploadComponent: ImageUploadComponent;

    public static init(): void {
        this._imagePreviewComponent = new ImagePreviewComponent();
        this._imageUploadComponent = new ImageUploadComponent();

        this._imagePreviewComponent.init();
        this._imageUploadComponent.init();

        this._imageUploadComponent.onImageUploadDone = function (file, fileReader): void {
            ArticleEditPage._imagePreviewComponent.setImage(fileReader.result);
        };

        this._imageUploadComponent.onImageUploadFail = function (): void {
            ArticleEditPage._imagePreviewComponent.setImage(null);
        };
    }
}