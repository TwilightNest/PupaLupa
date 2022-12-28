namespace PupaLupaServer.Services
{
    public class StatisticService : BackgroundService
    {
        private const int generalDelay = 1 * 10 * 1000; // 10 seconds

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, stoppingToken);
                await ProcessStatisticAsync();
            }
        }

        private static Task ProcessStatisticAsync()
        {
            for (int i = 0; i < 1; i++)
            {

            }

            return Task.FromResult("Done");
        }
    }
}