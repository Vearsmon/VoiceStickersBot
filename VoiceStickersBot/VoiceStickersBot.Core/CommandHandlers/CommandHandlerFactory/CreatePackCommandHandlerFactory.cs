using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CreatePackHandlers;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class CreatePackCommandHandlerFactory : CommandHandlerFactoryBase<ICreatePackCommandArguments>
{
    public override CommandType CommandType => CommandType.CreatePack;
    
    private readonly Dictionary<CreatePackStepName, Func<ICreatePackCommandArguments, ICommandHandler>>
        stepHandlerBuilders;

    public CreatePackCommandHandlerFactory(StickerPacksRepository stickerPacksRepository)
    {
        stepHandlerBuilders = new Dictionary<CreatePackStepName, Func<ICreatePackCommandArguments, ICommandHandler>>()
        {
            { CreatePackStepName.Cancel, ca =>  
                new CreatePackCancelHandler(
                    (CreatePackCancelArguments)ca) 
            },
            { CreatePackStepName.AddPack, ca => 
                new CreatePackAddPackHandler(
                    (CreatePackAddPackArguments)ca, stickerPacksRepository) 
            },
            { CreatePackStepName.SendInstructions,  ca => 
                new CreatePackSendInstructionsHandler(
                    (CreatePackSendInstructionsArguments)ca)
            }
        };
    }

    protected override ICommandHandler CreateCommandHandler(ICreatePackCommandArguments commandArguments)
    {
        return stepHandlerBuilders[commandArguments.StepName](commandArguments);
    }
}