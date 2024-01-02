using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CreatePackHandlers;

public class CreatePackAddPackHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.CreatePack;
    
    private readonly CreatePackAddPackArguments commandArguments;
    private readonly StickerPacksRepository stickerPacksRepository;

    public CreatePackAddPackHandler(
        CreatePackAddPackArguments commandArguments, 
        StickerPacksRepository stickerPacksRepository)
    {
        this.commandArguments = commandArguments;
        this.stickerPacksRepository = stickerPacksRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        await stickerPacksRepository
            .CreateStickerPackAsync(
                Guid.NewGuid(),
                commandArguments.PackName,
                commandArguments.ChatId.ToString())
            .ConfigureAwait(false);
        return new CreatePackAddPackResult(commandArguments.ChatId);
    }
}