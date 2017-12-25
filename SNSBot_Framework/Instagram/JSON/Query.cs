using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SNSBot.Instagram.JSON
{
	internal class Query
	{
		[JsonProperty("data")] internal JSON_Data Data;

		[JsonProperty("status")] internal String Status;

		internal class JSON_Data
		{
			[JsonProperty("user")] internal User User;
		}

		internal class User
		{
			[JsonProperty("edge_owner_to_timeline_media")] internal EdgeMedia EdgeOwnerToTimelineMedia;
		}

		internal class EdgeMedia
		{
			//List has Limitations to Int.MaxValue
			//Use Int. I think no one will upload it than Int.MaxValue.
			[JsonProperty("count")] internal Int32 Count;

			[JsonProperty("edges")] internal Collection<NodeWrap> Edges;

			[JsonProperty("page_info")] internal PageInfo PageInfo;
		}

		internal class NodeWrap
		{
			[JsonProperty("node")] internal Node Node;
		}

		internal class Node
		{
			[JsonProperty("id")] internal UInt64 Id;

			[JsonProperty("__typename")] internal String TypeName;

			[JsonProperty("edge_media_to_caption")] internal Captions EdgeMediaToCaption;

			[JsonProperty("shortcode")] internal String Shortcode;

			[JsonProperty("edge_media_to_comment")] internal Comments EdgeMediaToComment;

			[JsonProperty("comment_disabled")] internal Boolean CommentsDisabled;

			[JsonProperty("taken_at_timestamp")] internal UInt64 TakenAtTimestamp;

			[JsonProperty("dimensions")] internal Dimensions Dimensions;

			[JsonProperty("display_url")] internal String DisplayUrl;

			[JsonProperty("edge_media_preview_like")] internal Likes EdgeMediaPreviewLike;

			[JsonProperty("owner")] internal Owner Owner;

			[JsonProperty("thumbnail_src")] internal String ThumbnailSrc;

			[JsonProperty("thumbnail_resources")] internal Collection<ThumbnailResource> ThumbnailResources;

			[JsonProperty("is_video")] internal Boolean IsVideo;

			[JsonProperty("video_view_count")] internal UInt64 VideoViewCount;
		}

		internal class PageInfo
		{
			[JsonProperty("has_next_page")] internal Boolean HasNextPage;

			[JsonProperty("end_cursor")] internal String EndCursor;
		}

		internal class Captions
		{
			[JsonProperty("edges")] internal Collection<EdgeCaption> Edges;
		}

		internal class EdgeCaption
		{
			[JsonProperty("node")] internal NodeCaption Node;
		}

		internal class NodeCaption
		{
			[JsonProperty("text")] internal String Text;
		}

		internal class Comments
		{
			[JsonProperty("count")] internal UInt64 Count;
		}

		internal class Dimensions
		{
			[JsonProperty("height")] internal UInt32 Height;

			[JsonProperty("width")] internal UInt32 Width;
		}

		internal class Likes
		{
			[JsonProperty("count")] internal UInt64 Count;
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
	}
}
