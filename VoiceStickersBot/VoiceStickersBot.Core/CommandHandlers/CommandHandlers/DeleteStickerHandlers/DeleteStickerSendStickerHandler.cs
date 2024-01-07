using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeleteStickerResults;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;

public class DeleteStickerSendStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;

    private readonly DeleteStickerSendStickerArguments commandArguments;
    private readonly IStickersRepository stickersRepository;

    public DeleteStickerSendStickerHandler(
        DeleteStickerSendStickerArguments commandArguments,
        IStickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var sticker = await stickersRepository
            .GetAsync(commandArguments.StickerPackId, commandArguments.StickerId)
            .ConfigureAwait(false);
        
        var line = new List<InlineKeyboardButtonDto>() 
        { 
            new ("Назад", $"DS:SwKbdSt:{commandArguments.StickerPackId}:0:Increase:10"),
            new ("Удалить", $"DS:DeleteSticker:{commandArguments.StickerId}")
        };
        var keyboardDto = new InlineKeyboardDto(new List<List<InlineKeyboardButtonDto>>(), line);
        
        return new DeleteStickerSendStickerResult(
            commandArguments.ChatId,
            sticker,
            commandArguments.StickerPackId,
            keyboardDto,
            commandArguments.BotMessageId);
    }
}