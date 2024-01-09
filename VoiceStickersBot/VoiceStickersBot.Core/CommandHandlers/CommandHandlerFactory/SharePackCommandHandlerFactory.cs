using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class SharePackCommandHandlerFactory : CommandHandlerFactoryBase<ISharePackCommandArguments>
{
    public override CommandType CommandType => CommandType.SharePack;

    private readonly Dictionary<SharePackStepName, Func<ISharePackCommandArguments, ICommandHandler>> stepHandlerBuilders;

    public SharePackCommandHandlerFactory(
        IUsersRepository usersRepository, 
        IStickerPacksRepository stickerPacksRepository,
        IStickersRepository stickersRepository)
    {
        stepHandlerBuilders = new Dictionary<SharePackStepName, Func<ISharePackCommandArguments, ICommandHandler>>()
        {
            {
                SharePackStepName.SwKbdPc, ca => 
                    new SharePackSwitchKeyboardPacksHandler(
                        (SharePackSwitchKeyboardPacksArguments)ca, usersRepository)
            },
            {
                SharePackStepName.SwKbdSt,  ca => 
                    new SharePackSwitchKeyboardStickersHandler(
                        (SharePackSwitchKeyboardStickersArguments)ca, stickerPacksRepository)
            },
            {
                SharePackStepName.SendSticker, ca => 
                    new SharePackSendStickerHandler(
                        (SharePackSendStickerArguments)ca, stickersRepository)
            },
            {
                SharePackStepName.SendImportInstr, ca =>  
                    new SharePackSendImportInstructionsHandler(
                        (SharePackSendImportInstructionsArguments)ca)
            },
            {
                SharePackStepName.SendPackId, ca =>  
                    new SharePackSendPackIdHandler(
                        (SharePackSendPackIdArguments)ca)
            },
            {
                SharePackStepName.Choice, ca =>  
                    new SharePackChoiceHandler(
                        (SharePackChoiceArguments)ca)
            },
            {
                SharePackStepName.ImportPack, ca =>  
                    new SharePackImportPackHandler(
                        (SharePackImportPackArguments)ca, usersRepository, stickerPacksRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(ISharePackCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}