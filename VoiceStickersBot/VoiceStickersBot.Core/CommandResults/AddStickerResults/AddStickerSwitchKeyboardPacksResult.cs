namespace VoiceStickersBot.Core.CommandResults.AddStickerResults;

public class AddStickerSwitchKeyboardPacksResult : ICommandResult
{
    public long ChatId { get; }
    
    public AddStickerSwitchKeyboardPacksResult(long chatId)
    {
        ChatId = chatId;
    }
}