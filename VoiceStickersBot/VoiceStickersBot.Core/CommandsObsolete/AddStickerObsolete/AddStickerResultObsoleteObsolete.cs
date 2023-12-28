using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandsObsolete.AddStickerObsolete;

public class AddStickerResultObsoleteObsolete : ICommandResultObsoleteObsolete
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResultObsoleteObsolete SwitchKeyboardResultObsoleteObsolete { get; }
    public AddStickerResultObsoleteObsolete(UserBotState userBotState, SwitchKeyboardResultObsoleteObsolete switchKeyboardResultObsoleteObsolete)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResultObsoleteObsolete = switchKeyboardResultObsoleteObsolete;
    }
}