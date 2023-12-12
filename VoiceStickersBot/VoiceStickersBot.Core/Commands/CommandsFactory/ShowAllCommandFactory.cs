using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class ShowAllCommandFactory : ICommandFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Посмотреть все", "/show_all" };
    public ICommand CreateCommand(CommandObject commandObject)
    {
        return new ShowAllCommand();
    }
}