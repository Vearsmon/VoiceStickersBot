namespace VoiceStickersBot.Core.Commands;

public class RequestContext
{
    public string CommandText { get; }
    public long ChatId { get; }
    public int? MenuPage { get; }
    public UserBotState UserBotState { get; }
    
    public RequestContext(string commandText, long chatId, UserBotState userBotState, int? menuPage=null)
    {
        CommandText = commandText;
        ChatId = chatId;
        UserBotState = userBotState;
        MenuPage = menuPage;
    }
}