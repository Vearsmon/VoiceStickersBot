using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public class DeleteStickerDeleteStickerArguments : IDeleteStickerCommandArguments
{
    public CommandType CommandType => CommandType.DeleteSticker;
    public DeleteStickerStepName StepName => DeleteStickerStepName.DeleteSticker;
    
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    
    public DeleteStickerDeleteStickerArguments(Guid stickerPackId, Guid stickerId, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
    }

}