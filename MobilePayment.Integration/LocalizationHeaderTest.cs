using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MobilePayment.Web;
using MobilePayment.Web.Endpoints.Payments;
using Newtonsoft.Json;
using Xunit;

namespace MobilePayment.Integration
{
    public class LocalizationHeaderTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly StringContent _content;

        public LocalizationHeaderTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var request = JsonConvert.SerializeObject(new { Amount = 200, PhoneNumber = "7079239374" });
            _content = new StringContent(request, Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task Get_Correct_Ru_Localization_Header()
        {
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru-kz");
            var response = await _client.PostAsync(CreatePaymentCommand.Route, _content);

            response.EnsureSuccessStatusCode();
            response.Headers.GetValues("Accept-Language").Should().Contain("ru-kz");
        }

        [Fact]
        public async Task Get_Correct_Kz_Localization_Header()
        {
            _client.DefaultRequestHeaders.Add("Accept-Language", "kk-kz");
            var response = await _client.PostAsync(CreatePaymentCommand.Route, _content);

            response.EnsureSuccessStatusCode();
            response.Headers.GetValues("Accept-Language").Should().Contain("kk-kz");
        }

        [Fact]
        public async Task Get_Correct_Default_Localization_Header()
        {
            var response = await _client.PostAsync(CreatePaymentCommand.Route, _content);

            response.EnsureSuccessStatusCode();
            response.Headers.GetValues("Accept-Language").Should().Contain("ru-kz");
        }
    }
}