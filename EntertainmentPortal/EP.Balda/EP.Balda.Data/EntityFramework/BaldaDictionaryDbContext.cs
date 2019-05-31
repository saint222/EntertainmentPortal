using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Balda.Data.EntityFramework
{
    public class BaldaDictionaryDbContext
    {
        private static string _fileName;
        private static List<string> _stringData;

        public BaldaDictionaryDbContext(string fileName)
        {
            _fileName = fileName;
            var path = GetPath(_fileName);
            var stringData = GetTextFromFile(path);
            _stringData = stringData.Result;
        }

        public static List<string> Set<T>() where T : class
        {
            return _stringData;
        }

        private static string GetPath(string fileName)
        {
            //location in the "/AppData/" directory of our solution
            var startupPath = "";
            var directoryInfo =
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo?.Parent?.Parent != null)
                startupPath =
                    Path.Combine(directoryInfo.Parent.Parent.FullName,
                        $"AppData\\{fileName}");
            return startupPath;
        }

        public static async Task SaveChangesAsync()
        {
            await WriteTextToFile(GetPath(_fileName), _stringData);
        }

        private static async Task<List<string>> GetTextFromFile(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = await reader.ReadToEndAsync();
                return fileText
                    .ToUpper()
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None).ToList();
            }
        }

        private static async Task WriteTextToFile(string file, IEnumerable<string> lines)
        {
            if (File.Exists(file)) File.Delete(file);

            using (var writer = File.OpenWrite(file))
            {
                using (var streamWriter = new StreamWriter(writer))
                {
                    foreach (var line in lines)
                        await streamWriter.WriteLineAsync(line);
                }
            }
        }
    }
}