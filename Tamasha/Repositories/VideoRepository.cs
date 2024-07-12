using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamasha.Database;

namespace Tamasha.Repositories
{
    internal class VideoRepository
    {
        
        public void MakeNewVideo(string title, string description, string url, bool isAgeRestricted, string username)
        {
            string[] parameters = ["@Title","@Description","@Url","@IsAgeRestricted"];
            object[] values = { title, description, url,isAgeRestricted };
            SQL.ExecuteNonQueryStoreProcedure("MakeNewVideo", parameters, values);
            var userRes = SQL.ExecuteReaderStoreProcedure("GetUserIDByUsername", ["@Username"], [username]);
            var videoRes = SQL.ExecuteReaderStoreProcedure("GetVideoIDByUrl", ["@Url"], [url]);
            int userID = Convert.ToInt32(userRes[0].Replace(";", ""));
            int videoID = Convert.ToInt32(videoRes[0].Replace(";", ""));
            SQL.ExecuteNonQueryStoreProcedure("User_Videos", ["@VideoID", "@UserID", "@DatePosted"], [userID,videoID, DateTime.Now]);
        }
        
        public int GetVideoIdByUrl(string url)
        {
            var result = SQL.ExecuteReaderStoreProcedure("GetVideoIDByUrl", ["@Url"], [url]);
            var videoId = result[0].Replace(";","");
            return Convert.ToInt32(videoId);
        }
        
    }
}
