namespace MVPPhonebookApp.Core.Services
{
    public class ValidationException : Exception
    {
        public ValidationException() { }

        public ValidationException(string message) : 
            base(message) { }
        public ValidationException(string message, Exception innerException) : 
            base(message, innerException) { }
    }
}
