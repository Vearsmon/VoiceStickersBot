using SkbKontur.Cassandra.TimeBasedUuid;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerAddStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private AddStickerAddStickerArguments commandArguments;
    private IStickersRepository stickersRepository;

    //TODO: может переименовать эту и подобные команды на CommandTypeUploadSticker...
    public AddStickerAddStickerHandler(
        AddStickerAddStickerArguments commandArguments, 
        IStickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var stickerId = TimeGuid.NewGuid(Timestamp.Now).ToGuid();
        
        await stickersRepository.CreateAsync(
                stickerId,
                commandArguments.StickerName,
                "objstorbucket",
                commandArguments.StickerPackId)
            .ConfigureAwait(false);

        return new AddStickerAddStickerResult(
            commandArguments.ChatId,
            commandArguments.StickerName,
            stickerId,
            commandArguments.FileId);
    }
}