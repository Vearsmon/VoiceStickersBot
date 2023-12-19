using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllResultObsolete : ICommandResultObsolete
{ 
    public SwitchKeyboardResultObsolete SwitchKeyboardResultObsolete { get; }
    public ShowAllResultObsolete(SwitchKeyboardResultObsolete switchKeyboardResultObsolete)
    {
        SwitchKeyboardResultObsolete = switchKeyboardResultObsolete;
    }
}