namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendInstructionsResult : ICommandResult
{
    public long ChatId { get; }
    
    public AddStickerSendInstructionsResult(long chatId)
    {
        ChatId = chatId;
    }
}