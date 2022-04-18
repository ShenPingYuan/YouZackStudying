using LogServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConsoleLogProviderExtension
    {
        public static IServiceCollection AddConsoleLog(this IServiceCollection services)
        {
            return services.AddScoped<ILogProvider, ConsoleLogProvider>();
        }
    }
}
