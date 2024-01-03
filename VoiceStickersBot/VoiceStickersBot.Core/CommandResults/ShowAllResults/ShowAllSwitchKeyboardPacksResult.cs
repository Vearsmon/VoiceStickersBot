using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSwitchKeyboardPacksResult : ShowAllCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public string BotMessageId { get; }


    public ShowAllSwitchKeyboardPacksResult(long chatId, InlineKeyboardDto keyboardDto, string botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}