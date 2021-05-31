import {ComponentBase} from 'components/componentBase';
import {EventTypes} from 'constants/eventTypes';

export class ScrollComponent extends ComponentBase {
    public offset: number = 100;
    private _prevScroll: number;

    constructor() {
        super();
    }

    public onScrollBottom = () => void 0;

    init(): void {
        this.attachEventHandlers();
    }

    attachEventHandlers(): void {
        const component = this;

        jQuery(window).off().on(EventTypes.scroll, function (): void {
            const docHeight = jQuery(document).height() - component.offset;
            const isBottom = component._prevScroll < this.scrollY;

            if(isBottom && jQuery(window).scrollTop() + jQuery(window).height() > docHeight) {
                component.onScrollBottom();
            }

            component._prevScroll = this.scrollY;
        });
    }
}