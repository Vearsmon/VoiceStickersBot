using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public class DeleteStickerSendStickerArguments : IDeleteStickerCommandArguments
{
    public CommandType CommandType => CommandType.DeleteSticker;
    public DeleteStickerStepName StepName => DeleteStickerStepName.SendSticker;
    
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    public int? BotMessageId { get; }
    
    public DeleteStickerSendStickerArguments(
        Guid stickerPackId,
        Guid stickerId,
        long chatId,
        int? botMessageId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }

}