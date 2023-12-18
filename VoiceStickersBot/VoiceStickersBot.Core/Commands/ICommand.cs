namespace VoiceStickersBot.Core.Commands;

public interface ICommand
{
    public Type CommandType { get; }
    public RequestContext RequestContext { get; }
}