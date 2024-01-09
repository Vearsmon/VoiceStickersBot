namespace VoiceStickersBot.Core.CommandArguments;

public class QueryContext
{
    public string CommandType { get; }

    public string CommandStep { get; }

    public List<string> CommandArguments { get; }

    public long ChatId { get; }
    public string ChatType { get; }
    public int? BotMessageId { get; }

    public QueryContext(
        string commandType,
        string commandStep,
        List<string> commandArguments,
        long chatId,
        string chatType="Private",
        int? botMessageId=null)
    {
        CommandType = commandType;
        CommandStep = commandStep;
        CommandArguments = commandArguments;
        
        ChatId = chatId;
        ChatType = chatType;
        
        BotMessageId = botMessageId;
    }
}