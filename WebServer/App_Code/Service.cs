using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Tool;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;


/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "https://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService {

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    

    [WebMethod]
    public bool ReceiveData(Byte[] o,string type)
    {
        try
        {
            BLL.Message server = new BLL.Message();
            BLL.MessageBody message = new BLL.MessageBody();

            if (type == "ROS")
            {
                Tool.Message<ROSHeader> model = (Tool.Message<ROSHeader>)ObjectBinaryFormate.ChangeBytesToObject(o);
                if (message.Exists(model.sender.key, "ROS"))
                {
                    //server.updateROSData(model, "ROS");
                }
                else
                {
                    //server.saveROSData(model, "ROS");
                }
            }
            if (type == "PO")
            {
                Tool.Message<POHeader> model = (Tool.Message<POHeader>)ObjectBinaryFormate.ChangeBytesToObject(o);
                if (message.Exists(model.sender.key, "PO"))
                {
                    //server.updatePOData(model, "PO");
                }
                else
                {
                    //server.savePOData(model, "PO");
                }
            }
        }
        catch(Exception e ){
            throw e;
        }
        return true;
    }

    
    [WebMethod]
    public bool IsUpload()
    {
        BLL.Message server = new BLL.Message();
        return server.check(); ;
    }

    [WebMethod]
    public Byte[] SendData() 
    {
       BLL.MessageBody messageServer= new BLL.MessageBody();
       BLL.Message server = new BLL.Message();

       List<object> dic = new List<object>();

       string sql = " 1=1 ";

       sql += " and notes = 'OUTP' ";

       List<Model.MessageBody> list = messageServer.GetModelList(sql);
       foreach (Model.MessageBody message in list)
       {

           if (message.messageType == "ROSConfirm")
           {
               Dictionary<string, object> o = new Dictionary<string, object>();
               Tool.Message<ROSHeader> model = server.convert2Ros(message);
               o.Add(message.messageType, model);
               dic.Add(o);
           }
           if (message.messageType == "POConfirm")
           {
               Dictionary<string, object> o = new Dictionary<string, object>();
               Tool.Message<POHeader> model = server.convert2PO(message);
               o.Add(message.messageType, model);
               dic.Add(o);
           }

       }

       return ObjectBinaryFormate.ChangeObjectToBytes(dic);
        
    }

}
