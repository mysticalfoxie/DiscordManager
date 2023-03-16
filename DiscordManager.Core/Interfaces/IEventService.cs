using System.Reactive.Subjects;
using DiscordManager.Core.Events;

namespace DiscordManager.Core.Interfaces;

public interface IEventService
{
    Subject<ApplicationCommandCreatedEvent> ApplicationCommandCreated { get; }
    Subject<ApplicationCommandDeletedEvent> ApplicationCommandDeleted { get; }
    Subject<ApplicationCommandUpdatedEvent> ApplicationCommandUpdated { get; }
    Subject<AutoCompleteExecutedEvent> AutocompleteExecuted { get; }
    Subject<ButtonExecutedEvent> ButtonExecuted { get; }
    Subject<ChannelCreatedEvent> ChannelCreated { get; }
    Subject<ChannelDestroyedEvent> ChannelDestroyed { get; }
    Subject<ChannelUpdatedEvent> ChannelUpdated { get; }
    Subject<ConnectedEvent> Connected { get; }
    Subject<CurrentUserUpdatedEvent> CurrentUserUpdated { get; }
    Subject<DisconnectedEvent> Disconnected { get; }
    Subject<GuildAvailableEvent> GuildAvailable { get; }
    Subject<GuildJoinRequestDeletedEvent> GuildJoinRequestDeleted { get; }
    Subject<GuildMembersDownloadedEvent> GuildMembersDownloaded { get; }
    Subject<GuildMemberUpdatedEvent> GuildMemberUpdated { get; }
    Subject<GuildStickerCreatedEvent> GuildStickerCreated { get; }
    Subject<GuildStickerDeletedEvent> GuildStickerDeleted { get; }
    Subject<GuildStickerUpdatedEvent> GuildStickerUpdated { get; }
    Subject<GuildUnavailableEvent> GuildUnavailable { get; }
    Subject<GuildUpdatedEvent> GuildUpdated { get; }
    Subject<InteractionCreatedEvent> InteractionCreated { get; }
    Subject<InviteCreatedEvent> InviteCreated { get; }
    Subject<InviteDeletedEvent> InviteDeleted { get; }
    Subject<JoinedGuildEvent> JoinedGuild { get; }
    Subject<LatencyUpdatedEvent> LatencyUpdated { get; }
    Subject<LeftGuildEvent> LeftGuild { get; }
    Subject<LogEvent> Log { get; }
    Subject<LoggedInEvent> LoggedIn { get; }
    Subject<LoggedOutEvent> LoggedOut { get; }
    Subject<MessageCommandExecutedEvent> MessageCommandExecuted { get; }
    Subject<MessageDeletedEvent> MessageDeleted { get; }
    Subject<MessageReceivedEvent> MessageReceived { get; }
    Subject<MessagesBulkDeletedEvent> MessagesBulkDeleted { get; }
    Subject<MessageUpdatedEvent> MessageUpdated { get; }
    Subject<ModalSubmittedEvent> ModalSubmitted { get; }
    Subject<ReactionAddedEvent> ReactionAdded { get; }
    Subject<ReactionRemovedEvent> ReactionRemoved { get; }
    Subject<ReactionsClearedEvent> ReactionsCleared { get; }
    Subject<ReactionsRemovedForEmoteEvent> ReactionsRemovedForEmote { get; }
    Subject<RecipientAddedEvent> RecipientAdded { get; }
    Subject<RecipientRemovedEvent> RecipientRemoved { get; }
    Subject<RequestToSpeakEvent> RequestToSpeak { get; }
    Subject<RoleCreatedEvent> RoleCreated { get; }
    Subject<RoleDeletedEvent> RoleDeleted { get; }
    Subject<RoleUpdatedEvent> RoleUpdated { get; }
    Subject<SelectMenuExecutedEvent> SelectMenuExecuted { get; }
    Subject<SlashCommandExecutedEvent> SlashCommandExecuted { get; }
    Subject<SpeakerAddedEvent> SpeakerAdded { get; }
    Subject<SpeakerRemovedEvent> SpeakerRemoved { get; }
    Subject<StageEndedEvent> StageEnded { get; }
    Subject<StageStartedEvent> StageStarted { get; }
    Subject<StageUpdatedEvent> StageUpdated { get; }
    Subject<ThreadCreatedEvent> ThreadCreated { get; }
    Subject<ThreadDeletedEvent> ThreadDeleted { get; }
    Subject<ThreadMemberJoinedEvent> ThreadMemberJoined { get; }
    Subject<ThreadMemberLeftEvent> ThreadMemberLeft { get; }
    Subject<ThreadUpdatedEvent> ThreadUpdated { get; }
    Subject<UserBannedEvent> UserBanned { get; }
    Subject<UserCommandExecutedEvent> UserCommandExecuted { get; }
    Subject<UserIsTypingEvent> UserIsTyping { get; }
    Subject<UserJoinedEvent> UserJoined { get; }
    Subject<UserLeftEvent> UserLeft { get; }
    Subject<UserUnbannedEvent> UserUnbanned { get; }
    Subject<UserUpdatedEvent> UserUpdated { get; }
    Subject<UserVoiceStateUpdatedEvent> UserVoiceStateUpdated { get; }
    Subject<VoiceServerUpdatedEvent> VoiceServerUpdated { get; }
}