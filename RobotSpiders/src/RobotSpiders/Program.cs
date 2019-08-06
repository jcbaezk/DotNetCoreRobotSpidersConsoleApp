using System;
using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using RobotSpiders.Core.Mappers;
using RobotSpiders.Exporters.Exporters;
using RobotSpiders.InputReaders.Readers;
using RobotSpiders.Process.Processes;
using RobotSpiders.Validators;

namespace RobotSpiders
{
    class Program
    {
        private static IArgumentsValidator _argumentsValidator;
        private static IRobotSpidersProcess _robotSpidersProcess;

        static void Main(string[] args)
        {
            var serviceProvider = RegisterServices();
            ResolveDependencies(serviceProvider);

            if (_argumentsValidator.IsValid(args))
            {
                try
                {
                    var filePath = args[0];
                    _robotSpidersProcess.Start(filePath);

                    Console.WriteLine("Process finished, review the output file located in the same folder as the input file");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("File path to input data expected.");
            }
        }

        private static ServiceProvider RegisterServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IFileSystem, FileSystem>()
                .Scan(scan => scan
                    .FromAssemblyOf<IArgumentsValidator>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .Scan(scan => scan
                    .FromAssemblyOf<IFileOutputWriter>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .Scan(scan => scan
                    .FromAssemblyOf<ISpiderMapper>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .Scan(scan => scan
                    .FromAssemblyOf<IFileInputReader>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .Scan(scan => scan
                    .FromAssemblyOf<IRobotSpidersProcess>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static void ResolveDependencies(ServiceProvider serviceProvider)
        {
            _robotSpidersProcess = serviceProvider.GetService<IRobotSpidersProcess>();
            _argumentsValidator = serviceProvider.GetService<IArgumentsValidator>();
        }
    }
}
