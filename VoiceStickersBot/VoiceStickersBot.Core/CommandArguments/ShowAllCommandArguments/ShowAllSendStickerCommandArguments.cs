using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSendStickerCommandArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SendSticker;
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    
    public ShowAllSendStickerCommandArguments(Guid stickerPackId, Guid stickerId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
    }
}