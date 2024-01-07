using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Infra.ObjectStorage;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class AddStickerCommandHandlerFactory : CommandHandlerFactoryBase<IAddStickerCommandArguments>
{
    public override CommandType CommandType => CommandType.AddSticker;
    
    private readonly Dictionary<AddStickerStepName, Func<IAddStickerCommandArguments, ICommandHandler>>
        stepHandlerBuilders;

    public AddStickerCommandHandlerFactory(
        IObjectStorageClient objectStorageClient,
        IUsersRepository usersRepository,
        IStickerPacksRepository stickerPacksRepository, 
        IStickersRepository stickersRepository)
    {
        stepHandlerBuilders = new Dictionary<AddStickerStepName, Func<IAddStickerCommandArguments, ICommandHandler>>()
        {
            {
                AddStickerStepName.SwKbdPc, ca => 
                    new AddStickerSwitchKeyboardPacksHandler(
                        (AddStickerSwitchKeyboardPacksArguments)ca,
                        usersRepository) },
            { 
                AddStickerStepName.Cancel, ca => 
                    new AddStickerCancelHandler(
                        (AddStickerCancelArguments)ca) },
            { 
                AddStickerStepName.SwKbdSt, ca => 
                    new AddStickerSwitchKeyboardStickersHandler(
                        (AddStickerSwitchKeyboardStickersArguments)ca,
                        stickerPacksRepository) },
            {
                AddStickerStepName.SendSticker, ca =>
                    new AddStickerSendStickerHandler(
                        (AddStickerSendStickerArguments)ca, 
                        stickersRepository)
            },
            {
                AddStickerStepName.SendNameInstr, ca =>
                    new AddStickerSendNameInstructionsHandler(
                        (AddStickerSendNameInstructionsArguments)ca)
            },
            {
                AddStickerStepName.SendFileInstr, ca =>
                    new AddStickerSendFileInstructionsHandler(
                        (AddStickerSendFileInstructionsArguments)ca)
            },
            {
                AddStickerStepName.AddSticker, ca =>
                    new AddStickerAddStickerHandler(
                        (AddStickerAddStickerArguments)ca,
                        objectStorageClient,
                        stickersRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IAddStickerCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}