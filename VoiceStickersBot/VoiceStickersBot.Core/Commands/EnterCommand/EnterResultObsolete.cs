using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterResultObsolete : ICommandResultObsolete
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResultObsolete SwitchKeyboardResultObsolete { get; }
    public EnterResultObsolete(UserBotState userBotState, SwitchKeyboardResultObsolete switchKeyboardResultObsolete)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResultObsolete = switchKeyboardResultObsolete;
    }
}