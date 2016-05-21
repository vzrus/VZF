#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File YafBoardFolders.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:08 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

using System;

namespace YAF.Classes
{
    using YAF.Classes.Pattern;

    /// <summary>
    /// The yaf board folders.
    /// </summary>
    public class YafBoardFolders
    {
        /// <summary>
        /// Gets Current.
        /// </summary>
        public static YafBoardFolders Current
        {
            get
            {
                return PageSingleton<YafBoardFolders>.Instance;
            }
        }

        /// <summary>
        /// Gets BoardFolder.
        /// </summary>
        public static string BoardFolder
        {
            get
            {
                return Config.MultiBoardFolders
                           ? string.Format(Config.BoardRoot + "{0}/", YafControlSettings.Current.BoardID)
                           : Config.BoardRoot;
            }
        }

        /// <summary>
        /// Gets Uploads.
        /// </summary>
        public string Uploads
        {
            get
            {
                return String.Concat(BoardFolder, "Uploads");
            }
        }

        public string Albums
        {
            get
            {
                return string.Concat(BoardFolder, this.Uploads, this.PathSeparator, "Albums");
            }
        }

        public string TopicAttachments
        {
            get
            {
                return string.Concat(BoardFolder, this.Uploads, this.PathSeparator, "Attachments");
            }
        }

        /// <summary>
        /// Gets Themes.
        /// </summary>
        public string Themes
        {
            get
            {
                return string.Concat(BoardFolder, "Themes");
            }
        }

        /// <summary>
        /// Gets Images.
        /// </summary>
        public string Images
        {
            get
            {
                return string.Concat(BoardFolder, "Images");
            }
        }

        /// <summary>
        /// Gets Avatars.
        /// </summary>
        public string Avatars
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Avatars");
            }
        }

        /// <summary>
        /// Gets Categories.
        /// </summary>
        public string Categories
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Categories");
            }
        }

        /// <summary>
        /// Gets Categories.
        /// </summary>
        public string Forums
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Forums");
            }
        }

        /// <summary>
        /// Gets Emoticons.
        /// </summary>
        public string Emoticons
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Emoticons");
            }
        }

        /// <summary>
        /// Gets Medals.
        /// </summary>
        public string Medals
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Medals");
            }
        }

        /// <summary>
        /// Gets the path separator.
        /// </summary>
        public string PathSeparator
        {
            get
            {
                return "/";
            }
        }

        /// <summary>
        /// Gets Ranks.
        /// </summary>
        public string Ranks
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Ranks");
            }
        }

        /// <summary>
        /// Gets Topics.
        /// </summary>
        public string Topics
        {
            get
            {
                return string.Concat(BoardFolder, this.Images, this.PathSeparator, "Topics");
            }
        }
        /// <summary>
        /// Gets path to topic image folder with slash.
        /// </summary>
        public string TopicImages
        {
            get
            {
                return string.Concat(BoardFolder, this.Uploads, this.PathSeparator, "Images", this.PathSeparator, "Topics", this.PathSeparator);
            }
        }


    }
}