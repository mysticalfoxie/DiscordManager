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
    private readonly IDiscordClientService _discordClientService;

    public EventService(
        IConfigService configService,
        IDiscordClientService discordClientService)
    {
        _configService = configService;
        _discordClientService = discordClientService;
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
        _discordClientService.Client.ChannelCreated += OnChannelCreated;
        _discordClientService.Client.ChannelDestroyed += OnChannelDestroyed;
        _discordClientService.Client.MessageReceived += OnMessageReceived;
        _discordClientService.Client.RoleCreated += OnRoleCreated;
        _discordClientService.Client.RoleDeleted += OnRoleDeleted;
        _discordClientService.Client.JoinedGuild += OnJoinedGuild;
        _discordClientService.Client.LeftGuild += OnLeftGuild;
        _discordClientService.Client.GuildAvailable += OnGuildAvailable;
        _discordClientService.Client.GuildUnavailable += OnGuildUnavailable;
        _discordClientService.Client.GuildMembersDownloaded += OnGuildMembersDownloaded;
        _discordClientService.Client.GuildScheduledEventCancelled += OnGuildScheduledEventCancelled;
        _discordClientService.Client.GuildScheduledEventCompleted += OnGuildScheduledEventCompleted;
        _discordClientService.Client.GuildScheduledEventStarted += OnGuildScheduledEventStarted;
        _discordClientService.Client.IntegrationCreated += OnIntegrationCreated;
        _discordClientService.Client.IntegrationUpdated += OnIntegrationUpdated;
        _discordClientService.Client.UserJoined += OnUserJoined;
        _discordClientService.Client.VoiceServerUpdated += OnVoiceServerUpdated;
        _discordClientService.Client.RecipientAdded += OnRecipientAdded;
        _discordClientService.Client.RecipientRemoved += OnRecipientRemoved;
        _discordClientService.Client.InviteCreated += OnInviteCreated;
        _discordClientService.Client.InteractionCreated += OnInteractionCreated;
        _discordClientService.Client.ButtonExecuted += OnButtonExecuted;
        _discordClientService.Client.SelectMenuExecuted += OnSelectMenuExecuted;
        _discordClientService.Client.SlashCommandExecuted += OnSlashCommandExecuted;
        _discordClientService.Client.UserCommandExecuted += OnUserCommandExecuted;
        _discordClientService.Client.MessageCommandExecuted += OnMessageCommandExecuted;
        _discordClientService.Client.AutocompleteExecuted += OnAutocompleteExecuted;
        _discordClientService.Client.ModalSubmitted += OnModalSubmitted;
        _discordClientService.Client.ApplicationCommandCreated += OnApplicationCommandCreated;
        _discordClientService.Client.ApplicationCommandUpdated += OnApplicationCommandUpdated;
        _discordClientService.Client.ApplicationCommandDeleted += OnApplicationCommandDeleted;
        _discordClientService.Client.ThreadCreated += OnThreadCreated;
        _discordClientService.Client.ThreadMemberJoined += OnThreadMemberJoined;
        _discordClientService.Client.ThreadMemberLeft += OnThreadMemberLeft;
        _discordClientService.Client.StageStarted += OnStageStarted;
        _discordClientService.Client.StageEnded += OnStageEnded;
        _discordClientService.Client.GuildStickerCreated += OnGuildStickerCreated;
        _discordClientService.Client.GuildStickerDeleted += OnGuildStickerDeleted;
        _discordClientService.Client.RoleUpdated += OnRoleUpdated;
        _discordClientService.Client.GuildUpdated += OnGuildUpdated;
        _discordClientService.Client.UserLeft += OnUserLeft;
        _discordClientService.Client.UserBanned += OnUserBanned;
        _discordClientService.Client.UserUnbanned += OnUserUnbanned;
        _discordClientService.Client.InviteDeleted += OnInviteDeleted;
        _discordClientService.Client.UserUpdated += OnUserUpdated;
        _discordClientService.Client.StageUpdated += OnStageUpdated;
        _discordClientService.Client.RequestToSpeak += OnRequestToSpeak;
        _discordClientService.Client.SpeakerAdded += OnSpeakerAdded;
        _discordClientService.Client.SpeakerRemoved += OnSpeakerRemoved;
        _discordClientService.Client.GuildStickerUpdated += OnGuildStickerUpdated;
        _discordClientService.Client.ChannelUpdated += OnChannelUpdated;
        _discordClientService.Client.WebhooksUpdated += OnWebhooksUpdated;
        _discordClientService.Client.MessageDeleted += OnMessageDeleted;
        _discordClientService.Client.MessagesBulkDeleted += OnMessagesBulkDeleted;
        _discordClientService.Client.MessageUpdated += OnMessageUpdated;
        _discordClientService.Client.ReactionAdded += OnReactionAdded;
        _discordClientService.Client.ReactionRemoved += OnReactionRemoved;
        _discordClientService.Client.ReactionsRemovedForEmote += OnReactionsRemovedForEmote;
        _discordClientService.Client.ReactionsCleared += OnReactionsCleared;
        _discordClientService.Client.GuildJoinRequestDeleted += OnGuildJoinRequestDeleted;
        _discordClientService.Client.GuildScheduledEventCreated += OnGuildScheduledEventCreated;
        _discordClientService.Client.GuildScheduledEventUpdated += OnGuildScheduledEventUpdated;
        _discordClientService.Client.GuildScheduledEventUserAdd += OnGuildScheduledEventUserAdd;
        _discordClientService.Client.GuildScheduledEventUserRemove += OnGuildScheduledEventUserRemove;
        _discordClientService.Client.IntegrationDeleted += OnIntegrationDeleted;
        _discordClientService.Client.GuildMemberUpdated += OnGuildMemberUpdated;
        _discordClientService.Client.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
        _discordClientService.Client.CurrentUserUpdated += OnCurrentUserUpdated;
        _discordClientService.Client.UserIsTyping += OnUserIsTyping;
        _discordClientService.Client.PresenceUpdated += OnPresenceUpdated;
        _discordClientService.Client.ThreadUpdated += OnThreadUpdated;
        _discordClientService.Client.ThreadDeleted += OnThreadDeleted;
    }

    public Task OnApplicationCommandCreated(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandCreatedEvent
        {
            Command = command
        };
        ApplicationCommandCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnApplicationCommandDeleted(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandDeletedEvent
        {
            Command = command
        };
        ApplicationCommandDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnApplicationCommandUpdated(
        SocketApplicationCommand command)
    {
        if (!ShouldEmit(command.Guild))
            return Task.CompletedTask;

        var eventArgs = new ApplicationCommandUpdatedEvent
        {
            Command = command
        };
        ApplicationCommandUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnAutocompleteExecuted(
        SocketAutocompleteInteraction interaction)
    {
        if (!ShouldEmit(interaction.Channel))
            return Task.CompletedTask;

        var eventArgs = new AutocompleteExecutedEvent
        {
            Interaction = interaction
        };
        AutocompleteExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnButtonExecuted(
        SocketMessageComponent component)
    {
        if (!ShouldEmit(component.Channel))
            return Task.CompletedTask;

        var eventArgs = new ButtonExecutedEvent
        {
            Component = component
        };
        ButtonExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelCreated(
        SocketChannel channel)
    {
        if (!ShouldEmit(channel))
            return Task.CompletedTask;

        var eventArgs = new ChannelCreatedEvent
        {
            Channel = channel
        };
        ChannelCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelDestroyed(
        SocketChannel channel)
    {
        if (!ShouldEmit(channel))
            return Task.CompletedTask;

        var eventArgs = new ChannelDestroyedEvent
        {
            Channel = channel
        };
        ChannelDestroyed.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnChannelUpdated(
        SocketChannel oldChannel,
        SocketChannel newChannel)
    {
        if (!ShouldEmit(newChannel))
            return Task.CompletedTask;

        var eventArgs = new ChannelUpdatedEvent
        {
            OldChannel = oldChannel,
            NewChannel = newChannel
        };
        ChannelUpdated.OnNext(eventArgs);

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
        CurrentUserUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildAvailable(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new GuildAvailableEvent
        {
            Guild = guild
        };
        GuildAvailable.OnNext(eventArgs);

        return Task.CompletedTask;
    }


    public Task OnGuildJoinRequestDeleted(
        Cacheable<SocketGuildUser, ulong> guildUser,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new GuildJoinRequestDeletedEvent
        {
            GuildUser = guildUser,
            Guild = guild
        };
        GuildJoinRequestDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildMembersDownloaded(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new GuildMembersDownloadedEvent
        {
            Guild = guild
        };
        GuildMembersDownloaded.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildMemberUpdated(
        Cacheable<SocketGuildUser, ulong> oldGuildMember,
        SocketGuildUser newGuildMember)
    {
        if (!ShouldEmit(newGuildMember.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildMemberUpdatedEvent
        {
            OldGuildUser = oldGuildMember,
            NewGuildUser = newGuildMember
        };
        GuildMemberUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCancelled(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCancelledEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCancelled.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCompleted(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCompletedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCompleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventCreated(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventCreatedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventStarted(
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventStartedEvent
        {
            GuildEvent = guildEvent
        };
        GuildScheduledEventStarted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUpdated(
        Cacheable<SocketGuildEvent, ulong> oldGuildEvent,
        SocketGuildEvent newGuildEvent)
    {
        if (!ShouldEmit(newGuildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUpdatedEvent
        {
            OldGuildEvent = oldGuildEvent,
            NewGuildEvent = newGuildEvent
        };
        GuildScheduledEventUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUserAdd(
        Cacheable<SocketUser, RestUser, IUser, ulong> user,
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUserAddEvent
        {
            GuildEvent = guildEvent,
            User = user
        };
        GuildScheduledEventUserAdd.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildScheduledEventUserRemove(
        Cacheable<SocketUser, RestUser, IUser, ulong> user,
        SocketGuildEvent guildEvent)
    {
        if (!ShouldEmit(guildEvent.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildScheduledEventUserRemoveEvent
        {
            GuildEvent = guildEvent,
            User = user
        };
        GuildScheduledEventUserRemove.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerCreated(
        SocketCustomSticker sticker)
    {
        if (!ShouldEmit(sticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerCreatedEvent
        {
            Sticker = sticker
        };
        GuildStickerCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerDeleted(
        SocketCustomSticker sticker)
    {
        if (!ShouldEmit(sticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerDeletedEvent
        {
            Sticker = sticker
        };
        GuildStickerDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildStickerUpdated(
        SocketCustomSticker oldSticker,
        SocketCustomSticker newSticker)
    {
        if (!ShouldEmit(newSticker.Guild))
            return Task.CompletedTask;

        var eventArgs = new GuildStickerUpdatedEvent
        {
            OldSticker = oldSticker,
            NewSticker = newSticker
        };
        GuildStickerUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildUnavailable(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new GuildUnavailableEvent
        {
            Guild = guild
        };
        GuildUnavailable.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnGuildUpdated(
        SocketGuild oldGuild,
        SocketGuild newGuild)
    {
        if (!ShouldEmit(newGuild))
            return Task.CompletedTask;

        var eventArgs = new GuildUpdatedEvent
        {
            OldGuild = oldGuild,
            NewGuild = newGuild
        };
        GuildUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationCreated(IIntegration integration)
    {
        if (!ShouldEmit(integration.Guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationCreatedEvent
        {
            Integration = integration
        };
        IntegrationCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationDeleted(IGuild guild, ulong id, Optional<ulong> huh)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationDeletedEvent
        {
            Guild = guild,
            Id = id,
            Huh = huh
        };
        IntegrationDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnIntegrationUpdated(IIntegration integration)
    {
        if (!ShouldEmit(integration.Guild))
            return Task.CompletedTask;

        var eventArgs = new IntegrationUpdatedEvent
        {
            Integration = integration
        };
        IntegrationUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInteractionCreated(
        SocketInteraction interaction)
    {
        if (!ShouldEmit(interaction.Channel))
            return Task.CompletedTask;

        var eventArgs = new InteractionCreatedEvent
        {
            Interaction = interaction
        };
        InteractionCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInviteCreated(
        SocketInvite invite)
    {
        if (!ShouldEmit(invite.Guild))
            return Task.CompletedTask;

        var eventArgs = new InviteCreatedEvent
        {
            Invite = invite
        };
        InviteCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnInviteDeleted(
        SocketGuildChannel guildChannel,
        string invite)
    {
        if (!ShouldEmit(guildChannel))
            return Task.CompletedTask;

        var eventArgs = new InviteDeletedEvent
        {
            GuildChannel = guildChannel,
            Invite = invite
        };
        InviteDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnJoinedGuild(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new JoinedGuildEvent
        {
            Guild = guild
        };
        JoinedGuild.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnLeftGuild(
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new LeftGuildEvent
        {
            Guild = guild
        };
        LeftGuild.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageCommandExecuted(
        SocketMessageCommand command)
    {
        if (!ShouldEmit(command.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageCommandExecutedEvent
        {
            Command = command
        };
        MessageCommandExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }


    public Task OnMessageDeleted(
        Cacheable<IMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new MessageDeletedEvent
        {
            Message = message,
            Channel = channel
        };
        MessageDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageReceived(
        SocketMessage message)
    {
        if (!ShouldEmit(message.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageReceivedEvent
        {
            Message = message
        };
        MessageReceived.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessagesBulkDeleted(
        IReadOnlyCollection<Cacheable<IMessage, ulong>> messages,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new MessagesBulkDeletedEvent
        {
            Messages = messages,
            Channel = channel
        };
        MessagesBulkDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnMessageUpdated(
        Cacheable<IMessage, ulong> oldMessage,
        SocketMessage newMessage,
        ISocketMessageChannel channel)
    {
        if (!ShouldEmit(newMessage.Channel))
            return Task.CompletedTask;

        var eventArgs = new MessageUpdatedEvent
        {
            OldMessage = oldMessage,
            NewMessage = newMessage,
            Channel = channel
        };
        MessageUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnModalSubmitted(
        SocketModal modal)
    {
        if (!ShouldEmit(modal.Channel))
            return Task.CompletedTask;

        var eventArgs = new ModalSubmittedEvent
        {
            Modal = modal
        };
        ModalSubmitted.OnNext(eventArgs);

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
        PresenceUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionAdded(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionAddedEvent
        {
            Message = message,
            Channel = channel,
            Reaction = reaction
        };
        ReactionAdded.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionRemoved(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionRemovedEvent
        {
            Message = message,
            Channel = channel,
            Reaction = reaction
        };
        ReactionRemoved.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionsCleared(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionsClearedEvent
        {
            Message = message,
            Channel = channel
        };
        ReactionsCleared.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnReactionsRemovedForEmote(
        Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel,
        IEmote emote)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new ReactionsRemovedForEmoteEvent
        {
            Channel = channel,
            Message = message,
            Emote = emote
        };
        ReactionsRemovedForEmote.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRecipientAdded(
        SocketGroupUser groupUser)
    {
        var eventArgs = new RecipientAddedEvent
        {
            GroupUser = groupUser
        };
        RecipientAdded.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRecipientRemoved(
        SocketGroupUser groupUser)
    {
        var eventArgs = new RecipientRemovedEvent
        {
            GroupUser = groupUser
        };
        RecipientRemoved.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRequestToSpeak(
        SocketStageChannel stageChannel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(stageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new RequestToSpeakEvent
        {
            Channel = stageChannel,
            User = user
        };
        RequestToSpeak.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleCreated(
        SocketRole role)
    {
        if (!ShouldEmit(role.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleCreatedEvent
        {
            Role = role
        };
        RoleCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleDeleted(
        SocketRole role)
    {
        if (!ShouldEmit(role.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleDeletedEvent
        {
            Role = role
        };
        RoleDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnRoleUpdated(
        SocketRole oldRole,
        SocketRole newRole)
    {
        if (!ShouldEmit(newRole.Guild))
            return Task.CompletedTask;

        var eventArgs = new RoleUpdatedEvent
        {
            OldRole = oldRole,
            NewRole = newRole
        };
        RoleUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSelectMenuExecuted(
        SocketMessageComponent component)
    {
        if (!ShouldEmit(component.Channel))
            return Task.CompletedTask;

        var eventArgs = new SelectMenuExecutedEvent
        {
            Component = component
        };
        SelectMenuExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSlashCommandExecuted(
        SocketSlashCommand command)
    {
        if (!ShouldEmit(command.Channel))
            return Task.CompletedTask;

        var eventArgs = new SlashCommandExecutedEvent
        {
            Command = command
        };

        foreach (var option in eventArgs.Command.Data.Options)
            eventArgs.Arguments.Add(option.Name, option.Value);

        SlashCommandExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSpeakerAdded(
        SocketStageChannel stageChannel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(stageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new SpeakerAddedEvent
        {
            Channel = stageChannel,
            User = user
        };
        SpeakerAdded.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnSpeakerRemoved(
        SocketStageChannel channel,
        SocketGuildUser user)
    {
        if (!ShouldEmit(channel.Guild))
            return Task.CompletedTask;

        var eventArgs = new SpeakerRemovedEvent
        {
            Channel = channel,
            User = user
        };
        SpeakerRemoved.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageEnded(
        SocketStageChannel channel)
    {
        if (!ShouldEmit(channel))
            return Task.CompletedTask;

        var eventArgs = new StageEndedEvent
        {
            Channel = channel
        };
        StageEnded.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageStarted(
        SocketStageChannel channel)
    {
        if (!ShouldEmit(channel))
            return Task.CompletedTask;

        var eventArgs = new StageStartedEvent
        {
            Channel = channel
        };
        StageStarted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnStageUpdated(
        SocketStageChannel oldStageChannel,
        SocketStageChannel newStageChannel)
    {
        if (!ShouldEmit(newStageChannel.Guild))
            return Task.CompletedTask;

        var eventArgs = new StageUpdatedEvent
        {
            OldStageChannel = oldStageChannel,
            NewStageChannel = newStageChannel
        };
        StageUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadCreated(
        SocketThreadChannel channel)
    {
        if (!ShouldEmit(channel.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadCreatedEvent
        {
            Channel = channel
        };
        ThreadCreated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadDeleted(
        Cacheable<SocketThreadChannel, ulong> thread)
    {
        if (thread.HasValue && !ShouldEmit(thread.Value))
            return Task.CompletedTask;

        var eventArgs = new ThreadDeletedEvent
        {
            ThreadChannel = thread
        };
        ThreadDeleted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadMemberJoined(
        SocketThreadUser user)
    {
        if (!ShouldEmit(user.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadMemberJoinedEvent
        {
            User = user
        };
        ThreadMemberJoined.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadMemberLeft(
        SocketThreadUser user)
    {
        if (!ShouldEmit(user.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadMemberLeftEvent
        {
            User = user
        };
        ThreadMemberLeft.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnThreadUpdated(
        Cacheable<SocketThreadChannel, ulong> oldThread,
        SocketThreadChannel newThread)
    {
        if (!ShouldEmit(newThread.Guild))
            return Task.CompletedTask;

        var eventArgs = new ThreadUpdatedEvent
        {
            OldThreadChannel = oldThread,
            NewThreadChannel = newThread
        };
        ThreadUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserBanned(
        SocketUser user,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new UserBannedEvent
        {
            User = user,
            Guild = guild
        };
        UserBanned.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserCommandExecuted(
        SocketUserCommand command)
    {
        if (!ShouldEmit(command.Channel))
            return Task.CompletedTask;

        var eventArgs = new UserCommandExecutedEvent
        {
            Command = command
        };
        UserCommandExecuted.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserIsTyping(
        Cacheable<IUser, ulong> user,
        Cacheable<IMessageChannel, ulong> channel)
    {
        if (channel.HasValue && !ShouldEmit(channel.Value))
            return Task.CompletedTask;

        var eventArgs = new UserIsTypingEvent
        {
            User = user,
            Channel = channel
        };
        UserIsTyping.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserJoined(
        SocketGuildUser guildUser)
    {
        if (!ShouldEmit(guildUser.Guild))
            return Task.CompletedTask;

        var eventArgs = new UserJoinedEvent
        {
            GuildUser = guildUser
        };
        UserJoined.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserLeft(
        SocketGuild guild,
        SocketUser user)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new UserLeftEvent
        {
            Guild = guild,
            User = user
        };
        UserLeft.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnUserUnbanned(
        SocketUser user,
        SocketGuild guild)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new UserUnbannedEvent
        {
            User = user,
            Guild = guild
        };
        UserUnbanned.OnNext(eventArgs);

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
        UserUpdated.OnNext(eventArgs);

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
        UserVoiceStateUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnVoiceServerUpdated(
        SocketVoiceServer voiceServer)
    {
        if (!ShouldEmit(voiceServer.Guild))
            return Task.CompletedTask;

        var eventArgs = new VoiceServerUpdatedEvent
        {
            VoiceServer = voiceServer
        };
        VoiceServerUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    public Task OnWebhooksUpdated(
        SocketGuild guild,
        SocketChannel channel)
    {
        if (!ShouldEmit(guild))
            return Task.CompletedTask;

        var eventArgs = new WebhooksUpdatedEvent
        {
            Guild = guild,
            Channel = channel
        };
        WebhooksUpdated.OnNext(eventArgs);

        return Task.CompletedTask;
    }

    private bool ShouldEmit(IChannel channel)
    {
        if (_configService.GuildConfig?.Id is null)
            return true;

        return channel is IGuildChannel guildChannel
               && ShouldEmit(guildChannel.Guild);
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