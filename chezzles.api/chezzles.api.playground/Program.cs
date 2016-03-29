using chezzles.data.EF;
using chezzles.data.Model;
using chezzles.engine.Core.Game;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace chezzles.api.playground
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new GamesStoreContext())
            {
                var parser = new GameParser();
                var repo = new Repository<GameDTO>(context);
                for (int i = 1; i <= 8; i++)
                {
                    using (var errorLog = new StreamWriter($"..\\..\\puzzles\\errorLog.txt"))
                    using (var reader = new StreamReader($"..\\..\\puzzles\\m2_{i}.pgn"))
                    {
                        var builder = new StringBuilder();
                        var line = string.Empty;
                        while (!reader.EndOfStream)
                        {
                            builder.Clear();
                            builder.AppendLine(line);
                            while (!reader.EndOfStream && !(line = reader.ReadLine()).Contains("Event"))
                            {
                                if (line.Contains("Date"))
                                {
                                    line = line.Replace("-", ".");
                                    line.Replace("\"?\"", "\"????\"");
                                }

                                // Skip [Round] as it's not properly parsed by pgn
                                if (line.Contains("Round")) continue; 

                                builder.AppendLine(line);
                            }

                            var text = builder.ToString();
                            text = text.TrimEnd('\r', '\n');
                            try
                            {
                                foreach (var game in parser.Parse(text))
                                {
                                    var gameDto = new GameDTO()
                                    {
                                        PgnString = text
                                    };

                                    repo.Insert(gameDto);
                                    repo.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                errorLog.WriteLine($"File m2_{i}.pgn\n Error: {ex.Message}\n While parsing FEN: {text}");
                            }
                        }
                    }
                    Console.WriteLine($"File m2_{i}.pgn processed");
                }
            }
        }
    }
}
