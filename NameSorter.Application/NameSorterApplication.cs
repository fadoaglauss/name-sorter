using NameSorter.Services;

namespace NameSorter.Application
{
    public class NameSorterApplication(
        IIOService fileService,
        ISorterService sorter) : INameSorterApplication
    {
        private readonly IIOService _ioService = fileService;
        private readonly ISorterService _sorter = sorter;

        /// <inheritdoc/>
        public int Run(string[] args)
        {
            if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Usage: name-sorter <file-path>");
                return 1;
            }

            try
            {
                var names = _ioService.Read(args[0]);
                var sortedNames = _sorter.Run(names);

                _ioService.Write(args[0], sortedNames);

                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                }

                return 0;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"Invalid input: {ex.Message}");
                return 1;
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"File error: {ex.Message}");
                return 1;
            }
        }
    }
}
