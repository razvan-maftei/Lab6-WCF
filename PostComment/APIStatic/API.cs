using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PostComment.APIStatic {
    public static class API {
        public static bool AddPost(Post post)
        {
            using (var ctx = new ModelPostEntities())
            {
                var bResult = false;
                if (post.PostId == 0)
                {
                    var it = ctx.Entry(post).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public static Post UpdatePost(Post newPost)
        {
            using (var ctx = new ModelPostEntities())
            {
                // Ce e in bd. PK nu poate fi modificata
                var oldPost = ctx.Posts.Find(newPost.PostId);
                if (oldPost == null) return null;
                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }

        public static int DeletePost(int id)
        {
            using (var ctx = new ModelPostEntities())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From Post where postid =@p0", id);
            }
        }

        public static Post GetPostById(int id)
        {
            using (var ctx = new ModelPostEntities())
            {
                var items = from p in ctx.Posts where p.PostId == id select p;
                if (items != null) return items.Include(c => c.Comments).SingleOrDefault();
                return null;
            }
        }

        public static List<Post> GetAllPosts()
        {
            using (var ctx = new ModelPostEntities())
            {
                return ctx.Posts.Include("Comments").ToList();
            }
        }

        public static bool AddComment(Comment comment)
        {
            using (var ctx = new ModelPostEntities())
            {
                var bResult = false;
                if (comment == null || comment.PostPostId1 == 0) return bResult;
                if (comment.CommentId == 0)
                {
                    ctx.Entry(comment).State = EntityState.Added;
                    var p = ctx.Posts.Find(comment.PostPostId);
                    ctx.Entry(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public static Comment UpdateComment(Comment newComment)
        {
            using (var ctx = new ModelPostEntities())
            {
                var oldComment = ctx.Comments.Find(newComment.CommentId);
                if (newComment.Text != null) oldComment.Text = newComment.Text;
                if (oldComment.PostPostId != newComment.PostPostId && newComment.PostPostId1 != 0)
                    oldComment.PostPostId = newComment.PostPostId;
                ctx.SaveChanges();
                return oldComment;
            }
        }

        public static Comment GetCommentById(int id)
        {
            using (var ctx = new ModelPostEntities())
            {
                var items = from c in ctx.Comments where c.CommentId == id select c;
                return items.Include(p => p.PostPostId).SingleOrDefault();
            }
        }
    }
}
