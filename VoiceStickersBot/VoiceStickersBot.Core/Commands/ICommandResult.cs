namespace VoiceStickersBot.Core;

public interface ICommandResult
{
    bool EnsureSuccess { get; set; }
    CommandError GetError { get; set; }
}