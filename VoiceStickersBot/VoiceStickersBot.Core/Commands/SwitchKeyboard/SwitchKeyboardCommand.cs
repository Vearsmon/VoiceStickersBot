namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardCommand : ICommand
{
    public SwitchKeyboardCommand(int pageFrom, int stickersOnPage, string callbackText)
    {
        if (callbackText == "pageleft")
            PageChangeType = PageChangeType.Decrease;
        else if (callbackText == "pageright")
            PageChangeType = PageChangeType.Increase;

        PageFrom = pageFrom;
        StickersOnPage = stickersOnPage;
    }

    public PageChangeType PageChangeType { get; }
    public int PageFrom { get; }
    public int StickersOnPage { get; }
}