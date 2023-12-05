namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardCommand : ICommand
{
    public SwitchKeyboardCommand(int pageFrom, int stickersOnPage, string callbackText)
    {
        if (callbackText == "pageleft")
            PageChangeType = PageChangeType.Decrease;
        else if (callbackText == "pageright")
            PageChangeType = PageChangeType.Increase;

        this.pageFrom = pageFrom;
        this.stickersOnPage = stickersOnPage;
    }

    public PageChangeType PageChangeType { get; }
    public int pageFrom { get; }
    public int stickersOnPage { get; }
}