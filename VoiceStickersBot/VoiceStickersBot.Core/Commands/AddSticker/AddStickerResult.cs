namespace VoiceStickersBot.Core.Commands.AddSticker;

public class AddStickerResult : ICommandResult
{
    public UserBotState UserBotStateFrom { get; }
    
    public AddStickerResult(UserBotState userBotState)
    {
        UserBotStateFrom = userBotState;
    }
}