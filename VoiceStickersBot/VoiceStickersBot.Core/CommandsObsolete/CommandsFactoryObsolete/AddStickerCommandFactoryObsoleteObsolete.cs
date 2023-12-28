using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.CommandsObsolete.AddStickerObsolete;

namespace VoiceStickersBot.Core.CommandsObsolete.CommandsFactoryObsolete;

public class AddStickerCommandFactoryObsoleteObsolete : CommandFactoryObsoleteBaseObsolete
{
    public override IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Добавить стикер", "/add_sticker" };
    public override ICommandObsolete CreateCommand(RequestContextObsolete requestContext)
    {
        return new AddStickerCommand(requestContext);
    }
}