using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.AddSticker;

public class AddStickerResult : ICommandResult
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResult SwitchKeyboardResult { get; }
    public AddStickerResult(UserBotState userBotState, SwitchKeyboardResult switchKeyboardResult)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResult = switchKeyboardResult;
    }
}