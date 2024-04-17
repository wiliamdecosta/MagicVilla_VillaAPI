using MagicVilla_DB.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_DB.Utils
{
    public class RequestValidator
    {
        public RequestValidator() { }
        public Dictionary<String, List<String>> Errors { get; set; }

        public bool Validate(object request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                Dictionary<String, List<String>> _errors = new Dictionary<String, List<String>>();
                foreach (var validationResult in validationResults)
                {
                    string memberName = validationResult.MemberNames.First();
                    string firstLetterLowercase = char.ToLowerInvariant(memberName[0]).ToString();
                    string restOfTheString = memberName.Substring(1);
                    memberName = firstLetterLowercase + restOfTheString;
                    _errors.Add(memberName, [validationResult.ErrorMessage]);
                }
                Errors = _errors;
                return false;
            }

            return true;
        }
    }
}
