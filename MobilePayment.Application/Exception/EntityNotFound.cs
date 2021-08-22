namespace MobilePayment.Application.Exception
{
    public class EntityNotFound : System.Exception
    {
        public EntityNotFound(string name)
            : base(name)
        {
        }
    }
}