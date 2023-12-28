using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class ShowAllCommandHandlerFactory : CommandHandlerFactoryBase<IShowAllCommandArguments>
{
    public override CommandType CommandType => CommandType.ShowAll;
    private UsersRepository usersRepository { get; }
    private StickerPacksRepository stickerPacksRepository { get; }

    private readonly Dictionary<ShowAllStepName, Func<IShowAllCommandArguments, ICommandHandler>> stepHandlerBuilders;

    public ShowAllCommandHandlerFactory(UsersRepository usersRepository, StickerPacksRepository stickerPacksRepository)
    {
        this.usersRepository = usersRepository;
        this.stickerPacksRepository = stickerPacksRepository;
        
        stepHandlerBuilders = new Dictionary<ShowAllStepName, Func<IShowAllCommandArguments, ICommandHandler>>()
        {
            { ShowAllStepName.Cancel, ca =>  
                new ShowAllCancelCommandHandler(
                    (ShowAllCancelCommandArguments)ca) 
            },
            { ShowAllStepName.SwitchKeyboardPacks, ca => 
                new ShowAllSwitchKeyboardPacksCommandHandler(
                    (ShowAllSwitchKeyboardPacksCommandArguments)ca, usersRepository) 
            },
            { ShowAllStepName.SwitchKeyboardStickers,  ca => 
                new ShowAllSwitchKeyboardStickersCommandHandler(
                    (ShowAllSwitchKeyboardStickersCommandArguments)ca, stickerPacksRepository)
            },
            { ShowAllStepName.SendSticker, ca => 
                new ShowAllSendStickerCommandHandler(
                    (ShowAllSendStickerCommandArguments)ca, stickerPacksRepository)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(IShowAllCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}