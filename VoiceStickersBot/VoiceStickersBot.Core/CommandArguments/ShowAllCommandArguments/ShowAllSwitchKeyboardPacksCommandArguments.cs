using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardPacksCommandArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SwitchKeyboardPacks;
    
    public string UserId { get; }
    public int PageFrom { get; }
    public PageChangeDirection  Direction { get; }
    public int PacksOnPage { get; }
    
    public ShowAllSwitchKeyboardPacksCommandArguments(
        string userId, 
        int pageFrom, 
        PageChangeDirection direction, 
        int packsOnPage)
    {
        UserId = userId;
        PageFrom = pageFrom;
        Direction = direction;
        PacksOnPage = packsOnPage;
    }
}