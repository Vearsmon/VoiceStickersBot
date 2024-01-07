using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackChoiceHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackChoiceArguments commandArguments;

    public SharePackChoiceHandler(SharePackChoiceArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        var line = new List<List<InlineKeyboardButtonDto>>() 
        { 
            new() { new ("Импорт", "SP:SendImportInstr") },
            new() { new ("Экспорт", "SP:SwKbdPc:0:Increase:10") }
        };
        var keyboardDto = new InlineKeyboardDto(line, new List<InlineKeyboardButtonDto>());
        return new SharePackChoiceResult(commandArguments.ChatId, keyboardDto);
    }
}