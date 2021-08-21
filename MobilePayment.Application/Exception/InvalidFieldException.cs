namespace MobilePayment.Application.Exception
{
    public class InvalidFieldException : System.Exception
    {
        public InvalidFieldException(string fieldName)
            : base(fieldName)
        {
        }
    }
}