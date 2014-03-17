using System;
using System.Data;
using System.Collections.Generic;

using com.portal.db.Model;
namespace com.portal.db.BLL
{
    /// <summary>
    /// Site
    /// </summary>
    public partial class Site
    {
        private readonly com.portal.db.DAL.Site dal = new com.portal.db.DAL.Site();
        public Site()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SiteID)
        {
            return dal.Exists(SiteID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.Site model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.Site model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string SiteID)
        {

            return dal.Delete(SiteID);
        }

        public bool DeleteBySupplierID(string SupplierID) {

            return dal.DeleteBySupplierID(SupplierID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string SiteIDlist)
        {
            return dal.DeleteList(SiteIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.Site GetModel(string SiteID)
        {

            return dal.GetModel(SiteID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public com.portal.db.Model.Site GetModelByCache(string SiteID)
        //{

        //    string CacheKey = "SiteModel-" + SiteID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(SiteID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (com.portal.db.Model.Site)objModel;
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
        public List<com.portal.db.Model.Site> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.Site> DataTableToList(DataTable dt)
        {
            List<com.portal.db.Model.Site> modelList = new List<com.portal.db.Model.Site>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                com.portal.db.Model.Site model;
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

