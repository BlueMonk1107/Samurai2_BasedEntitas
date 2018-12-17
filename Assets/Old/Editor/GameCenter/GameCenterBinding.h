//
//  GameCenterBinding.h
//  GameCenterTest
//
//  Created by Mike on 9/3/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//



bool _gameCenterIsGameCenterAvailable();


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Player methods

void _gameCenterAuthenticateLocalPlayer();


bool _gameCenterIsPlayerAuthenticated();


const char * _gameCenterPlayerAlias();


bool _gameCenterIsUnderage();


void _gameCenterRetrieveFriends();



///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Leaderboard methods

void _gameCenterLoadLeaderboardCategoryTitles();


void _gameCenterReportScore( int score, const char * categoryId );


void _gameCenterShowLeaderboardWithTimeScope( int timeScope );


void _gameCenterShowLeaderboardWithTimeScopeAndCategoryId( int timeScope, const char * categoryId );


void _gameCenterRetrieveScores( bool friendsOnly, int timeScope, int start, int end );



///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Achievements methods

void _gameCenterReportAchievement( const char * identifier, float percent );


void _gameCenterGetAchievements();


void _gameCenterResetAchievements();


void _gameCenterShowAchievements();


void _gameCenterRetrieveAchievementMetadata();
