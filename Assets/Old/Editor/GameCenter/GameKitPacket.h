//
//  GameKitPacket.h
//  GameKitTest
//
//  Created by Mike on 8/25/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface GameKitPacket : NSObject <NSCoding>
{
	NSString *_gameObject;
	NSString *_methodName;
	NSString *_parameter;
}
@property (nonatomic, retain) NSString *gameObject;
@property (nonatomic, retain) NSString *methodName;
@property (nonatomic, retain) NSString *parameter;


+ (GameKitPacket*)gameKitPacketFromData:(NSData*)data;

- (id)initWithGameObject:(NSString*)gameObject methodName:(NSString*)methodName parameter:(NSString*)parameter;

- (NSData*)archivedData;

@end
