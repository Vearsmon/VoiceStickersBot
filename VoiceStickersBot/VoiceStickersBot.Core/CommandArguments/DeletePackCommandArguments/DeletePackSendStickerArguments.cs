namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackSendStickerArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.SendSticker;
    
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    
    public DeletePackSendStickerArguments(Guid stickerPackId, Guid stickerId, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
    }
}