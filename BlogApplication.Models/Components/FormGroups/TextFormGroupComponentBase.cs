using System.Web.Mvc;

namespace BlogApplication.Models.Components.FormGroups
{
    public abstract class TextFormGroupComponentBase : FormGroupComponentBase
    {
        [AllowHtml] public string Text { get; set; }
    }
}