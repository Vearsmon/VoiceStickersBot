namespace VoiceStickersBot.Core.Commands;

public interface ICommandResult
{
    bool EnsureSuccess { get; set; }
    CommandError GetError { get; set; }
}