namespace VoiceStickersBot.Core.Commands;

public interface ICommand
{
    public Type CommandType { get; }
    public UserBotState UserBotState { get; }
}