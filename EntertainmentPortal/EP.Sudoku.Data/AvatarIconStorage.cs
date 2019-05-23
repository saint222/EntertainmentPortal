using EP.Sudoku.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data
{
    class AvatarIconStorage
    {
        static List<AvatarIconDb> _storage = new List<AvatarIconDb>()
                                               {
                                                   new AvatarIconDb()
                                                   {
                                                       Id = 1,
                                                       Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                                                       IsBaseIcon = true,                                                       
                                                   },
                                                   new AvatarIconDb()
                                                   {
                                                       Id = 2,
                                                       Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Elf-icon.png",
                                                       IsBaseIcon = false,
                                                   },
                                                   new AvatarIconDb()
                                                   {
                                                       Id = 3,
                                                       Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Sorceress-Witch-icon.png",
                                                       IsBaseIcon = false,
                                                   },
                                                   new AvatarIconDb()
                                                   {
                                                       Id = 4,
                                                       Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Knight-icon.png",
                                                       IsBaseIcon = false,
                                                   },
                                               };
        public static List<AvatarIconDb> AvatarIcons => _storage;
    }
}
