using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;

public class DeletePackDeletePackHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private readonly DeletePackDeletePackArguments commandArguments;
    private readonly IUsersRepository usersRepository;

    public DeletePackDeletePackHandler(
        DeletePackDeletePackArguments commandArguments,
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        await usersRepository
            .RemoveStickerPack(commandArguments.ChatId.ToString(), commandArguments.StickerPackId)
            .ConfigureAwait(false);

        return new DeletePackDeletePackResult(commandArguments.ChatId);
    }
}