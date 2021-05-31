import {AjaxToken} from 'ajax/ajaxToken';

export class AjaxClient {
    public static post(settings: JQuery.AjaxSettings): JQuery.jqXHR {
        const data = settings['data'] as object;

        const ajaxSettings = {
            ...settings,
            type: 'POST',
            data: {
                ...data,
                __RequestVerificationToken: AjaxToken.getRequestVerificationToken()
            }
        } as JQuery.AjaxSettings;

        return jQuery.ajax(ajaxSettings);
    }

    public static postModel(model: object, url: string): JQuery.jqXHR {
        const ajaxSettings = {
            ...model,
            __RequestVerificationToken: AjaxToken.getRequestVerificationToken()
        } as JQuery.AjaxSettings;

        return jQuery.post(url, ajaxSettings);
    }
}