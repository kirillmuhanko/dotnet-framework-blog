using System.Collections.Generic;

namespace BlogApplication.Models.DomainModels.Common
{
    public class ResultStatusDomainModel
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}