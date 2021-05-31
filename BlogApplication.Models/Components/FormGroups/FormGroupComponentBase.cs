namespace BlogApplication.Models.Components.FormGroups
{
    public abstract class FormGroupComponentBase
    {
        public bool HasErrors => WasValidated && IsInvalid;

        public bool IsOk => WasValidated && !IsInvalid;

        private bool IsInvalid { get; set; }

        private bool WasValidated { get; set; }

        public void ResetValidation()
        {
            IsInvalid = false;
            WasValidated = false;
        }

        public void SetValidationState(bool isValid)
        {
            if (!isValid) IsInvalid = true;

            WasValidated = true;
        }
    }
}