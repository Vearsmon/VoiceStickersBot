using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public class DeleteStickerConfirmArguments : IDeleteStickerCommandArguments
{
    public CommandType CommandType => CommandType.DeleteSticker;
    public DeleteStickerStepName StepName => DeleteStickerStepName.Confirm;
    
    public Guid StickerPackId { get; }
    public long ChatId { get; }
    public int? BotMessageId { get; }
    
    public DeleteStickerConfirmArguments(Guid stickerPackId, long chatId, int? botMessageId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}