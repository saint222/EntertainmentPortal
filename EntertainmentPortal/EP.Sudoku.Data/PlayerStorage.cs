using EP.Sudoku.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data
{
    public static class PlayerStorage
    {
        static List<PlayerDb> _storage = new List<PlayerDb>()
                                               {
                                                   new PlayerDb()
                                                   {
                                                       Id = 1,
                                                       NickName = "First Static Player",
                                                       ExperiencePoint = 0,
                                                       Level = 0
                                                       //AvatarIconDb = new AvatarIconDb { Id = 4, Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png", IsBaseIcon = true}
                                                   },
                                                   new PlayerDb()
                                                   {
                                                       Id = 2,
                                                       NickName = "Second Static Player",
                                                       ExperiencePoint = 0,
                                                       Level = 0
                                                   },
                                                   new PlayerDb()
                                                   {
                                                       Id = 3,
                                                       NickName = "Third Static Player",
                                                       ExperiencePoint = 0,
                                                       Level = 0
                                                   },
                                               };
        public static List<PlayerDb> Players => _storage;
    }
}
