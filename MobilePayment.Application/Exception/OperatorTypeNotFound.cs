namespace MobilePayment.Application.Exception
{
    public class OperatorTypeNotFound : System.Exception
    {
        public OperatorTypeNotFound(string prefix)
            : base(prefix)
        {
        }
    }
}