import {ClassNames} from 'constants/classNames';
import {ComponentBase} from 'components/componentBase';
import {ComponentSelectors} from 'constants/componentSelectors';
import {CssProperties} from 'constants/cssProperties';
import {EventTypes} from 'constants/eventTypes';
import {InnerComponentSelectors} from 'constants/innerComponentSelectors';
import {LikeComponentParameters} from 'models/parameters/components/likeComponentParameters';

export class LikeComponent extends ComponentBase {
    public dislikeCssClass: string = ClassNames.textDanger;
    public likeCssClass: string = ClassNames.textSuccess;

    private readonly _parameters: LikeComponentParameters;
    private _isDeleted: boolean = false;
    private _isLiked: boolean;
    private _defaultDislikeCount: number;
    private _defaultLikeCount: number;
    private _dislikeCount: number;
    private _likeCount: number;

    constructor(parameters: LikeComponentParameters) {
        super();
        this._parameters = parameters;
        this.lock(this._parameters.isLocked);
    }

    public dislikeActionLinkJQuery = () => this.likeJQuery().find(InnerComponentSelectors.actionLink).first();
    public dislikeTextJQuery = () => this.dislikeActionLinkJQuery().find(InnerComponentSelectors.text);
    public likeActionLinkJQuery = () => this.likeJQuery().find(InnerComponentSelectors.actionLink).next();
    public likeJQuery = () => jQuery(ComponentSelectors.like);
    public likeTextJQuery = () => this.likeActionLinkJQuery().find(InnerComponentSelectors.text);

    init(): void {
        this.attachEventHandlers();
        this.mapProperties();
        this.changeLikeCount();
        this.lockUi(this.isLocked());
        this.updateUi();
    }

    attachEventHandlers(): void {
        const component = this;

        this.likeActionLinkJQuery().off().on(EventTypes.click, function (): void {
            component.like(true);
            component.post();
        });

        this.dislikeActionLinkJQuery().off().on(EventTypes.click, function (): void {
            component.like(false);
            component.post();
        });
    }

    public changeLikeCount(): void {
        this._likeCount = this._defaultLikeCount;
        this._dislikeCount = this._defaultDislikeCount;

        if (!this._isDeleted) {
            if (this._isLiked) {
                this._likeCount++;
            } else {
                this._dislikeCount++;
            }
        }
    }

    public like(isLiked: boolean): void {
        this.setLikeBoolean(isLiked);
        this.changeLikeCount();
        this.updateUi();
    }

    public lockUi(isEnabled: boolean): void {
        if (isEnabled) {
            this.dislikeActionLinkJQuery().css(CssProperties.pointerEvents, CssProperties.none);
            this.likeActionLinkJQuery().css(CssProperties.pointerEvents, CssProperties.none);
        } else {
            this.dislikeActionLinkJQuery().css(CssProperties.pointerEvents, String.empty);
            this.likeActionLinkJQuery().css(CssProperties.pointerEvents, String.empty);
        }
    }

    public mapProperties(): void {
        this._isLiked = this._parameters.model.isLiked;
        this._isDeleted = this._parameters.model.isDeleted;
        this._likeCount = parseInt(this.likeTextJQuery().text());
        this._dislikeCount = parseInt(this.dislikeTextJQuery().text());
        this._defaultLikeCount = this._likeCount;
        this._defaultDislikeCount = this._dislikeCount;

        if (!this._isDeleted) {
            if (this._isLiked) this._defaultLikeCount--;
            else this._defaultDislikeCount--;
        }

        if (this._defaultLikeCount < 0) this._defaultLikeCount = 0;
        if (this._defaultDislikeCount < 0) this._defaultDislikeCount = 0;
    }

    public post(): void {
        const apiUrl = this._parameters.apiUrl;

        const data = {
            articleId: this._parameters.model.articleId,
            isLiked: this._isLiked,
            isDeleted: this._isDeleted
        }

        this.ajax({
            url: apiUrl,
            data
        });
    }

    public setLikeBoolean(isLiked: boolean): void {
        if (this._isLiked === isLiked) {
            this._isDeleted = !this._isDeleted;
        } else {
            this._isDeleted = false;
        }

        this._isLiked = isLiked;
    }

    public updateUi(): void {
        this.likeActionLinkJQuery().removeClass(this.likeCssClass);
        this.dislikeActionLinkJQuery().removeClass(this.dislikeCssClass);

        if (this._isDeleted === false) {
            if (this._isLiked) {
                this.likeActionLinkJQuery().addClass(this.likeCssClass);
            } else {
                this.dislikeActionLinkJQuery().addClass(this.dislikeCssClass);
            }
        }

        this.likeTextJQuery().text(this._likeCount);
        this.dislikeTextJQuery().text(this._dislikeCount);
    }
}