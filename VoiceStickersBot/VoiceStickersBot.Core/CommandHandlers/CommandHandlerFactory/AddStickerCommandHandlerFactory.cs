using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class AddStickerCommandHandlerFactory : CommandHandlerFactoryBase<IAddStickerCommandArguments>
{
    public override CommandType CommandType => CommandType.AddSticker;
    
    private readonly Dictionary<AddStickerStepName, Func<IAddStickerCommandArguments, ICommandHandler>>
        stepHandlerBuilders;

    public AddStickerCommandHandlerFactory(StickerPacksRepository stickerPacksRepository)
    {
        stepHandlerBuilders = new Dictionary<AddStickerStepName, Func<IAddStickerCommandArguments, ICommandHandler>>()
        {
            {
                AddStickerStepName.SwKbdPc, ca => 
                    new AddStickerSwitchKeyboardPacksHandler(
                        (AddStickerSwitchKeyboardPacksArguments)ca) },
            { 
                AddStickerStepName.Cancel, ca => 
                    new AddStickerCancelHandler(
                        (AddStickerCancelArguments)ca) },
            { 
                AddStickerStepName.SwKbdSt, ca => 
                    new AddStickerSwitchKeyboardStickersHandler(
                        (AddStickerSwitchKeyboardStickersArguments)ca) },
            {
                AddStickerStepName.SendSticker, ca =>
                    new AddStickerSendStickerHandler(
                        (AddStickerSendStickerArguments)ca, 
                        stickerPacksRepository)
            },
            {
                AddStickerStepName.SendInstructions, ca =>
                    new AddStickerSendInstructionsHandler(
                        (AddStickerSendInstructionsArguments)ca)
            },
            {
                AddStickerStepName.AddSticker, ca =>
                    new AddStickerAddStickerHandler(
                        (AddStickerAddStickerArguments)ca,
                        stickerPacksRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IAddStickerCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}