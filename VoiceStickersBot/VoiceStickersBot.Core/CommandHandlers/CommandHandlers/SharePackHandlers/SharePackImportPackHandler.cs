using System.Runtime.CompilerServices;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackImportPackHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;
    
    private readonly SharePackImportPackArguments commandArguments;
    private readonly IUsersRepository usersRepository;

    public SharePackImportPackHandler(
        SharePackImportPackArguments commandArguments, 
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var isSucceeded = await usersRepository.
            TryAddStickerPackToUser(
                commandArguments.ChatId.ToString(),
                commandArguments.StickerPackId)
            .ConfigureAwait(false);
        return new SharePackImportPackResult(commandArguments.ChatId, isSucceeded);
    }
}