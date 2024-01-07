using SkbKontur.Cassandra.TimeBasedUuid;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerAddStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private AddStickerAddStickerArguments commandArguments;
    private IObjectStorageClient objectStorageClient;
    private IStickersRepository stickersRepository;

    public AddStickerAddStickerHandler(
        AddStickerAddStickerArguments commandArguments,
        IObjectStorageClient objectStorageClient, 
        IStickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.objectStorageClient = objectStorageClient;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var stickerId = TimeGuid.NewGuid(Timestamp.Now).ToGuid();
        
        var location = await objectStorageClient.PutObjectInStorage(
                "objstorbucket",
                stickerId,
                MimeTypes.Mpeg,
                commandArguments.Audio.ToArray())
            .ConfigureAwait(false);
        
        await commandArguments.Audio.DisposeAsync().ConfigureAwait(false);
        
        await stickersRepository.CreateAsync(
                stickerId,
                commandArguments.StickerName,
                location.ToString(),
                commandArguments.StickerPackId)
            .ConfigureAwait(false);

        return new AddStickerAddStickerResult(
            commandArguments.ChatId,
            commandArguments.StickerName,
            stickerId);
    }
}