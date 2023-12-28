namespace VoiceStickersBot.Core.Commands;

public class RequestContextObsolete
{
    public string CommandText { get; }
    public long ChatId { get; }
    public int? MenuPage { get; }
    public UserBotState UserBotState { get; }
    
    public RequestContextObsolete(string commandText, long chatId, UserBotState userBotState, int? menuPage=null)
    {
        CommandText = commandText;
        ChatId = chatId;
        UserBotState = userBotState;
        MenuPage = menuPage;
    }
}