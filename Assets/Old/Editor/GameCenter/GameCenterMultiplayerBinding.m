//
//  GameCenterMultiplayerBinding.m
//  GameCenterTest
//
//  Created by Mike on 9/7/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "GameCenterMultiplayerBinding.h"
#import <GameKit/GameKit.h>
#import "GameCenterMultiplayer.h"
#import "GameKitPacket.h"


char * GCMMakeStringCopy( const char* string )
{ 
	if( string == NULL )
		return NULL;
	char *res = (char*)malloc( strlen( string ) + 1 );
	strcpy( res, string );
	return res;
}

#define GetStringParam( x ) [NSString stringWithUTF8String:(x)]


void _gameCenterMultiplayerShowMatchmakerWithMinMaxPlayers( int minPlayers, int maxPlayers )
{
	[[GameCenterMultiplayer sharedManager] showMatchmakerWithMinPlayers:minPlayers maxPlayers:maxPlayers];
}


void _gameCenterMultiplayerSendDataReliably( bool reliably )
{
	GKMatchSendDataMode mode = ( reliably ) ? GKMatchSendDataReliable : GKMatchSendDataUnreliable;
	[GameCenterMultiplayer sharedManager].dataSendMode = mode;
}


const char * _gameCenterMultiplayerSendMessageToAllPeers( const char * gameObject, const char * method, const char * param )
{
	// turn our data into a GameKitPacker
	GameKitPacket *packet = [[GameKitPacket alloc] initWithGameObject:GetStringParam( gameObject )
														   methodName:GetStringParam( method )
															parameter:GetStringParam( param )];
	NSData *data = [packet archivedData];
	[packet release];
	
	NSString *error = [[GameCenterMultiplayer sharedManager] sendDataToAllPeers:data];
	if( error )
		return GCMMakeStringCopy( [error UTF8String] );
	return GCMMakeStringCopy( "" );
}


const char * _gameCenterMultiplayerSendMessageToPeers( const char * peerIds, const char * gameObject, const char * method, const char * param )
{
	// turn our data into a GameKitPacker
	GameKitPacket *packet = [[GameKitPacket alloc] initWithGameObject:GetStringParam( gameObject )
														   methodName:GetStringParam( method )
															parameter:GetStringParam( param )];
	
	NSData *data = [packet archivedData];
	[packet release];
	
	NSString *peerIdString = [NSString stringWithUTF8String:peerIds];
	NSArray *peerIdArray = [peerIdString componentsSeparatedByString:@","];
	
	NSString *error = [[GameCenterMultiplayer sharedManager] sendData:data toPeers:peerIdArray];
	if( error )
		return GCMMakeStringCopy( [error UTF8String] );
	return GCMMakeStringCopy( "" );
}


void _gameCenterMultiplayerDisconnectFromMatch()
{
	[[GameCenterMultiplayer sharedManager] disconnectFromMatch];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Voice Chat

bool _gameCenterMultiplayerIsVOIPAllowed()
{
	return [[GameCenterMultiplayer sharedManager] isVOIPAllowed];
}


void _gameCenterMultiplayerEnableVoiceChat( bool isEnabled )
{
	[[GameCenterMultiplayer sharedManager] enableVoiceChat:isEnabled];
}


void _gameCenterMultiplayerCloseAllOpenVoiceChats()
{
	[[GameCenterMultiplayer sharedManager] closeAllOpenVoiceChats];
}


void _gameCenterMultiplayerAddAndStartVoiceChatChannel( const char * channelName )
{
	[[GameCenterMultiplayer sharedManager] addAndStartVoiceChatChannel:GetStringParam( channelName )];
}


void _gameCenterMultiplayerStartVoiceChat( const char * channelName, bool shouldStart )
{
	[[GameCenterMultiplayer sharedManager] startVoiceChat:shouldStart withChannelName:GetStringParam( channelName )];
}


void _gameCenterMultiplayerEnableMicrophone( const char * channelName, bool shouldEnable )
{
	[[GameCenterMultiplayer sharedManager] enableMicrophone:shouldEnable forChat:GetStringParam( channelName )];
}


void _gameCenterMultiplayerSetVolume( const char * channelName, float volume )
{
	[[GameCenterMultiplayer sharedManager] setVolume:volume forChat:GetStringParam( channelName )];
}


void _gameCenterMultiplayerSetMute( const char * channelName, const char * playerId, bool shouldMute )
{
	[[GameCenterMultiplayer sharedManager] setMute:shouldMute playerId:GetStringParam( playerId ) forChat:GetStringParam( channelName )];
}


void _gameCenterMultiplayerReceiveUpdates( const char * channelName, bool shouldReceiveUpdates )
{
	[[GameCenterMultiplayer sharedManager] receiveUpdates:shouldReceiveUpdates forChatWithChannelName:GetStringParam( channelName )];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Manual matchmaking methods

void _gameCenterMultiplayerFindMatchProgrammaticallyWithMinMaxPlayers( int minPlayers, int maxPlayers )
{
	[[GameCenterMultiplayer sharedManager] findMatchProgrammaticallyWithMinPlayers:minPlayers maxPlayers:maxPlayers];
}


void _gameCenterMultiplayerCancelProgrammaticMatchRequest()
{
	[[GameCenterMultiplayer sharedManager] cancelProgrammaticMatchRequest];
}


void _gameCenterMultiplayerFindAllActivity()
{
	[[GameCenterMultiplayer sharedManager] findAllActivity];
}
