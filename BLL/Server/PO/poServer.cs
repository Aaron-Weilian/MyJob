using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool;
using Model = com.portal.db.Model;

namespace com.portal.db.BLL.PO
{
    public class poServer
    {
        /*---Segment2  :    IN-------------------OUT     */
        /*---status  :    Unread/Read ----Pending/OK     */
        /*---notes  :    Creation/IN-Process/Success      */


        public Model.MessageBody savePOData(Tool.Message<Tool.POHeader> xmlModel, string messageType, string filename,string exportName)
        {
            BLL.MessageBody MessageServer = new BLL.MessageBody();
            POHeader poHeaderMessageServer = new POHeader();
            POLine poLineMessageServer = new POLine();


            Model.MessageBody model = new Model.MessageBody();

            //try
            //{
                

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
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

                if ("POConfirm" == messageType)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("PO" == messageType)
                {
                    model.status = "Unread";
                    model.notes = "Creation";
                    model.segment2 = "IN";
                }

                model.segment4 = "F";

                MessageServer.Add(model);
                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [segment5] = '" + exportName + "'")[0];

                foreach (Tool.POHeader header in xmlModel.headers)
                {
                    Model.POHeader hmodel = new Model.POHeader();
                    hmodel.buyer = header.buyer;
                    hmodel.currency = header.currency;
                    hmodel.delivery_location = header.delivery_location;
                    hmodel.desc = header.desc;
                    hmodel.messageBodyID = model.messageID;
                    hmodel.po_date = header.po_date;
                    hmodel.po_number = header.po_number;
                    hmodel.po_type = header.po_type;
                    hmodel.terms_of_delivery = header.terms_of_delivery;
                    hmodel.terms_of_payment = header.term_of_payment;
                    hmodel.supplier = header.supplier;
                    hmodel.supplier_address = header.supplier_address;
                    hmodel.supplier_site = header.supplier_site;
                    hmodel.delivery_address = header.delivery_address;
                    hmodel.your_reference = header.your_reference;

                    poHeaderMessageServer.Add(hmodel);
                    hmodel = poHeaderMessageServer.GetModelList("po_number = '" + header.po_number + "' and messageBodyID ='" + model.messageID + "'")[0];


                    foreach (Tool.POLine line in header.polines)
                    {
                        Model.POLine lmodel = new Model.POLine();
                        lmodel.unit_price = line.unit_price;
                        lmodel.schedule_delivery_qty = line.schedule_delivery_qty;
                        lmodel.schedule_delivery_date = line.schedule_delivery_date;
                        lmodel.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                        lmodel.request_qty = line.request_qty;
                        lmodel.request_delivery_date = line.request_delivery_date;
                        lmodel.price_unit = line.price_unit;
                        lmodel.lineNo = line.lineNo;
                        lmodel.line_item_tatoal_amount = line.line_item_tatoal_amount;
                        lmodel.item_no = line.item_no;
                        lmodel.desc = line.desc;
                        lmodel.POHeaderID = hmodel.POHeaderID;
                        poLineMessageServer.Add(lmodel);
                    }

                }
                //model.segment4 = "T";
                //MessageServer.Update(model);
                return MessageServer.GetModelList("MessageType = '" + messageType + "'  and [segment5] = '" + exportName + "'")[0];
            //}
            //catch(Exception e){
            //    throw e; 
            //}
           
        }


        public Model.MessageBody updatePOData(Tool.Message<Tool.POHeader> xmlModel, string messageType, string key)
        {
            BLL.MessageBody MessageServer = new BLL.MessageBody();
            POHeader poHeaderMessageServer = new POHeader();
            POLine poLineMessageServer = new POLine();
            Model.MessageBody model = null;
            try
            {
                model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + key + "'")[0];


                if ("POConfirm" == messageType)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("PO" == messageType)
                {
                    model.status = "Unread";
                    model.notes = "Creation";
                    model.segment2 = "IN";
                }

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
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

                foreach (Tool.POHeader header in xmlModel.headers)
                {
                    Model.POHeader hmodel;
                    List<Model.POHeader> list = poHeaderMessageServer.GetModelList("po_number = '" + header.po_number + "' and messageBodyID ='" + model.messageID + "'");
                    if (list.Count > 0)
                    {
                        hmodel = list[0];
                        hmodel.buyer = header.buyer;
                        hmodel.currency = header.currency;
                        hmodel.delivery_location = header.delivery_location;
                        hmodel.delivery_address = header.delivery_address;
                        hmodel.desc = header.desc;
                        hmodel.messageBodyID = model.messageID;
                        hmodel.po_date = header.po_date;
                        hmodel.po_number = header.po_number;
                        hmodel.po_type = header.po_type;
                        hmodel.terms_of_delivery = header.terms_of_delivery;
                        hmodel.terms_of_payment = header.term_of_payment;
                        hmodel.your_reference = header.your_reference;
                        hmodel.order_type = header.order_class;
                        hmodel.supplier = header.supplier;
                        hmodel.supplier_site = header.supplier_site;
                        hmodel.supplier_address = header.supplier_address;



                        poHeaderMessageServer.Update(hmodel);

                    }
                    else
                    {
                        hmodel = new Model.POHeader();
                        hmodel.buyer = header.buyer;
                        hmodel.currency = header.currency;
                        hmodel.delivery_location = header.delivery_location;
                        hmodel.delivery_address = header.delivery_address;
                        hmodel.desc = header.desc;
                        hmodel.messageBodyID = model.messageID;
                        hmodel.po_date = header.po_date;
                        hmodel.po_number = header.po_number;
                        hmodel.po_type = header.po_type;
                        hmodel.terms_of_delivery = header.terms_of_delivery;
                        hmodel.terms_of_payment = header.term_of_payment;
                        hmodel.your_reference = header.your_reference;
                        hmodel.order_type = header.order_class;
                        hmodel.supplier = header.supplier;
                        hmodel.supplier_site = header.supplier_site;
                        hmodel.supplier_address = header.supplier_address;

                        poHeaderMessageServer.Add(hmodel);

                    }


                    hmodel = poHeaderMessageServer.GetModelList("po_number = '" + header.po_number + "' and messageBodyID ='" + model.messageID + "'")[0];


                    foreach (Tool.POLine line in header.polines)
                    {
                        Model.POLine lmodel = new Model.POLine();

                        List<Model.POLine> l = poLineMessageServer.GetModelList("[lineNo] = '" + line.lineNo + "' and POHeaderID ='" + hmodel.POHeaderID + "'");

                        if (l.Count > 0)
                        {
                            lmodel = l[0];
                            lmodel.unit_price = line.unit_price;
                            lmodel.schedule_delivery_qty = line.schedule_delivery_qty;
                            lmodel.schedule_delivery_date = line.schedule_delivery_date;
                            lmodel.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                            lmodel.request_qty = line.request_qty;
                            lmodel.request_delivery_date = line.request_delivery_date;
                            lmodel.price_unit = line.price_unit;
                            lmodel.lineNo = line.lineNo;
                            lmodel.line_item_tatoal_amount = line.line_item_tatoal_amount;
                            lmodel.item_no = line.item_no;
                            lmodel.desc = line.desc;
                            lmodel.curr = line.curr;
                            lmodel.POHeaderID = hmodel.POHeaderID;

                            poLineMessageServer.Update(lmodel);
                        }
                        else
                        {
                            lmodel.unit_price = line.unit_price;
                            lmodel.schedule_delivery_qty = line.schedule_delivery_qty;
                            lmodel.schedule_delivery_date = line.schedule_delivery_date;
                            lmodel.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                            lmodel.request_qty = line.request_qty;
                            lmodel.request_delivery_date = line.request_delivery_date;
                            lmodel.price_unit = line.price_unit;
                            lmodel.lineNo = line.lineNo;
                            lmodel.line_item_tatoal_amount = line.line_item_tatoal_amount;
                            lmodel.item_no = line.item_no;
                            lmodel.desc = line.desc;
                            lmodel.curr = line.curr;
                            lmodel.POHeaderID = hmodel.POHeaderID;

                            poLineMessageServer.Add(lmodel);
                        }



                    }

                }

                //model.segment4 = "T";
                //MessageServer.Update(model);
                return MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + key + "'")[0];
            }
            catch (Exception e) {
                throw e; 
            }

            
        }


        public Model.MessageBody mergePOData(Tool.Message<Tool.POHeader> xmlModel, string messageType, string filename)
        {

            BLL.MessageBody MessageServer = new BLL.MessageBody();
            POHeader poHeaderMessageServer = new POHeader();
            POLine poLineMessageServer = new POLine();

            Model.MessageBody model = new Model.MessageBody();

            try
            {
               
                List<Model.MessageBody> models = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'");

                if (models.Count > 0)
                    model = models[0];

                model.segment1 = filename;

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
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
                    if ("POConfirm" == messageType)
                    {
                        model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //model.status = "Pending";
                        model.notes = "IN-Process";
                        model.segment2 = "OUT";
                        model.segment3 = "Unread";
                    }
                    if ("PO" == messageType)
                    {
                        model.notes = "Creation";
                        model.segment2 = "IN";
                    }

                    MessageServer.Add(model);

                    model = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];
                }

                foreach (Tool.POHeader header in xmlModel.headers)
                {
                    Model.POHeader hmodel;
                    List<Model.POHeader> list = poHeaderMessageServer.GetModelList("po_number = '" + header.po_number + "' and messageBodyID ='" + model.messageID + "'");
                    if (list.Count > 0)
                    {
                        hmodel = list[0];
                        hmodel.buyer = header.buyer;
                        hmodel.currency = header.currency;
                        hmodel.delivery_location = header.delivery_location;
                        hmodel.delivery_address = header.delivery_address;
                        hmodel.desc = header.desc;
                        hmodel.messageBodyID = model.messageID;
                        hmodel.po_date = header.po_date;
                        hmodel.po_number = header.po_number;
                        hmodel.po_type = header.po_type;
                        hmodel.terms_of_delivery = header.terms_of_delivery;
                        hmodel.terms_of_payment = header.term_of_payment;
                        hmodel.your_reference = header.your_reference;
                        hmodel.order_type = header.order_class;
                        hmodel.supplier = header.supplier;
                        hmodel.supplier_site = header.supplier_site;
                        hmodel.supplier_address = header.supplier_address;



                        poHeaderMessageServer.Update(hmodel);

                    }
                    else
                    {
                        hmodel = new Model.POHeader();
                        hmodel.buyer = header.buyer;
                        hmodel.currency = header.currency;
                        hmodel.delivery_location = header.delivery_location;
                        hmodel.delivery_address = header.delivery_address;
                        hmodel.desc = header.desc;
                        hmodel.messageBodyID = model.messageID;
                        hmodel.po_date = header.po_date;
                        hmodel.po_number = header.po_number;
                        hmodel.po_type = header.po_type;
                        hmodel.terms_of_delivery = header.terms_of_delivery;
                        hmodel.terms_of_payment = header.term_of_payment;
                        hmodel.your_reference = header.your_reference;
                        hmodel.order_type = header.order_class;
                        hmodel.supplier = header.supplier;
                        hmodel.supplier_site = header.supplier_site;
                        hmodel.supplier_address = header.supplier_address;

                        poHeaderMessageServer.Add(hmodel);

                    }


                    hmodel = poHeaderMessageServer.GetModelList("po_number = '" + header.po_number + "' and messageBodyID ='" + model.messageID + "'")[0];


                    foreach (Tool.POLine line in header.polines)
                    {
                        Model.POLine lmodel = new Model.POLine();

                        List<Model.POLine> l = poLineMessageServer.GetModelList("[lineNo] = '" + line.lineNo + "' and POHeaderID ='" + hmodel.POHeaderID + "'");

                        if (l.Count > 0)
                        {
                            lmodel = l[0];
                            lmodel.unit_price = line.unit_price;
                            lmodel.schedule_delivery_qty = line.schedule_delivery_qty;
                            lmodel.schedule_delivery_date = line.schedule_delivery_date;
                            lmodel.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                            lmodel.request_qty = line.request_qty;
                            lmodel.request_delivery_date = line.request_delivery_date;
                            lmodel.price_unit = line.price_unit;
                            lmodel.lineNo = line.lineNo;
                            lmodel.line_item_tatoal_amount = line.line_item_tatoal_amount;
                            lmodel.item_no = line.item_no;
                            lmodel.desc = line.desc;
                            lmodel.curr = line.curr;
                            lmodel.POHeaderID = hmodel.POHeaderID;

                            poLineMessageServer.Update(lmodel);
                        }
                        else
                        {
                            lmodel.unit_price = line.unit_price;
                            lmodel.schedule_delivery_qty = line.schedule_delivery_qty;
                            lmodel.schedule_delivery_date = line.schedule_delivery_date;
                            lmodel.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                            lmodel.request_qty = line.request_qty;
                            lmodel.request_delivery_date = line.request_delivery_date;
                            lmodel.price_unit = line.price_unit;
                            lmodel.lineNo = line.lineNo;
                            lmodel.line_item_tatoal_amount = line.line_item_tatoal_amount;
                            lmodel.item_no = line.item_no;
                            lmodel.desc = line.desc;
                            lmodel.curr = line.curr;
                            lmodel.POHeaderID = hmodel.POHeaderID;

                            poLineMessageServer.Add(lmodel);
                        }



                    }

                }

                if ("POConfirm" == messageType)
                {
                    model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.status = "Pending";
                    model.notes = "IN-Process";
                    model.segment2 = "OUT";
                    model.segment3 = "Unread";
                }
                if ("PO" == messageType)
                {
                    model.status = "Unread";
                    model.notes = "Creation";
                }

                //model.segment4 = "T";
                //MessageServer.Update(model);
                return MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'")[0];
            }
            catch (Exception e)
            {
                throw e;
            }

        
        }


        public Boolean uploadPO(Tool.Message<Tool.POHeader> xmlModel, string messageType, string filename)
        {
            try
            {
                BLL.MessageBody MessageServer = new BLL.MessageBody();

                Model.MessageBody model = new Model.MessageBody();

                List<Model.MessageBody> list = MessageServer.GetModelList("MessageType = '" + messageType + "'  and [key] = '" + xmlModel.sender.key + "'");

                if (list.Count > 0) model = list[0];

                model.messageType = messageType;

                model.confirmDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                model.status = "Pending";
                model.notes = "IN-Process";
                model.segment1 = filename;
                model.segment2 = "OUT";
                model.segment3 = "Unread";
                model.segment4 = "F";

                model.key = xmlModel.sender.key;
                model.referenceid = xmlModel.sender.referenceID;
                model.messageName = xmlModel.sender.messageName;
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
        
        public Tool.Message<Tool.POHeader> convert2PO(Model.MessageBody model)
        {
            Tool.Message<Tool.POHeader> po = new Tool.Message<Tool.POHeader>();

            MessageSender sender = new MessageSender();
            MessagePartner partner = new MessagePartner();
            List<Tool.POHeader> list = new List<Tool.POHeader>();

            sender.edi_location_code = model.edi_location_code;
            sender.messageName = model.messageName;
            sender.key = model.key;
            sender.referenceID = model.referenceid;
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

            po.sender = sender;
            po.partner = partner;

            POHeader rh = new POHeader();

            List<Model.POHeader> hl = rh.GetModelList("messageBodyID = '" + model.messageID + "'");

            foreach (Model.POHeader header in hl)
            {
                Tool.POHeader pheader = new Tool.POHeader();
                pheader.buyer = header.buyer;
                pheader.currency = header.currency;
                pheader.delivery_location = header.delivery_location;
                pheader.desc = header.desc;
                pheader.po_date = header.po_date;
                pheader.po_number = header.po_number;
                pheader.po_type = header.po_type;
                pheader.terms_of_delivery = header.terms_of_delivery;
                pheader.term_of_payment = header.terms_of_payment;
                pheader.your_reference = header.your_reference;
                pheader.delivery_address = header.delivery_address;
                pheader.supplier = header.supplier;
                pheader.supplier_address = header.supplier_address;
                pheader.supplier_site = header.supplier_site;
                pheader.order_class = header.order_type;

                POLine rl = new POLine();

                List<Model.POLine> ll = rl.GetModelList("POHeaderID = '" + header.POHeaderID + "'");

                foreach (Model.POLine line in ll)
                {
                    Tool.POLine pline = new Tool.POLine();
                    pline.desc = line.desc;
                    pline.item_no = line.item_no;
                    pline.line_item_tatoal_amount = line.line_item_tatoal_amount;
                    pline.lineNo = line.lineNo;
                    pline.price_unit = line.price_unit;
                    pline.request_delivery_date = line.request_delivery_date;
                    pline.request_qty = line.request_qty;
                    pline.schedule_delivery_date = line.schedule_delivery_date;
                    pline.schedule_delivery_qty = line.schedule_delivery_qty;
                    pline.Schedule_Arrive_Date = line.Schedule_Arrive_Date;
                    pline.unit_price = line.unit_price;
                    pline.curr = line.curr;
                    pline.line_item_tatoal_amount = line.line_item_tatoal_amount;

                    pheader.polines.Add(pline);
                }
                list.Add(pheader);
            }
            po.headers = list;
            return po;
        }
    }
}
