namespace MagicVilla_DB.Exceptions
{
    public class InvalidRequestValueException: Exception
    {
        public Dictionary<String, List<String>>? Errors { get; set; }
        public String ErrorMessage { get; set; }
        public InvalidRequestValueException(Dictionary<String, List<String>> ? errors, string? message) : base(GetErrorMessage(message))
        {
            
            ErrorMessage = GetErrorMessage(message);
            Errors = errors;
        }

        public InvalidRequestValueException(Dictionary<String, List<String>>? errors) : base(GetErrorMessage(null))
        {

            ErrorMessage = GetErrorMessage(null);
            Errors = errors;
        }

        private static string GetErrorMessage(string? message)
        {
            return message ?? "INVALID_REQUEST_EXCEPTION";
        }
    }
}
