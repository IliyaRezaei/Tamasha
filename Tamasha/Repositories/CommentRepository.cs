using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamasha.Database;

namespace Tamasha.Repositories
{
    internal class CommentRepository
    {
        VideoRepository videoRepository = new VideoRepository();
        UserRepository userRepository = new UserRepository();
        public void MakeNewComment(string context, string url, string username)
        {
            SQL.ExecuteNonQueryStoreProcedure("MakeNewComment", ["@Context","@DatePosted"], [context, DateTime.Now]);
            var commentRes = SQL.ExecuteReaderStoreProcedure("GetBiggestCommentId");
            var commentId = commentRes[0].Replace(";", "");
            //int videoId = videoRepository.GetVideoIdByUrl(url); //URL ro vaghti darkhast be comment mikone az dakhele browser client ersal mishe
            int userId = userRepository.GetUserIdByUsername(username);
            SQL.ExecuteNonQueryStoreProcedure("Video_Comments", ["Video_ID","Comment_ID","User_ID"], [1/*videoId */, Convert.ToInt32(commentId),userId]);
        }
        //field int id dakhele tag html un comment gharar dade mishe va vaghti request midim un id hamrah ba text jadide comment ersal mishe be database
        public void UpdateComment(int id)
        {
            //INSERT INTO dbo.Comments where dbo.Comments.Comment_ID == id
        }
    }
}
