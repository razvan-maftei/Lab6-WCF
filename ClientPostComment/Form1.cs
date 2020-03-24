using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PostComment;

namespace ClientPostComment
{
    public partial class Form1 : Form {
        private List<Post> posts = new List<Post>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            posts = LoadPosts().ToList<Post>();
            dgp.DataSource = posts;
            dgp.Columns[0].Width = 0;
            if (dgp.Rows.Count > 0)
            {
                dgc.DataSource = posts[0].Comments;
            }
        }

        private static PostComment.Post[] LoadPosts() { PostCommentClient pc = new PostCommentClient(); PostComment.Post[] p = pc.GetPosts(); return p; }
        private void dgp_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return; dgc.DataSource = null;dgc.DataSource = posts[e.RowIndex].Comments;}
        }
}
