using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Threading;
using System.Threading.Tasks;
using Tool;

namespace Tool
{
    public class XMLUtil 
    {

        public static Tool.XMLMessage loadXML(string path, MessageContext body)
        {
            if (File.Exists(path))
            {

                XPathDocument xPathDocument = new XPathDocument(path);
                XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xPathNavigator.NameTable);

                try
                {

                    XPathNavigator node = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE", xmlNamespaceManager);
                    XMLMessage message = new XMLMessage();
                    MessageSender sSender = new MessageSender();
                    MessagePartner sPartner = new MessagePartner();

                    foreach (XPathNavigator sender in node.Select(@"SENDER", xmlNamespaceManager))
                    {
                        sSender.creationDateTime =
                           sender.SelectSingleNode(@"CREATIONDATETIME", xmlNamespaceManager)==null?
                           "":sender.SelectSingleNode(@"CREATIONDATETIME", xmlNamespaceManager).Value;
                        sSender.edi_location_code =
                            sender.SelectSingleNode(@"EDI_LOCATION_CODE", xmlNamespaceManager) == null ?
                           "" : sender.SelectSingleNode(@"EDI_LOCATION_CODE", xmlNamespaceManager).Value;
                        sSender.referenceID =
                            sender.SelectSingleNode(@"REFERENCEID", xmlNamespaceManager) == null ?
                           "" : sender.SelectSingleNode(@"REFERENCEID", xmlNamespaceManager).Value;
                        sSender.key =
                            sender.SelectSingleNode(@"MESSAGEID", xmlNamespaceManager) == null ?
                           "" : sender.SelectSingleNode(@"MESSAGEID", xmlNamespaceManager).Value;
                        sSender.messageName =
                            sender.SelectSingleNode(@"MESSAGE_NAME", xmlNamespaceManager) == null ?
                           "" : sender.SelectSingleNode(@"MESSAGE_NAME", xmlNamespaceManager).Value;
                        sSender.messageAlias =
                            sender.SelectSingleNode(@"MESSAGE_SHORT_NAME", xmlNamespaceManager) == null ?
                           "" : sender.SelectSingleNode(@"MESSAGE_SHORT_NAME", xmlNamespaceManager).Value;


                    }

                    foreach (XPathNavigator partner in node.Select(@"PARTNER_INFO", xmlNamespaceManager))
                    {


                        sPartner.vender_name =partner.SelectSingleNode(@"VENDOR_NAME", xmlNamespaceManager)==null?
                            "":partner.SelectSingleNode(@"VENDOR_NAME", xmlNamespaceManager).Value;
                        sPartner.vender_num =
                            partner.SelectSingleNode(@"VENDOR_NUM", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"VENDOR_NUM", xmlNamespaceManager).Value;
                        sPartner.vender_site =
                            partner.SelectSingleNode(@"VENDOR_SITE", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"VENDOR_SITE", xmlNamespaceManager).Value;
                        sPartner.vender_site_num =
                            partner.SelectSingleNode(@"VENDOR_SITE_NUM", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"VENDOR_SITE_NUM", xmlNamespaceManager).Value;
                        sPartner.street =
                            partner.SelectSingleNode(@"STREET", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"STREET", xmlNamespaceManager).Value;
                        sPartner.postal_code =
                            partner.SelectSingleNode(@"POSTAL_CODE", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"POSTAL_CODE", xmlNamespaceManager).Value;
                        sPartner.phone =
                            partner.SelectSingleNode(@"PHONE", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"PHONE", xmlNamespaceManager).Value;
                        sPartner.email =
                            partner.SelectSingleNode(@"EMAIL", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"EMAIL", xmlNamespaceManager).Value;
                        sPartner.duns_num =
                            partner.SelectSingleNode(@"DUNS_NUM", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"DUNS_NUM", xmlNamespaceManager).Value;
                        sPartner.contact_name =
                            partner.SelectSingleNode(@"CONTACT_NAME", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"CONTACT_NAME", xmlNamespaceManager).Value;
                        sPartner.city =
                            partner.SelectSingleNode(@"CITY", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"CITY", xmlNamespaceManager).Value;
                        sPartner.address =
                            partner.SelectSingleNode(@"ADDRESS1", xmlNamespaceManager) == null ?
                            "" : partner.SelectSingleNode(@"ADDRESS1", xmlNamespaceManager).Value;


                    }
                

                CreateBody(body, node.SelectSingleNode(@"MESSAGEBODY", xmlNamespaceManager), xmlNamespaceManager);



                message.sender = sSender;
                message.partner = sPartner;

                message.messagebody = body;

                return message;
                }
                catch (Exception e)
                {
                    throw e;

                }
            }
            else
            {
                return null;
            }
        }


        //构建整体
        private static void CreateBody(MessageContext body, XPathNavigator node, XmlNamespaceManager namespaceManager)
        {
            CreateData(body.structure, node,namespaceManager,ref body.data);
        }

        //通过迭代 构建数据结构（未考虑非根节点）
        private static Table CreateData(Structure body, XPathNavigator bodyNode, XmlNamespaceManager namespaceManager, ref Table data)
        {
            //CreateField(body, bodyNode.SelectSingleNode(body.name), data);

            //data.nodename = bodyNode.Name;

            foreach (Structure child in body.Childs)
            {
                Table childData = new Table();
                //data.childname = child.name;

                foreach (XPathNavigator node in bodyNode.Select(child.name, namespaceManager))
                {
                    Table brotherData = new Table();

                    //添加brotherData数据
                    //childData.brothername = node.Name;
                    childData.Brothers.Add(CreateField(child, node,namespaceManager, ref brotherData));

                    //添加brotherData子数据
                    CreateData(child, node,namespaceManager,ref brotherData);
                }
                
                data.Childs.Add(childData);
            }

            return data;
        }

        //构建节点内数据
        private static Table CreateField(Structure body, XPathNavigator bodyNode, XmlNamespaceManager namespaceManager, ref Table data)
        {
            

            foreach (Tool.Column field in body.Field)
            {

                if (field.isSys)
                {

                    XPathNavigator x = bodyNode.SelectSingleNode(field.xmlname, namespaceManager);

                    data.tableName = field.tablename;
                    data.addColumn(field);

                    if (!field.iskey & !field.isrefkey)
                    {
                        if (x != null)
                        {
                            data.addDataValue(field.fieldname, x.Value);
                        }
                        else
                        {
                            data.addDataValue(field.fieldname, "");
                        }
                    }
                    
                }
            }
            return data;
        }

       public static Tool.Message<ROSHeader> loadRosXML(string path)
       {

           XPathDocument xPathDocument = new XPathDocument(path);
           XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();

           Tool.Message<ROSHeader> ros = new Tool.Message<ROSHeader>();
           ros.headers = new List<ROSHeader>();

           MessageSender sender = new MessageSender();
           sender.creationDateTime = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/CREATIONDATETIME").Value;
           sender.edi_location_code = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/EDI_LOCATION_CODE").Value;
           sender.referenceID = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/REFERENCEID").Value;
           sender.key = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/MESSAGEID").Value;
           sender.messageName = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/MESSAGE_NAME").Value;
           sender.messageAlias = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/SENDER/MESSAGE_SHORT_NAME").Value;
           ros.sender = sender;

           MessagePartner partner = new MessagePartner();
           partner.vender_name = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/VENDOR_NAME").Value;
           partner.vender_num = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/VENDOR_NUM").Value;
           partner.vender_site = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/VENDOR_SITE").Value;
           partner.vender_site_num = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/VENDOR_SITE_NUM").Value;
           partner.street = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/STREET").Value;
           partner.postal_code = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/POSTAL_CODE").Value;
           partner.phone = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/PHONE").Value;
           partner.email = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/EMAIL").Value;
           partner.duns_num = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/DUNS_NUM").Value;
           partner.contact_name = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/CONTACT_NAME").Value;
           partner.city = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/CITY").Value;
           partner.address = xPathNavigator.SelectSingleNode(@"BMC_PORTAL_MESSAGE/PARTNER_INFO/ADDRESS1").Value;
           ros.partner = partner;

           XPathNodeIterator headerNodes = xPathNavigator.Select(@"BMC_PORTAL_MESSAGE/MESSAGEBODY/HEADER");
           foreach (XPathNavigator node in headerNodes)
           {
               ROSHeader header = new ROSHeader();
               header.item_no = node.SelectSingleNode(@"ITEM_NO").Value;
               header.description = node.SelectSingleNode(@"DESCRIPTION").Value;
               header.model = node.SelectSingleNode(@"MODEL").Value;
               header.glod_plan_flag = node.SelectSingleNode(@"GLOD_PLAN_FLAG").Value;
               header.category = node.SelectSingleNode(@"CAGEGORY").Value;
               header.buyer = node.SelectSingleNode(@"BUYER").Value;
               header.allocation_percent = node.SelectSingleNode(@"ALLOCATION_PERCENT").Value;
               header.lead_time = node.SelectSingleNode(@"LEAD_TIME").Value;
               header.stock_qty = node.SelectSingleNode(@"STOCK_QTY").Value;
               header.safe_stock = node.SelectSingleNode(@"SAFE_STOCK").Value;
               header.vmi_stock = node.SelectSingleNode(@"VMI_STOCK").Value;
               header.open_po_qty = node.SelectSingleNode(@"OPEN_PO_QTY").Value;

               XPathNodeIterator lineNodes = node.Select(@"LINES");
               foreach (XPathNavigator n in lineNodes)
               {
                   ROSLine line = new ROSLine();
                   line.demand_date = n.SelectSingleNode(@"DEMAND_DATE").Value;
                   line.demand_quantity = n.SelectSingleNode(@"DEMAND_QUANTITY").Value;
                   line.eta_qty = n.SelectSingleNode(@"ETA_QTY").Value;
                   line.etd_qty = n.SelectSingleNode(@"ETD_QTY").Value;
                   line.dio = n.SelectSingleNode(@"DIO").Value;
                   line.cum_eta = n.SelectSingleNode(@"CUM_ETA").Value;
                   line.po_no = n.SelectSingleNode(@"PO_NUMBER").Value;
                   line.shortage_qty = n.SelectSingleNode(@"SHORTAGE_QTY").Value;
                   header.lines.Add(line);
               }
               ros.headers.Add(header);
           }
           return ros;

       }

       public static ROSHeader createROSHeader(XmlNode HeaderNode)
       {

           try
           {

               ROSHeader ros = new ROSHeader();

               XmlNodeList xnl = HeaderNode.ChildNodes;

               Dictionary<String, String> dic = new Dictionary<string, string>();

               foreach (XmlNode xl in xnl)
               {

                   XmlElement xe = (XmlElement)xl;

                   if ("LINES".Equals(xe.Name.ToUpper()))
                   {

                       ros.lines.Add(createROSLine(xe));

                   }
                   else
                   {

                       dic.Add(xe.Name.ToUpper(), xe.InnerText);
                   }
               }

               ros.item_no = dic["ITEM_NO"].ToString();
               ros.description = dic["DESCRIPTION"].ToString();
               ros.model = dic["MODEL"].ToString();
               ros.glod_plan_flag = dic["GLOD_PLAN_FLAG"].ToString();
               ros.category = dic["CAGEGORY"].ToString();
               ros.buyer = dic["BUYER"].ToString();
               ros.allocation_percent = dic["ALLOCATION_PERCENT"].ToString();
               ros.lead_time = dic["LEAD_TIME"].ToString();
               ros.stock_qty = dic["STOCK_QTY"].ToString();
               ros.safe_stock = dic["SAFE_STOCK"].ToString();
               ros.vmi_stock = dic["VMI_STOCK"].ToString();
               ros.open_po_qty = dic["OPEN_PO_QTY"].ToString();

               return ros;
           }
           catch (Exception e) {

               throw (e);
           }
       }

       public static ROSLine createROSLine(XmlElement xnd)
       {
           try
           {
               ROSLine line = new ROSLine();

               XmlNodeList xnl = xnd.ChildNodes;

               Dictionary<String, String> dic = new Dictionary<string, string>();

               foreach (XmlNode xl in xnl)
               {

                   XmlElement xe = (XmlElement)xl;

                   dic.Add(xe.Name.ToUpper(), xe.InnerText);

               }

               line.demand_date = dic["DEMAND_DATE"];
               line.demand_quantity = dic["DEMAND_QUANTITY"];
               line.eta_qty = dic["ETA_QTY"];
               line.etd_qty = dic["ETD_QTY"];
               line.dio = dic["DIO"];
               line.cum_eta = dic["CUM_ETA"];
               line.po_no = dic["PO_NUMBER"];
               line.shortage_qty = dic["SHORTAGE_QTY"];
               return line;
           }
           catch (Exception e) {
               throw (e);
           }

       }

       public static Tool.Message<POHeader> loadPOXML(string path)
       {
           try
           {
               XmlDocument doc = new XmlDocument();

               Tool.Message<POHeader> po = new Tool.Message<POHeader>();
               List<Tool.POHeader> ph = new List<Tool.POHeader>();
               Tool.POHeader pheader = new Tool.POHeader();

               doc.Load(path);

               XmlNode xn = doc.SelectSingleNode("BMC_PORTAL_MESSAGE");

               XmlNodeList xnl = xn.ChildNodes;

               Dictionary<String, String> dic = new Dictionary<string, string>();

               foreach (XmlNode xl in xnl)
               {

                   XmlElement xe = (XmlElement)xl;

                   if ("SENDER".Equals(xe.Name.ToUpper()))
                   {

                       XmlNodeList node = xe.ChildNodes;
                       foreach (XmlNode x in node)
                       {
                           dic.Add(x.Name.ToUpper(), x.InnerText);
                       }

                   }
                   if ("PARTNER_INFO".Equals(xe.Name.ToUpper()))
                   {
                       XmlNodeList node = xe.ChildNodes;
                       foreach (XmlNode x in node)
                       {
                           dic.Add(x.Name.ToUpper(), x.InnerText);
                       }
                   }
                   //if ("PO_HEADER".Equals(xe.Name))
                   //{
                   //    pheader = createPOHeader(xe);

                   //}
                   if ("MESSAGEBODY".Equals(xe.Name.ToUpper()))
                   {
                       XmlNodeList node = xe.ChildNodes;
                       foreach (XmlNode x in node)
                       {
                           pheader = createPOHeader(x);
                       }

                   }
                   else
                   {

                       dic.Add(xe.Name.ToUpper(), xe.InnerText);
                   }


               }

               //po.headers = ph;

               MessageSender sender = new MessageSender();
               sender.creationDateTime = DateTime.Parse(dic["CREATIONDATETIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"); 
               //sender.edi_location_code = dic["EDI_Location_Code"].ToString();
               sender.ship_from = dic["SHIP_FROM"].ToString();
               sender.referenceID = dic["REFERENCEID"].ToString();
               sender.key = dic["MESSAGEID"].ToString();
               string name = dic["MESSAGE_NAME"].ToString();
               string[] str  = name.Split('_');
               str[str.Length-2] = str[str.Length-2]+"_"+pheader.buyer.ToString();
               int l = str.Length-1;

               string senderName = "";
               for (int i = 0; i < l;i++ )
               {
                   senderName += str[i] + "_";
               }
               sender.messageName = senderName + str[str.Length - 1];
               sender.messageAlias = dic["MESSAGE_SHORT_NAME"].ToString();
               

               po.sender = sender;

               MessagePartner partner = new MessagePartner();
               partner.vender_name = dic["VENDOR_NAME"].ToString();
               partner.vender_num = dic["VENDOR_NUM"].ToString();
               partner.vender_site = dic["VENDOR_SITE"].ToString();
               partner.vender_site_num = dic["VENDOR_SITE_NUM"].ToString();
               partner.street = dic["STREET"].ToString();
               partner.postal_code = dic["POSTAL_CODE"].ToString();
               partner.phone = dic["PHONE"].ToString();
               partner.email = dic["EMAIL"].ToString();
               partner.duns_num = dic["DUNS_NUM"].ToString();
               partner.contact_name = dic["CONTACT_NAME"].ToString();
               partner.city = dic["CITY"].ToString();
               partner.address = dic["ADDRESS1"].ToString();
               po.partner = partner;

               
               pheader.supplier = partner.vender_name;
               //pheader.supplier_address = partner.address;
               pheader.supplier_site = partner.vender_site;
               ph.Add(pheader);
               po.headers = ph;
               return po;

           }
           catch (Exception e) {
               throw (e);
           }
           
       }



       public static POHeader createPOHeader(XmlNode HeaderNode)
       {
           try
           {
               POHeader ph = new POHeader();

               XmlNodeList xnl = HeaderNode.ChildNodes;

               Dictionary<String, String> dic = new Dictionary<string, string>();
               String curr = "";
               foreach (XmlNode xl in xnl)
               {

                   XmlElement xe = (XmlElement)xl;

                   if ("CURRENCY".Equals(xe.Name.ToUpper()))
                   {
                       curr = xe.InnerText;
                   }
                   if ("LINES".Equals(xe.Name.ToUpper()))
                   {
                       ph.polines.Add(createPOLine(xe, curr));
                   }
                   else
                   {
                       dic.Add(xe.Name.ToUpper(), xe.InnerText);
                   }
               }
               ph.buyer = dic["BUYER"];
               ph.po_date = dic["PO_DATE"].ToString();
               ph.po_number = dic["PO_NUMBER"].ToString();
               ph.po_type = dic["PO_TYPE"].ToString();
               ph.your_reference = dic["YOUR_REFERNCE"].ToString();
               ph.desc = dic["PO_DESCRIPTION"].ToString();
               ph.delivery_location = dic["DELIVERY_LOCATION"].ToString();
               ph.terms_of_delivery = dic["TERMS_OF_DELIVERY"].ToString();
               ph.delivery_address = dic["DELIVERY_ADDRESS"].ToString();
               ph.supplier_address = dic["SUPPLIER_ADDRESS"].ToString();
               ph.term_of_payment = dic["TERM_OF_PAYMENT"].ToString();
               ph.currency = curr;
               return ph;
           }
           catch (Exception e) {
               throw (e);
           }
       }

       public static POLine createPOLine(XmlElement xnd,string curr)
       {
           try
           {
               POLine line = new POLine();

               XmlNodeList xnl = xnd.ChildNodes;

               Dictionary<String, String> dic = new Dictionary<string, string>();

               foreach (XmlNode xl in xnl)
               {

                   XmlElement xe = (XmlElement)xl;

                   dic.Add(xe.Name.ToUpper(), xe.InnerText);

               }


               line.lineNo = dic["LINENO"];
               line.item_no = dic["ITEM_NO"];
               line.desc = dic["DESCRIPTION"];
               line.request_qty = dic["REQUESTED_QTY"];
               line.request_delivery_date = dic["REQUESTED_DELIVERY_DATE"];
               line.schedule_delivery_date = dic["SCHEDULE_DELIVERY_DATE"];
               line.schedule_delivery_qty = dic["SCHEDULE_DELIVERY_QTY"];
               line.unit_price = dic["UNIT_PRICE"];
               line.price_unit = dic["PRICE_UNIT"];
               line.line_item_tatoal_amount = dic["LINE_ITEM_TATOAL_AMOUNT"];               
               line.curr = curr;

               return line;
           }
           catch (Exception e) {

               throw (e);
           }
       }


       public static void exportXml(Tool.XMLMessage mes,string newXmlFile)
       {
           try
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.PreserveWhitespace = false;
               XmlElement xmlroot = xmlDoc.CreateElement("BMC_PORTAL_MESSAGE");


               XmlElement sender = xmlDoc.CreateElement("SENDER");
               XmlElement partner = xmlDoc.CreateElement("PARTNER_INFO");

               XmlElement MessageID = xmlDoc.CreateElement("MESSAGEID");
               MessageID.InnerText = mes.sender.key;
               sender.AppendChild(MessageID);

               XmlElement reference = xmlDoc.CreateElement("REFERENCEID");
               reference.InnerText = mes.sender.referenceID;
               sender.AppendChild(reference);

               XmlElement rosName = xmlDoc.CreateElement("MESSAGE_NAME");
               rosName.InnerText = mes.sender.messageName.Replace("&", " ");
               sender.AppendChild(rosName);

               XmlElement ROS_SHORT_NAME = xmlDoc.CreateElement("MESSAGE_SHORT_NAME");
               ROS_SHORT_NAME.InnerText = mes.sender.messageAlias;
               sender.AppendChild(ROS_SHORT_NAME);

               XmlElement creationDateTime = xmlDoc.CreateElement("CREATIONDATETIME");
               creationDateTime.InnerText = mes.sender.creationDateTime.ToString();
               sender.AppendChild(creationDateTime);

               XmlElement vendorEDICode = xmlDoc.CreateElement("EDI_LOCATION_CODE");
               vendorEDICode.InnerText = mes.sender.edi_location_code;
               sender.AppendChild(vendorEDICode);

               XmlElement vendorName = xmlDoc.CreateElement("VENDOR_NAME");
               vendorName.InnerText = mes.partner.vender_name.Replace("&", " "); ;
               partner.AppendChild(vendorName);

               XmlElement vender_num = xmlDoc.CreateElement("VENDOR_NUM");
               vender_num.InnerText = mes.partner.vender_num;
               partner.AppendChild(vender_num);

               XmlElement vendorSite = xmlDoc.CreateElement("VENDOR_SITE");
               vendorSite.InnerText = mes.partner.vender_site;
               partner.AppendChild(vendorSite);

               XmlElement vender_site_num = xmlDoc.CreateElement("VENDOR_SITE_NUM");
               vender_site_num.InnerText = mes.partner.vender_site_num;
               partner.AppendChild(vender_site_num);

               XmlElement duns_num = xmlDoc.CreateElement("DUNS_NUM");
               duns_num.InnerText = mes.partner.duns_num;
               partner.AppendChild(duns_num);

               XmlElement Contact_name = xmlDoc.CreateElement("CONTACT_NAME");
               Contact_name.InnerText = mes.partner.contact_name.Replace("&", " "); ;
               partner.AppendChild(Contact_name);


               XmlElement email = xmlDoc.CreateElement("EMAIL");
               email.InnerText = mes.partner.email.Replace("&", " "); ;
               partner.AppendChild(email);

               XmlElement phone = xmlDoc.CreateElement("PHONE");
               phone.InnerText = mes.partner.phone;
               partner.AppendChild(phone);

               XmlElement Address1 = xmlDoc.CreateElement("ADDRESS1");
               Address1.InnerText = mes.partner.address.Replace("&", " "); ;
               partner.AppendChild(Address1);

               XmlElement street = xmlDoc.CreateElement("STREET");
               street.InnerText = mes.partner.street.Replace("&", " "); ;
               partner.AppendChild(street);


               XmlElement city = xmlDoc.CreateElement("CITY");
               city.InnerText = mes.partner.city.Replace("&", " "); ;
               partner.AppendChild(city);

               XmlElement postal_code = xmlDoc.CreateElement("POSTAL_CODE");
               postal_code.InnerText = mes.partner.postal_code;
               partner.AppendChild(postal_code);

               xmlroot.AppendChild(sender);
               xmlroot.AppendChild(partner);

               XmlElement MessageBody = xmlDoc.CreateElement("MESSAGEBODY");

               MessageBody = exportBody(xmlDoc, MessageBody, mes.messagebody.data);


               //foreach (Table data in mes.messagebody.data)
               //{
               //    XmlElement node = xmlDoc.CreateElement(data.tableName);


               //    MessageBody.AppendChild(node);
               //}

               xmlroot.AppendChild(MessageBody);
               xmlDoc.AppendChild(xmlroot);

               ////FileInfo info = new FileInfo(newXmlFile);
               ////if (File.Exists(newXmlFile))
               ////{
               ////    File.Delete(newXmlFile);
               ////}
               xmlDoc.PreserveWhitespace = true;
               xmlDoc.Save(newXmlFile);
           }
           catch (Exception e)
           {
               throw (e);
           }

       }

       private static XmlElement exportBody(XmlDocument xmlDoc, XmlElement root, Tool.Table mes)
       {
           

           if(!mes.isNullColumn){
            
               foreach(Tool.Column col in mes.getColumns()){

                   if (!col.isXml ) continue;

                   XmlElement model = xmlDoc.CreateElement(col.xmlname);
                   model.InnerText = mes.getDataValue(col.fieldname);
                   root.AppendChild(model);
               }
           
           }

           foreach (Tool.Table brother in mes.Brothers) {
               XmlElement xml = xmlDoc.CreateElement(brother.xmlname);
              root.AppendChild(exportBody(xmlDoc,xml, brother));

           }
           foreach (Tool.Table child in mes.Childs) {

              exportBody(xmlDoc,root, child);
           }


           return root;

          // mes.messagebody.data;
       
       }





       public static void exportRosXml(Tool.Message<ROSHeader> ros, string newXmlFile)
       {
           try
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.PreserveWhitespace = false;
               XmlElement xmlroot = xmlDoc.CreateElement("BMC_PORTAL_MESSAGE");


               XmlElement sender = xmlDoc.CreateElement("SENDER");
               XmlElement partner = xmlDoc.CreateElement("PARTNER_INFO");

               XmlElement MessageID = xmlDoc.CreateElement("MESSAGEID");
               MessageID.InnerText = ros.sender.key;
               sender.AppendChild(MessageID);

               XmlElement reference = xmlDoc.CreateElement("REFERENCEID");
               reference.InnerText = ros.sender.referenceID;
               sender.AppendChild(reference);

               XmlElement rosName = xmlDoc.CreateElement("MESSAGE_NAME");
               rosName.InnerText = ros.sender.messageName.Replace("&"," ");
               sender.AppendChild(rosName);

               XmlElement ROS_SHORT_NAME = xmlDoc.CreateElement("MESSAGE_SHORT_NAME");
               ROS_SHORT_NAME.InnerText = ros.sender.messageAlias;
               sender.AppendChild(ROS_SHORT_NAME);

               XmlElement creationDateTime = xmlDoc.CreateElement("CREATIONDATETIME");
               creationDateTime.InnerText = ros.sender.creationDateTime.ToString();
               sender.AppendChild(creationDateTime);

               XmlElement vendorEDICode = xmlDoc.CreateElement("EDI_LOCATION_CODE");
               vendorEDICode.InnerText = ros.sender.edi_location_code;
               sender.AppendChild(vendorEDICode);

               XmlElement vendorName = xmlDoc.CreateElement("VENDOR_NAME");
               vendorName.InnerText = ros.partner.vender_name.Replace("&", " "); ;
               partner.AppendChild(vendorName);

               XmlElement vender_num = xmlDoc.CreateElement("VENDOR_NUM");
               vender_num.InnerText = ros.partner.vender_num;
               partner.AppendChild(vender_num);

               XmlElement vendorSite = xmlDoc.CreateElement("VENDOR_SITE");
               vendorSite.InnerText = ros.partner.vender_site;
               partner.AppendChild(vendorSite);

               XmlElement vender_site_num = xmlDoc.CreateElement("VENDOR_SITE_NUM");
               vender_site_num.InnerText = ros.partner.vender_site_num;
               partner.AppendChild(vender_site_num);

               XmlElement duns_num = xmlDoc.CreateElement("DUNS_NUM");
               duns_num.InnerText = ros.partner.duns_num;
               partner.AppendChild(duns_num);

               XmlElement Contact_name = xmlDoc.CreateElement("CONTACT_NAME");
               Contact_name.InnerText = ros.partner.contact_name.Replace("&", " "); ;
               partner.AppendChild(Contact_name);


               XmlElement email = xmlDoc.CreateElement("EMAIL");
               email.InnerText = ros.partner.email.Replace("&", " "); ;
               partner.AppendChild(email);

               XmlElement phone = xmlDoc.CreateElement("PHONE");
               phone.InnerText = ros.partner.phone;
               partner.AppendChild(phone);

               XmlElement Address1 = xmlDoc.CreateElement("ADDRESS1");
               Address1.InnerText = ros.partner.address.Replace("&", " "); ;
               partner.AppendChild(Address1);

               XmlElement street = xmlDoc.CreateElement("STREET");
               street.InnerText = ros.partner.street.Replace("&", " "); ;
               partner.AppendChild(street);


               XmlElement city = xmlDoc.CreateElement("CITY");
               city.InnerText = ros.partner.city.Replace("&", " "); ;
               partner.AppendChild(city);

               XmlElement postal_code = xmlDoc.CreateElement("POSTAL_CODE");
               postal_code.InnerText = ros.partner.postal_code;
               partner.AppendChild(postal_code);

               xmlroot.AppendChild(sender);
               xmlroot.AppendChild(partner);

               XmlElement MessageBody = xmlDoc.CreateElement("MESSAGEBODY");

               foreach (ROSHeader rosheader in ros.headers)
               {

                   XmlElement header = xmlDoc.CreateElement("HEADER");

                   //XmlNode header = xmlDoc.CreateNode(XmlNodeType.Element, "HEADER",null);

                   XmlElement item_no = xmlDoc.CreateElement("ITEM_NO");

                   //XmlNode item_no = xmlDoc.CreateNode(XmlNodeType.Element, "ITEM_NO",null);
                   item_no.InnerText = rosheader.item_no.ToString();
                   header.AppendChild(item_no);

                   XmlElement desc = xmlDoc.CreateElement("DESCRIPTION");
                   desc.InnerText = rosheader.description.ToString().Replace("&", " "); 
                   header.AppendChild(desc);

                   XmlElement model = xmlDoc.CreateElement("MODEL");
                   model.InnerText = rosheader.model;
                   header.AppendChild(model);

                   XmlElement GLOD_PLAN_FLAG = xmlDoc.CreateElement("GLOD_PLAN_FLAG");
                   GLOD_PLAN_FLAG.InnerText = rosheader.glod_plan_flag;
                   header.AppendChild(GLOD_PLAN_FLAG);

                   XmlElement category = xmlDoc.CreateElement("CAGEGORY");
                   category.InnerText = rosheader.category;
                   header.AppendChild(category);

                   //XmlElement vendorName = xmlDoc.CreateElement("VENDOR_NAME");
                   //vendorName.InnerText = rosheader.vendor_name;
                   //header.AppendChild(vendorName);

                   //XmlElement vendorSite = xmlDoc.CreateElement("VENDOR_SITE");
                   //vendorSite.InnerText = rosheader.vendor_site;
                   //header.AppendChild(vendorSite);

                   XmlElement buyer = xmlDoc.CreateElement("BUYER");
                   buyer.InnerText = rosheader.buyer.Replace("&", " "); 
                   header.AppendChild(buyer);

                   XmlElement alloction = xmlDoc.CreateElement("ALLOCATION_PERCENT");
                   alloction.InnerText = rosheader.allocation_percent;
                   header.AppendChild(alloction);

                   XmlElement leadTime = xmlDoc.CreateElement("LEAD_TIME");
                   leadTime.InnerText = rosheader.lead_time;
                   header.AppendChild(leadTime);

                   XmlElement stockQty = xmlDoc.CreateElement("STOCK_QTY");
                   stockQty.InnerText = rosheader.stock_qty;
                   header.AppendChild(stockQty);

                   XmlElement safeStock = xmlDoc.CreateElement("SAFE_STOCK");
                   safeStock.InnerText = rosheader.safe_stock;
                   header.AppendChild(safeStock);

                   //XmlElement po_No = xmlDoc.CreateElement("PO_NUMBER");
                   //po_No.InnerText = rosheader.po_number;
                   //header.AppendChild(po_No);

                   XmlElement open_po_qty = xmlDoc.CreateElement("OPEN_PO_QTY");
                   open_po_qty.InnerText = rosheader.open_po_qty;
                   header.AppendChild(open_po_qty);

                   XmlElement VMI_STOCK = xmlDoc.CreateElement("VMI_STOCK");
                   VMI_STOCK.InnerText = rosheader.vmi_stock;
                   header.AppendChild(VMI_STOCK);

                   XmlElement UPDATE = xmlDoc.CreateElement("UPDATE");
                   UPDATE.InnerText = rosheader.updateflag;
                   header.AppendChild(UPDATE);

                   foreach (ROSLine line in rosheader.lines)
                   {

                       XmlElement lines = xmlDoc.CreateElement("LINES");

                       XmlElement demandDate = xmlDoc.CreateElement("DEMAND_DATE");
                       demandDate.InnerText = line.demand_date;
                       lines.AppendChild(demandDate);

                       XmlElement demandQuantity = xmlDoc.CreateElement("DEMAND_QUANTITY");
                       demandQuantity.InnerText = line.demand_quantity;
                       lines.AppendChild(demandQuantity);

                       XmlElement etaQty = xmlDoc.CreateElement("ETA_QTY");
                       etaQty.InnerText = line.eta_qty;
                       lines.AppendChild(etaQty);

                       XmlElement etdQty = xmlDoc.CreateElement("ETD_QTY");
                       etdQty.InnerText = line.etd_qty;
                       lines.AppendChild(etdQty);

                       XmlElement dio = xmlDoc.CreateElement("DIO");
                       dio.InnerText = line.dio;
                       lines.AppendChild(dio);

                       XmlElement shortageQty = xmlDoc.CreateElement("SHORTAGE_QTY");
                       shortageQty.InnerText = line.shortage_qty;
                       lines.AppendChild(shortageQty);

                       XmlElement cumEta = xmlDoc.CreateElement("CUM_ETA");
                       cumEta.InnerText = line.cum_eta;
                       lines.AppendChild(cumEta);


                       XmlElement po_No = xmlDoc.CreateElement("PO_NUMBER");
                       po_No.InnerText = rosheader.po_number;
                       lines.AppendChild(po_No);

                       header.AppendChild(lines);

                   }
                   MessageBody.AppendChild(header);
               }

               xmlroot.AppendChild(MessageBody);
               xmlDoc.AppendChild(xmlroot);

               //FileInfo info = new FileInfo(newXmlFile);
               //if (File.Exists(newXmlFile))
               //{
               //    File.Delete(newXmlFile);
               //}
               xmlDoc.PreserveWhitespace = true;
               xmlDoc.Save(newXmlFile);
           }
           catch (Exception e) {
               throw (e);
           }
       
       }



       public static void exportPoXml(Tool.Message<POHeader> po, string newXmlFile)
       {
           try
           {

               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.PreserveWhitespace = false;
               XmlElement xmlroot = xmlDoc.CreateElement("BMC_PORTAL_MESSAGE");


               XmlElement sender = xmlDoc.CreateElement("SENDER");
               XmlElement partner = xmlDoc.CreateElement("PARTNER_INFO");



               XmlElement MessageID = xmlDoc.CreateElement("MESSAGEID");
               MessageID.InnerText = po.sender.key;
               sender.AppendChild(MessageID);
              
               XmlElement reference = xmlDoc.CreateElement("REFERENCEID");
               reference.InnerText = po.sender.referenceID;
               sender.AppendChild(reference);

               XmlElement rosName = xmlDoc.CreateElement("MESSAGE_NAME");
               rosName.InnerText = po.sender.messageName.Replace("&", " ");
               sender.AppendChild(rosName);

               XmlElement ROS_SHORT_NAME = xmlDoc.CreateElement("MESSAGE_SHORT_NAME");
               ROS_SHORT_NAME.InnerText = po.sender.messageAlias;
               sender.AppendChild(ROS_SHORT_NAME);

               XmlElement creationDateTime = xmlDoc.CreateElement("CREATIONDATETIIME");
               creationDateTime.InnerText = po.sender.creationDateTime.ToString();
               sender.AppendChild(creationDateTime);

               XmlElement vendorEDICode = xmlDoc.CreateElement("SHIP_FROM");
               vendorEDICode.InnerText = po.sender.edi_location_code;
               sender.AppendChild(vendorEDICode);

               XmlElement vendorName = xmlDoc.CreateElement("VENDOR_NAME");
               vendorName.InnerText = po.partner.vender_name.Replace("&", " "); ;
               partner.AppendChild(vendorName);

               XmlElement vender_num = xmlDoc.CreateElement("VENDOR_NUM");
               vender_num.InnerText = po.partner.vender_num;
               partner.AppendChild(vender_num);

               XmlElement vendorSite = xmlDoc.CreateElement("VENDOR_SITE");
               vendorSite.InnerText = po.partner.vender_site.Replace("&", " "); ;
               partner.AppendChild(vendorSite);

               XmlElement vender_site_num = xmlDoc.CreateElement("VENDOR_SITE_NUM");
               vender_site_num.InnerText = po.partner.vender_site_num;
               partner.AppendChild(vender_site_num);

               XmlElement duns_num = xmlDoc.CreateElement("DUNS_NUM");
               duns_num.InnerText = po.partner.duns_num;
               partner.AppendChild(duns_num);

               XmlElement Contact_name = xmlDoc.CreateElement("CONCATE_NAME");
               Contact_name.InnerText = po.partner.contact_name.Replace("&", " "); ;
               partner.AppendChild(Contact_name);


               XmlElement email = xmlDoc.CreateElement("EMAIL");
               email.InnerText = po.partner.email;
               partner.AppendChild(email);

               XmlElement phone = xmlDoc.CreateElement("PHONE");
               phone.InnerText = po.partner.phone;
               partner.AppendChild(phone);

               XmlElement Address1 = xmlDoc.CreateElement("ADDRESS1");
               Address1.InnerText = po.partner.address.Replace("&", " "); ;
               partner.AppendChild(Address1);

               XmlElement street = xmlDoc.CreateElement("STREET");
               street.InnerText = po.partner.vender_num;
               partner.AppendChild(vendorSite);


               XmlElement city = xmlDoc.CreateElement("CITY");
               city.InnerText = po.partner.city;
               partner.AppendChild(city);

               XmlElement postal_code = xmlDoc.CreateElement("POSTAL_CODE");
               postal_code.InnerText = po.partner.postal_code;
               partner.AppendChild(postal_code);

               xmlroot.AppendChild(sender);
               xmlroot.AppendChild(partner);

               XmlElement MessageBody = xmlDoc.CreateElement("MESSAGEBODY");

               foreach (POHeader poheader in po.headers)
               {

                   XmlElement header = xmlDoc.CreateElement("PO_HEADER");

                   //XmlNode header = xmlDoc.CreateNode(XmlNodeType.Element, "HEADER",null);

                   XmlElement Delivery_address = xmlDoc.CreateElement("DELIVERY_ADDREDD");
                   Delivery_address.InnerText = poheader.delivery_address.ToString().Replace("&", " "); ;
                   header.AppendChild(Delivery_address);

                   XmlElement Supplier_address = xmlDoc.CreateElement("SUPPLIER_ADDREDD");
                   Supplier_address.InnerText = poheader.supplier_address.ToString().Replace("&", " "); ;
                   header.AppendChild(Supplier_address);



                   XmlElement Buyer = xmlDoc.CreateElement("BUYER");
                   Buyer.InnerText = poheader.buyer.ToString().Replace("&", " "); ;
                   header.AppendChild(Buyer);

                   XmlElement PO_Date = xmlDoc.CreateElement("PO_DATE");
                   PO_Date.InnerText = poheader.po_date.ToString();
                   header.AppendChild(PO_Date);

                   XmlElement PO_Number = xmlDoc.CreateElement("PO_NUMBER");
                   PO_Number.InnerText = poheader.po_number;
                   header.AppendChild(PO_Number);

                   XmlElement PO_Type = xmlDoc.CreateElement("PO_TYPE");
                   PO_Type.InnerText = poheader.po_type;
                   header.AppendChild(PO_Type);

                   XmlElement Your_Refernce = xmlDoc.CreateElement("YOUR_REFERENCE");
                   Your_Refernce.InnerText = poheader.your_reference.Replace("&", " "); ;
                   header.AppendChild(Your_Refernce);

                   XmlElement Description = xmlDoc.CreateElement("PO_DESCRIPTION");
                   Description.InnerText = poheader.desc.Replace("&", " "); ;
                   header.AppendChild(Description);

                   XmlElement Delivery_Location = xmlDoc.CreateElement("DELIVERY_LOCATION");
                   Delivery_Location.InnerText = poheader.delivery_location.Replace("&", " "); ;
                   header.AppendChild(Delivery_Location);

                   XmlElement Currency = xmlDoc.CreateElement("CURRENCY");
                   Currency.InnerText = poheader.currency.Replace("&", " "); ;
                   header.AppendChild(Currency);

                   XmlElement Terms_of_Delivery = xmlDoc.CreateElement("TERMS_OF_DELIVERY");
                   Terms_of_Delivery.InnerText = poheader.terms_of_delivery;
                   header.AppendChild(Terms_of_Delivery);

                   XmlElement TERM_OF_PAYMENT = xmlDoc.CreateElement("TERM_OF_PAYMENT");
                   TERM_OF_PAYMENT.InnerText = poheader.term_of_payment;
                   header.AppendChild(TERM_OF_PAYMENT);

                   


                   foreach (POLine line in poheader.polines)
                   {

                       XmlElement lines = xmlDoc.CreateElement("LINES");

                       XmlElement LineNo = xmlDoc.CreateElement("LINENO");
                       LineNo.InnerText = line.lineNo;
                       lines.AppendChild(LineNo);

                       XmlElement Item_No = xmlDoc.CreateElement("ITEM_NO");
                       Item_No.InnerText = line.item_no;
                       lines.AppendChild(Item_No);

                       XmlElement Desc = xmlDoc.CreateElement("DESCRIPTION");
                       Desc.InnerText = line.desc.Replace("&", " "); ;
                       lines.AppendChild(Desc);

                       XmlElement Requested_QTY = xmlDoc.CreateElement("REQUESTED_QTY");
                       Requested_QTY.InnerText = line.request_qty;
                       lines.AppendChild(Requested_QTY);

                       XmlElement Requested_Delivery_Date = xmlDoc.CreateElement("REQUESTED_DELIVERY_DATE");
                       Requested_Delivery_Date.InnerText = line.request_delivery_date;
                       lines.AppendChild(Requested_Delivery_Date);

                       XmlElement Unit_Price = xmlDoc.CreateElement("UNIT_PRICE");
                       Unit_Price.InnerText = line.unit_price;
                       lines.AppendChild(Unit_Price);

                       XmlElement Price_Unit = xmlDoc.CreateElement("PRICE_UNIT");
                       Price_Unit.InnerText = line.price_unit;
                       lines.AppendChild(Price_Unit);

                       XmlElement Line_Item_Tatoal_Amount = xmlDoc.CreateElement("LINE_ITEM_TATOAL_AMOUNT");
                       Line_Item_Tatoal_Amount.InnerText = line.line_item_tatoal_amount;
                       lines.AppendChild(Line_Item_Tatoal_Amount);

                       XmlElement Schedule_Delivery_Date = xmlDoc.CreateElement("SCHEDULE_DELIVERY_DATE");
                       Schedule_Delivery_Date.InnerText = line.schedule_delivery_date;
                       lines.AppendChild(Schedule_Delivery_Date);

                       XmlElement Schedule_Arrive_Date = xmlDoc.CreateElement("SCHEDULE_ARRIVE_DATE");
                       Schedule_Arrive_Date.InnerText = line.Schedule_Arrive_Date;
                       lines.AppendChild(Schedule_Arrive_Date);

                       XmlElement Schedule_Delivery_Qty = xmlDoc.CreateElement("SCHEDULE_DELIVERY_QTY");
                       Schedule_Delivery_Qty.InnerText = line.schedule_delivery_qty;
                       lines.AppendChild(Schedule_Delivery_Qty);

                       header.AppendChild(lines);

                   }
                   MessageBody.AppendChild(header);
               }

               xmlroot.AppendChild(MessageBody);
               xmlDoc.AppendChild(xmlroot);
               xmlDoc.PreserveWhitespace = true;
               xmlDoc.Save(newXmlFile);

           }
           catch (Exception e) {
               throw (e);
           }
       }
    
    }
}