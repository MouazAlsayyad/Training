namespace Training.Application.Helper.Validators
{
    public static class GuidValidator
    {
        public static bool BeAValidGuid(Guid? guid)
        {
            return guid.HasValue && guid.Value != Guid.Empty;
        }
    }
}
