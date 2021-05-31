using System.Collections.Generic;

namespace BlogApplication.Models.ViewModels.Base
{
    public abstract class ViewModelBase
    {
        protected ViewModelBase()
        {
            Errors = new List<string>();
        }

        public bool HasErrors => Errors?.Count == 0;

        private IList<string> Errors { get; }

        public void AddError(string message)
        {
            Errors.Add(message);
        }

        public void AddErrors(IEnumerable<string> messages)
        {
            ((List<string>) Errors).AddRange(messages);
        }

        public virtual void ResetValidation()
        {
        }
    }
}