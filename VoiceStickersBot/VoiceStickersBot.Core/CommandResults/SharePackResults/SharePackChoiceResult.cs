using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackChoiceResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    
    public SharePackChoiceResult(long chatId, InlineKeyboardDto keyboardDto)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
    }
}