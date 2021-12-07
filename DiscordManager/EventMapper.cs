using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCM.Events.Discord;
using System.Reflection;
using System.Reflection.Emit;

namespace DCM
{
    internal interface IEventMapper
    {
        void MapAllEvents();
    }

    internal class EventMapper : IEventMapper
    {
        private readonly IEventAggregator _eventEmitter;
        private readonly Discord _discord;

        public EventMapper(DiscordManager discordManager, Discord discord)
        {
            _eventEmitter = discordManager.EventAggregator;
            _discord = discord;
        }

        public void MapAllEvents()
        {
            foreach (var ev in _discord.Client.GetType().GetEvents())
            {
                var methodInfo = GetType().GetMethod($"DiscordClient_{ev.Name}")
                    ?? throw new InvalidOperationException($"Cannot find the method for event '{ev.Name}'.");
                var method = Delegate.CreateDelegate(ev.EventHandlerType, methodInfo);
                ev.AddEventHandler(_discord.Client, method);
            }
        }

        public async Task DiscordClient_UserCommandExecuted(SocketUserCommand command)
            => await _eventEmitter.PublishAsync<UserCommandExecutedEvent>(new(command));
        public async Task DiscordClient_ThreadUpdated(Cacheable<SocketThreadChannel, ulong> oldThread, SocketThreadChannel newThread)
            => await _eventEmitter.PublishAsync<ThreadUpdatedEvent>(new(oldThread, newThread));
        public async Task DiscordClient_ThreadMemberLeft(SocketThreadUser user)
            => await _eventEmitter.PublishAsync<ThreadMemberLeftEvent>(new(user));
        public async Task DiscordClient_ThreadMemberJoined(SocketThreadUser user)
            => await _eventEmitter.PublishAsync<ThreadMemberJoinedEvent>(new(user));
        public async Task DiscordClient_ThreadDeleted(Cacheable<SocketThreadChannel, ulong> thread)
            => await _eventEmitter.PublishAsync<ThreadDeletedEvent>(new(thread));
        public async Task DiscordClient_ThreadCreated(SocketThreadChannel thread)
            => await _eventEmitter.PublishAsync<ThreadCreatedEvent>(new(thread));
        public async Task DiscordClient_StageUpdated(SocketStageChannel oldChannel, SocketStageChannel newChannel)
            => await _eventEmitter.PublishAsync<StageUpdatedEvent>(new(oldChannel, newChannel));
        public async Task DiscordClient_StageStarted(SocketStageChannel channel)
            => await _eventEmitter.PublishAsync<StageStartedEvent>(new(channel));
        public async Task DiscordClient_StageEnded(SocketStageChannel channel)
            => await _eventEmitter.PublishAsync<StageEndedEvent>(new(channel));
        public async Task DiscordClient_SpeakerRemoved(SocketStageChannel channel, SocketGuildUser user)
            => await _eventEmitter.PublishAsync<SpeakerRemovedEvent>(new(channel, user));
        public async Task DiscordClient_SpeakerAdded(SocketStageChannel channel, SocketGuildUser user)
            => await _eventEmitter.PublishAsync<SpeakerAddedEvent>(new(channel, user));
        public async Task DiscordClient_SlashCommandExecuted(SocketSlashCommand command)
            => await _eventEmitter.PublishAsync<SlashCommandExecutedEvent>(new(command));
        public async Task DiscordClient_SelectMenuExecuted(SocketMessageComponent component)
            => await _eventEmitter.PublishAsync<SelectMenuExecutedEvent>(new(component));
        public async Task DiscordClient_RequestToSpeak(SocketStageChannel channel, SocketGuildUser user)
            => await _eventEmitter.PublishAsync<RequestToSpeakEvent>(new(channel, user));
        public async Task DiscordClient_MessageCommandExecuted(SocketMessageCommand command)
            => await _eventEmitter.PublishAsync<MessageCommandExecutedEvent>(new(command));
        public async Task DiscordClient_InteractionCreated(SocketInteraction interaction)
            => await _eventEmitter.PublishAsync<InteractionCreatedEvent>(new(interaction));
        public async Task DiscordClient_GuildStickerUpdated(SocketCustomSticker oldSticker, SocketCustomSticker newSticker)
            => await _eventEmitter.PublishAsync<GuildStickerUpdatedEvent>(new(oldSticker, newSticker));
        public async Task DiscordClient_GuildStickerDeleted(SocketCustomSticker sticker)
            => await _eventEmitter.PublishAsync<GuildStickerDeletedEvent>(new(sticker));
        public async Task DiscordClient_GuildStickerCreated(SocketCustomSticker sticker)
            => await _eventEmitter.PublishAsync<GuildStickerCreatedEvent>(new(sticker));
        public async Task DiscordClient_GuildJoinRequestDeleted(Cacheable<SocketGuildUser, ulong> user, SocketGuild guild)
            => await _eventEmitter.PublishAsync<GuildJoinRequestDeletedEvent>(new(user, guild));
        public async Task DiscordClient_AutocompleteExecuted(SocketAutocompleteInteraction interaction)
            => await _eventEmitter.PublishAsync<AutoCompleteExecutedEvent>(new(interaction));
        public async Task DiscordClient_ApplicationCommandUpdated(SocketApplicationCommand command)
            => await _eventEmitter.PublishAsync<ApplicationCommandDeletedEvent>(new(command));
        public async Task DiscordClient_ApplicationCommandDeleted(SocketApplicationCommand command)
            => await _eventEmitter.PublishAsync<ApplicationCommandDeletedEvent>(new(command));
        public async Task DiscordClient_ApplicationCommandCreated(SocketApplicationCommand command)
            => await _eventEmitter.PublishAsync<ApplicationCommandCreatedEvent>(new(command));
        public async Task DiscordClient_ChannelCreated(SocketChannel channel)
            => await _eventEmitter.PublishAsync<ChannelCreatedEvent>(new(channel));
        public async Task DiscordClient_ChannelDestroyed(SocketChannel channel)
            => await _eventEmitter.PublishAsync<ChannelDestroyedEvent>(new(channel));
        public async Task DiscordClient_ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
            => await _eventEmitter.PublishAsync<ChannelUpdatedEvent>(new(oldChannel, newChannel));
        public async Task DiscordClient_Connected()
            => await _eventEmitter.PublishAsync<ConnectedEvent>(new());
        public async Task DiscordClient_CurrentUserUpdated(SocketSelfUser oldUser, SocketSelfUser newUser)
            => await _eventEmitter.PublishAsync<CurrentUserUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_Disconnected(Exception exception)
            => await _eventEmitter.PublishAsync<DisconnectedEvent>(new(exception));
        public async Task DiscordClient_GuildAvailable(SocketGuild guild)
            => await _eventEmitter.PublishAsync<GuildAvailableEvent>(new(guild));
        public async Task DiscordClient_GuildMembersDownloaded(SocketGuild guild)
            => await _eventEmitter.PublishAsync<GuildMembersDownloadedEvent>(new(guild));
        public async Task DiscordClient_GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> oldUser, SocketGuildUser newUser)
            => await _eventEmitter.PublishAsync<GuildMemberUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_GuildUnavailable(SocketGuild guild)
            => await _eventEmitter.PublishAsync<GuildUnavailableEvent>(new(guild));
        public async Task DiscordClient_GuildUpdated(SocketGuild oldGuild, SocketGuild newGuild)
            => await _eventEmitter.PublishAsync<GuildUpdatedEvent>(new(oldGuild, newGuild));
        public async Task DiscordClient_InviteCreated(SocketInvite invite)
            => await _eventEmitter.PublishAsync<InviteCreatedEvent>(new(invite));
        public async Task DiscordClient_InviteDeleted(SocketGuildChannel originChannel, string inviteCode)
            => await _eventEmitter.PublishAsync<InviteDeletedEvent>(new(originChannel, inviteCode));
        public async Task DiscordClient_JoinedGuild(SocketGuild guild)
            => await _eventEmitter.PublishAsync<JoinedGuildEvent>(new(guild));
        public async Task DiscordClient_LatencyUpdated(int oldLatency, int newLatency)
            => await _eventEmitter.PublishAsync<LatencyUpdatedEvent>(new(oldLatency, newLatency));
        public async Task DiscordClient_LeftGuild(SocketGuild guild)
            => await _eventEmitter.PublishAsync<LeftGuildEvent>(new(guild));
        public async Task DiscordClient_Log(LogMessage message)
            => await _eventEmitter.PublishAsync<LogEvent>(new(message));
        public async Task DiscordClient_LoggedIn()
            => await _eventEmitter.PublishAsync<LoggedInEvent>(new());
        public async Task DiscordClient_LoggedOut()
            => await _eventEmitter.PublishAsync<LoggedOutEvent>(new());
        public async Task DiscordClient_MessageDeleted(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
            => await _eventEmitter.PublishAsync<MessageDeletedEvent>(new(message, channel));
        public async Task DiscordClient_MessageReceived(SocketMessage message)
            => await _eventEmitter.PublishAsync<MessageReceivedEvent>(new(message));
        public async Task DiscordClient_MessagesBulkDeleted(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages, Cacheable<IMessageChannel, ulong> channel)
            => await _eventEmitter.PublishAsync<MessagesBulkDeletedEvent>(new(messages, channel));
        public async Task DiscordClient_MessageUpdated(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage, ISocketMessageChannel channel)
            => await _eventEmitter.PublishAsync<MessageUpdatedEvent>(new(oldMessage, newMessage, channel));
        public async Task DiscordClient_ReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
            => await _eventEmitter.PublishAsync<ReactionAddedEvent>(new(message, channel, reaction));
        public async Task DiscordClient_ReactionRemoved(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
            => await _eventEmitter.PublishAsync<ReactionRemovedEvent>(new(message, channel, reaction));
        public async Task DiscordClient_ReactionsCleared(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
            => await _eventEmitter.PublishAsync<ReactionsClearedEvent>(new(message, channel));
        public async Task DiscordClient_ReactionsRemovedForEmote(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, IEmote emote)
            => await _eventEmitter.PublishAsync<ReactionsRemovedForEmoteEvent>(new(message, channel, emote));
        public async Task DiscordClient_Ready()
            => await _eventEmitter.PublishAsync<ReadyEvent>(new());
        public async Task DiscordClient_RecipientAdded(SocketGroupUser user)
            => await _eventEmitter.PublishAsync<RecipientAddedEvent>(new(user));
        public async Task DiscordClient_RecipientRemoved(SocketGroupUser user)
            => await _eventEmitter.PublishAsync<RecipientRemovedEvent>(new(user));
        public async Task DiscordClient_RoleCreated(SocketRole role)
            => await _eventEmitter.PublishAsync<RoleCreatedEvent>(new(role));
        public async Task DiscordClient_RoleDeleted(SocketRole role)
            => await _eventEmitter.PublishAsync<RoleDeletedEvent>(new(role));
        public async Task DiscordClient_RoleUpdated(SocketRole oldRole, SocketRole newRole)
            => await _eventEmitter.PublishAsync<RoleUpdatedEvent>(new(oldRole, newRole));
        public async Task DiscordClient_UserBanned(SocketUser user, SocketGuild guild)
            => await _eventEmitter.PublishAsync<UserBannedEvent>(new(user, guild));
        public async Task DiscordClient_UserIsTyping(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel)
            => await _eventEmitter.PublishAsync<UserIsTypingEvent>(new(user, channel));
        public async Task DiscordClient_UserJoined(SocketGuildUser guild)
            => await _eventEmitter.PublishAsync<UserJoinedEvent>(new(guild));
        public async Task DiscordClient_UserLeft(SocketGuildUser guild)
            => await _eventEmitter.PublishAsync<UserLeftEvent>(new(guild));
        public async Task DiscordClient_UserUnbanned(SocketUser user, SocketGuild guild)
            => await _eventEmitter.PublishAsync<UserUnbannedEvent>(new(user, guild));
        public async Task DiscordClient_UserUpdated(SocketUser oldUser, SocketUser newUser)
            => await _eventEmitter.PublishAsync<UserUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_UserVoiceStateUpdated(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
            => await _eventEmitter.PublishAsync<UserVoiceStateUpdatedEvent>(new(user, oldState, newState));
        public async Task DiscordClient_VoiceServerUpdated(SocketVoiceServer voiceServer)
            => await _eventEmitter.PublishAsync<VoiceServerUpdatedEvent>(new(voiceServer));
    }
}
