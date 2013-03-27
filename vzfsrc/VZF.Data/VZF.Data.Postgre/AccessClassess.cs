/* VZF by vzrus
 * Copyright (C) 2006-2013 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure was primarily based on MS SQL Server code,
 * created by YAF(YetAnotherForum) developers  * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace VZF.Data.Postgre
{
    using System;
    using System.Data;

    using Npgsql;

    using YAF.Classes.Data;

    /// <summary>
    /// The class creates accessmask_list stored procedure data access functionality.
    /// </summary>
    public sealed class accessmask_list
    {

        #region Common Properties & Fields
        private static volatile accessmask_list instance;
        private string connectionString;
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static accessmask_list Instance
        {
            get { return Nested.instance; }
        }
        class Nested
        {
            static Nested() { }
            internal static readonly accessmask_list
                  instance = new accessmask_list();
        }
        #endregion

        private NpgsqlCommand cmd;
        private NpgsqlParameter _boardId;
        private NpgsqlParameter _accessMaskID;
        private NpgsqlParameter _excludeFlags;

        private accessmask_list()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("accessmask_list");
            this.cmd.CommandType = CommandType.StoredProcedure;

            this._boardId = this.cmd.Parameters.Add(new NpgsqlParameter("i_board", NpgsqlTypes.NpgsqlDbType.Integer));
            this._accessMaskID = this.cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlTypes.NpgsqlDbType.Integer));
            this._excludeFlags = this.cmd.Parameters.Add(new NpgsqlParameter("i_excludeflags", NpgsqlTypes.NpgsqlDbType.Integer));

        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public DataTable AccessMaskList(string connectionString, object boardId, object accessMaskID, object excludeFlags)
        {
            this._boardId.Value = boardId;
            this._accessMaskID.Value = accessMaskID;
            this._excludeFlags.Value = excludeFlags;

            return PostgreDbAccess.GetData(this.cmd,connectionString);
        }
    }
    /// <summary>
    /// The class creates deletes an accessmask stored procedure data access functionality.
    /// </summary>
    public sealed class accessmask_delete
    {

        #region Common Properties & Fields
        private static volatile accessmask_delete instance;
        private string connectionString;

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static accessmask_delete Instance
        {
            get { return Nested.instance; }
        }
        class Nested
        {
            static Nested() { }
            internal static readonly accessmask_delete
                  instance = new accessmask_delete();
        }
        #endregion

        private NpgsqlCommand cmd;
 
        private NpgsqlParameter _accessMaskID;
      

        private accessmask_delete()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("accessmask_delete");
            this.cmd.CommandType = CommandType.StoredProcedure;

            this._accessMaskID = this.cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlTypes.NpgsqlDbType.Integer));

        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public bool AccessMaskDelete(string connectionString, object accessMaskID)
        {
            this._accessMaskID.Value = accessMaskID;
            return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(this.cmd,connectionString)) != 0;
      
        }
    }
    /// <summary>
    /// The class creates an accessmask stored procedure data access functionality.
    /// </summary>
    public sealed class accessmask_save
    {

        #region Common Properties & Fields
        private static volatile accessmask_save instance;
        private string connectionString;
       

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static accessmask_save Instance
        {
            get { return Nested.instance; }
        }
        class Nested
        {
            static Nested() { }
            internal static readonly accessmask_save
                  instance = new accessmask_save();
        }
        #endregion

        private NpgsqlCommand cmd;

        private NpgsqlParameter _accessMaskID;
        private NpgsqlParameter _boardId;
        private NpgsqlParameter _name;
        private NpgsqlParameter _readAccess;
        private NpgsqlParameter _postAccess;
        private NpgsqlParameter _replyAccess;
        private NpgsqlParameter _priorityAccess; 
        private NpgsqlParameter _pollAccess;
        private NpgsqlParameter _voteAccess;
        private NpgsqlParameter _moderatorAccess;
        private NpgsqlParameter _editAccess;
        private NpgsqlParameter _deleteAccess;
        private NpgsqlParameter _uploadAccess;
        private NpgsqlParameter _downloadAccess;
        private NpgsqlParameter _sortOrder;

        private accessmask_save()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("accessmask_save");
            this.cmd.CommandType = CommandType.StoredProcedure;

            this._accessMaskID = this.cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlTypes.NpgsqlDbType.Integer));
            this._boardId = this.cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlTypes.NpgsqlDbType.Integer));
            this._name = this.cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlTypes.NpgsqlDbType.Varchar));
            this._readAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_readaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._postAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_postaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._replyAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_replyaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._priorityAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_priorityaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._pollAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_pollaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._voteAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_voteaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._moderatorAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_moderatoraccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._editAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_editaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._deleteAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_deleteaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._uploadAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_uploadaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._downloadAccess = this.cmd.Parameters.Add(new NpgsqlParameter("i_downloadaccess", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._sortOrder = this.cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlTypes.NpgsqlDbType.Smallint));	

        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public void AccessMaskSave(string connectionString, object accessMaskID, 
            object boardId, object name, object readAccess, object postAccess, 
            object replyAccess, object priorityAccess, object pollAccess, 
            object voteAccess, object moderatorAccess, object editAccess, 
            object deleteAccess, object uploadAccess, object downloadAccess, 
            object sortOrder)
        {
            this._accessMaskID.Value = accessMaskID;
            this._boardId.Value = boardId;
            this._name.Value = name;
            this._readAccess.Value = readAccess;
            this._postAccess.Value = postAccess;
            this._replyAccess.Value = replyAccess;
            this._priorityAccess.Value = priorityAccess; 
            this._pollAccess.Value = pollAccess;
            this._voteAccess.Value = voteAccess;
            this._moderatorAccess.Value = moderatorAccess;
            this._editAccess.Value = editAccess;
            this._deleteAccess.Value = deleteAccess;
            this._uploadAccess.Value = uploadAccess;
            this._downloadAccess.Value = downloadAccess;
            this._sortOrder.Value = sortOrder;

            PostgreDbAccess.ExecuteNonQuery(this.cmd, connectionString);
        }
    }
    /// <summary>
    /// The class creates active_list stored procedure data access functionality.
    /// </summary>
    public sealed class active_list
    {

        #region Common Properties & Fields
        private static volatile active_list instance;
        private string connectionString;
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static active_list Instance
        {
            get { return Nested.instance; }
        }
        class Nested
        {
            static Nested() { }
            internal static readonly active_list
                  instance = new active_list();
        }
        #endregion

        private NpgsqlCommand cmd;
        private NpgsqlParameter _boardId;
        private NpgsqlParameter _guests;
        private NpgsqlParameter _showCrawlers;
        private NpgsqlParameter _interval;
        private NpgsqlParameter _styledNicks;


        private active_list()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("active_list");
            this.cmd.CommandType = CommandType.StoredProcedure;
            this._boardId = this.cmd.Parameters.Add(new NpgsqlParameter("i_board", NpgsqlTypes.NpgsqlDbType.Integer));
            this._guests = this.cmd.Parameters.Add(new NpgsqlParameter("i_guests", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._showCrawlers = this.cmd.Parameters.Add(new NpgsqlParameter("i_showcrawlers", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._interval = this.cmd.Parameters.Add(new NpgsqlParameter("i_interval", NpgsqlTypes.NpgsqlDbType.Integer));
            this._styledNicks = this.cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlTypes.NpgsqlDbType.Boolean));
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public DataTable ActiveList(string connectionString, object boardId, object guests, object showCrawlers, int interval, object styledNicks)
        {
            this._boardId.Value = boardId;
            this._guests.Value = guests;
            this._showCrawlers.Value = showCrawlers;
            this._interval.Value = interval;
            this._styledNicks.Value = styledNicks;

            return PostgreDbAccess.GetData(this.cmd, connectionString);
        }
    }
    /// <summary>
    /// The class creates active_list_user stored procedure data access functionality.
    /// </summary>
    public sealed class active_list_user
    {

        #region Common Properties & Fields
        private static volatile active_list_user instance;
        private string connectionString;

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static active_list_user Instance
        {
            get
            {
                return active_list_user_Nested.instance_active_list_user;
            }
        }

        #endregion

        private NpgsqlCommand cmd;
        private NpgsqlParameter _boardId;
        private NpgsqlParameter _userId;
        private NpgsqlParameter _guests;
        private NpgsqlParameter _showCrawlers;
        private NpgsqlParameter _interval;
        private NpgsqlParameter _styledNicks;


        active_list_user()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("active_list_user");
            this.cmd.CommandType = CommandType.StoredProcedure;
            this._boardId = this.cmd.Parameters.Add(new NpgsqlParameter("i_board", NpgsqlTypes.NpgsqlDbType.Integer));
            this._userId = this.cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlTypes.NpgsqlDbType.Integer));
            this._guests = this.cmd.Parameters.Add(new NpgsqlParameter("i_guests", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._showCrawlers = this.cmd.Parameters.Add(new NpgsqlParameter("i_showcrawlers", NpgsqlTypes.NpgsqlDbType.Boolean));
            this._interval = this.cmd.Parameters.Add(new NpgsqlParameter("i_interval", NpgsqlTypes.NpgsqlDbType.Integer));
            this._styledNicks = this.cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlTypes.NpgsqlDbType.Boolean));
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public DataTable ActiveListUser(string connectionString, object boardId, object userId, 
            object guests, object showCrawlers, int activeTime, object styledNicks)
        {
            this._boardId.Value = boardId;
            this._userId.Value = userId;
            this._guests.Value = guests;
            this._showCrawlers.Value = showCrawlers;
            this._interval.Value = activeTime;
            this._styledNicks.Value = styledNicks;

            return PostgreDbAccess.GetData(this.cmd, connectionString);
        }
        class active_list_user_Nested
        {
            static active_list_user_Nested() { }
            internal static readonly active_list_user
                  instance_active_list_user = new active_list_user();
        }
    }
    /// <summary>
    /// The class creates active_listforum stored procedure data access functionality.
    /// </summary>
    public sealed class active_listforum
    {

        #region Common Properties & Fields
        private static volatile active_listforum instance;
        private string connectionString;

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static active_listforum Instance
        {
            get
            {
                return active_listforum_Nested.instance;
            }
        }

        #endregion

        private NpgsqlCommand cmd;
        private NpgsqlParameter _forumId;
        private NpgsqlParameter _styledNicks;
   

        active_listforum()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("active_listforum");
            this.cmd.CommandType = CommandType.StoredProcedure;
            this._forumId = this.cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlTypes.NpgsqlDbType.Integer));
          
            this._styledNicks = this.cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlTypes.NpgsqlDbType.Boolean));
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public DataTable ActiveListForum(string connectionString, object forumID, object styledNicks)
        {
            this._forumId.Value = forumID;
            this._styledNicks.Value = styledNicks;

            return PostgreDbAccess.GetData(this.cmd, connectionString);
        }
        class active_listforum_Nested
        {
            internal static readonly active_listforum
                  instance = new active_listforum();
            static active_listforum_Nested() { }
            
        }
    }
    /// <summary>
    /// The class creates active_list stored procedure data access functionality.
    /// </summary>
    public sealed class active_stats
    {

        #region Common Properties & Fields
        private static volatile active_stats instance;
        private string connectionString;

        /// <summary>
        /// An info about a user who accesses the class
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        /// <summary>
        /// A contractor without multithreading locks
        /// </summary>
        public static active_stats Instance
        {
            get { return Nested.instance; }
        }
        class Nested
        {
            static Nested() { }
            internal static readonly active_stats
                  instance = new active_stats();
        }
        #endregion

        private NpgsqlCommand cmd;
        private NpgsqlParameter _boardId;

        private active_stats()
        {
            // Common properites
            this.connectionString = string.Empty;
            // Specific data
            this.cmd = PostgreDbAccess.GetCommand("active_stats");
            this.cmd.CommandType = CommandType.StoredProcedure;
            this._boardId = this.cmd.Parameters.Add(new NpgsqlParameter("i_board", NpgsqlTypes.NpgsqlDbType.Integer));
          }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public DataRow ActiveStats(string connectionString, object boardId)
        {
            this._boardId.Value = boardId;

            using (DataTable dt = PostgreDbAccess.GetData(this.cmd, connectionString))
            {
                return dt.Rows[0];
            }
        }
    }

}
