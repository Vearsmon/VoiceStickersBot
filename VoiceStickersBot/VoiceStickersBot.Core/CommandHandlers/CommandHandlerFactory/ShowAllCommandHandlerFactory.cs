using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory : CommandHandlerFactoryBase<IShowAllCommandArguments>
{
    public override CommandType CommandType => CommandType.ShowAll;

    private readonly Dictionary<ShowAllStepName, Func<IShowAllCommandArguments, ICommandHandler>> stepHandlerBuilders;

    public ShowAllCommandHandlerFactory(
        IUsersRepository usersRepository, 
        IStickerPacksRepository stickerPacksRepository,
        IStickersRepository stickersRepository)
    {
        stepHandlerBuilders = new Dictionary<ShowAllStepName, Func<IShowAllCommandArguments, ICommandHandler>>()
        {
            { 
                ShowAllStepName.Cancel, ca =>  
                    new ShowAllCancelCommandHandler(
                        (ShowAllCancelCommandArguments)ca) 
            },
            { 
                ShowAllStepName.SwKbdPc, ca => 
                    new ShowAllSwitchKeyboardPacksCommandHandler(
                        (ShowAllSwitchKeyboardPacksCommandArguments)ca, usersRepository) 
            },
            { 
                ShowAllStepName.SwKbdSt,  ca => 
                    new ShowAllSwitchKeyboardStickersCommandHandler(
                        (ShowAllSwitchKeyboardStickersCommandArguments)ca, stickerPacksRepository)
            },
            { 
                ShowAllStepName.SendSticker, ca => 
                    new ShowAllSendStickerCommandHandler(
                        (ShowAllSendStickerCommandArguments)ca, stickersRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IShowAllCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}