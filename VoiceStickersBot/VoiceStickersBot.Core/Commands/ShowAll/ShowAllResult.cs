namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllResult : ICommandResult
{ 
    public UserBotState UserBotStateFrom { get; }
    
    public ShowAllResult(UserBotState userBotState)
    {
        UserBotStateFrom = userBotState;
    }
}