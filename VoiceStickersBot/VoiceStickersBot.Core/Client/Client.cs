using System.Reflection;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

namespace VoiceStickersBot.Core.Client;

public class Client
{
    private readonly TgApiCommandHandlerService tgApiCommandHandlerService;

    public Client(TgApiCommandHandlerService tgApiCommandHandlerService)
    {
        this.tgApiCommandHandlerService = tgApiCommandHandlerService;
    }

    public async Task<ICommandResult> Handle(ICommandArguments commandArguments)
    {
        var result = await tgApiCommandHandlerService.Handle(commandArguments).ConfigureAwait(false);
        if (result.EnsureSuccess)
            return result.Result;

        throw result.Error; //обработка ошибок
    }
}