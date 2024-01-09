namespace VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;

public class ShowAllSendStickerArguments : IShowAllCommandArguments
{
    public CommandType CommandType => CommandType.ShowAll;

    public ShowAllStepName StepName => ShowAllStepName.SendSticker;
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    
    public ShowAllSendStickerArguments(
        Guid stickerPackId,
        Guid stickerId,
        long chatId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
    }
}