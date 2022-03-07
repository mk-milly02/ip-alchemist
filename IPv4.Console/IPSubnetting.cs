using Spectre.Console;

namespace IPv4.Console
{
    public class IPSubnetting
    {
        static string AskForAvailableIPAddress()
        {
            var ip = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] What is the available IP Address [blue]eg. 196.128.0.4[/]: ")
                .PromptStyle(new Style(Color.Aqua))
                .Validate(ip => IPv4Extensions.IsIPAddress(ip)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! This is not a vaild IP address.[/]")));

            return ip;
        }

        static int AskForNumberOfSubnets()
        {
            var subnets = AnsiConsole.Prompt(
                new TextPrompt<string>("[lime]?[/] Enter your desired number of subnets: ")
                .PromptStyle(new Style(Color.DarkOrange))
                .Validate(number => IPv4Extensions.ValidateNumberOfSubnets(number)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]! Invalid number of subnets.[/]"))
            );

            return int.Parse(subnets);
        }

        static List<int> AskForNumberOfHosts()
        {
            int numberOfSubnets = AskForNumberOfSubnets();
            List<int> output = new();

            for (int i = 0; i < numberOfSubnets; i++)
            {
                var hosts = AnsiConsole.Prompt(
                    new TextPrompt<string>($"[lime]?[/] Enter number of hosts in subnet {i++}: ")
                    .PromptStyle(new Style(Color.Purple4))
                    .Validate(number => int.TryParse(number, out _)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]! Invalid number of hosts.[/]")));

                output.Add(int.Parse(hosts));
            }

            return output;
        }
    }
}