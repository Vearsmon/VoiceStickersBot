namespace VoiceStickersBot.Core.Commands;

public class RequestContex
{
    public RequestContex(long chatId, int? menuPage=null)
    {
        ChatId = chatId;
        MenuPage = menuPage;
    }
    public long ChatId { get; }
    public int? MenuPage { get; }
}