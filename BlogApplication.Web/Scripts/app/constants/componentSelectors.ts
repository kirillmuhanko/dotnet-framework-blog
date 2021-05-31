export module ComponentSelectors {
    const _commentId = '#comment';
    const _component = 'component';
    const _imageId = '#image';

    export const comment = `${_commentId}-${_component}`;
    export const commentBox = `${_commentId}-box-${_component}`;
    export const commentEditor = `${_commentId}-editor-${_component}`;
    export const commentLoader = `${_commentId}-loader-${_component}`;
    export const imagePreview = `${_imageId}-preview-${_component}`;
    export const imageUpload = `${_imageId}-upload-${_component}`;
    export const like = `#like-${_component}`;
    export const spinner = `#spinner-${_component}`;
}