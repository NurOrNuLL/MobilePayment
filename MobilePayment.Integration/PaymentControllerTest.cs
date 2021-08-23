using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Integration.Fakes;
using MobilePayment.Web;
using MobilePayment.Web.Endpoints.Payments;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MobilePayment.Integration
{
    public class PaymentControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PaymentControllerTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PaymentContext>));

                    services.Remove(dbContextOptions);
                    services.RemoveAll(typeof(IMobileOperator));

                    services.AddScoped<IMobileOperator, FakeMobileOperatorFailure>();
                    services.AddScoped<IMobileOperator, FakeMobileOperatorSuccess>();
                    services.AddScoped<IMobileOperator, FakeMobileOperatorThrowException>();

                    services.AddScoped<ITransactionRepository, FakeTransactionRepository>();
                    services.AddScoped<IOperatorPrefixRepository, FakeOperatorPrefixRepository>();
                }))
                .CreateClient();

           
        }

        [Fact]
        public async Task Should_Return_Success_Result()
        {
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru-kz");
            var request = JsonConvert.SerializeObject(new { Amount = 200, PhoneNumber = "5060000000" });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync(CreatePaymentCommand.Route, content);

            var textResponse = await response.Content.ReadAsStringAsync();
            textResponse.Should().ContainAll("Beeline", "Success");
        }
        
        [Fact]
        public async Task Should_Return_Failure_Result()
        {
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru-kz");
            var request = JsonConvert.SerializeObject(new { Amount = 200, PhoneNumber = "5050000000" });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync(CreatePaymentCommand.Route, content);

            var textResponse = await response.Content.ReadAsStringAsync();
            textResponse.Should().ContainAll("Altel", "Failure");
        }
        
        [Fact]
        public async Task Should_Throw_Exception_Result()
        {
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru-kz");
            var request = JsonConvert.SerializeObject(new { Amount = 200, PhoneNumber = "5070000000" });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync(CreatePaymentCommand.Route, content);

            var textResponse = await response.Content.ReadAsStringAsync();
            textResponse.Should().ContainAll("Сервис не отвечает");
        }
    }
}