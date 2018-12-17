//
//  GameKitPacket.m
//  GameKitTest
//
//  Created by Mike on 8/25/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "GameKitPacket.h"


@implementation GameKitPacket

@synthesize gameObject = _gameObject, methodName = _methodName, parameter = _parameter;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Class methods

+ (GameKitPacket*)gameKitPacketFromData:(NSData*)data
{
	return (GameKitPacket*)[NSKeyedUnarchiver unarchiveObjectWithData:data];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

- (id)initWithGameObject:(NSString*)gameObject methodName:(NSString*)methodName parameter:(NSString*)parameter
{
	if( ( self = [super init] ) )
	{
		self.gameObject = gameObject;
		self.methodName = methodName;
		self.parameter = parameter;
	}
	return self;
}


- (void)dealloc
{
	[_gameObject release];
	[_methodName release];
	[_parameter release];
	
	[super dealloc];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (NSData*)archivedData
{
	return [NSKeyedArchiver archivedDataWithRootObject:self];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSCoding

- (void)encodeWithCoder:(NSCoder*)aCoder
{
	[aCoder encodeObject:_gameObject forKey:@"gameObject"];
	[aCoder encodeObject:_methodName forKey:@"methodName"];
	[aCoder encodeObject:_parameter forKey:@"parameter"];
}


- (id)initWithCoder:(NSCoder*)aDecoder
{
	if( ( self = [self init] ) )
	{
		self.gameObject = [aDecoder decodeObjectForKey:@"gameObject"];
		self.methodName = [aDecoder decodeObjectForKey:@"methodName"];
		self.parameter = [aDecoder decodeObjectForKey:@"parameter"];
	}
	return self;
}


@end
