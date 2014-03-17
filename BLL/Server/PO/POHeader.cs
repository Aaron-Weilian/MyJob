using System;
using System.Data;
using System.Collections.Generic;
//using Maticsoft.Common;
using com.portal.db.Model;
namespace com.portal.db.BLL.PO
{
    /// <summary>
    /// POHeader
    /// </summary>
    public partial class POHeader
    {
        private readonly com.portal.db.DAL.POHeader dal = new com.portal.db.DAL.POHeader();
        public POHeader()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string POHeaderID)
        {
            return dal.Exists(POHeaderID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.POHeader model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.POHeader model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string POHeaderID)
        {

            return dal.Delete(POHeaderID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string POHeaderIDlist)
        {
            return dal.DeleteList(POHeaderIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.POHeader GetModel(string POHeaderID)
        {

            return dal.GetModel(POHeaderID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public com.portal.db.Model.POHeader GetModelByCache(string POHeaderID)
        //{

        //    string CacheKey = "POHeaderModel-" + POHeaderID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(POHeaderID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (com.portal.db.Model.POHeader)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.POHeader> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.POHeader> DataTableToList(DataTable dt)
        {
            List<com.portal.db.Model.POHeader> modelList = new List<com.portal.db.Model.POHeader>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                com.portal.db.Model.POHeader model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

