namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSwitchKeyboardStickersResult : ICommandResult
{
    public long ChatId { get; }
    public AddStickerSwitchKeyboardStickersResult(long chatId)
    {
        ChatId = chatId;
    }
}