using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendStickerResult : ICommandResult
{
    public long ChatId { get; }
    public Sticker Sticker { get; }
    
    public AddStickerSendStickerResult(long chatId, Sticker sticker)
    {
        ChatId = chatId;
        Sticker = sticker;
    }

}