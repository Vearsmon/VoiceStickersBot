using VoiceStickersBot.Core.Commands.AddSticker;

namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class AddStickerCommandFactory : CommandFactoryBase
{
    public override IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Добавить стикер", "/add_sticker" };
    public override ICommand CreateCommand(CommandObject commandObject)
    {
        return new AddStickerCommand();
    }
}