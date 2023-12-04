namespace VoiceStickersBot.Core;

public record InlineKeyboardDto(List<InlineKeyboardButtonDto> Buttons, List<InlineKeyboardButtonDto> LastButtons);