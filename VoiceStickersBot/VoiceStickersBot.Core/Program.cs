using Ninject;
using VoiceStickersBot.Core.SchemaConfigurators;
using VoiceStickersBot.Core.UsersRepository;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

var container = new StandardKernel();

container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();

var t = container.Get<SchemaConfiguratorCore>();
t.Configure();

var cl = container.Get<IVsbDatabaseCluster>();
using var db = cl.GetTable<UserEntity>();
await db.PerformCreateRequestAsync(new UserEntity(){Id="123", Text = "123"}, new CancellationToken());

Console.WriteLine((await db.PerformReadonlyRequestAsync(x => x.Where(entity => entity.Id == "123")
    , new CancellationToken()))[0].Id);