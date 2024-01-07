namespace VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

public class DeleteStickerDeleteStickerResult : DeleteStickerCommandResultBase
{
    public override long ChatId { get; }
    
    public DeleteStickerDeleteStickerResult(long chatId)
    {
        ChatId = chatId;
    }
}