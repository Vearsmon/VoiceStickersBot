namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllCommand : ICommand
{
    public Type CommandType => typeof(ShowAllCommand);
    public UserBotState UserBotState { get; }
    
    public ShowAllCommand(UserBotState userBotState)
    {
        UserBotState = userBotState;
    }

}