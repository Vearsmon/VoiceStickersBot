using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardStickersResult : ShowAllCommandResultBase
{
    public override long ChatId { get; }
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }

    public ShowAllSwitchKeyboardStickersResult(long chatId, InlineKeyboardDto keyboardDto, string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}