using System.Web;

namespace BlogApplication.Models.Components.FormGroups
{
    public class ImageFormGroupComponent : FormGroupComponentBase
    {
        public HttpPostedFileBase Image { get; set; }
    }
}