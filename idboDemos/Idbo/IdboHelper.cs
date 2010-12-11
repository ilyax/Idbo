// Idbo
//
// Copyright © Ilyas Kolasinac Osmanogullari, 2010
// ilyax.os@hotmail.com || http://www.ilyax.com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Idbo
{
    public class IdboConnection
    {

        readonly static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "idb.yap");
        public static IObjectContainer db;

        public static void SetDb()
        {
            db = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), dbPath);
        }

        public static void CloseDB()
        {
            db.Close();
        }
    }// class end

    public class IdboHelper<T> : IdboConnection
    {
        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        public static void Insert(object entity)
        {
            db.Store(entity);
            db.Commit();
        }

        /// <summary>
        /// Select All T
        /// </summary>
        /// <returns></returns>
        public static List<T> SelectAll()
        {   
            var result = (from T o in db select o).ToList<T>();

            return result;
        }

        /// <summary>
        /// SelectByID T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T SelectByID(string id)
        {
            IEnumerable<T> result = db.QueryByExample(default(T)).Cast<T>();

            dynamic entity = null;

            foreach (var tmpEntity in result)
            {
                entity = tmpEntity;

                if (entity.ID == id)
                {
                    return (T)entity;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Update(object entity)
        {
            try
            {
                db.Store(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Delete(object entity)
        {
            try
            {
                db.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }// class end

    public class IdboTools
    {
        /// <summary>
        /// Create GUI for ID
        /// </summary>
        /// <returns></returns>
        public static string CreateGUI()
        {
            return System.Guid.NewGuid().ToString();
        }
    }// class end
}
