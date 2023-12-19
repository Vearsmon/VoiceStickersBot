using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.AddSticker;

public class AddStickerResultObsolete : ICommandResultObsolete
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResultObsolete SwitchKeyboardResultObsolete { get; }
    public AddStickerResultObsolete(UserBotState userBotState, SwitchKeyboardResultObsolete switchKeyboardResultObsolete)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResultObsolete = switchKeyboardResultObsolete;
    }
}