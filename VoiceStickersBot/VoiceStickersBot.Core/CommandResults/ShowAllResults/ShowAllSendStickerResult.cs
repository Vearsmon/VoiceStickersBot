using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandResults.ShowAllResults;

public class ShowAllSendStickerResult : ICommandResult
{
    public long ChatId { get; }
    public Sticker Sticker { get; }
    
    public ShowAllSendStickerResult(long chatId, Sticker sticker)
    {
        ChatId = chatId;
        Sticker = sticker;
    }

}