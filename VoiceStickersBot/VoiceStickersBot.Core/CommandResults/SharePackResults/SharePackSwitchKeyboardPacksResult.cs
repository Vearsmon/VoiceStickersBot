using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSwitchKeyboardPacksResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public int? BotMessageId { get; }

    public SharePackSwitchKeyboardPacksResult(long chatId, InlineKeyboardDto keyboardDto, int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}