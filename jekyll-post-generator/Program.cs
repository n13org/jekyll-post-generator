using NLipsum.Core;
using System.Text;

namespace dummy_post_generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var start = args[0].Split("-");
                var end = args[1].Split("-");
                var count = int.Parse(args[2]); // 75
                var dir = args[3]; // ..\..\_posts\generated

                // Create folder if it does not exists
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                Generate(
                    new DateTime(int.Parse(start[0]), int.Parse(start[1]), int.Parse(start[2])),
                    new DateTime(int.Parse(end[0]), int.Parse(end[1]), int.Parse(end[2])),
                    count,
                    dir
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurred: " + ex.ToString());

                Console.WriteLine("Please use the program like : dotnet run <START-DATE> <END-DATE> <COUNT-OF-POSTS> <OUTPUT-PATH>");
                Console.WriteLine("Example from Source Code    : dotnet run 2017-01-01 2020-03-31 75 \".\\posts\\generated\"");
                Console.WriteLine("Example from Windows BINARY : .\\jekyll-post-genertor.exe 2017-01-01 2020-03-31 75 \".\\posts\\generated\"");
            }
        }

        /// <summary>
        /// Creates a various number of posts between a time span
        /// </summary>
        /// <param name="start">Start Date, in yyyy-MM-dd</param>
        /// <param name="end">End Date, in yyyy-MM-dd</param>
        /// <param name="count">How many posts between start and end date</param>
        /// <param name="dir">Target Directory of the generated posts. The folder must exist.</param>
        public static void Generate(DateTime start, DateTime end, int count, string dir)
        {
            var delta = (end - start).TotalDays / (count - 1);
            Console.WriteLine("Start : " + start.ToString());
            Console.WriteLine("End   : " + end.ToString());
            Console.WriteLine("Delta : " + delta.ToString());

            var gen = new LipsumGenerator();

            var minTitleWords = 1;
            var maxTitleWords = 3;
            var minParagraphs = 2;
            var maxParagraphs = 17;
            uint minParagraphsWords = 13;
            uint maxParagraphsWords = 250;

            var postNo = 1;
            Random rnd = new Random();
            DateTime d = start;
            while (d <= end)
            {
                var date = d.ToString("yyyy-MM-dd");

                var title = string.Join("-", gen.GenerateWords(rnd.Next(minTitleWords, maxTitleWords)));

                var filename = date + "-" + title + ".md";
                // Console.WriteLine(filename);                

                var paragraphs = gen.GenerateParagraphs(rnd.Next(minParagraphs, maxParagraphs), new Paragraph(minParagraphsWords, maxParagraphsWords));
                var content = string.Join("\n\n", paragraphs);

                var sb = new StringBuilder();
                sb.AppendLine("---");
                sb.AppendLine("layout: post");
                sb.AppendLine("title: \"" + title + "\"");
                sb.AppendLine("date: " + date + " 20:15:00 +0200");
                sb.AppendLine("categories: [ \"generated\" ]");
                sb.AppendLine("tags: [ \"kargware\", \"theme\", \"dummy\", \"generated\" ]");
                sb.AppendLine("---");
                sb.AppendLine(content);

                var sw = new StreamWriter(dir + Path.DirectorySeparatorChar + filename, false, Encoding.UTF8);
                sw.WriteLine(sb.ToString().TrimEnd());
                sw.Close();

                Console.WriteLine(postNo.ToString("00000") + " " + filename + " has been written!");

                d = d.AddDays(delta);
                postNo++;
            }
        }
    }
}