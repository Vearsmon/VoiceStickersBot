using Concentus.Enums;
using Concentus.Oggfile;
using Concentus.Structs;
using NAudio.Wave;
using Telegram.Bot;
using VoiceStickersBot.Core;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public class AddStickerCommandArgumentsFactory : ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Добавить стикер", "AS" };
    
    private readonly Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>> stepCommandBuilders;
    private readonly ITelegramBotClient bot;

    public AddStickerCommandArgumentsFactory(ITelegramBotClient bot)
    {
        this.bot = bot;
        
        stepCommandBuilders = new Dictionary<AddStickerStepName, Func<QueryContext, ICommandArguments>>()
        {
            { AddStickerStepName.SwKbdPc, BuildAddStickerSwitchKeyboardPacksArguments},
            { AddStickerStepName.Cancel, q => new AddStickerCancelArguments()},
            { AddStickerStepName.SwKbdSt, BuildAddStickerSwitchKeyboardStickersArguments},
            { AddStickerStepName.SendSticker, BuildAddStickerSendStickerArguments},
            { AddStickerStepName.SendNameInstr, BuildAddStickerSendNameInstructionsArguments },
            { AddStickerStepName.SendFileInstr, BuildAddStickerSendFileInstructionsArguments},
            { AddStickerStepName.AddSticker, BuildAddStickerAddStickerArguments}
        };
    }

    public ICommandArguments CreateCommand(QueryContext queryContext)
    {
        if (!Enum.TryParse(queryContext.CommandStep, out AddStickerStepName stepName))
            throw new ArgumentException(
                $"Invalid step name [{queryContext.CommandStep}] for {queryContext.CommandType}");
        
        return stepCommandBuilders[stepName](queryContext);
    }

    private ICommandArguments BuildAddStickerSwitchKeyboardPacksArguments(QueryContext queryContext)
    {
        const int argumentsCount = 3;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!int.TryParse(queryContext.CommandArguments[0], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 0. Should be positive int.");
        
        if (!Enum.TryParse(queryContext.CommandArguments[1], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be PageChangeDirection.");
        
        if (!int.TryParse(queryContext.CommandArguments[2], out var packsOnPage) || packsOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be positive int.");

        return new AddStickerSwitchKeyboardPacksArguments(
            pageFrom,
            direction,
            packsOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }
    
    private ICommandArguments BuildAddStickerSwitchKeyboardStickersArguments(QueryContext queryContext)
    {
        const int argumentsCount = 4;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!int.TryParse(queryContext.CommandArguments[1], out var pageFrom) || pageFrom < 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be positive int.");

        if (!Enum.TryParse(queryContext.CommandArguments[2], out PageChangeDirection direction))
            throw new ArgumentException(
                "Invalid argument at index 2. Should be PageChangeDirection.");
        
        if (!int.TryParse(queryContext.CommandArguments[3], out var stickersOnPage) || stickersOnPage < 0)
            throw new ArgumentException(
                "Invalid argument at index 3. Should be positive int.");
        
        return new AddStickerSwitchKeyboardStickersArguments(
            stickerPackId,
            pageFrom,
            direction,
            stickersOnPage,
            queryContext.ChatId,
            queryContext.BotMessageId);
    }

    private ICommandArguments BuildAddStickerSendStickerArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (!Guid.TryParse(queryContext.CommandArguments[1], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");
        
        return new AddStickerSendStickerArguments(
            stickerPackId,
            stickerId,
            queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerSendNameInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 1;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        return new AddStickerSendNameInstructionsArguments(stickerPackId, queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerSendFileInstructionsArguments(QueryContext queryContext)
    {
        const int argumentsCount = 2;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");
        
        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");

        var stickerName = queryContext.CommandArguments[1];
        if (stickerName.Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be Guid.");
        
        return new AddStickerSendFileInstructionsArguments(stickerPackId, stickerName, queryContext.ChatId);
    }
    
    private ICommandArguments BuildAddStickerAddStickerArguments(QueryContext queryContext)
    {
        const int argumentsCount = 4;
        
        if (queryContext.CommandArguments.Count != argumentsCount)
            throw new ArgumentException(
                $"Invalid arguments count [{queryContext.CommandArguments.Count}]. Should be {argumentsCount}");

        if (!Guid.TryParse(queryContext.CommandArguments[0], out var stickerPackId))
            throw new ArgumentException(
                "Invalid argument at index 0. Should be Guid.");
        
        if (queryContext.CommandArguments[1].Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 1. Should be non empty string.");

        var fileId = queryContext.CommandArguments[2];
        if (fileId.Length == 0)
            throw new ArgumentException(
                "Invalid argument at index 2. Should be non empty string.");
        
        var stream = new MemoryStream();
        bot.GetInfoAndDownloadFileAsync(fileId, stream)
            .GetAwaiter()
            .GetResult();
        if (queryContext.CommandArguments[3] == "audio/mpeg")
        {
            stream = ConvertAudioToOpus(stream)
                .GetAwaiter()
                .GetResult();
        }
        
        return new AddStickerAddStickerArguments(
            stickerPackId,
            queryContext.CommandArguments[1],
            stream,
            queryContext.ChatId);
    }

    //TODO: вынести кудато
    private async Task<MemoryStream> ConvertAudioToOpus(MemoryStream audio)
    {
        /*var byteBuffer = new byte[16 * 1024];
        var memoryStream = new MemoryStream();
        var bytesRead = 0;
        while ((bytesRead = await audio.ReadAsync(byteBuffer).ConfigureAwait(false)) > 0)
        {
            await memoryStream.WriteAsync(byteBuffer, 0, bytesRead).ConfigureAwait(false);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);*/
        audio.Seek(0, SeekOrigin.Begin);
        using (var source = audio)
        using (var mp3Reader = new Mp3FileReader(source))
        using (var memo = new MemoryStream())
        {
            var bufferFloat = new byte[mp3Reader.Length / (mp3Reader.WaveFormat.BitsPerSample / 8)];
            var count = mp3Reader.Read(bufferFloat, 0, bufferFloat.Length);
            
            var buffShort = new short[count];
            var scale = (float)(short.MaxValue);
            for (int i = 0; i < count; i++)
            {
                buffShort[i] = (short)(bufferFloat[i] * scale);
            }
            
            var encoder = OpusEncoder.Create(48000, 
                mp3Reader.WaveFormat.Channels, 
                OpusApplication.OPUS_APPLICATION_AUDIO);

            encoder.Bitrate = 65536;
            
            var tags = new OpusTags();
            tags.Fields[OpusTagName.Title] = "Title";
            tags.Fields[OpusTagName.Artist] = "Artist";
            
            var oggOut = new OpusOggWriteStream(encoder, memo, tags);

            oggOut.WriteSamples(buffShort, 0, buffShort.Length);
            oggOut.Finish();

            var result = memo.ToArray();

            return new MemoryStream(result);
        }
    }
}