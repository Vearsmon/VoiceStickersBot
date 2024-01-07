namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public interface IAddStickerCommandArguments : ICommandArguments
{
    public AddStickerStepName StepName { get; }
}