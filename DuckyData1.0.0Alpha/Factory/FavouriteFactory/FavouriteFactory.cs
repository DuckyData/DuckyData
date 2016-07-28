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
        public List<VideoFavouriteDisplay> getVideoList(string userId,string query) {

            getDatabase();
            var videoList = from b in appDB.VideoFavourites select b;
            if(!String.IsNullOrEmpty(query))
            {
                videoList = videoList.Where(b => b.VideoTitle.Contains(query) && b.UserId.Equals(userId)).OrderBy(g => g.VideoTitle);
            }
            videoList = videoList.Where(b=>b.UserId.Equals(userId)).OrderBy(b => b.VideoTitle);
            return Mapper.Map<List<VideoFavouriteDisplay>>(videoList);

        }

        public List<MusicFavouriteDisplay> getAudioList(string userId,string query)
        {
            getDatabase();
            var audioList = from b in appDB.MusicFavourites select b;
            if(!String.IsNullOrEmpty(query))
            {
                audioList = audioList.Where(b => b.Album.Contains(query)
                                      || b.Artist.Contains(query)
                                      || b.MusicTitle.Equals(query)
                                      && b.UserId.Equals(userId)).OrderBy(b => b.Album).ThenBy(b=>b.Artist).ThenBy(b=>b.MusicTitle);
            }
            audioList = audioList.Where(b => b.UserId.Equals(userId)).OrderBy(b => b.Album);
            return Mapper.Map<List<MusicFavouriteDisplay>>(audioList);
        }

        public MusicFavourite findFavAudioById(int? id) {
            getDatabase();
            try {
                var audio = appDB.MusicFavourites.Find(id);
                return audio;
            }
            catch(Exception e) {
                return null;
            }
        }

        public bool removeFavAudioById(int id) {
            getDatabase();
            try
            {
                var audio = appDB.MusicFavourites.Find(id);
                appDB.MusicFavourites.Remove(audio);
                appDB.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}