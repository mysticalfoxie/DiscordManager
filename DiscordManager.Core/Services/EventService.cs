using System.Reactive.Subjects;
using DCM.Core.Attributes;
using DCM.Core.Events;
using DCM.Core.Interfaces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DCM.Core.Services;

[Injectable(typeof(IEventService))]
public class EventService : IEventService
{
    private readonly IConfigService _configService;
    private readonly IDiscordService _discordService;

    public EventService(
        IConfigService configService,
        IDiscordService discordService)
    {
        _configService = configService;
        _discordService = discordService;
    }


    public Subject<ChannelCreatedEvent> ChannelCreated { get; } = new();
    public Subject<ChannelDestroyedEvent> ChannelDestroyed { get; } = new();
    public Subject<ChannelUpdatedEvent> ChannelUpdated { get; } = new();
    public Subject<MessageReceivedEvent> MessageReceived { get; } = new();
    public Subject<MessageDeletedEvent> MessageDeleted { get; } = new();
    public Subject<MessagesBulkDeletedEvent> MessagesBulkDeleted { get; } = new();
    public Subject<MessageUpdatedEvent> MessageUpdated { get; } = new();
    public Subject<ReactionAddedEvent> ReactionAdded { get; } = new();
    public Subject<ReactionRemovedEvent> ReactionRemoved { get; } = new();
    public Subject<ReactionsClearedEvent> ReactionsCleared { get; } = new();
    public Subject<ReactionsRemovedForEmoteEvent> ReactionsRemovedForEmote { get; } = new();
    public Subject<RoleCreatedEvent> RoleCreated { get; } = new();
    public Subject<RoleDeletedEvent> RoleDeleted { get; } = new();
    public Subject<RoleUpdatedEvent> RoleUpdated { get; } = new();
    public Subject<JoinedGuildEvent> JoinedGuild { get; } = new();
    public Subject<LeftGuildEvent> LeftGuild { get; } = new();
    public Subject<GuildAvailableEvent> GuildAvailable { get; } = new();
    public Subject<GuildUnavailableEvent> GuildUnavailable { get; } = new();
    public Subject<GuildMembersDownloadedEvent> GuildMembersDownloaded { get; } = new();
    public Subject<GuildUpdatedEvent> GuildUpdated { get; } = new();
    public Subject<GuildJoinRequestDeletedEvent> GuildJoinRequestDeleted { get; } = new();
    public Subject<GuildScheduledEventCreatedEvent> GuildScheduledEventCreated { get; } = new();
    public Subject<GuildScheduledEventUpdatedEvent> GuildScheduledEventUpdated { get; } = new();
    public Subject<GuildScheduledEventCancelledEvent> GuildScheduledEventCancelled { get; } = new();
    public Subject<GuildScheduledEventCompletedEvent> GuildScheduledEventCompleted { get; } = new();
    public Subject<GuildScheduledEventStartedEvent> GuildScheduledEventStarted { get; } = new();
    public Subject<GuildScheduledEventUserAddEvent> GuildScheduledEventUserAdd { get; } = new();
    public Subject<GuildScheduledEventUserRemoveEvent> GuildScheduledEventUserRemove { get; } = new();
    public Subject<IntegrationCreatedEvent> IntegrationCreated { get; } = new();
    public Subject<IntegrationUpdatedEvent> IntegrationUpdated { get; } = new();
    public Subject<IntegrationDeletedEvent> IntegrationDeleted { get; } = new();
    public Subject<UserJoinedEvent> UserJoined { get; } = new();
    public Subject<UserLeftEvent> UserLeft { get; } = new();
    public Subject<UserBannedEvent> UserBanned { get; } = new();
    public Subject<UserUnbannedEvent> UserUnbanned { get; } = new();
    public Subject<UserUpdatedEvent> UserUpdated { get; } = new();
    public Subject<GuildMemberUpdatedEvent> GuildMemberUpdated { get; } = new();
    public Subject<UserVoiceStateUpdatedEvent> UserVoiceStateUpdated { get; } = new();
    public Subject<VoiceServerUpdatedEvent> VoiceServerUpdated { get; } = new();
    public Subject<CurrentUserUpdatedEvent> CurrentUserUpdated { get; } = new();
    public Subject<UserIsTypingEvent> UserIsTyping { get; } = new();
    public Subject<RecipientAddedEvent> RecipientAdded { get; } = new();
    public Subject<RecipientRemovedEvent> RecipientRemoved { get; } = new();
    public Subject<PresenceUpdatedEvent> PresenceUpdated { get; } = new();
    public Subject<InviteCreatedEvent> InviteCreated { get; } = new();
    public Subject<InviteDeletedEvent> InviteDeleted { get; } = new();
    public Subject<InteractionCreatedEvent> InteractionCreated { get; } = new();
    public Subject<ButtonExecutedEvent> ButtonExecuted { get; } = new();
    public Subject<SelectMenuExecutedEvent> SelectMenuExecuted { get; } = new();
    public Subject<SlashCommandExecutedEvent> SlashCommandExecuted { get; } = new();
    public Subject<UserCommandExecutedEvent> UserCommandExecuted { get; } = new();
    public Subject<MessageCommandExecutedEvent> MessageCommandExecuted { get; } = new();
    public Subject<AutocompleteExecutedEvent> AutocompleteExecuted { get; } = new();
    public Subject<ModalSubmittedEvent> ModalSubmitted { get; } = new();
    public Subject<ApplicationCommandCreatedEvent> ApplicationCommandCreated { get; } = new();
    public Subject<ApplicationCommandUpdatedEvent> ApplicationCommandUpdated { get; } = new();
    public Subject<ApplicationCommandDeletedEvent> ApplicationCommandDeleted { get; } = new();
    public Subject<ThreadCreatedEvent> ThreadCreated { get; } = new();
    public Subject<ThreadUpdatedEvent> ThreadUpdated { get; } = new();
    public Subject<ThreadDeletedEvent> ThreadDeleted { get; } = new();
    public Subject<ThreadMemberJoinedEvent> ThreadMemberJoined { get; } = new();
    public Subject<ThreadMemberLeftEvent> ThreadMemberLeft { get; } = new();
    public Subject<StageStartedEvent> StageStarted { get; } = new();
    public Subject<StageEndedEvent> StageEnded { get; } = new();
    public Subject<StageUpdatedEvent> StageUpdated { get; } = new();
    public Subject<RequestToSpeakEvent> RequestToSpeak { get; } = new();
    public Subject<SpeakerAddedEvent> SpeakerAdded { get; } = new();
    public Subject<SpeakerRemovedEvent> SpeakerRemoved { get; } = new();
    public Subject<GuildStickerCreatedEvent> GuildStickerCreated { get; } = new();
    public Subject<GuildStickerUpdatedEvent> GuildStickerUpdated { get; } = new();
    public Subject<GuildStickerDeletedEvent> GuildStickerDeleted { get; } = new();
    public Subject<WebhooksUpdatedEvent> WebhooksUpdated { get; } = new();

    public void MapEvents()
    {
        _discordService.Client.ChannelCreated += OnChannelCreated;
        _discordService.Client.ChannelDestroyed += OnChannelDestroyed;
        _discordService.Client.MessageReceived += OnMessageReceived;
        _discordService.Client.RoleCreated += OnRoleCreated;
        _discordService.Client.RoleDeleted += OnRoleDeleted;
        _discordService.Client.JoinedGuild += OnJoinedGuild;
        _discordService.Client.LeftGuild += OnLeftGuild;
        _discordService.Client.GuildAvailable += OnGuildAvailable;
        _discordService.Client.GuildUnavailable += OnGuildUnavailable;
        _discordService.Client.GuildMembersDownloaded += OnGuildMembersDownloaded;
        _discordService.Client.GuildScheduledEventCancelled += OnGuildScheduledEventCancelled;
        _discordService.Client.GuildScheduledEventCompleted += OnGuildScheduledEventCompleted;
        _discordService.Client.GuildScheduledEventStarted += OnGuildScheduledEventStarted;
        _discordService.Client.IntegrationCreated += OnIntegrationCreated;
        _discordService.Client.IntegrationUpdated += OnIntegrationUpdated;
        _discordService.Client.UserJoined += OnUserJoined;
        _discordService.Client.VoiceServerUpdated += OnVoiceServerUpdated;
        _discordService.Client.RecipientAdded += OnRecipientAdded;
        _discordService.Client.RecipientRemoved += OnRecipientRemoved;
        _discordService.Client.InviteCreated += OnInviteCreated;
        _discordService.Client.InteractionCreated += OnInteractionCreated;
        _discordService.Client.ButtonExecuted += OnButtonExecuted;
        _discordService.Client.SelectMenuExecuted += OnSelectMenuExecuted;
        _discordService.Client.SlashCommandExecuted += OnSlashCommandExecuted;
        _discordService.Client.UserCommandExecuted += OnUserCommandExecuted;
        _discordService.Client.MessageCommandExecuted += OnMessageCommandExecuted;
        _discordService.Client.AutocompleteExecuted += OnAutocompleteExecuted;
        _discordService.Client.ModalSubmitted += OnModalSubmitted;
        _discordService.Client.ApplicationCommandCreated += OnApplicationCommandCreated;
        _discordService.Client.ApplicationCommandUpdated += OnApplicationCommandUpdated;
        _discordService.Client.ApplicationCommandDeleted += OnApplicationCommandDeleted;
        _discordService.Client.ThreadCreated += OnThreadCreated;
        _discordService.Client.ThreadMemberJoined += OnThreadMemberJoined;
        _discordService.Client.ThreadMemberLeft += OnThreadMemberLeft;
        _discordService.Client.StageStarted += OnStageStarted;
        _discordService.Client.StageEnded += OnStageEnded;
        _discordService.Client.GuildStickerCreated += OnGuildStickerCreated;
        _discordService.Client.GuildStickerDeleted += OnGuildStickerDeleted;
        _discordService.Client.RoleUpdated += OnRoleUpdated;
        _discordService.Client.GuildUpdated += OnGuildUpdated;
        _discordService.Client.UserLeft += OnUserLeft;
        _discordService.Client.UserBanned += OnUserBanned;
        _discordService.Client.UserUnbanned += OnUserUnbanned;
        _discordService.Client.InviteDeleted += OnInviteDeleted;
        _discordService.Client.UserUpdated += OnUserUpdated;
        _discordService.Client.StageUpdated += OnStageUpdated;
        _discordService.Client.RequestToSpeak += OnRequestToSpeak;
        _discordService.Client.SpeakerAdded += OnSpeakerAdded;
        _discordService.Client.SpeakerRemoved += OnSpeakerRemoved;
        _discordService.Client.GuildStickerUpdated += OnGuildStickerUpdated;
        _discordService.Client.ChannelUpdated += OnChannelUpdated;
        _discordService.Client.WebhooksUpdated += OnWebhooksUpdated;
        _discordService.Client.MessageDeleted += OnMessageDeleted;
        _discordService.Client.MessagesBulkDeleted += OnMessagesBulkDeleted;
        _discordService.Client.MessageUpdated += OnMessageUpdated;
        _discordService.Client.ReactionAdded += OnReactionAdded;
        _discordService.Client.ReactionRemoved += OnReactionRemoved;
        _discordService.Client.ReactionsRemovedForEmote += OnReactionsRemovedForEmote;
        _discordService.Client.ReactionsCleared += OnReactionsCleared;
        _discordService.Client.GuildJoinRequestDeleted += OnGuildJoinRequestDeleted;
        _discordService.Client.GuildScheduledEventCreated += OnGuildScheduledEventCreated;
        _discordService.Client.GuildScheduledEventUpdated += OnGuildScheduledEventUpdated;
        _discordService.Client.GuildScheduledEventUserAdd += OnGuildScheduledEventUserAdd;
        _discordService.Client.GuildScheduledEventUserRemove += OnGuildScheduledEventUserRemove;
        _discordService.Client.IntegrationDeleted += OnIntegrationDeleted;
        _discordService.Client.GuildMemberUpdated += OnGuildMemberUpdated;
        _discordService.Client.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
        _discordService.Client.CurrentUserUpdated += OnCurrentUserUpdated;
        _discordService.Client.UserIsTyping += OnUserIsTyping;
        _discordService.Client.PresenceUpdated += OnPresenceUpdated;
        _discordService.Client.ThreadUpdated += OnThreadUpdated;
        _discordService.Client.ThreadDeleted += OnThreadDeleted;
    }

    public Task OnApplicationCommandCreated(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(guild: command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandCreatedEvent
        {
            Command = command
        };
        ApplicationCommandCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnApplicationCommandDeleted(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(guild: command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandDeletedEvent
        {
            Command = command
        };
        ApplicationCommandDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnApplicationCommandUpdated(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(guild: command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandUpdatedEvent
        {
            Command = command
        };
        ApplicationCommandUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnAutocompleteExecuted(
        SocketAutocompleteInteraction interaction)
    {
        if (!ShouldEmit(channel: interaction.Channel))
            return Task.CompletedTask;

        var eventArgs = new AutocompleteExecutedEvent
        {
            Interaction = interaction
        };
        AutocompleteExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnButtonExecuted(
        SocketMessageComponent component)
    {
        if (!ShouldEmit(channel: component.Channel))
            return Task.CompletedTask;

        var eventArgs = new ButtonExecutedEvent
        {
            Component = component
        };
        ButtonExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelCreated(
        SocketChannel channel)
    {
        if (!ShouldEmit(channel: channel))
            return Task.CompletedTask;

        var eventArgs = new ChannelCreatedEvent
        {
            Channel = channel
        };
        ChannelCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelDestroyed(
        SocketChannel channel)
    {
        if (!ShouldEmit(channel: channel))
            return Task.CompletedTask;

        var eventArgs = new ChannelDestroyedEvent
        {
            Channel = channel
        };
        ChannelDestroyed.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelUpdated(
        SocketChannel oldChannel,
        SocketChannel newChannel)
    {
        if (!ShouldEmit(channel: newChannel))
            return Task.CompletedTask;

        var eventArgs = new ChannelUpdatedEvent
        {
            OldChannel = oldChannel,
            NewChannel = newChannel
        };
        ChannelUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnCurrentUserUpdated(
        SocketSelfUser oldSelfUser,
        SocketSelfUser newSelfUser)
    {
        var eventArgs = new CurrentUserUpdatedEvent
        {
            OldSelfUser = oldSelfUser,
            NewSelfUser = newSelfUser
        };
        CurrentUserUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildAvailable(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new GuildAvailableEvent
        {
            Guild = guild
        };
        GuildAvailable.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }


    public Task OnGuildJoinRequestDeleted(
        Cacheable<SocketGuildUser, ulong> guildUser,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new GuildJoinRequestDeletedEvent
        {
            GuildUser = guildUser,
            Guild = guild
        };
        GuildJoinRequestDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildMembersDownloaded(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new GuildMembersDownloadedEvent
        {
            Guild = guild
        };
        GuildMembersDownloaded.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildMemberUpdated(
        Cacheable<SocketGuildUser, ulong> oldGuildMember,
        SocketGuildUser newGuildMember)
    {
        if (!ShouldEmit(guild: newGuildMember.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildMemberUpdatedEvent
        {
            OldGuildUser = oldGuildMember,
            NewGuildUser = newGuildMember
        };
        GuildMemberUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCancelled(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCancelledEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCancelled.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCompleted(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCompletedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCompleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCreated(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCreatedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventStarted(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventStartedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventStarted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUpdated(
        Cacheable<SocketGuildEvent, ulong> oldGuildEvent,
        SocketGuildEvent newGuildEvent)
    {
        if (!ShouldEmit(guild: newGuildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUpdatedEvent
        {
            OldGuildEvent = oldGuildEvent,
            NewGuildEvent = newGuildEvent
        };
        GuildScheduledEventUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUserAdd(
        Cacheable<SocketUser, RestUser, IUser, ulong> user,
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUserAddEvent
        {
            GuildEvent = guildEvent,
            User = user
        };
        GuildScheduledEventUserAdd.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUserRemove(
        Cacheable<SocketUser, RestUser, IUser, ulong> user,
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guild: guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUserRemoveEvent
        {
            GuildEvent = guildEvent,
            User = user
        };
        GuildScheduledEventUserRemove.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerCreated(
        SocketCustomSticker sticker)
    {
        if (!ShouldEmit(guild: sticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerCreatedEvent
        {
            Sticker = sticker
        };
        GuildStickerCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerDeleted(
        SocketCustomSticker sticker)
    {
        if (!ShouldEmit(guild: sticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerDeletedEvent
        {
            Sticker = sticker
        };
        GuildStickerDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerUpdated(
        SocketCustomSticker oldSticker,
        SocketCustomSticker newSticker)
    {
        if (!ShouldEmit(guild: newSticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerUpdatedEvent
        {
            OldSticker = oldSticker,
            NewSticker = newSticker
        };
        GuildStickerUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildUnavailable(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new GuildUnavailableEvent
        {
            Guild = guild
        };
        GuildUnavailable.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildUpdated(
        SocketGuild oldGuild,
        SocketGuild newGuild)
    {
        if (!ShouldEmit(guild: newGuild))
            return Task.CompletedTask;

        var eventArgs = new GuildUpdatedEvent
        {
            OldGuild = oldGuild,
            NewGuild = newGuild
        };
        GuildUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationCreated(IIntegration integration)
    {
        if (!ShouldEmit(guild: integration.Guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationCreatedEvent
        {
            Integration = integration
        };
        IntegrationCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationDeleted(IGuild guild, ulong id, Optional<ulong> huh)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationDeletedEvent
        {
            Guild = guild,
            Id = id,
            Huh = huh
        };
        IntegrationDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationUpdated(IIntegration integration)
    {
        if (!ShouldEmit(guild: integration.Guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationUpdatedEvent
        {
            Integration = integration
        };
        IntegrationUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInteractionCreated(
        SocketInteraction interaction)
    {
        if (!ShouldEmit(channel: interaction.Channel))
            return Task.CompletedTask;

        var eventArgs = new InteractionCreatedEvent
        {
            Interaction = interaction
        };
        InteractionCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInviteCreated(
        SocketInvite invite)
    {
        if (!ShouldEmit(guild: invite.Guild))
            return Task.CompletedTask;

        var eventArgs = new InviteCreatedEvent
        {
            Invite = invite
        };
        InviteCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInviteDeleted(
        SocketGuildChannel guildChannel,
        string invite)
    {
        if (!ShouldEmit(channel: guildChannel))
            return Task.CompletedTask;

        var eventArgs = new InviteDeletedEvent
        {
            GuildChannel = guildChannel,
            Invite = invite
        };
        InviteDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnJoinedGuild(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new JoinedGuildEvent
        {
            Guild = guild
        };
        JoinedGuild.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnLeftGuild(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new LeftGuildEvent
        {
            Guild = guild
        };
        LeftGuild.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageCommandExecuted(
        SocketMessageCommand command)
    {
        if (!ShouldEmit(channel: command.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageCommandExecutedEvent
        {
            Command = command
        };
        MessageCommandExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }


    public Task OnMessageDeleted(
        Cacheable<IMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new MessageDeletedEvent
        {
            Message = message,
            Channel = channel
        };
        MessageDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageReceived(
        SocketMessage message)
    {
        if (!ShouldEmit(channel: message.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageReceivedEvent
        {
            Message = message
        };
        MessageReceived.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessagesBulkDeleted(
        IReadOnlyCollection<Cacheable<IMessage, ulong>> messages,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new MessagesBulkDeletedEvent
        {
            Messages = messages,
            Channel = channel
        };
        MessagesBulkDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageUpdated(
        Cacheable<IMessage, ulong> oldMessage,
        SocketMessage newMessage,
        ISocketMessageChannel channel)
    {
        if (!ShouldEmit(channel: newMessage.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageUpdatedEvent
        {
            OldMessage = oldMessage,
            NewMessage = newMessage,
            Channel = channel
        };
        MessageUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnModalSubmitted(
        SocketModal modal)
    {
        if (!ShouldEmit(channel: modal.Channel))
            return Task.CompletedTask;

        var eventArgs = new ModalSubmittedEvent
        {
            Modal = modal
        };
        ModalSubmitted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnPresenceUpdated(
        SocketUser user,
        SocketPresence oldPresence,
        SocketPresence newPresence)
    {
        var eventArgs = new PresenceUpdatedEvent
        {
            User = user,
            OldPresence = oldPresence,
            NewPresence = newPresence
        };
        PresenceUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionAdded(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionAddedEvent
        {
            Message = message,
            Channel = channel,
            Reaction = reaction
        };
        ReactionAdded.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionRemoved(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionRemovedEvent
        {
            Message = message,
            Channel = channel,
            Reaction = reaction
        };
        ReactionRemoved.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionsCleared(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionsClearedEvent
        {
            Message = message,
            Channel = channel
        };
        ReactionsCleared.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionsRemovedForEmote(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        IEmote emote)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionsRemovedForEmoteEvent
        {
            Channel = channel,
            Message = message,
            Emote = emote
        };
        ReactionsRemovedForEmote.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRecipientAdded(
        SocketGroupUser groupUser)
    {
        var eventArgs = new RecipientAddedEvent
        {
            GroupUser = groupUser
        };
        RecipientAdded.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRecipientRemoved(
        SocketGroupUser groupUser)
    {
        var eventArgs = new RecipientRemovedEvent
        {
            GroupUser = groupUser
        };
        RecipientRemoved.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRequestToSpeak(
        SocketStageChannel stageChannel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(guild: stageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new RequestToSpeakEvent
        {
            Channel = stageChannel,
            User = user
        };
        RequestToSpeak.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleCreated(
        SocketRole role)
    {
        if (!ShouldEmit(guild: role.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleCreatedEvent
        {
            Role = role
        };
        RoleCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleDeleted(
        SocketRole role)
    {
        if (!ShouldEmit(guild: role.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleDeletedEvent
        {
            Role = role
        };
        RoleDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleUpdated(
        SocketRole oldRole,
        SocketRole newRole)
    {
        if (!ShouldEmit(guild: newRole.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleUpdatedEvent
        {
            OldRole = oldRole,
            NewRole = newRole
        };
        RoleUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSelectMenuExecuted(
        SocketMessageComponent component)
    {
        if (!ShouldEmit(channel: component.Channel))
            return Task.CompletedTask;

        var eventArgs = new SelectMenuExecutedEvent
        {
            Component = component
        };
        SelectMenuExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSlashCommandExecuted(
        SocketSlashCommand command)
    {
        if (!ShouldEmit(channel: command.Channel))
            return Task.CompletedTask;

        var eventArgs = new SlashCommandExecutedEvent
        {
            Command = command
        };
        SlashCommandExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSpeakerAdded(
        SocketStageChannel stageChannel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(guild: stageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new SpeakerAddedEvent
        {
            Channel = stageChannel,
            User = user
        };
        SpeakerAdded.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSpeakerRemoved(
        SocketStageChannel channel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(guild: channel.Guild))
            return Task.CompletedTask;

        var eventArgs = new SpeakerRemovedEvent
        {
            Channel = channel,
            User = user
        };
        SpeakerRemoved.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageEnded(
        SocketStageChannel channel)
    {
        if (!ShouldEmit(channel: channel))
            return Task.CompletedTask;

        var eventArgs = new StageEndedEvent
        {
            Channel = channel
        };
        StageEnded.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageStarted(
        SocketStageChannel channel)
    {
        if (!ShouldEmit(channel: channel))
            return Task.CompletedTask;

        var eventArgs = new StageStartedEvent
        {
            Channel = channel
        };
        StageStarted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageUpdated(
        SocketStageChannel oldStageChannel,
        SocketStageChannel newStageChannel)
    {
        if (!ShouldEmit(guild: newStageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new StageUpdatedEvent
        {
            OldStageChannel = oldStageChannel,
            NewStageChannel = newStageChannel
        };
        StageUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadCreated(
        SocketThreadChannel channel)
    {
        if (!ShouldEmit(guild: channel.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadCreatedEvent
        {
            Channel = channel
        };
        ThreadCreated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadDeleted(
        Cacheable<SocketThreadChannel, ulong> thread)
    {
        if (thread.HasValue && !ShouldEmit(channel: thread.Value))
            return Task.CompletedTask;

        var eventArgs = new ThreadDeletedEvent
        {
            ThreadChannel = thread
        };
        ThreadDeleted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadMemberJoined(
        SocketThreadUser user)
    {
        if (!ShouldEmit(guild: user.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadMemberJoinedEvent
        {
            User = user
        };
        ThreadMemberJoined.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadMemberLeft(
        SocketThreadUser user)
    {
        if (!ShouldEmit(guild: user.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadMemberLeftEvent
        {
            User = user
        };
        ThreadMemberLeft.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadUpdated(
        Cacheable<SocketThreadChannel, ulong> oldThread,
        SocketThreadChannel newThread)
    {
        if (!ShouldEmit(guild: newThread.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadUpdatedEvent
        {
            OldThreadChannel = oldThread,
            NewThreadChannel = newThread
        };
        ThreadUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserBanned(
        SocketUser user,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new UserBannedEvent
        {
            User = user,
            Guild = guild
        };
        UserBanned.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserCommandExecuted(
        SocketUserCommand command)
    {
        if (!ShouldEmit(channel: command.Channel))
            return Task.CompletedTask;

        var eventArgs = new UserCommandExecutedEvent
        {
            Command = command
        };
        UserCommandExecuted.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserIsTyping(
        Cacheable<IUser, ulong> user,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel: channel.Value))
            return Task.CompletedTask;

        var eventArgs = new UserIsTypingEvent
        {
            User = user,
            Channel = channel
        };
        UserIsTyping.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserJoined(
        SocketGuildUser guildUser)
    {
        if (!ShouldEmit(guild: guildUser.Guild))
            return Task.CompletedTask;

        var eventArgs = new UserJoinedEvent
        {
            GuildUser = guildUser
        };
        UserJoined.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserLeft(
        SocketGuild guild,
        SocketUser user)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new UserLeftEvent
        {
            Guild = guild,
            User = user
        };
        UserLeft.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserUnbanned(
        SocketUser user,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new UserUnbannedEvent
        {
            User = user,
            Guild = guild
        };
        UserUnbanned.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserUpdated(
        SocketUser oldUser,
        SocketUser newUser)
    {
        var eventArgs = new UserUpdatedEvent
        {
            OldUser = oldUser,
            NewUser = newUser
        };
        UserUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserVoiceStateUpdated(
        SocketUser user,
        SocketVoiceState oldVoiceState,
        SocketVoiceState newVoiceState)
    {
        var eventArgs = new UserVoiceStateUpdatedEvent
        {
            User = user,
            OldVoiceState = oldVoiceState,
            NewVoiceState = newVoiceState
        };
        UserVoiceStateUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnVoiceServerUpdated(
        SocketVoiceServer voiceServer)
    {
        if (!ShouldEmit(cacheableGuild: voiceServer.Guild))
            return Task.CompletedTask;

        var eventArgs = new VoiceServerUpdatedEvent
        {
            VoiceServer = voiceServer
        };
        VoiceServerUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    public Task OnWebhooksUpdated(
        SocketGuild guild,
        SocketChannel channel)
    {
        if (!ShouldEmit(guild: guild))
            return Task.CompletedTask;

        var eventArgs = new WebhooksUpdatedEvent
        {
            Guild = guild,
            Channel = channel
        };
        WebhooksUpdated.OnNext(value: eventArgs);

        return Task.CompletedTask;
    }

    private bool ShouldEmit(IChannel channel)
    {
        if (_configService.GuildConfig?.Id is null)
            return true;

        return channel is IGuildChannel guildChannel
               && ShouldEmit(guild: guildChannel.Guild);
    }

    private bool ShouldEmit(IGuild guild)
    {
        if (_configService.GuildConfig?.Id is null)
            return true;

        return guild.Id == _configService.GuildConfig.Id;
    }

    private bool ShouldEmit(Cacheable<IGuild, ulong> cacheableGuild)
    {
        if (!cacheableGuild.HasValue)
            return true;

        if (_configService.GuildConfig?.Id is null)
            return true;

        return cacheableGuild.Id == _configService.GuildConfig.Id;
    }
}