using System;
using System.Collections.Generic;

public delegate void CommandOutput(string result);
public delegate string CommandInput();

interface ICommand
{
    void Execute(CommandOutput output, CommandInput input);
}

class AddCommand : ICommand
{
    public void Execute(CommandOutput output, CommandInput input)
    {
        int x = int.Parse(input());
        int y = int.Parse(input());
        output($"Result: {x + y}");
    }
}

class SubtractCommand : ICommand
{
    public void Execute(CommandOutput output, CommandInput input)
    {
        int x = int.Parse(input());
        int y = int.Parse(input());
        output($"Result: {x - y}");
    }
}

class ExitCommand : ICommand
{
    private readonly Action _exitHandler;

    public ExitCommand(Action exitHandler) => _exitHandler = exitHandler;

    public void Execute(CommandOutput output, CommandInput input)
    {
        _exitHandler();
        output("Exiting program...");
    }
}

class InvalidCommand : ICommand
{
    public void Execute(CommandOutput output, CommandInput input) => output("Invalid command");
}

class CalculatorProgram
{
    private bool _running = true;
    private readonly Dictionary<string, ICommand> _commands;

    public CalculatorProgram()
    {
        _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase)
        {
            { "add", new AddCommand() },
            { "sub", new SubtractCommand() },
            { "exit", new ExitCommand(() => _running = false) }
        };
    }

    public void Run()
    {
        CommandInput input = () => Console.ReadLine() ?? string.Empty;
        CommandOutput output = Console.WriteLine;

        while (_running)
        {
            output("Enter command (add/sub/exit):");
            string command = input().Trim();

            if (_commands.TryGetValue(command, out ICommand? cmd))
            {
                cmd.Execute(output, input);
            }
            else
            {
                new InvalidCommand().Execute(output, input);
            }
        }
    }
}

class Program
{
    static void Main(string[] args) => new CalculatorProgram().Run();
}