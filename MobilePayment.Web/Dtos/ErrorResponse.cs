using System.Collections.Generic;

namespace MobilePayment.Web.Dtos
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public int Code { get; init; }
        public string Title { get; init; }
        public IEnumerable<string> Errors { get; init; }
    }
}