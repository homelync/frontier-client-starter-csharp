using System;
using System.Threading.Tasks;
using Frontier.Service;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Tests.Integration
{
    public class SimpleFrontierServiceTests
    {
        private readonly SimpleFrontierService simpleService;
        private readonly ITestOutputHelper logger;

        public SimpleFrontierServiceTests(ITestOutputHelper testOutputHelper)
        {
            simpleService = new SimpleFrontierService(TestHelper.GetHttpClientFactory(), TestHelper.GetConfigurationService());
            logger = testOutputHelper;
        }

        [Fact]
        public async Task GetAllDevices()
        {
            var devices = await simpleService.GetDevices();
            Assert.Equal(50, devices.Count);
        }

        [Fact]
        public async Task GetAllProperties()
        {
            var properties = await simpleService.GetProperties();
            Assert.Equal(5, properties.Count);
        }

        [Fact]
        public async Task GetReadingsForOneDayAndOneProperty()
        {
            var propertyReference = TestHelper.GetConfigurationService().GetTestConfiguration().TestPropertReference;
            var readings = await simpleService.GetReadings(propertyReference, new DateTime(2022, 10, 10));

            // Uncomment this lines to log results.
            // logger.WriteLine(String.Join(Environment.NewLine, readings.Select(x => x.ToString())));

            // Save to your database here etc.
        }

        [Fact]
        public async Task GetReadingsForOneProperty()
        {
            var propertyReference = TestHelper.GetConfigurationService().GetTestConfiguration().TestPropertReference;
            // Get readings for the last 5 days, you should track this value. Data available for 30 days.
            var startDate = DateTime.UtcNow.AddDays(-5); ;
            var endDate = DateTime.UtcNow;
            var syncDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);

            while (syncDate < endDate)
            {
                var readings = await simpleService.GetReadings(propertyReference, syncDate);
                syncDate = syncDate.AddDays(1);

                // Uncomment this lines to log results.
                // logger.WriteLine(String.Join(Environment.NewLine, readings.Select(x => x.ToString())));

                // Save to your database here etc.
            }
        }

        [Fact]
        public async Task GetReadingsForAllProperties()
        {
            var properties = await simpleService.GetProperties();

            foreach (var p in properties)
            {
                // Get readings for the last 5 days, you should track this value. Data available for 30 days.
                var startDate = DateTime.UtcNow.AddDays(-5); ;
                var endDate = DateTime.UtcNow;
                var syncDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);

                while (syncDate < endDate)
                {
                    var readings = await simpleService.GetReadings(p.Reference, syncDate);
                    var log = String.Join(Environment.NewLine, readings.Select(x => x.ToString()));
                    syncDate = syncDate.AddDays(1);

                    // Uncomment this lines to log results.
                    // logger.WriteLine(String.Join(Environment.NewLine, readings.Select(x => x.ToString())));

                    // Save to your database here etc.
                }
            }
        }
    }
}