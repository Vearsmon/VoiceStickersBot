using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class DeleteStickerCommandHandlerFactory : CommandHandlerFactoryBase<IDeleteStickerCommandArguments>
{
    public override CommandType CommandType => CommandType.DeleteSticker;
    
    private readonly Dictionary<DeleteStickerStepName, Func<IDeleteStickerCommandArguments, ICommandHandler>>
        stepHandlerBuilders;

    public DeleteStickerCommandHandlerFactory(
        IUsersRepository usersRepository,
        IStickerPacksRepository stickerPacksRepository,
        IStickersRepository stickersRepository)
    {
        stepHandlerBuilders = new Dictionary<DeleteStickerStepName, Func<IDeleteStickerCommandArguments, ICommandHandler>>()
        {
            {
                DeleteStickerStepName.SwKbdPc, ca =>
                    new DeleteStickerSwitchKeyboardPacksHandler(
                        (DeleteStickerSwitchKeyboardPacksArguments)ca, usersRepository)
            },
            {
                DeleteStickerStepName.SwKbdSt, ca =>
                    new DeleteStickerSwitchKeyboardStickersHandler(
                        (DeleteStickerSwitchKeyboardStickersArguments)ca, stickerPacksRepository)
            },
            {
                DeleteStickerStepName.SendSticker, ca =>
                    new DeleteStickerSendStickerHandler(
                        (DeleteStickerSendStickerArguments)ca, stickersRepository)
            },
            {
                DeleteStickerStepName.Cancel, ca =>
                    new DeleteStickerCancelHandler(
                        (DeleteStickerCancelArguments)ca)
            },
            {
                DeleteStickerStepName.DeleteSticker, ca =>
                    new DeleteStickerDeleteStickerHandler(
                        (DeleteStickerDeleteStickerArguments)ca, stickersRepository)
            },
            {
                DeleteStickerStepName.Confirm, ca =>
                    new DeleteStickerConfirmHandler(
                        (DeleteStickerConfirmArguments)ca)
            }
        };
    }
    protected override ICommandHandler CreateCommandHandler(IDeleteStickerCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}