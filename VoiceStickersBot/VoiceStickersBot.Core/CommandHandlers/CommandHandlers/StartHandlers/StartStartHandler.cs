using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.StartCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.StartResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.StartHandlers;

public class StartStartHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.Start;

    private readonly StartStartArguments commandArguments;

    public StartStartHandler(StartStartArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new StartStartResult(commandArguments.ChatId);
    }
}