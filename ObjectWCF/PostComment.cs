using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PostComment;

namespace ObjectWCF
{

    public class PostComment : IPostComment
    {
        bool InterfaceComment.AddComment(Comment comment)
        {
            return comment.AddComment();
        }

        bool InterfacePost.AddPost(Post post)
        {
            return post.AddPost();
        }

        int InterfacePost.DeletePost(int id)
        {
            Post post = new Post();
            return post.DeletePost(id);
        }

        Comment InterfaceComment.GetCommentById(int id)
        {
            Comment comment = new Comment();
            return comment.GetCommentById(id);
        }

        Post InterfacePost.GetPostById(int id)
        {
            Post post = new Post();
            Console.WriteLine("GetPostById. Id = {0}", id);
            post = post.GetPostById(id);
            Console.WriteLine("Post returnat. Id = {0}, Description = {1}", post.PostId, post.Description);
            return post;
        }

        List<Post> InterfacePost.GetPosts()
        {
            Post post = new Post();
            return post.GetAllPosts();
        }

        Comment InterfaceComment.UpdateComment(Comment newComment)
        {
            return newComment.UpdateComment(newComment);
        }

        Post InterfacePost.UpdatePost(Post post)
        {
            return post.UpdatePost(post);
        }

    }
}
