using DCM;

namespace TestClient
{
    class Program
    {
        public static void Main() { }

        #region Start the client with the TestPlugin. 
        //static async Task Main()
        //    => await new DiscordManager()
        //        .WithCredentials(new(<Your Token>))
        //        .AddPlugin(typeof(TestPlugin))
        //        .StartAsync();
        #endregion

        #region Read Embed Json and send a message.
        //public static async Task Main()
        //{
        //    var messageData = new ConfigurationBuilder()
        //        .AddJsonFile(Path.Combine("appsettings.json"), false, true)
        //        .Build()
        //        .Get<ConfigData>()
        //        .Message;

        //    var dcm = await new DiscordManager()
        //        .WithCredentials(new("<YourToken>"))
        //        .StartAsync();

        //    var guild = await dcm.Client.GetRequiredGuildAsync(0 /* your guild id */);
        //    var channel = await guild.GetRequiredTextChannelAsync(0 /* your channel id */);
        //    var message = await channel.SendMessageAsync(
        //        text: messageData.Content,
        //        embed: messageData.Embed.ToEmbedData(new Dictionary<string, object>
        //        {   // Set the variables that you want to access in the embed json
        //            ["DiscordUrl"] = "https://cdn.discordapp.com/embed/avatars/0.png",
        //            ["Guild"] = guild
        //        }).Build());
        //}
        #endregion
    }

    #region TestPlugin
    public class TestPlugin : Plugin
    {
        //private readonly IDiscordClient _client;
        //private readonly IEventEmitter _eventEmitter;

        //public TestPlugin(IDiscordClient client, IEventEmitter eventEmitter)
        //{
        //    _client = client;
        //    _eventEmitter = eventEmitter;
        //}

        //public override async Task StartAsync()
        //{
        //    var channel = await GetTestChannel();
        //    var hintMessage = await channel.SendMessageAsync("Ask me something!! \\:D");

        //    var message = await channel
        //        .CreateMessageCollector(_eventEmitter)
        //        //.WithFilter(m => m.Content.Contains("!"))
        //        //.WithFilter(m => m.Author.Id == <Test User>)
        //        .AddListener(async message =>
        //        {
        //            await channel.SendMessageAsync(embed: new EmbedBuilder().Build(new()
        //            {
        //                Text = "Uhm!"
        //            }));
        //        }, true)
        //        .WaitForMessage();

        //    await message.Channel.SendMessageAsync(embed: new EmbedBuilder().Build(new()
        //    {
        //        Text = "I don't know!"
        //    }));
        //}

        //private async Task<ITextChannel> GetTestChannel() 
        //{
        //    var guild = await _client.GetGuildAsync(<TestGuild>);
        //    var channel = await guild.GetTextChannelAsync(<TestChannel>);

        //    Console.WriteLine(string.Format("Using channel '{0}' on the guild '{1}'.",
        //        channel.Name,
        //        guild.Name));

        //    return channel;
        //}
    }
    #endregion
}
