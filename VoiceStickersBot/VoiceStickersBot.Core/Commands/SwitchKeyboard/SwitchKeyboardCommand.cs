namespace VoiceStickersBot.Core;

public class SwitchKeyboardCommand : ICommand
{

    public PageChangeType PageChangeType { get; }
    public int pageFrom { get; }
    public int stickersOnPage { get; }
    

    public SwitchKeyboardCommand(int pageFrom, int stickersOnPage, string callbackText)
    {
        if (callbackText == "pageleft")
            PageChangeType = PageChangeType.Decrease;
        else if (callbackText == "pageright")
            PageChangeType = PageChangeType.Increase;

        this.pageFrom = pageFrom;
        this.stickersOnPage = stickersOnPage;
    }
}