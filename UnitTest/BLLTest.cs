using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.portal.db.BLL;
using Tool;

namespace UnitTest
{
    [TestClass]
    public class BLLTest
    {
        //[TestMethod]
        //public void TestConvertMethod()
        //{
        //    ConvertModel cm = new ConvertModel();
        //    Tool.MessageContext body = new Tool.MessageContext();

        //    body.structure = cm.Convert("NROS", true, cm.getRoot("NROS"));

        //    //Tool.XMLMessage message = Tool.XMLUtil.loadXML(@"C:\Broker\Production\Backup\OUT\2014\02\IC0561_TP02_L050_NROS_20140210_154707_189_0.xml", body);



        //    ServerImpl si = new ServerImpl();
           
        //    //si.mergeData(message, "ROS", "IN", "123");

        //    Dictionary<string, string> param = new Dictionary<string, string>();
        //    param.Add("messageID", "4dc9f1e3b4ba4e4bba9d64fa6cfd3a10");

        //    com.portal.db.Model.MessageBody bo = (com.portal.db.Model.MessageBody)si.GetModel("message", typeof(com.portal.db.Model.MessageBody), param);

        //    Tool.XMLMessage message2 = cm.Convert2Data(bo, body.structure);


        //    Tool.XMLUtil.exportXml(message2, @"C:\Broker\Production\OUT\123.xml");


        
        //}

        //[TestMethod]
        //public void TestConvertMethod2()
        //{
        //    ConvertModel cm = new ConvertModel();
        //    Tool.MessageContext body = new Tool.MessageContext();

        //    body.structure = cm.Convert("PO", true, cm.getRoot("PO"));

        //    Tool.XMLMessage message = Tool.XMLUtil.loadXML(@"C:\Broker\Production\OUT\IC0561_TP02_L050_PO_20140218_093340_64498351.xml", body);



        //    ServerImpl si = new ServerImpl();
            
        //    si.mergeData(message, "PO", "IN", "123");

        //    //Dictionary<string, string> param = new Dictionary<string, string>();
        //    //param.Add("messageID", "b845e198552847e6b7ce64c0daff7a9f");

        //    //com.portal.db.Model.MessageBody bo = (com.portal.db.Model.MessageBody)si.GetModel("message", typeof(com.portal.db.Model.MessageBody), param);

        //    //Tool.XMLMessage message2 = cm.Convert2Data(bo, body.structure);


        //    //Tool.XMLUtil.exportXml(message2, @"C:\Broker\Production\OUT\456.xml");



        //}
    }
}
