namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public record InlineKeyboardDto(List<InlineKeyboardButtonDto> Buttons, List<InlineKeyboardButtonDto> LastButtons);