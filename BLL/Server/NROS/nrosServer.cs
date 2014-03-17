using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool;
using com.portal.db.BLL;
using Model = com.portal.db.Model;

namespace com.portal.db.BLL.NROS
{
    public class nrosServer
    {


        public Model.MessageBody saveROSData(Tool.Message<ROSHeader> xmlModel, string messageType, string flag, string filename,string exportName)
        {

            BLL.MessageBody MessageServer = new BLL.MessageBody();
            RosHeader rosHeaderMessageServer = new RosHeader();
            RosLines rosLineMessageServer = new RosLines();

            Model.MessageBody model = new Model.MessageBody();

            //try
            //{
                

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = filename;
                model.segment5 = exportName;
                model.messageType = messageType;

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("IN" == flag)
                {
                    model.status = "Unread";
                    model.segment2 = "IN";
                    model.notes = "Creation";
                }

                model.segment4 = "F";
                MessageServer.Add(model);
                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [segment5] = '" + exportName + "'")[0];

                foreach (ROSHeader header in xmlModel.headers)
                {
                    Model.RosHeader hmodel = new Model.RosHeader();
                    hmodel.OpenPoQty = header.open_po_qty;
                    hmodel.PoNumber = header.po_number;
                    hmodel.Glod_plan_flag = header.glod_plan_flag;
                    hmodel.RosAllocationPercent = header.allocation_percent;
                    hmodel.RosBuyer = header.buyer;
                    hmodel.RosCategory = header.category;
                    hmodel.RosDesc = header.description;
                    hmodel.RosItemNO = header.item_no;
                    hmodel.RosLeadTime = header.lead_time;
                    hmodel.RosModel = header.model;
                    hmodel.RosSafeStock = header.safe_stock;
                    hmodel.RosStockQty = header.stock_qty;
                    hmodel.MessageBodyID = model.messageID;
                    hmodel.UpdateFlag = header.updateflag;

                    rosHeaderMessageServer.Add(hmodel);
                    hmodel = rosHeaderMessageServer.GetModelList("RosItemNO = '" + header.item_no + "' and messageBodyID ='" + model.messageID + "'")[0];

                    foreach (ROSLine line in header.lines)
                    {
                        Model.RosLines lmodel = new Model.RosLines();
                        lmodel.RosDemandDate = line.demand_date;
                        lmodel.RosDemandQuantity = line.demand_quantity;
                        lmodel.RosDio = line.dio;
                        lmodel.RosEtaQty = line.eta_qty;
                        lmodel.RosEtdQty = line.etd_qty;
                        lmodel.RosCumEta = line.cum_eta;
                        lmodel.RosShortageQty = line.shortage_qty;
                        lmodel.RosHeaderID = hmodel.RosHeaderID;
                        lmodel.PO_NO = line.po_no;
                        rosLineMessageServer.Add(lmodel);
                    }
                }

                //model.segment4 = "T";
                //MessageServer.Update(model);

                return MessageServer.GetModelList("MessageType = '" + messageType + "' and [segment5] = '" + exportName + "'")[0];

            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

        }

        public Model.MessageBody updateROSData(Tool.Message<ROSHeader> xmlModel, string messageType, string flag, string key)
        {
            BLL.MessageBody MessageServer = new BLL.MessageBody();
            RosHeader rosHeaderMessageServer = new RosHeader();
            RosLines rosLineMessageServer = new RosLines();
            Model.MessageBody model = null;
            try
            {

                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + key + "'")[0];

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("IN" == flag)
                {
                    model.status = "Unread";
                    model.notes = "Creation";
                    model.segment2 = "IN";
                }

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = xmlModel.filename;

                model.messageType = messageType;

                model.segment4 = "F";
                MessageServer.Update(model);

                foreach (ROSHeader header in xmlModel.headers)
                {
                    if (header.updateflag == "Y")
                    {

                        Model.RosHeader hmodel = new Model.RosHeader();
                        List<Model.RosHeader> rlist = rosHeaderMessageServer.GetModelList("RosItemNO = '" + header.item_no + "' and messageBodyID ='" + model.messageID + "'");
                        if (rlist.Count > 0)
                        {
                            hmodel = rlist[0];
                            hmodel.OpenPoQty = header.open_po_qty;
                            hmodel.PoNumber = header.po_number;
                            hmodel.Glod_plan_flag = header.glod_plan_flag;
                            hmodel.RosAllocationPercent = header.allocation_percent;
                            hmodel.RosBuyer = header.buyer;
                            hmodel.RosCategory = header.category;
                            hmodel.RosDesc = header.description;
                            hmodel.RosLeadTime = header.lead_time;
                            hmodel.RosModel = header.model;
                            hmodel.RosSafeStock = header.safe_stock;
                            hmodel.RosStockQty = header.stock_qty;
                            hmodel.VmiStock = header.vmi_stock;
                            hmodel.UpdateFlag = header.updateflag;
                            rosHeaderMessageServer.Update(hmodel);

                        }
                        //else
                        //{
                        //    hmodel = new Model.RosHeader();
                        //    hmodel.OpenPoQty = header.open_po_qty;
                        //    hmodel.PoNumber = header.po_number;
                        //    hmodel.Glod_plan_flag = header.glod_plan_flag;
                        //    hmodel.RosAllocationPercent = header.allocation_percent;
                        //    hmodel.RosBuyer = header.buyer;
                        //    hmodel.RosCategory = header.category;
                        //    hmodel.RosDesc = header.description;
                        //    hmodel.RosLeadTime = header.lead_time;
                        //    hmodel.RosModel = header.model;
                        //    hmodel.RosSafeStock = header.safe_stock;
                        //    hmodel.RosStockQty = header.stock_qty;
                        //    hmodel.RosItemNO = header.item_no;
                        //    hmodel.MessageBodyID = model.messageID;
                        //    hmodel.UpdateFlag = header.updateflag;

                        //    rosHeaderMessageServer.Add(hmodel);
                        //}

                        hmodel = rosHeaderMessageServer.GetModelList("RosItemNO = '" + header.item_no + "' and messageBodyID ='" + model.messageID + "'")[0];

                        foreach (ROSLine line in header.lines)
                        {
                            Model.RosLines lmodel = new Model.RosLines();
                            List<Model.RosLines> list = rosLineMessageServer.GetModelList("RosDemandDate = '" + line.demand_date + "' and RosHeaderID ='" + hmodel.RosHeaderID + "'");
                            if (list.Count > 0)
                            {
                                lmodel = list[0];
                                //lmodel.RosDemandDate = DateTime.Parse(line.demand_date);
                                lmodel.RosDemandQuantity = line.demand_quantity;
                                lmodel.RosDio = line.dio;
                                lmodel.RosEtaQty = line.eta_qty;
                                lmodel.RosEtdQty = line.etd_qty;
                                lmodel.RosCumEta = line.cum_eta;
                                lmodel.RosShortageQty = line.shortage_qty;
                                //lmodel.RosHeaderID = hmodel.RosHeaderID;
                                lmodel.PO_NO = line.po_no;
                                rosLineMessageServer.Update(lmodel);
                            }
                            else
                            {
                                //lmodel = rosLineMessageServer.GetModelList("RosDemandDate = '" + line.demand_date + "' and RosHeaderID ='" + hmodel.RosHeaderID + "'")[0];
                                lmodel.RosDemandDate = line.demand_date;
                                lmodel.RosDemandQuantity = line.demand_quantity;
                                lmodel.RosDio = line.dio;
                                lmodel.RosEtaQty = line.eta_qty;
                                lmodel.RosEtdQty = line.etd_qty;
                                lmodel.RosCumEta = line.cum_eta;
                                lmodel.RosShortageQty = line.shortage_qty;
                                lmodel.RosHeaderID = hmodel.RosHeaderID;
                                lmodel.PO_NO = line.po_no;
                                rosLineMessageServer.Add(lmodel);
                            }
                        }
                    }
                }

                // model.segment4 = "T";
                //MessageServer.Update(model);

            }
            catch (Exception e)
            {
                throw e;
            }


            return MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + key + "'")[0];
        }



        public Boolean mergeROSData(Tool.Message<ROSHeader> xmlModel, string messageType, string flag, string filename)
        {
            try
            {
                BLL.MessageBody MessageServer = new BLL.MessageBody();
                RosHeader rosHeaderMessageServer = new RosHeader();
                RosLines rosLineMessageServer = new RosLines();

                Model.MessageBody model = new Model.MessageBody();

                List<Model.MessageBody> models = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'");

                if (models.Count > 0)
                    model = models[0];

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;

                model.segment1 = filename;

                model.messageType = messageType;

                model.segment4 = "F";

                

                if (models.Count > 0)
                    MessageServer.Update(model);
                else
                {
                    if ("OUT" == flag)
                    {
                        model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        model.notes = "IN-Process";
                        model.segment2 = "OUT";
                        model.segment3 = "Unread";
                        //model.status = "Pending";
                    }
                    if ("IN" == flag)
                    {
                        model.notes = "Creation";
                        model.segment2 = "IN";
                    }

                    MessageServer.Add(model);

                    model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];
                }

                foreach (ROSHeader header in xmlModel.headers)
                {
                    Model.RosHeader hmodel = new Model.RosHeader();
                    List<Model.RosHeader> rlist = rosHeaderMessageServer.GetModelList("RosItemNO = '" + header.item_no + "' and messageBodyID ='" + model.messageID + "'");
                    if (rlist.Count > 0)
                    {
                        hmodel = rlist[0];
                        hmodel.OpenPoQty = header.open_po_qty;
                        hmodel.PoNumber = header.po_number;
                        hmodel.Glod_plan_flag = header.glod_plan_flag;
                        hmodel.RosAllocationPercent = header.allocation_percent;
                        hmodel.RosBuyer = header.buyer;
                        hmodel.RosCategory = header.category;
                        hmodel.RosDesc = header.description;
                        hmodel.RosLeadTime = header.lead_time;
                        hmodel.RosModel = header.model;
                        hmodel.RosSafeStock = header.safe_stock;
                        hmodel.RosStockQty = header.stock_qty;
                        hmodel.VmiStock = header.vmi_stock;
                        hmodel.UpdateFlag = header.updateflag;

                        rosHeaderMessageServer.Update(hmodel);

                    }
                    else
                    {
                        hmodel = new Model.RosHeader();
                        hmodel.OpenPoQty = header.open_po_qty;
                        hmodel.PoNumber = header.po_number;
                        hmodel.Glod_plan_flag = header.glod_plan_flag;
                        hmodel.RosAllocationPercent = header.allocation_percent;
                        hmodel.RosBuyer = header.buyer;
                        hmodel.RosCategory = header.category;
                        hmodel.RosDesc = header.description;
                        hmodel.RosLeadTime = header.lead_time;
                        hmodel.RosModel = header.model;
                        hmodel.RosSafeStock = header.safe_stock;
                        hmodel.RosStockQty = header.stock_qty;
                        hmodel.RosItemNO = header.item_no;
                        hmodel.MessageBodyID = model.messageID;
                        hmodel.UpdateFlag = header.updateflag;

                        rosHeaderMessageServer.Add(hmodel);
                    }

                    hmodel = rosHeaderMessageServer.GetModelList("RosItemNO = '" + header.item_no + "' and messageBodyID ='" + model.messageID + "'")[0];


                    foreach (ROSLine line in header.lines)
                    {
                        Model.RosLines lmodel = new Model.RosLines();
                        List<Model.RosLines> list = rosLineMessageServer.GetModelList("RosDemandDate = '" + line.demand_date + "' and RosHeaderID ='" + hmodel.RosHeaderID + "'");
                        if (list.Count > 0)
                        {
                            lmodel = list[0];
                            //lmodel.RosDemandDate = DateTime.Parse(line.demand_date);
                            lmodel.RosDemandQuantity = line.demand_quantity;
                            lmodel.RosDio = line.dio;
                            lmodel.RosEtaQty = line.eta_qty;
                            lmodel.RosEtdQty = line.etd_qty;
                            lmodel.RosCumEta = line.cum_eta;
                            lmodel.RosShortageQty = line.shortage_qty;
                            //lmodel.RosHeaderID = hmodel.RosHeaderID;
                            lmodel.PO_NO = line.po_no;
                            rosLineMessageServer.Update(lmodel);
                        }
                        else
                        {

                            //lmodel = rosLineMessageServer.GetModelList("RosDemandDate = '" + line.demand_date + "' and RosHeaderID ='" + hmodel.RosHeaderID + "'")[0];
                            lmodel.RosDemandDate = line.demand_date;
                            lmodel.RosDemandQuantity = line.demand_quantity;
                            lmodel.RosDio = line.dio;
                            lmodel.RosEtaQty = line.eta_qty;
                            lmodel.RosEtdQty = line.etd_qty;
                            lmodel.RosCumEta = line.cum_eta;
                            lmodel.RosShortageQty = line.shortage_qty;
                            lmodel.RosHeaderID = hmodel.RosHeaderID;
                            lmodel.PO_NO = line.po_no;
                            rosLineMessageServer.Add(lmodel);
                        }



                    }

                }

                if ("OUT" == flag)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                    model.status = "Pending";
                    
                }
                if ("IN" == flag)
                {
                    model.notes = "Creation";
                    model.status = "Unread";
                }

                model.segment4 = "T";
                MessageServer.Update(model);

            }
            catch (Exception e)
            {
                throw e;
            }


            return true;
        }



        public Boolean uploadROS(Tool.Message<ROSHeader> xmlModel, string messageType, string filename)
        {
            try
            {
                BLL.MessageBody MessageServer = new BLL.MessageBody();

                Model.MessageBody model = new Model.MessageBody();

                List<Model.MessageBody> list = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'");

                if (list.Count > 0) model = list[0];

                model.messageType = messageType;

                model.confirmDateTime = DateTime.Now.ToShortDateString();
                model.status = "Pending";
                model.notes = "IN-Process";
                model.segment1 = filename;
                model.segment2 = "OUT";
                model.segment3 = "Unread";
                model.segment4 = "F";

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
                model.messageAlias = xmlModel.sender.messageAlias;
                model.edi_location_code = xmlModel.sender.edi_location_code;
                model.creationDateTime = xmlModel.sender.creationDateTime;

                model.vender_name = xmlModel.partner.vender_name;
                model.vender_site = xmlModel.partner.vender_site;
                model.vender_num = xmlModel.partner.vender_num;
                model.vender_site_num = xmlModel.partner.vender_site_num;
                model.address = xmlModel.partner.address;
                model.city = xmlModel.partner.city;
                model.contact_name = xmlModel.partner.contact_name;
                model.duns_num = xmlModel.partner.duns_num;
                model.email = xmlModel.partner.email;
                model.street = xmlModel.partner.street;
                model.phone = xmlModel.partner.phone;
                model.postal_code = xmlModel.partner.postal_code;



                if (list.Count > 0)
                {
                    MessageServer.Update(model);
                }
                else
                {
                    MessageServer.Add(model);
                }



            }
            catch (Exception e)
            {
                throw e;
            }


            return true;
        }





        public Tool.Message<ROSHeader> convert2Ros(Model.MessageBody model)
        {

            try
            {
                Tool.Message<ROSHeader> ros = new Tool.Message<ROSHeader>();

                MessageSender sender = new MessageSender();
                MessagePartner partner = new MessagePartner();
                List<ROSHeader> list = new List<ROSHeader>();

                sender.edi_location_code = model.edi_location_code;
                sender.messageAlias = model.messageAlias;
                sender.messageName = model.messageName;
                sender.referenceID = model.referenceid;
                sender.key = model.key;
                sender.creationDateTime = model.creationDateTime.ToString();

                partner.vender_name = model.vender_name;
                partner.vender_site = model.vender_site;
                partner.vender_site_num = model.vender_site_num;
                partner.vender_num = model.vender_num;
                partner.address = model.address;
                partner.city = model.city;
                partner.contact_name = model.contact_name;
                partner.duns_num = model.duns_num;
                partner.email = model.email;
                partner.phone = model.phone;
                partner.postal_code = model.postal_code;
                partner.street = model.street;

                ros.sender = sender;
                ros.partner = partner;

                RosHeader rh = new RosHeader();

                List<Model.RosHeader> hl = rh.GetModelList("messageBodyID = '" + model.messageID + "'");

                foreach (Model.RosHeader header in hl)
                {

                    ROSHeader rheader = new ROSHeader();
                    rheader.allocation_percent = header.RosAllocationPercent;
                    rheader.glod_plan_flag = header.Glod_plan_flag;
                    rheader.buyer = header.RosBuyer;
                    rheader.category = header.RosCategory;
                    rheader.description = header.RosDesc;
                    rheader.item_no = header.RosItemNO;
                    rheader.lead_time = header.RosLeadTime;
                    rheader.model = header.RosModel;
                    rheader.open_po_qty = header.OpenPoQty;
                    rheader.po_number = header.PoNumber;
                    rheader.safe_stock = header.RosSafeStock;
                    rheader.stock_qty = header.RosStockQty;
                    rheader.vmi_stock = header.VmiStock;
                    rheader.updateflag = header.UpdateFlag;

                    RosLines rl = new RosLines();

                    List<Model.RosLines> ll = rl.GetModelList("RosHeaderID = '" + header.RosHeaderID + "'");

                    foreach (Model.RosLines line in ll)
                    {

                        ROSLine rline = new ROSLine();
                        rline.demand_date = line.RosDemandDate;
                        rline.demand_quantity = line.RosDemandQuantity;
                        rline.dio = line.RosDio;
                        rline.eta_qty = line.RosEtaQty;
                        rline.etd_qty = line.RosEtdQty;
                        rline.cum_eta = line.RosCumEta;
                        rline.shortage_qty = line.RosShortageQty;
                        rline.po_no = line.PO_NO;
                        rheader.lines.Add(rline);
                    }
                    list.Add(rheader);
                }
                ros.headers = list;
                return ros;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
