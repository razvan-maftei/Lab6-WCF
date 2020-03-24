using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using PostComment;
using PostComment.DTO;

namespace ObjectWCF
{
    [ServiceContract]
    interface InterfacePost {
        [OperationContract]
        bool AddPost(Post post);

        [OperationContract]
        Post UpdatePost(Post post);

        [OperationContract]
        int DeletePost(int id);

        [OperationContract]
        Post GetPostById(int id);

        [OperationContract]
        List<Post> GetPosts();
    }

    [ServiceContract]
    interface InterfaceComment {
        [OperationContract]
        bool AddComment(Comment comment);

        [OperationContract]
        Comment UpdateComment(Comment newComment);

        [OperationContract]
        Comment GetCommentById(int id);
    }

    [ServiceContract]
    public interface IPost {
        [OperationContract]
        void Cleanup();

        [OperationContract]
        PostDTO GetPostById(int id);

        [OperationContract]
        PostDTO GetPostByTitle(string title);

        [OperationContract]
        PostDTO SubmitPost(PostDTO post);

        [OperationContract]
        PostDTO UpdatePost(PostDTO newPost);

        [OperationContract]
        bool DeletePost(int postId);

        [OperationContract]
        List<PostDTO> GetAllPosts();
    }

    [ServiceContract]
    public interface IComment {
        [OperationContract]
        CommentDTO GetCommentById(int id);

        [OperationContract]
        CommentDTO SubmitComment(CommentDTO comment);

        [OperationContract(Name = "AddComment")]
        CommentDTO SubmitComment(int postId, CommentDTO comment);

        [OperationContract]
        CommentDTO UpdateComment(CommentDTO oldComment, CommentDTO newComment);

        [OperationContract]
        bool DeleteComment(int commentId);
    }

    [ServiceContract]
    public interface ILoadData {
        [OperationContract]
        List<PostDTO> GetAllPostsAndRelatedComments();
    }

    [ServiceContract]
    public interface IPostComment : IPost, IComment, ILoadData {

    }
}
