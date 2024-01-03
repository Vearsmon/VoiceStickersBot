using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSendStickerResult : ShowAllCommandResultBase
{
    public override long ChatId { get; }
    public Sticker Sticker { get; }

    public ShowAllSendStickerResult(long chatId, Sticker sticker)
    {
        ChatId = chatId;
        Sticker = sticker;
    }
}