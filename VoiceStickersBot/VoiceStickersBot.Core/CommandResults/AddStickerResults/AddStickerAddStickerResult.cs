namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerAddStickerResult : ICommandResult
{
    public long ChatId { get; }

    public AddStickerAddStickerResult(long chatId)
    {
        ChatId = chatId;
    }
}