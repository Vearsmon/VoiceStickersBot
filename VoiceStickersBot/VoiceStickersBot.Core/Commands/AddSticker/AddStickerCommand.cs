namespace VoiceStickersBot.Core.Commands.AddSticker;

public class AddStickerCommand : ICommand
{
    public Type CommandType => typeof(AddStickerCommand);
    public UserBotState UserBotState { get; }
    
    public AddStickerCommand(UserBotState userBotState)
    {
        UserBotState = userBotState;
    }
}