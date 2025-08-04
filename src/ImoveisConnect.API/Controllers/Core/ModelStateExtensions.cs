using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ImoveisConnect.API.Controllers.Core
{
    public static class ModelStateExtensions
    {
        public static Dictionary<string, List<string>> GetValidationErrors(this ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, List<string>>();

            foreach (var entry in modelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    errors[entry.Key] = entry.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();
                }
            }

            return errors;
        }
    }
}
