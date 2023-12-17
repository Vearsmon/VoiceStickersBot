namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterResult : ICommandResult
{
    public UserBotState UserBotStateFrom { get; }
    
    public EnterResult(UserBotState userBotState)
    {
        UserBotStateFrom = userBotState;
    }
}