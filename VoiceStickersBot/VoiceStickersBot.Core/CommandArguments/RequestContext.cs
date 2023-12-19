namespace VoiceStickersBot.Core.CommandArguments;

public class RequestContext
{
    public string CommandType { get; }

    public string CommandStep { get; }

    public IReadOnlyList<string> CommandArguments { get; }

    public long ChatId { get; }
    public int? MenuPage { get; }

    public RequestContext(
        string commandType,
        string commandStep,
        IReadOnlyList<string> commandArguments,
        long chatId,
        int? menuPage = null)
    {
        CommandType = commandType;
        CommandStep = commandStep;
        CommandArguments = commandArguments;

        ChatId = chatId;
        MenuPage = menuPage;
    }
}