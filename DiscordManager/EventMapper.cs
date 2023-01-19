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
        private readonly IEventAggregator _eventAggregator;
        private readonly Discord _discord;

        public EventMapper(
            IEventAggregator eventAggregator,
            Discord discord)
        {
            _eventAggregator = eventAggregator;
            _discord = discord;
        }

        public void MapAllEvents()
        {
            _discord.Client.ApplicationCommandCreated += DiscordClient_ApplicationCommandCreated;
            _discord.Client.ApplicationCommandDeleted += DiscordClient_ApplicationCommandDeleted;
            _discord.Client.ApplicationCommandUpdated += DiscordClient_ApplicationCommandUpdated;
            _discord.Client.ModalSubmitted += DiscordClient_ModalSubmitted;
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
            _discord.Client.ButtonExecuted += DiscordClient_ButtonExecuted;
        }

        public async Task DiscordClient_ModalSubmitted(SocketModal modal)
            => await _eventAggregator.PublishAsync<ModalSubmittedEvent>(new(modal));

        public async Task DiscordClient_ButtonExecuted(SocketMessageComponent component)
            => await _eventAggregator.PublishAsync<ButtonExecutedEvent>(new(component));
        public async Task DiscordClient_UserCommandExecuted(SocketUserCommand command)
            => await _eventAggregator.PublishAsync<UserCommandExecutedEvent>(new(command));
        public async Task DiscordClient_ThreadUpdated(Cacheable<SocketThreadChannel, ulong> oldThread, SocketThreadChannel newThread)
            => await _eventAggregator.PublishAsync<ThreadUpdatedEvent>(new(oldThread, newThread));
        public async Task DiscordClient_ThreadMemberLeft(SocketThreadUser user)
            => await _eventAggregator.PublishAsync<ThreadMemberLeftEvent>(new(user));
        public async Task DiscordClient_ThreadMemberJoined(SocketThreadUser user)
            => await _eventAggregator.PublishAsync<ThreadMemberJoinedEvent>(new(user));
        public async Task DiscordClient_ThreadDeleted(Cacheable<SocketThreadChannel, ulong> thread)
            => await _eventAggregator.PublishAsync<ThreadDeletedEvent>(new(thread));
        public async Task DiscordClient_ThreadCreated(SocketThreadChannel thread)
            => await _eventAggregator.PublishAsync<ThreadCreatedEvent>(new(thread));
        public async Task DiscordClient_StageUpdated(SocketStageChannel oldChannel, SocketStageChannel newChannel)
            => await _eventAggregator.PublishAsync<StageUpdatedEvent>(new(oldChannel, newChannel));
        public async Task DiscordClient_StageStarted(SocketStageChannel channel)
            => await _eventAggregator.PublishAsync<StageStartedEvent>(new(channel));
        public async Task DiscordClient_StageEnded(SocketStageChannel channel)
            => await _eventAggregator.PublishAsync<StageEndedEvent>(new(channel));
        public async Task DiscordClient_SpeakerRemoved(SocketStageChannel channel, SocketGuildUser user)
            => await _eventAggregator.PublishAsync<SpeakerRemovedEvent>(new(channel, user));
        public async Task DiscordClient_SpeakerAdded(SocketStageChannel channel, SocketGuildUser user)
            => await _eventAggregator.PublishAsync<SpeakerAddedEvent>(new(channel, user));
        public async Task DiscordClient_SlashCommandExecuted(SocketSlashCommand command)
            => await _eventAggregator.PublishAsync<SlashCommandExecutedEvent>(new(command));
        public async Task DiscordClient_SelectMenuExecuted(SocketMessageComponent component)
            => await _eventAggregator.PublishAsync<SelectMenuExecutedEvent>(new(component));
        public async Task DiscordClient_RequestToSpeak(SocketStageChannel channel, SocketGuildUser user)
            => await _eventAggregator.PublishAsync<RequestToSpeakEvent>(new(channel, user));
        public async Task DiscordClient_MessageCommandExecuted(SocketMessageCommand command)
            => await _eventAggregator.PublishAsync<MessageCommandExecutedEvent>(new(command));
        public async Task DiscordClient_InteractionCreated(SocketInteraction interaction)
            => await _eventAggregator.PublishAsync<InteractionCreatedEvent>(new(interaction));
        public async Task DiscordClient_GuildStickerUpdated(SocketCustomSticker oldSticker, SocketCustomSticker newSticker)
            => await _eventAggregator.PublishAsync<GuildStickerUpdatedEvent>(new(oldSticker, newSticker));
        public async Task DiscordClient_GuildStickerDeleted(SocketCustomSticker sticker)
            => await _eventAggregator.PublishAsync<GuildStickerDeletedEvent>(new(sticker));
        public async Task DiscordClient_GuildStickerCreated(SocketCustomSticker sticker)
            => await _eventAggregator.PublishAsync<GuildStickerCreatedEvent>(new(sticker));
        public async Task DiscordClient_GuildJoinRequestDeleted(Cacheable<SocketGuildUser, ulong> user, SocketGuild guild)
            => await _eventAggregator.PublishAsync<GuildJoinRequestDeletedEvent>(new(user, guild));
        public async Task DiscordClient_AutocompleteExecuted(SocketAutocompleteInteraction interaction)
            => await _eventAggregator.PublishAsync<AutoCompleteExecutedEvent>(new(interaction));
        public async Task DiscordClient_ApplicationCommandUpdated(SocketApplicationCommand command)
            => await _eventAggregator.PublishAsync<ApplicationCommandDeletedEvent>(new(command));
        public async Task DiscordClient_ApplicationCommandDeleted(SocketApplicationCommand command)
            => await _eventAggregator.PublishAsync<ApplicationCommandDeletedEvent>(new(command));
        public async Task DiscordClient_ApplicationCommandCreated(SocketApplicationCommand command)
            => await _eventAggregator.PublishAsync<ApplicationCommandCreatedEvent>(new(command));
        public async Task DiscordClient_ChannelCreated(SocketChannel channel)
            => await _eventAggregator.PublishAsync<ChannelCreatedEvent>(new(channel));
        public async Task DiscordClient_ChannelDestroyed(SocketChannel channel)
            => await _eventAggregator.PublishAsync<ChannelDestroyedEvent>(new(channel));
        public async Task DiscordClient_ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
            => await _eventAggregator.PublishAsync<ChannelUpdatedEvent>(new(oldChannel, newChannel));
        public async Task DiscordClient_Connected()
            => await _eventAggregator.PublishAsync<ConnectedEvent>(new());
        public async Task DiscordClient_CurrentUserUpdated(SocketSelfUser oldUser, SocketSelfUser newUser)
            => await _eventAggregator.PublishAsync<CurrentUserUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_Disconnected(Exception exception)
            => await _eventAggregator.PublishAsync<DisconnectedEvent>(new(exception));
        public async Task DiscordClient_GuildAvailable(SocketGuild guild)
            => await _eventAggregator.PublishAsync<GuildAvailableEvent>(new(guild));
        public async Task DiscordClient_GuildMembersDownloaded(SocketGuild guild)
            => await _eventAggregator.PublishAsync<GuildMembersDownloadedEvent>(new(guild));
        public async Task DiscordClient_GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> oldUser, SocketGuildUser newUser)
            => await _eventAggregator.PublishAsync<GuildMemberUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_GuildUnavailable(SocketGuild guild)
            => await _eventAggregator.PublishAsync<GuildUnavailableEvent>(new(guild));
        public async Task DiscordClient_GuildUpdated(SocketGuild oldGuild, SocketGuild newGuild)
            => await _eventAggregator.PublishAsync<GuildUpdatedEvent>(new(oldGuild, newGuild));
        public async Task DiscordClient_InviteCreated(SocketInvite invite)
            => await _eventAggregator.PublishAsync<InviteCreatedEvent>(new(invite));
        public async Task DiscordClient_InviteDeleted(SocketGuildChannel originChannel, string inviteCode)
            => await _eventAggregator.PublishAsync<InviteDeletedEvent>(new(originChannel, inviteCode));
        public async Task DiscordClient_JoinedGuild(SocketGuild guild)
            => await _eventAggregator.PublishAsync<JoinedGuildEvent>(new(guild));
        public async Task DiscordClient_LatencyUpdated(int oldLatency, int newLatency)
            => await _eventAggregator.PublishAsync<LatencyUpdatedEvent>(new(oldLatency, newLatency));
        public async Task DiscordClient_LeftGuild(SocketGuild guild)
            => await _eventAggregator.PublishAsync<LeftGuildEvent>(new(guild));
        public async Task DiscordClient_Log(LogMessage message)
            => await _eventAggregator.PublishAsync<LogEvent>(new(message));
        public async Task DiscordClient_LoggedIn()
            => await _eventAggregator.PublishAsync<LoggedInEvent>(new());
        public async Task DiscordClient_LoggedOut()
            => await _eventAggregator.PublishAsync<LoggedOutEvent>(new());
        public async Task DiscordClient_MessageDeleted(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
            => await _eventAggregator.PublishAsync<MessageDeletedEvent>(new(message, channel));
        public async Task DiscordClient_MessageReceived(SocketMessage message)
            => await _eventAggregator.PublishAsync<MessageReceivedEvent>(new(message));
        public async Task DiscordClient_MessagesBulkDeleted(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages, Cacheable<IMessageChannel, ulong> channel)
            => await _eventAggregator.PublishAsync<MessagesBulkDeletedEvent>(new(messages, channel));
        public async Task DiscordClient_MessageUpdated(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage, ISocketMessageChannel channel)
            => await _eventAggregator.PublishAsync<MessageUpdatedEvent>(new(oldMessage, newMessage, channel));
        public async Task DiscordClient_ReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
            => await _eventAggregator.PublishAsync<ReactionAddedEvent>(new(message, channel, reaction));
        public async Task DiscordClient_ReactionRemoved(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
            => await _eventAggregator.PublishAsync<ReactionRemovedEvent>(new(message, channel, reaction));
        public async Task DiscordClient_ReactionsCleared(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
            => await _eventAggregator.PublishAsync<ReactionsClearedEvent>(new(message, channel));
        public async Task DiscordClient_ReactionsRemovedForEmote(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, IEmote emote)
            => await _eventAggregator.PublishAsync<ReactionsRemovedForEmoteEvent>(new(message, channel, emote));
        public async Task DiscordClient_Ready()
            => await _eventAggregator.PublishAsync<ReadyEvent>(new());
        public async Task DiscordClient_RecipientAdded(SocketGroupUser user)
            => await _eventAggregator.PublishAsync<RecipientAddedEvent>(new(user));
        public async Task DiscordClient_RecipientRemoved(SocketGroupUser user)
            => await _eventAggregator.PublishAsync<RecipientRemovedEvent>(new(user));
        public async Task DiscordClient_RoleCreated(SocketRole role)
            => await _eventAggregator.PublishAsync<RoleCreatedEvent>(new(role));
        public async Task DiscordClient_RoleDeleted(SocketRole role)
            => await _eventAggregator.PublishAsync<RoleDeletedEvent>(new(role));
        public async Task DiscordClient_RoleUpdated(SocketRole oldRole, SocketRole newRole)
            => await _eventAggregator.PublishAsync<RoleUpdatedEvent>(new(oldRole, newRole));
        public async Task DiscordClient_UserBanned(SocketUser user, SocketGuild guild)
            => await _eventAggregator.PublishAsync<UserBannedEvent>(new(user, guild));
        public async Task DiscordClient_UserIsTyping(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel)
            => await _eventAggregator.PublishAsync<UserIsTypingEvent>(new(user, channel));
        public async Task DiscordClient_UserJoined(SocketGuildUser guild)
            => await _eventAggregator.PublishAsync<UserJoinedEvent>(new(guild));
        public async Task DiscordClient_UserLeft(SocketGuild guild, SocketUser user)
            => await _eventAggregator.PublishAsync<UserLeftEvent>(new(guild, user));
        public async Task DiscordClient_UserUnbanned(SocketUser user, SocketGuild guild)
            => await _eventAggregator.PublishAsync<UserUnbannedEvent>(new(user, guild));
        public async Task DiscordClient_UserUpdated(SocketUser oldUser, SocketUser newUser)
            => await _eventAggregator.PublishAsync<UserUpdatedEvent>(new(oldUser, newUser));
        public async Task DiscordClient_UserVoiceStateUpdated(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
            => await _eventAggregator.PublishAsync<UserVoiceStateUpdatedEvent>(new(user, oldState, newState));
        public async Task DiscordClient_VoiceServerUpdated(SocketVoiceServer voiceServer)
            => await _eventAggregator.PublishAsync<VoiceServerUpdatedEvent>(new(voiceServer));
    }
}
