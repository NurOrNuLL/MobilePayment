using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobilePayment.Unit.Helpers
{
    public static class FakeValidationResult
    {
        public static List<ValidationResult> ValidateModel<T>(T model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            var tryValidateObject = Validator.TryValidateObject(model, context, result, true);
            return result;
        }
    }
}