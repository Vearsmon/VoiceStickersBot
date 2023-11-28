using System.ComponentModel.DataAnnotations.Schema;
using Ninject;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

var container = new StandardKernel();
container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
container.Bind<IVsbDatabaseClusterOptionsProvider>().To<PostgresVsbDatabaseClusterOptionsProvider>();


var cluster = container.Get<IVsbDatabaseCluster>();
using var t = cluster.GetTable<User>();
// await t.PerformWriteRequestAsync(new User { Id = 2, Age = 2 }).ConfigureAwait(false);'
Console.WriteLine("Start of transaction");
var ent1 = await t
    .PerformReadonlyRequestAsync(db => db.Where(user => user.Id == 1))
    .ConfigureAwait(false);
Console.WriteLine(ent1.Count);
Console.WriteLine(ent1[0].Id);
var ent2 = await t
    .PerformReadonlyRequestAsync(db => db.Where(user => user.Id == 2))
    .ConfigureAwait(false);
Console.WriteLine(ent2.Count);
Console.WriteLine(ent2[0].Id);

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}