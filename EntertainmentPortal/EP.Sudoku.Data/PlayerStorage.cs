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
                                                       Level = 0,
                                                       AvatarIconDb = new AvatarIconDb ()
                                                       {
                                                           Id = 1,
                                                           Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                                                       }
                                                   },
                                                   new PlayerDb()
                                                   {
                                                       Id = 2,
                                                       NickName = "Second Static Player",
                                                       ExperiencePoint = 0,
                                                       Level = 0,
                                                       AvatarIconDb = new AvatarIconDb ()
                                                       {
                                                           Id = 2,
                                                           Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Elf-icon.png",

                                                       }
                                                   },
                                                   new PlayerDb()
                                                   {
                                                       Id = 3,
                                                       NickName = "Third Static Player",
                                                       ExperiencePoint = 0,
                                                       Level = 0,
                                                       AvatarIconDb = new AvatarIconDb()
                                                       {
                                                           Id = 3,
                                                           Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Sorceress-Witch-icon.png",
                                                       
                                                       }
                                                   },
                                               };
        public static List<PlayerDb> Players => _storage;
    }
}
