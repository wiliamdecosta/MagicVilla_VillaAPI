namespace MagicVilla_DB.Utils
{
    public class EVUtil
    {
        public static string GetValue(string varName)
        {
            string? result = Environment.GetEnvironmentVariable(varName);
            return result;
        }
    }
}
