namespace VoiceStickersBot.Core.Commands;

public class RequestContext
{
    public long ChatId { get; }
    public int? MenuPage { get; }
    public UserBotState UserBotState { get; }
    
    public RequestContext(long chatId, UserBotState userBotState, int? menuPage=null)
    {
        ChatId = chatId;
        UserBotState = userBotState;
        MenuPage = menuPage;
    }
}