//
//  GameCenterBinding.m
//  GameCenterTest
//
//  Created by Mike on 9/3/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "GameCenterBinding.h"
#import "GameCenterManager.h"
#include <GameKit/GameKit.h>


char * GCMakeStringCopy( const char* string )
{ 
	if( string == NULL )
		return NULL;
	char *res = (char*)malloc( strlen( string ) + 1 );
	strcpy( res, string );
	return res;
}

#define GetStringParam( x ) [NSString stringWithUTF8String:(x)]



bool _gameCenterIsGameCenterAvailable()
{
	return [GameCenterManager isGameCenterAvailable];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Player methods

void _gameCenterAuthenticateLocalPlayer()
{
	[[GameCenterManager sharedManager] authenticateLocalPlayer];
}


bool _gameCenterIsPlayerAuthenticated()
{
	return [[GameCenterManager sharedManager] isPlayerAuthenticated];
}


const char * _gameCenterPlayerAlias()
{
	NSString *alias = [[GameCenterManager sharedManager] playerAlias];
	return GCMakeStringCopy( [alias UTF8String] );
}


bool _gameCenterIsUnderage()
{
	return [[GameCenterManager sharedManager] isUnderage];
}


void _gameCenterRetrieveFriends()
{
	[[GameCenterManager sharedManager] retrieveFriends];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Leaderboard methods

void _gameCenterLoadLeaderboardCategoryTitles()
{
	[[GameCenterManager sharedManager] loadLeaderboardCategoryTitles];
}


void _gameCenterReportScore( int score, const char * categoryId )
{
	[[GameCenterManager sharedManager] reportScore:score forCategory:GetStringParam( categoryId )];
}


void _gameCenterShowLeaderboardWithTimeScope( int timeScope )
{
	[[GameCenterManager sharedManager] showLeaderboardWithTimeScope:timeScope];
}


void _gameCenterShowLeaderboardWithTimeScopeAndCategoryId( int timeScope, const char * categoryId )
{
	[[GameCenterManager sharedManager] showLeaderboardWithTimeScope:timeScope category:GetStringParam( categoryId )];
}


void _gameCenterRetrieveScores( bool friendsOnly, int timeScope, int start, int end )
{
	[[GameCenterManager sharedManager] retrieveScores:friendsOnly timeScope:timeScope range:NSMakeRange( start, end )];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Achievements methods

void _gameCenterReportAchievement( const char * identifier, float percent )
{
	[[GameCenterManager sharedManager] reportAchievementIdentifier:GetStringParam( identifier ) percentComplete:percent];
}


void _gameCenterGetAchievements()
{
	[[GameCenterManager sharedManager] getAchievements];
}


void _gameCenterResetAchievements()
{
	[[GameCenterManager sharedManager] resetAchievements];
}


void _gameCenterShowAchievements()
{
	[[GameCenterManager sharedManager] showAchievements];
}


void _gameCenterRetrieveAchievementMetadata()
{
	[[GameCenterManager sharedManager] retrieveAchievementMetadata];
}
