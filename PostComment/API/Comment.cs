using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
    public partial class Comment
    {
        public bool AddComment()
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                bool bResult = false;
                if (this == null || this.PostPostId1 == 0)
                {
                    return bResult;
                }

                if (this.CommentId == 0)
                {
                    ctx.Entry<Comment>(this).State = EntityState.Added;
                    Post p = ctx.Posts.Find(this.PostPostId1);
                    ctx.Entry<Post>(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public Comment UpdateComment(Comment newComment)
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                Comment oldComment = ctx.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                {
                    oldComment.Text = newComment.Text;
                }

                if ((oldComment.PostPostId1 != newComment.PostPostId1) && newComment.PostPostId1 != 0)
                {
                    oldComment.PostPostId1 = newComment.PostPostId1;
                }

                ctx.SaveChanges();
                return oldComment;
            }
        }

        public Comment GetCommentById(int id)
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                var items = from c in ctx.Comments where (c.CommentId == id) select c;
                return items.Include(p => p.PostPostId1).SingleOrDefault();
            }
        }
    }
}
