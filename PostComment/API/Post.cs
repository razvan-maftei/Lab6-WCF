using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PostComment {
    public partial class Post {
        public bool AddPost()
        {
            using (var context = new ModelPostEntities())
            {
                var bResult = false;
                if (this.PostId == 0)
                {
                    var it = context.Entry<Post>(this).State = EntityState.Added;
                    context.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public PostComment.Post UpdatePost(PostComment.Post newPost)
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                PostComment.Post oldPost = ctx.Posts.Find(newPost.PostId);
                if (oldPost == null) return null;
                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }

        public int DeletePost(int id)
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                return ctx.Database.ExecuteSqlCommand("Delete from Posts where postid = @p0", id);
            }
        }

        public PostComment.Post GetPostById(int Id)
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                var items = from p in ctx.Posts where (p.PostId == Id) select p;
                if (items != null)
                {
                    return items.Include(c => c.Comments).SingleOrDefault();
                }

                return null;
            }
        }

        public List<Post> GetAllPosts()
        {
            using (ModelPostEntities ctx = new ModelPostEntities())
            {
                return ctx.Posts.Include("Comments").ToList<Post>();
            }
        }
    }
}