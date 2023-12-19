namespace VoiceStickersBot.Core.CommandArguments;

public class RequestContext<StepNameType>
{
    public string CommandText { get; }
    public long ChatId { get; }
    public int? MenuPage { get; }
    public UserBotState UserBotState { get; }
    public StepNameType StepName { get; }
    
    public RequestContext(string commandText, long chatId, UserBotState userBotState, StepNameType stepName, int? menuPage = null)
    {
        CommandText = commandText;
        ChatId = chatId;
        UserBotState = userBotState;
        MenuPage = menuPage;
        StepName = stepName;
    }
}