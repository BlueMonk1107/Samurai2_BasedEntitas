//
//  GameCenterMultiplayer.h
//  GameCenterTest
//
//  Created by Mike on 9/3/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GameKit/GameKit.h>


// Comment out this line to turn off logging
#define DEBUG = 1

#ifdef DEBUG
#define ALog(format, ...) NSLog(@"%@", [NSString stringWithFormat:format, ## __VA_ARGS__]);
#else
#define ALog(format, ...)
#endif


typedef enum {
    PacketTypeVoice			= 0,
    PacketTypeGameKitPacket	= 1
} PacketType;


@interface GameCenterMultiplayer : NSObject <GKMatchmakerViewControllerDelegate, GKMatchDelegate>
{
	GKMatch *_currentMatch;
	NSMutableArray *_connectedPeers;
	
	GKMatchSendDataMode _dataSendMode;
	NSMutableDictionary *_voiceChatChannels;
}
@property (nonatomic, retain) GKMatch *currentMatch;
@property (nonatomic, retain) NSMutableArray *connectedPeers;

@property (nonatomic, assign) GKMatchSendDataMode dataSendMode;
@property (nonatomic, retain) NSMutableDictionary *voiceChatChannels;


+ (GameCenterMultiplayer*)sharedManager;


- (void)showMatchmakerWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers;

- (NSString*)sendDataToAllPeers:(NSData*)data;

- (NSString*)sendData:(NSData*)data toPeers:(NSArray*)peerArray;

- (void)disconnectFromMatch;


// Voice Chat
- (BOOL)isVOIPAllowed;

- (void)enableVoiceChat:(BOOL)isEnabled;

- (void)closeAllOpenVoiceChats;

- (void)addAndStartVoiceChatChannel:(NSString*)channelName;

- (void)startVoiceChat:(BOOL)shouldStart withChannelName:(NSString*)channelName;

- (void)enableMicrophone:(BOOL)shouldEnable forChat:(NSString*)channelName;

- (void)setVolume:(float)volume forChat:(NSString*)channelName;

- (void)setMute:(BOOL)shouldMute playerId:(NSString*)playerId forChat:(NSString*)channelName;

- (void)receiveUpdates:(BOOL)shouldReceiveUpdates forChatWithChannelName:(NSString*)channelName;



// Manual matchmaking methods
- (void)findMatchProgrammaticallyWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers;

- (void)cancelProgrammaticMatchRequest;

- (void)findAllActivity;

@end
