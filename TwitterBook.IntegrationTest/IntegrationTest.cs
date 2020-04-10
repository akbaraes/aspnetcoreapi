
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

using TwitterBook.Data;
using TwitterBook.Models;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace TwitterBook.IntegrationTest
{
    public class IntegrationTest 
    {
        protected readonly HttpClient TestClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        protected IntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            TestClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        protected async Task SetAuthenticateKey()
        {
            //AuthResponse authResponse = await DoLogin();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwNmM0YTcyNC1kYTQ2LTQzNGEtODg5Zi03NTdmNTliOGVlOTYiLCJyb2xlIjoiQWRtaW4sRWRpdG9yIiwiZW1haWwiOiJha2Jhcmppbm5hLm1AZ21haWwuY29tIiwibmJmIjoxNTg2NTE0MjE0LCJleHAiOjE2MTgwNTAyMTMsImlhdCI6MTU4NjUxNDIxNH0.Rg2hPW1ohr2jPMDngTQFfVeIvlfTxreH0PXnkiNeSng";

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        private async Task<AuthResponse> DoLogin()
        {

            AuthResponse authResponse = new AuthResponse();
            HttpClient client = new HttpClient();


            //client.BaseAddress = new Uri("http://localhost:5007/api/Login");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //string urlParameters = "?Email=akbarjinna.m@gmail.com&Password=Jinna@1988";
            // List data response.
            HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:5007/api/Login", new { Email = "akbarjinna.m@gmail.com", Password = "Jinna@1988" }).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                authResponse = await response.Content.ReadAsAsync<AuthResponse>();


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

            return authResponse;
        }

        
    }

    public class AuthResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }

    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();


            });
        }
    }
}
