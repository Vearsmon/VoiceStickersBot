using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSwitchKeyboardStickersCommandArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SwitchKeyboardStickers;

    private readonly Guid stickerPackId;

    public ShowAllSwitchKeyboardStickersCommandArguments(Guid stickerPackId)
    {
        this.stickerPackId = stickerPackId;
    }
}