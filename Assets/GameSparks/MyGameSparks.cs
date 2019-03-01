#pragma warning disable 612,618
#pragma warning disable 0114
#pragma warning disable 0108

using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
		public class LogEventRequest_gsf_character_action : GSTypedRequest<LogEventRequest_gsf_character_action, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_character_action() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_character_action");
		}
		
		public LogEventRequest_gsf_character_action Set_action_name( string value )
		{
			request.AddString("action_name", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_character_action : GSTypedRequest<LogChallengeEventRequest_gsf_character_action, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_character_action() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_character_action");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_character_action SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_character_action Set_action_name( string value )
		{
			request.AddString("action_name", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_character_create : GSTypedRequest<LogEventRequest_gsf_character_create, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_character_create() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_character_create");
		}
		
		public LogEventRequest_gsf_character_create Set_body( string value )
		{
			request.AddString("body", value);
			return this;
		}
		
		public LogEventRequest_gsf_character_create Set_height( string value )
		{
			request.AddString("height", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_character_create : GSTypedRequest<LogChallengeEventRequest_gsf_character_create, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_character_create() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_character_create");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_character_create SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_character_create Set_body( string value )
		{
			request.AddString("body", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_character_create Set_height( string value )
		{
			request.AddString("height", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_character_delete : GSTypedRequest<LogEventRequest_gsf_character_delete, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_character_delete() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_character_delete");
		}
	}
	
	public class LogChallengeEventRequest_gsf_character_delete : GSTypedRequest<LogChallengeEventRequest_gsf_character_delete, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_character_delete() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_character_delete");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_character_delete SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_character_get : GSTypedRequest<LogEventRequest_gsf_character_get, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_character_get() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_character_get");
		}
	}
	
	public class LogChallengeEventRequest_gsf_character_get : GSTypedRequest<LogChallengeEventRequest_gsf_character_get, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_character_get() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_character_get");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_character_get SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_character_get_random : GSTypedRequest<LogEventRequest_gsf_character_get_random, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_character_get_random() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_character_get_random");
		}
	}
	
	public class LogChallengeEventRequest_gsf_character_get_random : GSTypedRequest<LogChallengeEventRequest_gsf_character_get_random, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_character_get_random() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_character_get_random");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_character_get_random SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_check : GSTypedRequest<LogEventRequest_gsf_clan_check, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_check() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_check");
		}
		
		public LogEventRequest_gsf_clan_check Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_check : GSTypedRequest<LogChallengeEventRequest_gsf_clan_check, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_check() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_check");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_check SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_check Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_create : GSTypedRequest<LogEventRequest_gsf_clan_create, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_create() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_create");
		}
		
		public LogEventRequest_gsf_clan_create Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
		
		public LogEventRequest_gsf_clan_create Set_clan_message( string value )
		{
			request.AddString("clan_message", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_create : GSTypedRequest<LogChallengeEventRequest_gsf_clan_create, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_create() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_create");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_create SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_create Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_create Set_clan_message( string value )
		{
			request.AddString("clan_message", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_drop : GSTypedRequest<LogEventRequest_gsf_clan_drop, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_drop() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_drop");
		}
		
		public LogEventRequest_gsf_clan_drop Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_drop : GSTypedRequest<LogChallengeEventRequest_gsf_clan_drop, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_drop() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_drop");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_drop SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_drop Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_get : GSTypedRequest<LogEventRequest_gsf_clan_get, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_get() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_get");
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_get : GSTypedRequest<LogChallengeEventRequest_gsf_clan_get, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_get() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_get");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_get SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_leave : GSTypedRequest<LogEventRequest_gsf_clan_leave, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_leave() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_leave");
		}
		
		public LogEventRequest_gsf_clan_leave Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_leave : GSTypedRequest<LogChallengeEventRequest_gsf_clan_leave, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_leave() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_leave");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_leave SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_leave Set_clan_name( string value )
		{
			request.AddString("clan_name", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_logs_add : GSTypedRequest<LogEventRequest_gsf_clan_logs_add, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_logs_add() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_logs_add");
		}
		
		public LogEventRequest_gsf_clan_logs_add Set_log( string value )
		{
			request.AddString("log", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_logs_add : GSTypedRequest<LogChallengeEventRequest_gsf_clan_logs_add, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_logs_add() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_logs_add");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_logs_add SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_logs_add Set_log( string value )
		{
			request.AddString("log", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_logs_get : GSTypedRequest<LogEventRequest_gsf_clan_logs_get, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_logs_get() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_logs_get");
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_logs_get : GSTypedRequest<LogChallengeEventRequest_gsf_clan_logs_get, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_logs_get() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_logs_get");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_logs_get SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_request_get : GSTypedRequest<LogEventRequest_gsf_clan_request_get, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_request_get() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_get");
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_request_get : GSTypedRequest<LogChallengeEventRequest_gsf_clan_request_get, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_request_get() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_get");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_request_get SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_request_join : GSTypedRequest<LogEventRequest_gsf_clan_request_join, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_request_join() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_join");
		}
		
		public LogEventRequest_gsf_clan_request_join Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_request_join : GSTypedRequest<LogChallengeEventRequest_gsf_clan_request_join, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_request_join() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_join");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_request_join SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_request_join Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_request_reply : GSTypedRequest<LogEventRequest_gsf_clan_request_reply, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_request_reply() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_reply");
		}
		
		public LogEventRequest_gsf_clan_request_reply Set_decision( string value )
		{
			request.AddString("decision", value);
			return this;
		}
		
		public LogEventRequest_gsf_clan_request_reply Set_player_id( string value )
		{
			request.AddString("player_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_request_reply : GSTypedRequest<LogChallengeEventRequest_gsf_clan_request_reply, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_request_reply() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_request_reply");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_request_reply SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_request_reply Set_decision( string value )
		{
			request.AddString("decision", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_request_reply Set_player_id( string value )
		{
			request.AddString("player_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_search : GSTypedRequest<LogEventRequest_gsf_clan_search, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_search() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_search");
		}
		
		public LogEventRequest_gsf_clan_search Set_search_field( string value )
		{
			request.AddString("search_field", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_search : GSTypedRequest<LogChallengeEventRequest_gsf_clan_search, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_search() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_search");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_search SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_search Set_search_field( string value )
		{
			request.AddString("search_field", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_search_random : GSTypedRequest<LogEventRequest_gsf_clan_search_random, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_search_random() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_search_random");
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_search_random : GSTypedRequest<LogChallengeEventRequest_gsf_clan_search_random, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_search_random() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_search_random");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_search_random SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clan_update_message : GSTypedRequest<LogEventRequest_gsf_clan_update_message, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clan_update_message() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clan_update_message");
		}
		
		public LogEventRequest_gsf_clan_update_message Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
		
		public LogEventRequest_gsf_clan_update_message Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_clan_update_message : GSTypedRequest<LogChallengeEventRequest_gsf_clan_update_message, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clan_update_message() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clan_update_message");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clan_update_message SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_update_message Set_clan_id( string value )
		{
			request.AddString("clan_id", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_clan_update_message Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_clear_all : GSTypedRequest<LogEventRequest_gsf_clear_all, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_clear_all() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_clear_all");
		}
	}
	
	public class LogChallengeEventRequest_gsf_clear_all : GSTypedRequest<LogChallengeEventRequest_gsf_clear_all, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_clear_all() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_clear_all");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_clear_all SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_email_delete : GSTypedRequest<LogEventRequest_gsf_email_delete, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_email_delete() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_email_delete");
		}
		
		public LogEventRequest_gsf_email_delete Set_mail_id( string value )
		{
			request.AddString("mail_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_email_delete : GSTypedRequest<LogChallengeEventRequest_gsf_email_delete, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_email_delete() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_email_delete");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_email_delete SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_delete Set_mail_id( string value )
		{
			request.AddString("mail_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_email_query : GSTypedRequest<LogEventRequest_gsf_email_query, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_email_query() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_email_query");
		}
		
		public LogEventRequest_gsf_email_query Set_receiver( string value )
		{
			request.AddString("receiver", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_email_query : GSTypedRequest<LogChallengeEventRequest_gsf_email_query, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_email_query() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_email_query");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_email_query SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_query Set_receiver( string value )
		{
			request.AddString("receiver", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_email_retrieve_attachment : GSTypedRequest<LogEventRequest_gsf_email_retrieve_attachment, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_email_retrieve_attachment() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_email_retrieve_attachment");
		}
		
		public LogEventRequest_gsf_email_retrieve_attachment Set_mail_id( string value )
		{
			request.AddString("mail_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_email_retrieve_attachment : GSTypedRequest<LogChallengeEventRequest_gsf_email_retrieve_attachment, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_email_retrieve_attachment() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_email_retrieve_attachment");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_email_retrieve_attachment SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_retrieve_attachment Set_mail_id( string value )
		{
			request.AddString("mail_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_email_send : GSTypedRequest<LogEventRequest_gsf_email_send, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_email_send() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_email_send");
		}
		
		public LogEventRequest_gsf_email_send Set_receiver( string value )
		{
			request.AddString("receiver", value);
			return this;
		}
		
		public LogEventRequest_gsf_email_send Set_title( string value )
		{
			request.AddString("title", value);
			return this;
		}
		
		public LogEventRequest_gsf_email_send Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
		
		public LogEventRequest_gsf_email_send Set_sender( string value )
		{
			request.AddString("sender", value);
			return this;
		}
		
		public LogEventRequest_gsf_email_send Set_datetime( string value )
		{
			request.AddString("datetime", value);
			return this;
		}
		public LogEventRequest_gsf_email_send Set_currency1( long value )
		{
			request.AddNumber("currency1", value);
			return this;
		}			
		public LogEventRequest_gsf_email_send Set_currency2( long value )
		{
			request.AddNumber("currency2", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_gsf_email_send : GSTypedRequest<LogChallengeEventRequest_gsf_email_send, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_email_send() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_email_send");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_email_send SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_receiver( string value )
		{
			request.AddString("receiver", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_title( string value )
		{
			request.AddString("title", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_sender( string value )
		{
			request.AddString("sender", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_datetime( string value )
		{
			request.AddString("datetime", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_email_send Set_currency1( long value )
		{
			request.AddNumber("currency1", value);
			return this;
		}			
		public LogChallengeEventRequest_gsf_email_send Set_currency2( long value )
		{
			request.AddNumber("currency2", value);
			return this;
		}			
	}
	
	public class LogEventRequest_gsf_leaderboard_xp_sum : GSTypedRequest<LogEventRequest_gsf_leaderboard_xp_sum, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_leaderboard_xp_sum() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_leaderboard_xp_sum");
		}
		public LogEventRequest_gsf_leaderboard_xp_sum Set_xp_amount( long value )
		{
			request.AddNumber("xp_amount", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_gsf_leaderboard_xp_sum : GSTypedRequest<LogChallengeEventRequest_gsf_leaderboard_xp_sum, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_leaderboard_xp_sum() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_leaderboard_xp_sum");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_leaderboard_xp_sum SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_leaderboard_xp_sum Set_xp_amount( long value )
		{
			request.AddNumber("xp_amount", value);
			return this;
		}			
	}
	
	public class LogEventRequest_gsf_team_create : GSTypedRequest<LogEventRequest_gsf_team_create, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_team_create() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_team_create");
		}
		
		public LogEventRequest_gsf_team_create Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_team_create : GSTypedRequest<LogChallengeEventRequest_gsf_team_create, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_team_create() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_team_create");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_team_create SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_team_create Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_team_drop : GSTypedRequest<LogEventRequest_gsf_team_drop, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_team_drop() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_team_drop");
		}
		
		public LogEventRequest_gsf_team_drop Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_team_drop : GSTypedRequest<LogChallengeEventRequest_gsf_team_drop, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_team_drop() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_team_drop");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_team_drop SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_team_drop Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_team_join : GSTypedRequest<LogEventRequest_gsf_team_join, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_team_join() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_team_join");
		}
		
		public LogEventRequest_gsf_team_join Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_team_join : GSTypedRequest<LogChallengeEventRequest_gsf_team_join, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_team_join() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_team_join");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_team_join SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_team_join Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_team_leave : GSTypedRequest<LogEventRequest_gsf_team_leave, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_team_leave() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_team_leave");
		}
		
		public LogEventRequest_gsf_team_leave Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
		
		public LogEventRequest_gsf_team_leave Set_team_type( string value )
		{
			request.AddString("team_type", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_team_leave : GSTypedRequest<LogChallengeEventRequest_gsf_team_leave, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_team_leave() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_team_leave");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_team_leave SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_team_leave Set_team_id( string value )
		{
			request.AddString("team_id", value);
			return this;
		}
		public LogChallengeEventRequest_gsf_team_leave Set_team_type( string value )
		{
			request.AddString("team_type", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_version_check : GSTypedRequest<LogEventRequest_gsf_version_check, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_version_check() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_version_check");
		}
		
		public LogEventRequest_gsf_version_check Set_version( string value )
		{
			request.AddString("version", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_gsf_version_check : GSTypedRequest<LogChallengeEventRequest_gsf_version_check, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_version_check() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_version_check");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_version_check SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_gsf_version_check Set_version( string value )
		{
			request.AddString("version", value);
			return this;
		}
	}
	
	public class LogEventRequest_gsf_team_list_join_request : GSTypedRequest<LogEventRequest_gsf_team_list_join_request, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_gsf_team_list_join_request() : base("LogEventRequest"){
			request.AddString("eventKey", "gsf_team_list_join_request");
		}
	}
	
	public class LogChallengeEventRequest_gsf_team_list_join_request : GSTypedRequest<LogChallengeEventRequest_gsf_team_list_join_request, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_gsf_team_list_join_request() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "gsf_team_list_join_request");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_gsf_team_list_join_request SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_test : GSTypedRequest<LogEventRequest_test, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_test() : base("LogEventRequest"){
			request.AddString("eventKey", "test");
		}
		
		public LogEventRequest_test Set_clanid( string value )
		{
			request.AddString("clanid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_test : GSTypedRequest<LogChallengeEventRequest_test, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_test() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "test");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_test SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_test Set_clanid( string value )
		{
			request.AddString("clanid", value);
			return this;
		}
	}
	
}
	
	
	
namespace GameSparks.Api.Requests{
	
	public class LeaderboardDataRequest_leaderboard_xp : GSTypedRequest<LeaderboardDataRequest_leaderboard_xp,LeaderboardDataResponse_leaderboard_xp>
	{
		public LeaderboardDataRequest_leaderboard_xp() : base("LeaderboardDataRequest"){
			request.AddString("leaderboardShortCode", "leaderboard_xp");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LeaderboardDataResponse_leaderboard_xp (response);
		}		
		
		/// <summary>
		/// The challenge instance to get the leaderboard data for
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// The offset into the set of leaderboards returned
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetOffset( long offset )
		{
			request.AddNumber("offset", offset);
			return this;
		}
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public LeaderboardDataRequest_leaderboard_xp SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
		
	}

	public class AroundMeLeaderboardRequest_leaderboard_xp : GSTypedRequest<AroundMeLeaderboardRequest_leaderboard_xp,AroundMeLeaderboardResponse_leaderboard_xp>
	{
		public AroundMeLeaderboardRequest_leaderboard_xp() : base("AroundMeLeaderboardRequest"){
			request.AddString("leaderboardShortCode", "leaderboard_xp");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new AroundMeLeaderboardResponse_leaderboard_xp (response);
		}		
		
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_leaderboard_xp SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
	}
}

namespace GameSparks.Api.Responses{
	
	public class _LeaderboardEntry_leaderboard_xp : LeaderboardDataResponse._LeaderboardData{
		public _LeaderboardEntry_leaderboard_xp(GSData data) : base(data){}
		public long? xp_amount{
			get{return response.GetNumber("xp_amount");}
		}
	}
	
	public class LeaderboardDataResponse_leaderboard_xp : LeaderboardDataResponse
	{
		public LeaderboardDataResponse_leaderboard_xp(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> Data_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> First_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> Last_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
	}
	
	public class AroundMeLeaderboardResponse_leaderboard_xp : AroundMeLeaderboardResponse
	{
		public AroundMeLeaderboardResponse_leaderboard_xp(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> Data_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> First_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_leaderboard_xp> Last_leaderboard_xp{
			get{return new GSEnumerable<_LeaderboardEntry_leaderboard_xp>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_leaderboard_xp(data);});}
		}
	}
}	

namespace GameSparks.Api.Messages {

		public class ScriptMessage_new_email : ScriptMessage {
		
			public new static Action<ScriptMessage_new_email> Listener;
	
			public ScriptMessage_new_email(GSData data) : base(data){}
	
			private static ScriptMessage_new_email Create(GSData data)
			{
				ScriptMessage_new_email message = new ScriptMessage_new_email (data);
				return message;
			}
	
			static ScriptMessage_new_email()
			{
				handlers.Add (".ScriptMessage_new_email", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}

}
