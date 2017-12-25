using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNSBot.Instagram.JSON;

namespace SNSBot.Instagram
{
	public class InstagramPost
	{
		#region DATA

		public readonly String Type;
		public readonly UInt64 Id;
		public readonly Boolean IsCommentDisabled;
		public readonly Dimensions Dimensions;
		public readonly String GatingInfo;
		public readonly String MediaPreview;
		public readonly UInt64 Owner;
		public readonly String ThumbnailSrc;
		public readonly List<Thumbnail> ThumbnailResources;
		public readonly Boolean IsVideo;
		public readonly String Code;
		public readonly DateTime Date;
		public readonly String DisplaySrc;
		public readonly UInt64 VideoViews;
		public readonly String Caption;
		public readonly UInt64 CommentCount;
		public readonly UInt64 LikeCount;

		#endregion

		internal InstagramPost(JSON.Query.Node node)
		{
			Type = node.TypeName;
			Id = node.Id;
			IsCommentDisabled = node.CommentsDisabled;
			Dimensions = new Dimensions(node.Dimensions);
			GatingInfo = null;
			MediaPreview = null;
			Owner = node.Owner.Id;
			ThumbnailSrc = node.ThumbnailSrc;
			ThumbnailResources = new List<Thumbnail>(node.ThumbnailResources.Count);
			foreach (Query.ThumbnailResource resource in node.ThumbnailResources)
				ThumbnailResources.Add(new Thumbnail(resource));
			IsVideo = node.IsVideo;
			Code = node.Shortcode;
			Date = new DateTime((long)node.TakenAtTimestamp);
			DisplaySrc = node.DisplayUrl;
			VideoViews = node.VideoViewCount;
			Caption = Uri.UnescapeDataString(node.EdgeMediaToCaption.Edges[0].Node.Text);
			CommentCount = node.EdgeMediaToComment.Count;
			LikeCount = node.EdgeMediaPreviewLike.Count;
		}

		internal InstagramPost(JSON.UserData.Node node)
		{
			Type = node.TypeName;
			Id = node.Id;
			IsCommentDisabled = node.CommentsDisabled;
			Dimensions = new Dimensions(node.Dimensions);
			GatingInfo = node.GatingInfo;
			MediaPreview = node.MediaPreview;
			Owner = node.Owner.Id;
			ThumbnailSrc = node.ThumbnailSrc;
			ThumbnailResources = new List<Thumbnail>(node.ThumbnailResources.Count);
			foreach (UserData.ThumbnailResource resource in node.ThumbnailResources)
				ThumbnailResources.Add(new Thumbnail(resource));
			IsVideo = node.IsVideo;
			Code = node.Code;
			Date = new DateTime((long)node.Date);
			DisplaySrc = node.DisplaySrc;
			VideoViews = node.VideoViews;
			Caption = node.Caption;
			CommentCount = node.Comments.Count;
			LikeCount = node.Likes.Count;
		}

		public InstagramPost(UInt64 articleId)
		{
			throw new NotImplementedException("Not Yet Implemented.");
		}
	}

	public class Thumbnail
	{
		public readonly String Src;
		public readonly UInt64 Width;
		public readonly UInt64 Height;

		public Thumbnail(String src, UInt64 width, UInt64 height)
		{
			Src = src;
			Width = width;
			Height = height;
		}

		internal Thumbnail(JSON.Query.ThumbnailResource thumbnail)
		{
			Src = thumbnail.Src;
			Width = thumbnail.ConfigWidth;
			Height = thumbnail.ConfigHeight;
		}

		internal Thumbnail(JSON.UserData.ThumbnailResource thumbnail)
		{
			Src = thumbnail.Src;
			Width = thumbnail.ConfigWidth;
			Height = thumbnail.ConfigHeight;
		}
	}

	public class Dimensions
	{
		public readonly UInt64 Width;
		public readonly UInt64 Height;

		public Dimensions(UInt64 width, UInt64 height)
		{
			Width = width;
			Height = height;
		}

		internal Dimensions(JSON.Query.Dimensions dimensions)
		{
			Width = dimensions.Width;
			Height = dimensions.Height;
		}

		internal Dimensions(JSON.UserData.Dimensions dimensions)
		{
			Width = dimensions.Width;
			Height = dimensions.Height;
		}
	}
	
}
