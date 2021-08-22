namespace MobilePayment.Application.Exception
{
    public class MobileServerNotResponse : System.Exception
    {
        public MobileServerNotResponse(string serverName)
            : base(serverName)
        {
        }
    }
}