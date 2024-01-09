using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class DeletePackCommandHandlerFactory : CommandHandlerFactoryBase<IDeletePackCommandArguments>
{
    public override CommandType CommandType => CommandType.DeletePack;
    
    private readonly Dictionary<DeletePackStepName, Func<IDeletePackCommandArguments, ICommandHandler>>
        stepHandlerBuilders;

    public DeletePackCommandHandlerFactory(
        IUsersRepository usersRepository,
        IStickerPacksRepository stickerPacksRepository,
        IStickersRepository stickersRepository)
    {
        stepHandlerBuilders = new Dictionary<DeletePackStepName, Func<IDeletePackCommandArguments, ICommandHandler>>()
        {
            {
                DeletePackStepName.SwKbdPc, ca =>
                    new DeletePackSwitchKeyboardPacksHandler(
                        (DeletePackSwitchKeyboardPacksArguments)ca, usersRepository)
            },
            {
                DeletePackStepName.SwKbdSt, ca =>
                    new DeletePackSwitchKeyboardStickersHandler(
                        (DeletePackSwitchKeyboardStickersArguments)ca, stickerPacksRepository)
            },
            {
                DeletePackStepName.SendSticker, ca =>
                    new DeletePackSendStickerHandler(
                        (DeletePackSendStickerArguments)ca, stickersRepository)
            },
            {
                DeletePackStepName.DeletePack, ca =>
                    new DeletePackDeletePackHandler(
                        (DeletePackDeletePackArguments)ca, usersRepository)
            },
            {
                DeletePackStepName.Confirm, ca =>
                    new DeletePackConfirmHandler(
                        (DeletePackConfirmArguments)ca)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IDeletePackCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}