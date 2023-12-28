using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.Commands.ShowAll;

public class ShowAllResultObsoleteObsolete : ICommandResultObsoleteObsolete
{ 
    public SwitchKeyboardResultObsoleteObsolete SwitchKeyboardResultObsoleteObsolete { get; }
    public ShowAllResultObsoleteObsolete(SwitchKeyboardResultObsoleteObsolete switchKeyboardResultObsoleteObsolete)
    {
        SwitchKeyboardResultObsoleteObsolete = switchKeyboardResultObsoleteObsolete;
    }
}