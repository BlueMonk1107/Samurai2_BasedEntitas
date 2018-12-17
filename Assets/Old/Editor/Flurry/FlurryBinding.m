//
//  FlurryBinding.m
//  Unity-iPhone
//
//  Created by Marek Rabas on 9/19/10.
//  Copyright 2010 MADFINGER Games. All rights reserved.
//

#import "FlurryAPI.h"

#define GetStringParam( x ) [NSString stringWithUTF8String:(x)]

void uncaughtExceptionHandler(NSException *exception) {
	[FlurryAPI logError:@"Uncaught" message:@"Crash!" exception:exception];
} 

void _FlurryLogIn( const char * key)
{
	[FlurryAPI setAppCircleEnabled:NO];
	[FlurryAPI setSessionReportsOnPauseEnabled:YES];
	[FlurryAPI startSession:GetStringParam(key)];
}

void _FlurryLogEvent( const char * event)
{
	[FlurryAPI logEvent:GetStringParam(event)];
}

void _FlurryLogStartGame( const char* gameType, const char* difficulty)
{
	[FlurryAPI logEvent:@"Game Start" withParameters:
		[NSDictionary dictionaryWithObjectsAndKeys:
					GetStringParam(gameType), @"GameType",
					GetStringParam(difficulty), @"Difficulty",
		 nil]];
}

void _FlurryLogPerformedCombo( const char* comboName)
{
	[FlurryAPI logEvent:@"Combo Used" withParameters:
		[NSDictionary dictionaryWithObjectsAndKeys:
					GetStringParam(comboName), @"Name",
		 nil]];
}

void _FlurryLogDeath( const char* levelAndZone)
{
	[FlurryAPI logEvent:@"Player death" withParameters:
		[NSDictionary dictionaryWithObjectsAndKeys:
					GetStringParam(levelAndZone), @"Level+Zone",
		 nil]];
}

void _FlurryLogEndOfMission(  const char* event)
{
	[FlurryAPI logEvent:@"Mission finished" withParameters:
		[NSDictionary dictionaryWithObjectsAndKeys:
					GetStringParam(event), @"mission",
		 nil]];
}

void _FlurryLogUncaughtException()
{
	NSSetUncaughtExceptionHandler(&uncaughtExceptionHandler);
}

void _FlurryShowAppcircle()
{
	printf_console("calling %s", __FUNCTION__);
	[FlurryAPI openCatalog:@"Samurai II: Vengeance" canvasOrientation:@"portrait" canvasAnimated:YES];
}
