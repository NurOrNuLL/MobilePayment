using System.Collections.Generic;

namespace MobilePayment.Web.Dtos
{
    public class ErrorResponse
    {
        public int Code { get; init; }
        public string Title { get; init; }
        public List<string> Errors { get; init; }
    }
}