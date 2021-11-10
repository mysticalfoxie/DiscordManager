using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCM.Events.Discord;

namespace DCM
{
    internal interface IEventMapper
    {
        void MapAllEvents();
    }

    internal class EventMapper : IEventMapper
    {
        private readonly IEventEmitter _eventEmitter;
        private readonly Discord _discord;

        public EventMapper(DiscordManager discordManager, Discord discord)
        {
            _eventEmitter = discordManager.EventEmitter;
            _discord = discord;
        }

        public void MapAllEvents()
        {
            _discord.Client.ApplicationCommandCreated += DiscordClient_ApplicationCommandCreated;
            _discord.Client.ApplicationCommandDeleted += DiscordClient_ApplicationCommandDeleted;
            _discord.Client.ApplicationCommandUpdated += DiscordClient_ApplicationCommandUpdated;
            _discord.Client.AutocompleteExecuted += DiscordClient_AutocompleteExecuted;
            _discord.Client.GuildJoinRequestDeleted += DiscordClient_GuildJoinRequestDeleted;
            _discord.Client.GuildStickerCreated += DiscordClient_GuildStickerCreated;
            _discord.Client.GuildStickerDeleted += DiscordClient_GuildStickerDeleted;
            _discord.Client.GuildStickerUpdated += DiscordClient_GuildStickerUpdated;
            _discord.Client.InteractionCreated += DiscordClient_InteractionCreated;
            _discord.Client.MessageCommandExecuted += DiscordClient_MessageCommandExecuted;
            _discord.Client.RequestToSpeak += DiscordClient_RequestToSpeak;
            _discord.Client.SelectMenuExecuted += DiscordClient_SelectMenuExecuted;
            _discord.Client.SlashCommandExecuted += DiscordClient_SlashCommandExecuted;
            _discord.Client.SpeakerAdded += DiscordClient_SpeakerAdded;
            _discord.Client.SpeakerRemoved += DiscordClient_SpeakerRemoved;
            _discord.Client.StageEnded += DiscordClient_StageEnded;
            _discord.Client.StageStarted += DiscordClient_StageStarted;
            _discord.Client.StageUpdated += DiscordClient_StageUpdated;
            _discord.Client.ThreadCreated += DiscordClient_ThreadCreated;
            _discord.Client.ThreadDeleted += DiscordClient_ThreadDeleted;
            _discord.Client.ThreadMemberJoined += DiscordClient_ThreadMemberJoined;
            _discord.Client.ThreadMemberLeft += DiscordClient_ThreadMemberLeft;
            _discord.Client.ThreadUpdated += DiscordClient_ThreadUpdated;
            _discord.Client.UserCommandExecuted += DiscordClient_UserCommandExecuted;
            _discord.Client.ChannelCreated += DiscordClient_ChannelCreated;
            _discord.Client.ChannelDestroyed += DiscordClient_ChannelDestroyed;
            _discord.Client.ChannelUpdated += DiscordClient_ChannelUpdated;
            _discord.Client.Connected += DiscordClient_Connected;
            _discord.Client.CurrentUserUpdated += DiscordClient_CurrentUserUpdated;
            _discord.Client.Disconnected += DiscordClient_Disconnected;
            _discord.Client.GuildAvailable += DiscordClient_GuildAvailable;
            _discord.Client.GuildMembersDownloaded += DiscordClient_GuildMembersDownloaded;
            _discord.Client.GuildMemberUpdated += DiscordClient_GuildMemberUpdated;
            _discord.Client.GuildUnavailable += DiscordClient_GuildUnavailable;
            _discord.Client.GuildUpdated += DiscordClient_GuildUpdated;
            _discord.Client.InviteCreated += DiscordClient_InviteCreated;
            _discord.Client.InviteDeleted += DiscordClient_InviteDeleted;
            _discord.Client.JoinedGuild += DiscordClient_JoinedGuild;
            _discord.Client.LatencyUpdated += DiscordClient_LatencyUpdated;
            _discord.Client.LeftGuild += DiscordClient_LeftGuild;
            _discord.Client.Log += DiscordClient_Log;
            _discord.Client.LoggedIn += DiscordClient_LoggedIn;
            _discord.Client.LoggedOut += DiscordClient_LoggedOut;
            _discord.Client.MessageDeleted += DiscordClient_MessageDeleted;
            _discord.Client.MessageReceived += DiscordClient_MessageReceived;
            _discord.Client.MessagesBulkDeleted += DiscordClient_MessagesBulkDeleted;
            _discord.Client.MessageUpdated += DiscordClient_MessageUpdated;
            _discord.Client.ReactionAdded += DiscordClient_ReactionAdded;
            _discord.Client.ReactionRemoved += DiscordClient_ReactionRemoved;
            _discord.Client.ReactionsCleared += DiscordClient_ReactionsCleared;
            _discord.Client.ReactionsRemovedForEmote += DiscordClient_ReactionsRemovedForEmote;
            _discord.Client.Ready += DiscordClient_Ready;
            _discord.Client.RecipientAdded += DiscordClient_RecipientAdded;
            _discord.Client.RecipientRemoved += DiscordClient_RecipientRemoved;
            _discord.Client.RoleCreated += DiscordClient_RoleCreated;
            _discord.Client.RoleDeleted += DiscordClient_RoleDeleted;
            _discord.Client.RoleUpdated += DiscordClient_RoleUpdated;
            _discord.Client.UserBanned += DiscordClient_UserBanned;
            _discord.Client.UserIsTyping += DiscordClient_UserIsTyping;
            _discord.Client.UserJoined += DiscordClient_UserJoined;
            _discord.Client.UserLeft += DiscordClient_UserLeft;
            _discord.Client.UserUnbanned += DiscordClient_UserUnbanned;
            _discord.Client.UserUpdated += DiscordClient_UserUpdated;
            _discord.Client.UserVoiceStateUpdated += DiscordClient_UserVoiceStateUpdated;
            _discord.Client.VoiceServerUpdated += DiscordClient_VoiceServerUpdated;
        }

        private Task DiscordClient_UserCommandExecuted(SocketUserCommand command)
        {
            _eventEmitter.Emit(new UserCommandExecutedEvent(command));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ThreadUpdated(Cacheable<SocketThreadChannel, ulong> oldThread, SocketThreadChannel newThread)
        {
            _eventEmitter.Emit(new ThreadUpdatedEvent(oldThread, newThread));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ThreadMemberLeft(SocketThreadUser user)
        {
            _eventEmitter.Emit(new ThreadMemberLeftEvent(user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ThreadMemberJoined(SocketThreadUser user)
        {
            _eventEmitter.Emit(new ThreadMemberJoinedEvent(user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ThreadDeleted(Cacheable<SocketThreadChannel, ulong> thread)
        {
            _eventEmitter.Emit(new ThreadDeletedEvent(thread));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ThreadCreated(SocketThreadChannel thread)
        {
            _eventEmitter.Emit(new ThreadCreatedEvent(thread));
            return Task.CompletedTask;
        }

        private Task DiscordClient_StageUpdated(SocketStageChannel oldChannel, SocketStageChannel newChannel)
        {
            _eventEmitter.Emit(new StageUpdatedEvent(oldChannel, newChannel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_StageStarted(SocketStageChannel channel)
        {
            _eventEmitter.Emit(new StageStartedEvent(channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_StageEnded(SocketStageChannel channel)
        {
            _eventEmitter.Emit(new StageEndedEvent(channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_SpeakerRemoved(SocketStageChannel channel, SocketGuildUser user)
        {
            _eventEmitter.Emit(new SpeakerRemovedEvent(channel, user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_SpeakerAdded(SocketStageChannel channel, SocketGuildUser user)
        {
            _eventEmitter.Emit(new SpeakerAddedEvent(channel, user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_SlashCommandExecuted(SocketSlashCommand command)
        {
            _eventEmitter.Emit(new SlashCommandExecutedEvent(command));
            return Task.CompletedTask;    
        }

        private Task DiscordClient_SelectMenuExecuted(SocketMessageComponent component)
        {
            _eventEmitter.Emit(new SelectMenuExecutedEvent(component));
            return Task.CompletedTask;
        }

        private Task DiscordClient_RequestToSpeak(SocketStageChannel channel, SocketGuildUser user)
        {
            _eventEmitter.Emit(new RequestToSpeakEvent(channel, user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_MessageCommandExecuted(SocketMessageCommand command)
        {
            _eventEmitter.Emit(new MessageCommandExecutedEvent(command));
            return Task.CompletedTask;
        }

        private Task DiscordClient_InteractionCreated(SocketInteraction interaction)
        {
            _eventEmitter.Emit(new InteractionCreatedEvent(interaction));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildStickerUpdated(SocketCustomSticker oldSticker, SocketCustomSticker newSticker)
        {
            _eventEmitter.Emit(new GuildStickerUpdatedEvent(oldSticker, newSticker));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildStickerDeleted(SocketCustomSticker sticker)
        {
            _eventEmitter.Emit(new GuildStickerDeletedEvent(sticker));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildStickerCreated(SocketCustomSticker sticker)
        {
            _eventEmitter.Emit(new GuildStickerCreatedEvent(sticker));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildJoinRequestDeleted(Cacheable<SocketGuildUser, ulong> user, SocketGuild guild)
        {
            _eventEmitter.Emit(new GuildJoinRequestDeletedEvent(user, guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_AutocompleteExecuted(SocketAutocompleteInteraction interaction)
        {
            _eventEmitter.Emit(new AutoCompleteExecutedEvent(interaction));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ApplicationCommandUpdated(SocketApplicationCommand command)
        {
            _eventEmitter.Emit(new ApplicationCommandDeletedEvent(command));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ApplicationCommandDeleted(SocketApplicationCommand command)
        {
            _eventEmitter.Emit(new ApplicationCommandDeletedEvent(command));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ApplicationCommandCreated(SocketApplicationCommand command)
        {
            _eventEmitter.Emit(new ApplicationCommandCreatedEvent(command));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ChannelCreated(SocketChannel channel)
        {
            _eventEmitter.Emit(new ChannelCreatedEvent(channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ChannelDestroyed(SocketChannel channel)
        {
            _eventEmitter.Emit(new ChannelDestroyedEvent(channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
        {
            _eventEmitter.Emit(new ChannelUpdatedEvent(oldChannel, newChannel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_Connected()
        {
            _eventEmitter.Emit(new ConnectedEvent());
            return Task.CompletedTask;
        }

        private Task DiscordClient_CurrentUserUpdated(SocketSelfUser oldUser, SocketSelfUser newUser)
        {
            _eventEmitter.Emit(new CurrentUserUpdatedEvent(oldUser, newUser));
            return Task.CompletedTask;
        }

        private Task DiscordClient_Disconnected(Exception exception)
        {
            _eventEmitter.Emit(new DisconnectedEvent(exception));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildAvailable(SocketGuild guild)
        {
            _eventEmitter.Emit(new GuildAvailableEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildMembersDownloaded(SocketGuild guild)
        {
            _eventEmitter.Emit(new GuildMembersDownloadedEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> oldUser, SocketGuildUser newUser)
        {
            _eventEmitter.Emit(new GuildMemberUpdatedEvent(oldUser, newUser));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildUnavailable(SocketGuild guild)
        {
            _eventEmitter.Emit(new GuildUnavailableEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_GuildUpdated(SocketGuild oldGuild, SocketGuild newGuild)
        {
            _eventEmitter.Emit(new GuildUpdatedEvent(oldGuild, newGuild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_InviteCreated(SocketInvite invite)
        {
            _eventEmitter.Emit(new InviteCreatedEvent(invite));
            return Task.CompletedTask;
        }

        private Task DiscordClient_InviteDeleted(SocketGuildChannel originChannel, string inviteCode)
        {
            _eventEmitter.Emit(new InviteDeletedEvent(originChannel, inviteCode));
            return Task.CompletedTask;
        }

        private Task DiscordClient_JoinedGuild(SocketGuild guild)
        {
            _eventEmitter.Emit(new JoinedGuildEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_LatencyUpdated(int oldLatency, int newLatency)
        {
            _eventEmitter.Emit(new LatencyUpdatedEvent(oldLatency, newLatency));
            return Task.CompletedTask;
        }

        private Task DiscordClient_LeftGuild(SocketGuild guild)
        {
            _eventEmitter.Emit(new LeftGuildEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_Log(LogMessage message)
        {
            _eventEmitter.Emit(new LogEvent(message));
            return Task.CompletedTask;
        }

        private Task DiscordClient_LoggedIn()
        {
            _eventEmitter.Emit(new LoggedInEvent());
            return Task.CompletedTask;
        }

        private Task DiscordClient_LoggedOut()
        {
            _eventEmitter.Emit(new LoggedOutEvent());
            return Task.CompletedTask;
        }

        private Task DiscordClient_MessageDeleted(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
        {
            _eventEmitter.Emit(new MessageDeletedEvent(message, channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_MessageReceived(SocketMessage message)
        {
            _eventEmitter.Emit(new MessageReceivedEvent(message));
            return Task.CompletedTask;
        }

        private Task DiscordClient_MessagesBulkDeleted(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages, Cacheable<IMessageChannel, ulong> channel)
        {
            _eventEmitter.Emit(new MessagesBulkDeletedEvent(messages, channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_MessageUpdated(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage, ISocketMessageChannel channel)
        {
            _eventEmitter.Emit(new MessageUpdatedEvent(oldMessage, newMessage, channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            _eventEmitter.Emit(new ReactionAddedEvent(message, channel, reaction));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ReactionRemoved(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            _eventEmitter.Emit(new ReactionRemovedEvent(message, channel, reaction));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ReactionsCleared(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
        {
            _eventEmitter.Emit(new ReactionsClearedEvent(message, channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_ReactionsRemovedForEmote(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, IEmote emote)
        {
            _eventEmitter.Emit(new ReactionsRemovedForEmoteEvent(message, channel, emote));
            return Task.CompletedTask;
        }

        private Task DiscordClient_Ready()
        {
            _eventEmitter.Emit(new ReadyEvent());
            return Task.CompletedTask;
        }

        private Task DiscordClient_RecipientAdded(SocketGroupUser user)
        {
            _eventEmitter.Emit(new RecipientAddedEvent(user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_RecipientRemoved(SocketGroupUser user)
        {
            _eventEmitter.Emit(new RecipientRemovedEvent(user));
            return Task.CompletedTask;
        }

        private Task DiscordClient_RoleCreated(SocketRole role)
        {
            _eventEmitter.Emit(new RoleCreatedEvent(role));
            return Task.CompletedTask;
        }

        private Task DiscordClient_RoleDeleted(SocketRole role)
        {
            _eventEmitter.Emit(new RoleDeletedEvent(role));
            return Task.CompletedTask;
        }

        private Task DiscordClient_RoleUpdated(SocketRole oldRole, SocketRole newRole)
        {
            _eventEmitter.Emit(new RoleUpdatedEvent(oldRole, newRole));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserBanned(SocketUser user, SocketGuild guild)
        {
            _eventEmitter.Emit(new UserBannedEvent(user, guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserIsTyping(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel)
        {
            _eventEmitter.Emit(new UserIsTypingEvent(user, channel));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserJoined(SocketGuildUser guild)
        {
            _eventEmitter.Emit(new UserJoinedEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserLeft(SocketGuildUser guild)
        {
            _eventEmitter.Emit(new UserLeftEvent(guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserUnbanned(SocketUser user, SocketGuild guild)
        {
            _eventEmitter.Emit(new UserUnbannedEvent(user, guild));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserUpdated(SocketUser oldUser, SocketUser newUser)
        {
            _eventEmitter.Emit(new UserUpdatedEvent(oldUser, newUser));
            return Task.CompletedTask;
        }

        private Task DiscordClient_UserVoiceStateUpdated(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        {
            _eventEmitter.Emit(new UserVoiceStateUpdatedEvent(user, oldState, newState));
            return Task.CompletedTask;
        }

        private Task DiscordClient_VoiceServerUpdated(SocketVoiceServer voiceServer)
        {
            _eventEmitter.Emit(new VoiceServerUpdatedEvent(voiceServer));
            return Task.CompletedTask;
        }
    }
}
