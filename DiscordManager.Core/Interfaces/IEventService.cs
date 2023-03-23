using System.Reactive.Subjects;
using DCM.Core.Events;

namespace DCM.Core.Interfaces;

public interface IEventService
{
    Subject<ChannelCreatedEvent> ChannelCreated { get; }
    Subject<ChannelDestroyedEvent> ChannelDestroyed { get; }
    Subject<ChannelUpdatedEvent> ChannelUpdated { get; }
    Subject<MessageReceivedEvent> MessageReceived { get; }
    Subject<MessageDeletedEvent> MessageDeleted { get; }
    Subject<MessagesBulkDeletedEvent> MessagesBulkDeleted { get; }
    Subject<MessageUpdatedEvent> MessageUpdated { get; }
    Subject<ReactionAddedEvent> ReactionAdded { get; }
    Subject<ReactionRemovedEvent> ReactionRemoved { get; }
    Subject<ReactionsClearedEvent> ReactionsCleared { get; }
    Subject<ReactionsRemovedForEmoteEvent> ReactionsRemovedForEmote { get; }
    Subject<RoleCreatedEvent> RoleCreated { get; }
    Subject<RoleDeletedEvent> RoleDeleted { get; }
    Subject<RoleUpdatedEvent> RoleUpdated { get; }
    Subject<JoinedGuildEvent> JoinedGuild { get; }
    Subject<LeftGuildEvent> LeftGuild { get; }
    Subject<GuildAvailableEvent> GuildAvailable { get; }
    Subject<GuildUnavailableEvent> GuildUnavailable { get; }
    Subject<GuildMembersDownloadedEvent> GuildMembersDownloaded { get; }
    Subject<GuildUpdatedEvent> GuildUpdated { get; }
    Subject<GuildJoinRequestDeletedEvent> GuildJoinRequestDeleted { get; }
    Subject<GuildScheduledEventCreatedEvent> GuildScheduledEventCreated { get; }
    Subject<GuildScheduledEventUpdatedEvent> GuildScheduledEventUpdated { get; }
    Subject<GuildScheduledEventCancelledEvent> GuildScheduledEventCancelled { get; }
    Subject<GuildScheduledEventCompletedEvent> GuildScheduledEventCompleted { get; }
    Subject<GuildScheduledEventStartedEvent> GuildScheduledEventStarted { get; }
    Subject<GuildScheduledEventUserAddEvent> GuildScheduledEventUserAdd { get; }
    Subject<GuildScheduledEventUserRemoveEvent> GuildScheduledEventUserRemove { get; }
    Subject<IntegrationCreatedEvent> IntegrationCreated { get; }
    Subject<IntegrationUpdatedEvent> IntegrationUpdated { get; }
    Subject<IntegrationDeletedEvent> IntegrationDeleted { get; }
    Subject<UserJoinedEvent> UserJoined { get; }
    Subject<UserLeftEvent> UserLeft { get; }
    Subject<UserBannedEvent> UserBanned { get; }
    Subject<UserUnbannedEvent> UserUnbanned { get; }
    Subject<UserUpdatedEvent> UserUpdated { get; }
    Subject<GuildMemberUpdatedEvent> GuildMemberUpdated { get; }
    Subject<UserVoiceStateUpdatedEvent> UserVoiceStateUpdated { get; }
    Subject<VoiceServerUpdatedEvent> VoiceServerUpdated { get; }
    Subject<CurrentUserUpdatedEvent> CurrentUserUpdated { get; }
    Subject<UserIsTypingEvent> UserIsTyping { get; }
    Subject<RecipientAddedEvent> RecipientAdded { get; }
    Subject<RecipientRemovedEvent> RecipientRemoved { get; }
    Subject<PresenceUpdatedEvent> PresenceUpdated { get; }
    Subject<InviteCreatedEvent> InviteCreated { get; }
    Subject<InviteDeletedEvent> InviteDeleted { get; }
    Subject<InteractionCreatedEvent> InteractionCreated { get; }
    Subject<ButtonExecutedEvent> ButtonExecuted { get; }
    Subject<SelectMenuExecutedEvent> SelectMenuExecuted { get; }
    Subject<SlashCommandExecutedEvent> SlashCommandExecuted { get; }
    Subject<UserCommandExecutedEvent> UserCommandExecuted { get; }
    Subject<MessageCommandExecutedEvent> MessageCommandExecuted { get; }
    Subject<AutocompleteExecutedEvent> AutocompleteExecuted { get; }
    Subject<ModalSubmittedEvent> ModalSubmitted { get; }
    Subject<ApplicationCommandCreatedEvent> ApplicationCommandCreated { get; }
    Subject<ApplicationCommandUpdatedEvent> ApplicationCommandUpdated { get; }
    Subject<ApplicationCommandDeletedEvent> ApplicationCommandDeleted { get; }
    Subject<ThreadCreatedEvent> ThreadCreated { get; }
    Subject<ThreadUpdatedEvent> ThreadUpdated { get; }
    Subject<ThreadDeletedEvent> ThreadDeleted { get; }
    Subject<ThreadMemberJoinedEvent> ThreadMemberJoined { get; }
    Subject<ThreadMemberLeftEvent> ThreadMemberLeft { get; }
    Subject<StageStartedEvent> StageStarted { get; }
    Subject<StageEndedEvent> StageEnded { get; }
    Subject<StageUpdatedEvent> StageUpdated { get; }
    Subject<RequestToSpeakEvent> RequestToSpeak { get; }
    Subject<SpeakerAddedEvent> SpeakerAdded { get; }
    Subject<SpeakerRemovedEvent> SpeakerRemoved { get; }
    Subject<GuildStickerCreatedEvent> GuildStickerCreated { get; }
    Subject<GuildStickerUpdatedEvent> GuildStickerUpdated { get; }
    Subject<GuildStickerDeletedEvent> GuildStickerDeleted { get; }
    Subject<WebhooksUpdatedEvent> WebhooksUpdated { get; }
    void MapEvents();
}