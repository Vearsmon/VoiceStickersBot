using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class ShowAllCommandFactory : ICommandFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Показать все", "/show_all" };
    public ICommand CreateCommand(RequestContext requestContext)
    {
        return new ShowAllCommand(requestContext);
    }
}