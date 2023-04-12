using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System;
using Faker;

namespace WeatherApiTests
{
    [TestFixture]
    public class WeatherApiTests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://api.openweathermap.org/data/2.5/");
        }

        [Test]
        public void OpenGetWeatherByCityName()
        {

            // Arrange
            var country = Faker.Address.Country();
            var request = new RestRequest("weather", Method.GET);
            Console.WriteLine("The Country is " + country );
            request.AddParameter("q", country);
            request.AddParameter("appid", "2b1fd2d7f77ccf1b7de9b441571b39b8");

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            /*Assert.That(response.Headers.Count, Is.GreaterThan(0));
            Assert.That(response.Content.Length, Is.GreaterThan(0));
           // Assert.IsTrue(response.Content.Contains("London"));*/
            var deserializer = new JsonDeserializer();
            var post = deserializer.Deserialize<Post>(response);
            long timeZoneOffset = post.timezone;
            long UnnixDate = post.dt;

            Console.WriteLine($"timeZone: {post.timezone} ");
            long linuxDate = UnnixDate; // Example Linux date in Unix timestamp format
            
           // Console.WriteLine(timeString);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(linuxDate);
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeZoneOffset);
            DateTimeOffset adjustedDateTimeOffset = dateTimeOffset.Add(timeSpan);

            Console.WriteLine("Current time with time zone offset: " + adjustedDateTimeOffset.ToString());
        }

        [Test]
        public void OpenGetWeatherByZipCode()
        {
            // Arrange
            var request = new RestRequest("weather", Method.GET);
            request.AddParameter("zip", "673522,in");
            request.AddParameter("appid", "2b1fd2d7f77ccf1b7de9b441571b39b8");

            // Act
            var response = client.Execute(request);
            //Console.WriteLine(response.Content);
            


            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            // Assert.That(response.Headers.Count, Is.GreaterThan(0));
            //Assert.That(response.Content.Length, Is.GreaterThan(0));
            //Assert.IsTrue(response.Content.Contains("New York"));
            var deserializer = new JsonDeserializer();
            var post = deserializer.Deserialize<Post>(response);

            Console.WriteLine($"timeZone: {post.timezone} ");
        }
        [Test]
        public void FakerStuff()
        {
            var country = Faker.Address.Country(); // Generates a random country name
            //var capital = Faker.Address.CapitalCity(country); // Generates the capital city of the specified country
            Console.WriteLine("The capital of " + country + " is " );

            long unixTime = 1681078052; // Example Unix timestamp
            int timeZoneOffset = 7200; // Example time zone offset in seconds (1 hour = 3600 seconds)

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeZoneOffset);
            DateTimeOffset adjustedDateTimeOffset = dateTimeOffset.Add(timeSpan);

            Console.WriteLine("Current time with time zone offset: " + adjustedDateTimeOffset.ToString());

        }

        class Post
        {
            public int timezone { get; set; }
            public int dt { get; set; }

        }
    }
}
