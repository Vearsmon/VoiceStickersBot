using VoiceStickersBot.Core.CommandArguments;
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
                    new ShowAllCancelHandler(
                        (ShowAllCancelArguments)ca) 
            },
            { 
                ShowAllStepName.SwKbdPc, ca => 
                    new ShowAllSwitchKeyboardPacksHandler(
                        (ShowAllSwitchKeyboardPacksArguments)ca, usersRepository) 
            },
            { 
                ShowAllStepName.SwKbdSt,  ca => 
                    new ShowAllSwitchKeyboardStickersHandler(
                        (ShowAllSwitchKeyboardStickersArguments)ca, stickerPacksRepository)
            },
            { 
                ShowAllStepName.SendSticker, ca => 
                    new ShowAllSendStickerHandler(
                        (ShowAllSendStickerArguments)ca, stickersRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IShowAllCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}