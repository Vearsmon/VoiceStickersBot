namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterCommand : ICommand
{
    public Type CommandType => typeof(EnterCommand);
    public UserBotState UserBotState { get; }
    
    public EnterCommand(UserBotState userBotState)
    {
        UserBotState = userBotState;
    }
}