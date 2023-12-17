namespace VoiceStickersBot.Core.Commands;

public interface ICommandResult
{
    public UserBotState UserBotStateFrom { get; }
}