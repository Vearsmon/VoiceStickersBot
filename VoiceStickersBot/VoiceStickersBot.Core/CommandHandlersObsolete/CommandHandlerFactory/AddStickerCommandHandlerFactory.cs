using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands.AddSticker;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlerFactory;

public class AddStickerCommandHandlerFactory : CommandHandlerFactoryBase<AddStickerCommand>
{
    public override Type CommandType => typeof(AddStickerCommand);
    protected override ICommandHandler CreateCommandHandler(AddStickerCommand command)
    {
        return new AddStickerCommandHandler(command);
    }
}