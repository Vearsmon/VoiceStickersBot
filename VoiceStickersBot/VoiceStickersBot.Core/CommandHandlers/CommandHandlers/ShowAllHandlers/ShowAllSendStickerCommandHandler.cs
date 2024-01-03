using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllSendStickerCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllSendStickerCommandArguments commandArguments;
    private readonly StickersRepository stickersRepository;

    public ShowAllSendStickerCommandHandler(
        ShowAllSendStickerCommandArguments commandArguments,
        StickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var sticker = await stickersRepository
            .GetAsync(commandArguments.StickerPackId, commandArguments.StickerId)
            .ConfigureAwait(false);

        return new ShowAllSendStickerResult(commandArguments.ChatId, sticker);
    }
}