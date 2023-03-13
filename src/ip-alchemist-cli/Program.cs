// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text;
using Spectre.Console;

Console.Title = "ip-alchemist-cli";
Console.OutputEncoding = Encoding.UTF8;

AnsiConsole.Write(new FigletText("ip-alchemist-cli").Color(Color.DarkSlateGray2));

var version = Assembly.GetExecutingAssembly().GetName().Version;
AnsiConsole.MarkupLine("[italic]version {0}[/]".PadLeft(110), version);

Console.ReadLine();
