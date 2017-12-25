using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SNSBot.Instagram.JSON
{
	internal class UserData
	{
		[JsonProperty("user")] internal JSONUser User;

		[JsonProperty("logging_page_id")] internal String LoggingPageId;

		internal class JSONUser
		{
			[JsonProperty("biography")] internal String Biography;

			[JsonProperty("blocked_by_viewer")] internal Boolean BlockedByViewer;

			[JsonProperty("country_block")] internal Boolean CountryBlock;

			[JsonProperty("external_url")] internal String ExternalUrl;

			[JsonProperty("external_url_linkshimmed")] internal String ExternalUrlLinkshimmed;

			[JsonProperty("followed_by")] internal Follower FollowedBy;

			[JsonProperty("follows")] internal Follows Follows;

			[JsonProperty("follows_viewer")] internal Boolean FollowsViewer;

			[JsonProperty("full_name")] internal String FullName;

			[JsonProperty("has_blocked_viewer")] internal Boolean HasBlockedViewer;

			[JsonProperty("has_requested_viewer")] internal Boolean HasRequestedViewer;

			[JsonProperty("id")]internal UInt64 Id;

			[JsonProperty("is_private")] internal Boolean IsPrivate;

			[JsonProperty("is_verified")] internal Boolean IsVerified;

			[JsonProperty("profile_pic_url")] internal String ProfilePicUrl;

			[JsonProperty("profile_pic_url_hd")] internal String ProfilePicUrlHd;

			[JsonProperty("requested_by_viewer")] internal Boolean RequestedByViewer;

			[JsonProperty("username")] internal String Username;

			[JsonProperty("connected_fb_page")] internal String ConnectedFbPage;

			[JsonProperty("media")] internal Media Media;

			[JsonProperty("saved_media")] internal Media SavedMedia;

			[JsonProperty("media_collections")] internal Media MediaCollections;
		}

		internal class Follower
		{
			[JsonProperty("count")] internal UInt64 Count;
		}

		internal class Follows
		{
			[JsonProperty("count")] internal UInt64 Count;
		}

		internal class Media
		{
			[JsonProperty("nodes")] internal Collection<Node> Nodes;

			[JsonProperty("count")] internal Int32 Count;

			[JsonProperty("page_info")] internal PageInfo PageInfo;
		}

		internal class Node
		{
			[JsonProperty("__typename")] internal String TypeName;

			[JsonProperty("id")] internal UInt64 Id;

			[JsonProperty("comments_disabled")] internal Boolean CommentsDisabled;

			[JsonProperty("dimensions")] internal Dimensions Dimensions;

			[JsonProperty("gating_info")] internal String GatingInfo;

			[JsonProperty("media_preview")] internal String MediaPreview;

			[JsonProperty("owner")] internal Owner Owner;

			[JsonProperty("thumbnail_src")] internal String ThumbnailSrc;

			[JsonProperty("thumbnail_resources")] internal Collection<ThumbnailResource> ThumbnailResources;

			[JsonProperty("is_video")] internal Boolean IsVideo;

			[JsonProperty("code")] internal String Code;

			[JsonProperty("date")] internal UInt64 Date;

			[JsonProperty("display_src")] internal String DisplaySrc;

			[JsonProperty("video_views")] internal UInt64 VideoViews;

			[JsonProperty("caption")] internal String Caption;

			[JsonProperty("comments")] internal Comments Comments;

			[JsonProperty("likes")] internal Likes Likes;
		}

		internal class Dimensions
		{
			[JsonProperty("height")] internal UInt32 Height;

			[JsonProperty("width")] internal UInt32 Width;
		}

		internal class Owner
		{
			[JsonProperty("id")] internal UInt64 Id;
		}

		internal class ThumbnailResource
		{
			[JsonProperty("src")] internal String Src;

			[JsonProperty("config_height")] internal UInt32 ConfigHeight;

			[JsonProperty("config_width")] internal UInt32 ConfigWidth;
		}

		internal class Comments
		{
			[JsonProperty("count")] internal UInt64 Count;
		}

		internal class Likes
		{
			[JsonProperty("count")] internal UInt64 Count;
		}

		internal class PageInfo
		{
			[JsonProperty("has_next_page")] internal Boolean HasNextPage;

			[JsonProperty("end_cursor")] internal String EndCursor;
		}
	}
}
