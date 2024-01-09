namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerAddStickerResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }

    public AddStickerAddStickerResult(
        long chatId)
    {
        ChatId = chatId;
    }
}