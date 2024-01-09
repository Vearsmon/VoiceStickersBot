using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.SharePackResults;

public class SharePackSwitchKeyboardPacksResult : SharePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public bool HasPacks { get; }
    public int? BotMessageId { get; }

    public SharePackSwitchKeyboardPacksResult(
        long chatId,
        InlineKeyboardDto keyboardDto,
        bool hasPacks,
        int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        HasPacks = hasPacks;
        BotMessageId = botMessageId;
    }
}