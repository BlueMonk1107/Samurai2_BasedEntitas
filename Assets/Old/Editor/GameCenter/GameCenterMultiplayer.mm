//
//  GameCenterMultiplayer.m
//  GameCenterTest
//
//  Created by Mike on 9/3/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "GameCenterMultiplayer.h"
#import "GameCenterManager.h"
#import "GameKitPacket.h"
#import <AVFoundation/AVFoundation.h>


void UnitySendMessage( const char * className, const char * methodName, const char * param );



@implementation GameCenterMultiplayer

@synthesize currentMatch = _currentMatch, connectedPeers = _connectedPeers, dataSendMode = _dataSendMode,
			voiceChatChannels = _voiceChatChannels;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Class Methods

+ (GameCenterMultiplayer*)sharedManager
{
	static GameCenterMultiplayer *sharedSingleton;

	if( !sharedSingleton )
		sharedSingleton = [[GameCenterMultiplayer alloc] init];
	
	return sharedSingleton;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

- (id)init
{
	// If GameCenter isnt available, early out with nil to avoid a crash
	if( ![GameCenterManager isGameCenterAvailable] )
		return nil;
	
	if( ( self = [super init] ) )
	{
		// create our peers array
		_connectedPeers = [[NSMutableArray alloc] init];
		
		// default to unreliable sends
		_dataSendMode = GKMatchSendDataUnreliable;
		
		// We need this up ASAP after launch for invite handling
		[GKMatchmaker sharedMatchmaker].inviteHandler = ^( GKInvite *acceptedInvite, NSArray *playersToInvite )
		{
			ALog( @"received invite from: %@, with players: %@", acceptedInvite.inviter, playersToInvite );

			// Insert application-specific code here to clean up any games in progress.
			if( acceptedInvite )
			{
				GKMatchmakerViewController *mmvc = [[[GKMatchmakerViewController alloc] initWithInvite:acceptedInvite] autorelease];
				mmvc.matchmakerDelegate = self;
				[[GameCenterManager sharedManager] showViewControllerModallyInWrapper:mmvc];
			}
			else if( playersToInvite )
			{
				GKMatchRequest *request = [[[GKMatchRequest alloc] init] autorelease];
				request.minPlayers = 2;
				request.maxPlayers = 4;
				request.playersToInvite = playersToInvite;
				
				GKMatchmakerViewController *mmvc = [[[GKMatchmakerViewController alloc] initWithMatchRequest:request] autorelease];
				mmvc.matchmakerDelegate = self;
				[[GameCenterManager sharedManager] showViewControllerModallyInWrapper:mmvc];
			}
		};
	}
	
	return self;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Private

// Wrapper for sending packets so we can prefix them to know if they are voice or not
- (NSData*)prefixPacketData:data ofType:(PacketType)packetType
{
    NSMutableData *newPacket = [NSMutableData dataWithCapacity:( [data length] + sizeof(uint32_t) )];
	
    // Both game and voice data is prefixed with the PacketType so the peer knows where to send it.
    uint32_t swappedType = CFSwapInt32HostToBig((uint32_t)packetType);
    [newPacket appendBytes:&swappedType length:sizeof(uint32_t)];
    [newPacket appendData:data];

	return newPacket;
}


// Wrapper to set the audio category
- (void)setAudioCategory:(NSString*)category
{
	AVAudioSession *audioSession = [AVAudioSession sharedInstance];
	
	NSError *error = NULL;
	[audioSession setCategory:category error:&error];
	
	if( !error )
		[audioSession setActive:YES error:&error];
	
	if( error )
		ALog( @"############# GameKit: error setting up audio session: %@", [error localizedDescription] );
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (void)showMatchmakerWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers
{
    GKMatchRequest *request = [[[GKMatchRequest alloc] init] autorelease];
    request.minPlayers = minPlayers;
    request.maxPlayers = maxPlayers;
	
    GKMatchmakerViewController *mmvc = [[[GKMatchmakerViewController alloc] initWithMatchRequest:request] autorelease];
    mmvc.matchmakerDelegate = self;
    [[GameCenterManager sharedManager] showViewControllerModallyInWrapper:mmvc];
}


// returns either nil or an error string
- (NSString*)sendDataToAllPeers:(NSData*)data
{
	// Prefix the packet so we can identifiy it later
    NSData *newPacket = [self prefixPacketData:data ofType:PacketTypeGameKitPacket];
	NSError *error = NULL;
	
	// TODO: remove this
	if( ![_currentMatch sendDataToAllPlayers:newPacket withDataMode:_dataSendMode error:&error] )
		return [error localizedDescription];
	return nil;
}


// returns either nil or an error string
- (NSString*)sendData:(NSData*)data toPeers:(NSArray*)peerArray
{
	// Prefix the packet so we can identifiy it later
    NSData *newPacket = [self prefixPacketData:data ofType:PacketTypeGameKitPacket];
	NSError *error = NULL;
	
	// TODO: remove this
	if( ![_currentMatch sendData:newPacket toPlayers:peerArray withDataMode:_dataSendMode error:&error] )
		return [error localizedDescription];
	return nil;
}


- (void)disconnectFromMatch
{
	for( NSString *playerId in _currentMatch.playerIDs )
	{
		[_connectedPeers removeObject:playerId];
		UnitySendMessage( "GameCenterMultiplayerManager", "playerDidDisconnectFromMatch", [playerId UTF8String] );
	}
	
	[self closeAllOpenVoiceChats];
	[_currentMatch disconnect];
	self.currentMatch = nil;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Voice Chat

- (BOOL)isVOIPAllowed
{
	return [GKVoiceChat isVoIPAllowed];
}


- (void)enableVoiceChat:(BOOL)isEnabled
{
	if( isEnabled )
	{
		[self setAudioCategory:AVAudioSessionCategoryPlayAndRecord];
		
		// if we have any open chats, cancel them now
		[self closeAllOpenVoiceChats];
		
		// create a dictionary to hold the chats
		_voiceChatChannels = [[NSMutableDictionary alloc] initWithCapacity:1];
	}
	else
	{
		[self setAudioCategory:AVAudioSessionCategorySoloAmbient];
		
		// if we have any open chats, cancel them now then kill the dictionary
		[self closeAllOpenVoiceChats];
		self.voiceChatChannels = nil;
	}
}


- (void)closeAllOpenVoiceChats
{
	NSArray *allChatChannels = [_voiceChatChannels allKeys];
	
	for( NSString *channel in allChatChannels )
	{
		GKVoiceChat *chat = [_voiceChatChannels objectForKey:channel];
		[chat stop];
	}
}


- (void)addAndStartVoiceChatChannel:(NSString*)channelName
{
	GKVoiceChat *chat = [_currentMatch voiceChatWithName:channelName];
	if( !chat )
		return;
	
	[_voiceChatChannels setObject:chat forKey:channelName];
	[chat start];
	chat.active = YES;
}


- (void)startVoiceChat:(BOOL)shouldStart withChannelName:(NSString*)channelName
{
	GKVoiceChat *chat = [_voiceChatChannels objectForKey:channelName];
	
	( shouldStart ) ? [chat start] : [chat stop];
}


- (void)enableMicrophone:(BOOL)shouldEnable forChat:(NSString*)channelName
{
	GKVoiceChat *chat = [_voiceChatChannels objectForKey:channelName];
	chat.active = shouldEnable;
}


- (void)setVolume:(float)volume forChat:(NSString*)channelName
{
	GKVoiceChat *chat = [_voiceChatChannels objectForKey:channelName];
	chat.volume = volume;
}


- (void)setMute:(BOOL)shouldMute playerId:(NSString*)playerId forChat:(NSString*)channelName
{
	GKVoiceChat *chat = [_voiceChatChannels objectForKey:channelName];
	[chat setMute:shouldMute forPlayer:playerId];
}


- (void)receiveUpdates:(BOOL)shouldReceiveUpdates forChatWithChannelName:(NSString*)channelName
{
	GKVoiceChat *chat = [_voiceChatChannels objectForKey:channelName];
	
	// stop receiving updates. kill the block
	if( !shouldReceiveUpdates )
	{
		chat.playerStateUpdateHandler = nil;
		return;
	}
	
	// voice chat player state update handler
	chat.playerStateUpdateHandler = ^( NSString *playerId, GKVoiceChatPlayerState state )
	{
		switch( state )
		{
			case GKVoiceChatPlayerSpeaking:
			{
				UnitySendMessage( "GameCenterMultiplayerManager", "playerIsSpeaking", [playerId UTF8String] );
				break;
			}
			case GKVoiceChatPlayerSilent:
			{
				UnitySendMessage( "GameCenterMultiplayerManager", "playerIsSilent", [playerId UTF8String] );
				break;
			}
		}
	};
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Manual Matchmaking with no UI

// find a match for the player without displaying the standard user interface
- (void)findMatchProgrammaticallyWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers
{
    GKMatchRequest *request = [[[GKMatchRequest alloc] init] autorelease];
    request.minPlayers = minPlayers;
    request.maxPlayers = maxPlayers;
	
    [[GKMatchmaker sharedMatchmaker] findMatchForRequest:request withCompletionHandler:^( GKMatch *match, NSError *error )
	 {
		 if( error )
		 {
			 UnitySendMessage( "GameCenterMultiplayerManager", "findMatchProgramaticallyFailed", [[error localizedDescription] UTF8String] );
		 }
		 else if( match )
		 {
			 // Use a retaining property to retain the match.
			 self.currentMatch = match;
			 match.delegate = self;

			 NSString *expectedPlayerCount = [NSString stringWithFormat:@"%i", match.expectedPlayerCount];
			 UnitySendMessage( "GameCenterMultiplayerManager", "findMatchProgramaticallyFinishedWithExpectedPlayerCount", [expectedPlayerCount UTF8String] );
		 }
	 }];
}


- (void)cancelProgrammaticMatchRequest
{
	[[GKMatchmaker sharedMatchmaker] cancel];
}


- (void)addPlayersToCurrentMatch:(NSArray*)players
{
	// not so sure about this
    GKMatchRequest *request = [[[GKMatchRequest alloc] init] autorelease];
    request.minPlayers = 2;
    request.maxPlayers = 4;
	
	[[GKMatchmaker sharedMatchmaker] addPlayersToMatch:_currentMatch matchRequest:request completionHandler:^( NSError *error )
	 {
		 if( error )
			 UnitySendMessage( "GameCenterMultiplayerManager", "addPlayersToCurrentMatchFailed", [[error localizedDescription] UTF8String] );
	 }];
}


// gets the total number of players online
- (void)findAllActivity
{
    [[GKMatchmaker sharedMatchmaker] queryActivityWithCompletionHandler:^( NSInteger activity, NSError *error )
	 {
		 if( error )
			 UnitySendMessage( "GameCenterMultiplayerManager", "findAllActivityFailed", [[error localizedDescription] UTF8String] );
		 else
			 UnitySendMessage( "GameCenterMultiplayerManager", "findAllActivityFinished", [[NSString stringWithFormat:@"%i", activity] UTF8String] );
	 }];
}


- (void)findAllActivityForPlayerGroup:(int)playerGroup
{
	[[GKMatchmaker sharedMatchmaker] queryPlayerGroupActivity:playerGroup withCompletionHandler:^( NSInteger activity, NSError *error )
	 {
		 if( error )
			 UnitySendMessage( "GameCenterMultiplayerManager", "findAllActivityForPlayerGroupFailed", [[error localizedDescription] UTF8String] );
		 else
			 UnitySendMessage( "GameCenterMultiplayerManager", "findAllActivityForPlayerGroupFinished", [[NSString stringWithFormat:@"%i", activity] UTF8String] );
	 }];
}


// For later when multiplayer is added
- (void)receiveMatchBestScores:(GKMatch*)match
{
	GKLeaderboard *query = [[GKLeaderboard alloc] initWithPlayerIDs:match.playerIDs];
	
	if( !query )
		return;
	
	[query loadScoresWithCompletionHandler: ^( NSArray *scores, NSError *error )
	 {
		 if( error )
		 {
			 UnitySendMessage( "GameCenterMultiplayerManager", "retrieveMatchesBestScoresDidFail", [[error localizedDescription] UTF8String] );
		 }
		 
		 if( scores )
		 {
			 // process the score information.
		 }
	 }];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark GKMatchmakerViewControllerDelegate

- (void)matchmakerViewControllerWasCancelled:(GKMatchmakerViewController*)viewController
{
	[[GameCenterManager sharedManager] dismissWrappedController];
	
	UnitySendMessage( "GameCenterMultiplayerManager", "matchmakerWasCancelled", "" );
}


- (void)matchmakerViewController:(GKMatchmakerViewController*)viewController didFailWithError:(NSError*)error
{
	[[GameCenterManager sharedManager] dismissWrappedController];
    // Display the error to the user.
}


- (void)matchmakerViewController:(GKMatchmakerViewController*)viewController didFindMatch:(GKMatch*)match
{
	[[GameCenterManager sharedManager] dismissWrappedController];
	
	// Use a retaining property to retain the match.
    self.currentMatch = match;
	match.delegate = self;
	
	NSString *expectedPlayerCount = [NSString stringWithFormat:@"%i", match.expectedPlayerCount];
	UnitySendMessage( "GameCenterMultiplayerManager", "matchmakerFoundMatchWithExpectedPlayerCount", [expectedPlayerCount UTF8String] );
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Using Matches
#pragma mark GKMatchDelegate

- (void)match:(GKMatch*)match player:(NSString*)playerId didChangeState:(GKPlayerConnectionState)state
{
    switch( state )
    {
        case GKPlayerStateConnected:
        {
			ALog( @"############# didChangeState: Connected - playerId: %@", playerId );
			
			[_connectedPeers addObject:playerId];
			UnitySendMessage( "GameCenterMultiplayerManager", "playerDidConnectToMatch", [playerId UTF8String] );
			break;
		}
        case GKPlayerStateDisconnected:
		{
			ALog( @"############# didChangeState: Disonnected - playerId: %@", playerId );
			
			[_connectedPeers removeObject:playerId];
			UnitySendMessage( "GameCenterMultiplayerManager", "playerDidDisconnectFromMatch", [playerId UTF8String] );
			break;
		}
    }
}


- (void)match:(GKMatch*)match connectionWithPlayerFailed:(NSString*)playerID withError:(NSError*)error
{
	UnitySendMessage( "GameCenterMultiplayerManager", "findMatchProgramaticallyFailed", [[error localizedDescription] UTF8String] );
}


- (void)match:(GKMatch*)match didFailWithError:(NSError*)error
{
	UnitySendMessage( "GameCenterMultiplayerManager", "findMatchProgramaticallyFailed", [[error localizedDescription] UTF8String] );
}


- (void)match:(GKMatch*)match didReceiveData:(NSData*)data fromPlayer:(NSString*)playerID
{
    PacketType header;
    uint32_t swappedHeader;
    
	if( data.length >= sizeof(uint32_t) )
	{    
        [data getBytes:&swappedHeader length:sizeof(uint32_t)];
        header = (PacketType)CFSwapInt32BigToHost(swappedHeader);
        NSRange payloadRange = { sizeof(uint32_t), [data length] - sizeof(uint32_t) };
        NSData *payload = [data subdataWithRange:payloadRange];
        
        // Check the header to see if this is a voice or a game packet
        if( header == PacketTypeVoice )
		{
			//[[GKVoiceChatService defaultVoiceChatService] receivedData:payload fromParticipantID:peerId];
		}
        else if( header == PacketTypeGameKitPacket )
		{
			// Read the bytes in and pass it off to Unity
			GameKitPacket *packet = [GameKitPacket gameKitPacketFromData:payload];
			
			UnitySendMessage( [packet.gameObject UTF8String], [packet.methodName UTF8String], [packet.parameter UTF8String] );
		}
    }
}



@end
