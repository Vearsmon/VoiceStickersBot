using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandResults;

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