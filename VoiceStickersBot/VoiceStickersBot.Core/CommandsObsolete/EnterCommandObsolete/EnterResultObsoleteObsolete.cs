using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterResultObsoleteObsolete : ICommandResultObsoleteObsolete
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResultObsoleteObsolete SwitchKeyboardResultObsoleteObsolete { get; }
    public EnterResultObsoleteObsolete(UserBotState userBotState, SwitchKeyboardResultObsoleteObsolete switchKeyboardResultObsoleteObsolete)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResultObsoleteObsolete = switchKeyboardResultObsoleteObsolete;
    }
}