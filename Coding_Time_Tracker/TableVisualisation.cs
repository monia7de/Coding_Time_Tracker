using ConsoleTableExt;

namespace coding_time_tracker
{
    internal class TableVisualisation
    {
        internal static void ShowTable<T>(List<T> tableData) where T : class
        {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Habits")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
            
        }
    }
}