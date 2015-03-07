/* Yet Another Forum.net
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */



namespace YAF.Core.Tasks
{
	using System;

	using VZF.Data.Common;

	using YAF.Types.Constants;
	
	using YAF.Types.Interfaces;
	using VZF.Utils;

	/// <summary>
	/// The forum delete task.
	/// </summary>
    public class CategoryDeleteTask : LongBackgroundTask,ICriticalBackgroundTask,IBlockableTask
	{
		#region Constants and Fields

		/// <summary>
		/// The _task name.
		/// </summary>
		private const string _TaskName = "CategoryDeleteTask";
      

		#endregion

		#region Properties

		/// <summary>
		/// Gets TaskName.
		/// </summary>
		public static string TaskName
		{
			get
			{
				return _TaskName;
			}
		}

        /// <summary>
        /// The Blocking Task Names.
        /// </summary>
        private static readonly string[] BlockingTaskNames = Constants.ForumRebuild.BlockingTaskNames;


		/// <summary>
		/// Gets or sets CategoryId.
		/// </summary>
		public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets NewCategoryId.
        /// </summary>
        public int? NewCategoryId { get; set; }

		/// <summary>
		/// Gets or sets Forum New Id.
		/// </summary>
		public int ForumNewId { get; set; }

        /// <summary>
        /// Gets or sets ModuleId.
        /// </summary>      
        public int ModuleId { get; set; }

		#endregion

		#region Public Methods

	    /// <summary>
	    /// Creates the Forum Delete Task
	    /// </summary>
	    /// <param name="boardId">
	    /// The board id.
	    /// </param>
	    /// <param name="forumId">
	    /// The forum id.
	    /// </param>
	    /// <param name="failureMessage"> 
	    /// The failure message - is empty if task is launched successfully.
	    /// </param>
	    /// <returns>
	    /// Returns if Task was Successfull
	    /// </returns>
	    public static bool Start(int mid,int categoryId, int? newCategoryId,  out string failureMessage)
		{

            failureMessage = string.Empty;
			if (YafContext.Current.Get<ITaskModuleManager>() == null)
			{
				return false;
			}
            if (!YafContext.Current.Get<ITaskModuleManager>().AreTasksRunning(BlockingTaskNames))
            {
                YafContext.Current.Get<ITaskModuleManager>().StartTask(TaskName,
                                                                       () =>
                                                                       new CategoryDeleteTask
                                                                           {
                                                                               ModuleId = mid,
                                                                               CategoryId = categoryId,                                                                               
                                                                               NewCategoryId = newCategoryId      
                                                                           });
            }
            else
            {
                failureMessage = "You can't delete category while some of the blocking {0} tasks are running.".FormatWith(BlockingTaskNames.ToDelimitedString(","));
                return false;
            }

		    return true;
		}

		/// <summary>
		/// The run once.
		/// </summary>
		public override void RunOnce()
		{
            try
            {
                this.Logger.Info("Starting Category {0} delete task.",this.CategoryId);
                CommonDb.category_delete(this.ModuleId, this.CategoryId, this.NewCategoryId);
                this.Logger.Info("Category (ID: {0}) Delete Task Complete.",this.CategoryId);				
			}
			catch (Exception x)
			{
                this.Logger.Error(x,"Error In Category (ID: {0}) Delete Task".FormatWith(this.CategoryId),x);
			}
		}

		#endregion
	}
}