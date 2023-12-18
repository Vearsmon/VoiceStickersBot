using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllResult : ICommandResult
{ 
    public UserBotState UserBotStateFrom { get; }
    public SwitchKeyboardResult SwitchKeyboardResult { get; }
    public ShowAllResult(UserBotState userBotState, SwitchKeyboardResult switchKeyboardResult)
    {
        UserBotStateFrom = userBotState;
        SwitchKeyboardResult = switchKeyboardResult;
    }
}