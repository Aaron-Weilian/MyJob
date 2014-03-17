using System;
using System.Data;
using System.Collections.Generic;
using com.portal.db.DAL;
using com.portal.db.Model;
using Microsoft.ApplicationBlocks.Data;

namespace com.portal.db.BLL
{
    public partial class UploadFileList
    {
        private readonly com.portal.db.DAL.UploadFileList dal = new com.portal.db.DAL.UploadFileList();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(com.portal.db.Model.UploadFileList model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(com.portal.db.Model.UploadFileList model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UploadFileListID)
        {

            return dal.Delete(UploadFileListID);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.UploadFileList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.UploadFileList> GetModelFileList(string strWhere)
        {
            DataSet ds = dal.GetFileList(strWhere);
            return DataTableToListNOStream(ds.Tables[0]);
        }

        public List<com.portal.db.Model.UploadFileList> GetUploadMessages(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(GetAppSetting.GetConnSetting(), CommandType.Text, sql);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<com.portal.db.Model.UploadFileList> DataTableToList(DataTable dt)
        {
            List<com.portal.db.Model.UploadFileList> modelList = new List<com.portal.db.Model.UploadFileList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                com.portal.db.Model.UploadFileList model;
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



        public List<com.portal.db.Model.UploadFileList> DataTableToListNOStream(DataTable dt)
        {
            List<com.portal.db.Model.UploadFileList> modelList = new List<com.portal.db.Model.UploadFileList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                com.portal.db.Model.UploadFileList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModelNOStream(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
    
    
    }
}
