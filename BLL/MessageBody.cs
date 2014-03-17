using System;
using System.Data;
using System.Collections.Generic;
//using Maticsoft.Common;
using com.portal.db.DAL;
using com.portal.db.Model;
using Microsoft.ApplicationBlocks.Data;

namespace com.portal.db.BLL
{
    /// <summary>
    /// MessageBody
    /// </summary>
    public partial class MessageBody
    {
        private readonly com.portal.db.DAL.MessageBody dal = new com.portal.db.DAL.MessageBody();
        public MessageBody()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReferenceID, string MessageType)
        {
            return dal.Exists(ReferenceID, MessageType);
        }

        //public bool ExistRefs(string ReferenceID, string MessageType)
        //{
        //    return dal.ExistRefs(ReferenceID, MessageType);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.MessageBody model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.MessageBody model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string messageID)
        {

            return dal.Delete(messageID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string messageIDlist)
        {
            return dal.DeleteList(messageIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public com.portal.db.Model.MessageBody GetModel(string messageID)
        {

            return dal.GetModel(messageID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public com.portal.db.Model.MessageBody GetModelByCache(string messageID)
        //{

        //    string CacheKey = "MessageBodyModel-" + messageID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(messageID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (com.portal.db.Model.MessageBody)objModel;
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
        public List<com.portal.db.Model.MessageBody> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Model.MessageBody> GetOutBoundMessages(string sql){
            DataSet ds = SqlHelper.ExecuteDataset(GetAppSetting.GetConnSetting(), CommandType.Text, sql);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.MessageBody> DataTableToList(DataTable dt)
        {
            List<com.portal.db.Model.MessageBody> modelList = new List<com.portal.db.Model.MessageBody>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                com.portal.db.Model.MessageBody model;
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

