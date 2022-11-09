using System;
using System.Threading.Tasks;
using Frontier.Service;
using Frontier.Services;
using Frontier.Utility;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Tests.Integration
{
    public class AdvancedFrontierServiceTests
    {
        private readonly AuthenticationService authService;
        private readonly IRichLogger logger;
        private readonly AdvanecdFrontierService frontierService;
        private readonly RestService restService;
        private readonly IRichLogger testLogger;

        public AdvancedFrontierServiceTests(ITestOutputHelper testOutputHelper)
        {
            logger = TestHelper.GetLogger();
            testLogger = TestHelper.GetTestLogger(testOutputHelper);
            authService = new AuthenticationService(TestHelper.GetHttpClientFactory(), TestHelper.GetConfigurationService(), logger);
            restService = new RestService(TestHelper.GetHttpClientFactory(), authService, logger);
            frontierService = new AdvanecdFrontierService(TestHelper.GetConfigurationService(), restService, logger);
        }

        [Fact]
        public async Task GetAllDevices()
        {
            var devices = await frontierService.GetDevices();
            Assert.Equal(89, devices.Count);
        }

        [Fact]
        public async Task GetAllProperties()
        {
            var range = Enumerable.Range(0, 500);
            var properties = await frontierService.GetProperties();
            Assert.Equal(5, properties.Count);
        }

        [Fact]
        public async Task GetReadingsForAllProperties()
        {
            var properties = await frontierService.GetProperties();

            foreach (var p in properties)
            {
                // Get readings for the last 5 days, you should track this value. Data available for 30 days.
                var startDate = DateTime.UtcNow.AddDays(-5); ;
                var endDate = DateTime.UtcNow;
                var syncDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);

                while (syncDate < endDate)
                {
                    var readings = await frontierService.GetReadings(p.Reference, syncDate);
                    var log = String.Join(Environment.NewLine, readings.Select(x => x.ToString()));
                    syncDate = syncDate.AddDays(1);

                    // Uncomment this lines to log results.
                    logger.LogInformation(String.Join(Environment.NewLine, readings.Select(x => x.ToString())));

                    // Save to your database here etc.
                }
            }
        }

        [Fact]
        public async Task GetReadingsForOneProprty()
        {
            var propertyReference = TestHelper.GetConfigurationService().GetTestConfiguration().TestPropertReference;

            // Get readings for the last 5 days, you should track this value. Data available for 30 days.
            var startDate = DateTime.UtcNow.AddDays(-5); ;
            var endDate = DateTime.UtcNow;
            var syncDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);

            while (syncDate < endDate)
            {
                var readings = await frontierService.GetReadings(propertyReference, syncDate);
                var log = String.Join(Environment.NewLine, readings.Select(x => x.ToString()));
                syncDate = syncDate.AddDays(1);

                // Uncomment this lines to log results.
                logger.LogInformation(String.Join(Environment.NewLine, readings.Select(x => x.ToString())));

                // Save to your database here etc.
            }
        }
    }
}