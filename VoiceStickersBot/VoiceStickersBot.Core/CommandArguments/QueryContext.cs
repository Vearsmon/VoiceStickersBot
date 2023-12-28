namespace VoiceStickersBot.Core.CommandArguments;

public class QueryContext
{
    public string CommandType { get; }

    public string CommandStep { get; }

    public IReadOnlyList<string> CommandArguments { get; }

    public long ChatId { get; }
    public string BotMessageId { get; }
    public int? MenuPage { get; }

    public QueryContext(
        string commandType,
        string commandStep,
        IReadOnlyList<string> commandArguments,
        long chatId,
        string botMessageId = null,
        int? menuPage = null)
    {
        CommandType = commandType;
        CommandStep = commandStep;
        CommandArguments = commandArguments;

        BotMessageId = botMessageId;
        ChatId = chatId;
        MenuPage = menuPage;
    }
}