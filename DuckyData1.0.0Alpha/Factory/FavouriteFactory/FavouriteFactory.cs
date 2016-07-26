using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.MusicFetch;
using DuckyData1._0._0Alpha.ViewModels.VideoFetch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.FavouriteFactory
{
    public class FavouriteFactory
    {
        private ApplicationDbContext appDB;

        public FavouriteFactory()
        {
            getDatabase();
        }

        public void getDatabase()
        {
            if(this.appDB == null)
            {
                appDB = new ApplicationDbContext();
            }
        }

        public bool addMusicFavourite(MusicFavouriteAdd music) {
            getDatabase();
            try {
                MusicFavourite fMusic = new MusicFavourite();
                fMusic = Mapper.Map<MusicFavourite>(music);

                appDB.MusicFavourites.Add(fMusic);
                appDB.SaveChanges();
                return true;
            }
            catch(Exception e) {
                return false;
            }
        }


        public bool addVideoFavourite(VideoFavouriteAdd video) {
            getDatabase();
            try
            {
                VideoFavourite fVideo = new VideoFavourite();
                fVideo = Mapper.Map<VideoFavourite>(video);

                appDB.VideoFavourites.Add(fVideo);
                appDB.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }



        //
        public IEnumerable<VideoFavouriteDisplay> getVideoList(string userId,string query) {

            getDatabase();
            var videoList = from b in appDB.VideoFavourites select b;
            if(!String.IsNullOrEmpty(query))
            {
                videoList = videoList.Where(b => b.VideoTitle.Contains(query) && b.UserId.Equals(userId)

                                            ).OrderBy(g => g.VideoTitle);
            }
            videoList = videoList.Where(b=>b.UserId.Equals(userId)).OrderBy(b => b.VideoTitle);
            return Mapper.Map<IEnumerable<VideoFavouriteDisplay>>(videoList);

        }
    }
}