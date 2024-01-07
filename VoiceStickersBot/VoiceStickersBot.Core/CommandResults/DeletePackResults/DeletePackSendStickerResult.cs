using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackSendStickerResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    public Sticker Sticker { get; }
    public Guid StickerPackId { get; }

    public DeletePackSendStickerResult(long chatId, Sticker sticker, Guid stickerPackId)
    {
        ChatId = chatId;
        Sticker = sticker;
        StickerPackId = stickerPackId;
    }
}