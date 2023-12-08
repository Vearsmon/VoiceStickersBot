using Ninject;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;
using VoiceStickersBot.Core.Repositories.UsersRepository;
using VoiceStickersBot.Core.SchemaConfigurators;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

var container = new StandardKernel();

container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();

container.Bind<IStickersRepository>().To<StickersRepository>();
container.Bind<IStickerPacksRepository>().To<StickerPacksRepository>();
container.Bind<IUsersRepository>().To<UsersRepository>();

var stickersPacksRepository = container.Get<StickerPacksRepository>();
var stickersRepository = container.Get<StickersRepository>();
var usersRepository = container.Get<UsersRepository>();

var schemaCreator = container.Get<SchemaConfiguratorCore>();
await schemaCreator.ConfigureAsync().ConfigureAwait(false);

var userId = Guid.NewGuid().ToString();
var stickerPackId = Guid.NewGuid();
await usersRepository.Create(userId).ConfigureAwait(false);
await stickersPacksRepository.CreateStickerPackAsync(stickerPackId, "pack", userId).ConfigureAwait(false);
await stickersRepository.CreateAsync(Guid.NewGuid(), "sticker", "location", stickerPackId).ConfigureAwait(false);

var stickers = await stickersPacksRepository
    .GetStickerPackAsync(stickerPackId)
    .ConfigureAwait(false);
// foreach (var s in stickers)
// {
//     Console.WriteLine(s.Id);
// }

var stickerPacks = await usersRepository.GetStickerPacksOwned(userId, true).ConfigureAwait(false);
foreach (var s in stickerPacks)
{
    Console.WriteLine(s.Id);
    foreach (var st in s.Stickers ?? new List<Sticker>()) Console.WriteLine(st.Id);
}