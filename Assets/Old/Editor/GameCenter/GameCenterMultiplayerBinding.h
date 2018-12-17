//
//  GameCenterMultiplayerBinding.h
//  GameCenterTest
//
//  Created by Mike on 9/7/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//


void _gameCenterMultiplayerShowMatchmakerWithMinMaxPlayers( int minPlayers, int maxPlayers );


void _gameCenterMultiplayerSendDataReliably( bool reliably );


const char * _gameCenterMultiplayerSendMessageToAllPeers( const char * gameObject, const char * method, const char * param );


const char * _gameCenterMultiplayerSendMessageToPeers( const char * peerIds, const char * gameObject, const char * method, const char * param );


void _gameCenterMultiplayerDisconnectFromMatch();


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Voice Chat

bool _gameCenterMultiplayerIsVOIPAllowed();


void _gameCenterMultiplayerEnableVoiceChat( bool isEnabled );


void _gameCenterMultiplayerCloseAllOpenVoiceChats();


void _gameCenterMultiplayerAddAndStartVoiceChatChannel( const char * channelName );


void _gameCenterMultiplayerStartVoiceChat( const char * channelName, bool shouldStart );


void _gameCenterMultiplayerEnableMicrophone( const char * channelName, bool shouldEnable );


void _gameCenterMultiplayerSetVolume( const char * channelName, float volume );


void _gameCenterMultiplayerSetMute( const char * channelName, const char * playerId, bool shouldMute );


void _gameCenterMultiplayerReceiveUpdates( const char * channelName, bool shouldReceiveUpdates );



///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Manual matchmaking methods

void _gameCenterMultiplayerFindMatchProgrammaticallyWithMinMaxPlayers( int minPlayers, int maxPlayers );


void _gameCenterMultiplayerCancelProgrammaticMatchRequest();


void _gameCenterMultiplayerFindAllActivity();
