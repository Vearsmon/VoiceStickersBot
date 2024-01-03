using VoiceStickersBot.Core.Contracts;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class SwitchKeyboardExtensions
{
    public static List<InlineKeyboardButtonDto> BuildMainKeyboardStickers(
        string callbackPrefix,
        List<Sticker> stickers,
        int pageFrom,
        int pageTo,
        int countOnPage)
    {
        var direction = pageTo > pageFrom ? PageChangeDirection.Increase : PageChangeDirection.Decrease;
        
        var startIndex = direction == PageChangeDirection.Increase
            ? (pageTo - 1) * countOnPage
            : (pageFrom - 2) * countOnPage;
        var endIndex = direction == PageChangeDirection.Increase
            ? countOnPage * (pageFrom + 1)
            : pageTo * countOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < stickers.Count && i < endIndex; i++)
            buttons.Add(new InlineKeyboardButtonDto(stickers[i].Name!, callbackPrefix + $":{stickers[i].StickerFullId.StickerId}"));

        return buttons;
    }
    
    public static List<InlineKeyboardButtonDto> BuildMainKeyboardPacks(
        string callbackPrefix,
        List<StickerPack> packs,
        int pageFrom,
        int pageTo,
        int countOnPage)
    {
        var direction = pageTo > pageFrom ? PageChangeDirection.Increase : PageChangeDirection.Decrease;
        
        var startIndex = direction == PageChangeDirection.Increase
            ? (pageTo - 1) * countOnPage
            : (pageFrom - 2) * countOnPage;
        var endIndex = direction == PageChangeDirection.Increase
            ? countOnPage * (pageFrom + 1)
            : pageTo * countOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < packs!.Count && i < endIndex; i++)
            buttons.Add(new InlineKeyboardButtonDto(
                packs[i].Name!,
                callbackPrefix + $":{packs[i].Id}:0:Increase:10"));

        return buttons;
    }

    public static List<InlineKeyboardButtonDto> BuildLastLine(
        string callbackPrefix,
        string entityId,
        int pageTo,
        int countOnPage,
        int entityCount)
    {
        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        
        if (pageTo > 1)
        {
            var buttonCallback = callbackPrefix + $":{entityId}:{pageTo}:Decrease:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", buttonCallback));
        }

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", $"page:{pageTo}"));

        if (pageTo <= entityCount / countOnPage)
        {
            var buttonCallback = callbackPrefix + $":{entityId}:{pageTo}:Increase:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", buttonCallback));
        }

        return lastLineButtons;
    }
}