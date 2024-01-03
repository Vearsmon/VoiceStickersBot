namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSendInstructionsResult : AddStickerCommandResultBase
{
    public override long ChatId { get; }

    public AddStickerSendInstructionsResult(long chatId)
    {
        ChatId = chatId;
    }
}