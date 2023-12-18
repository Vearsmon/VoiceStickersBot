using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.EnterCommand;

public class EnterResult : ICommandResult
{
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResult SwitchKeyboardResult { get; }
    public EnterResult(UserBotState userBotState, SwitchKeyboardResult switchKeyboardResult)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResult = switchKeyboardResult;
    }
}