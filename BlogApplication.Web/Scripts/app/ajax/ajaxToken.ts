export class AjaxToken {
    public static getRequestVerificationToken(html: string = '#__AjaxAntiForgeryForm'): string | number | string[] | undefined {
        const antiForgeryForm = jQuery(html);
        return jQuery('input[name="__RequestVerificationToken"]', antiForgeryForm).val();
    }
}