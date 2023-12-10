using Ninject;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Core.SchemaConfigurators;
using VoiceStickersBot.Infra.VSBApplication;

var app = new CoreTestApplication();
await app.RunAsync(new CancellationToken()).ConfigureAwait(false);

internal class CoreTestApplication : VsbApplicationBase
{
    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        var stickersPacksRepository = container.Get<IStickerPacksRepository>();
        var stickersRepository = container.Get<IStickersRepository>();
        var usersRepository = container.Get<IUsersRepository>();

        var schemaCreator = container.Get<SchemaConfiguratorCore>();
        await schemaCreator.ConfigureAsync().ConfigureAwait(false);

        var userId = Guid.NewGuid().ToString();
        var stickerPackId = Guid.NewGuid();
        await usersRepository.Create(userId).ConfigureAwait(false);
        await stickersPacksRepository.CreateStickerPackAsync(stickerPackId, "pack", userId).ConfigureAwait(false);
        await stickersRepository.CreateAsync(Guid.NewGuid(), "sticker", "location", stickerPackId)
            .ConfigureAwait(false);

        var stickers = await stickersPacksRepository
            .GetStickerPackAsync(stickerPackId)
            .ConfigureAwait(false);
        Console.WriteLine(1);
        foreach (var s in stickers.Stickers ?? new List<Sticker>()) Console.WriteLine(s.Id);

        Console.WriteLine(2);
        var stickerPacks = await usersRepository.GetStickerPacksOwned(userId, true).ConfigureAwait(false);
        foreach (var s in stickerPacks)
        {
            Console.WriteLine(s.Id);
            foreach (var st in s.Stickers ?? new List<Sticker>()) Console.WriteLine(st.Id);
        }
    }
}