using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public interface IDeletePackCommandArguments : ICommandArguments
{
    public DeletePackStepName StepName { get; }
}