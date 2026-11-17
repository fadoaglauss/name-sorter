using Microsoft.Extensions.DependencyInjection;
using NameSorter.Application;
using NameSorter.Services;

var services = new ServiceCollection();

services.AddSingleton<IIOService, IOService>();
services.AddSingleton<ISorterService, SorterService>();
services.AddTransient<INameSorterApplication, NameSorterApplication>();

using var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider
    .GetRequiredService<INameSorterApplication>();

return application.Run(args);