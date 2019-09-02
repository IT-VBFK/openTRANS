﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using static openTRANS.Common;

namespace openTRANS
{
    [XmlRoot("ORDER", Namespace = "http://www.opentrans.org/XMLSchema/2.1", IsNullable = false)]
    public class Order
    {
        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation = "http://www.opentrans.org/XMLSchema/2.1%20opentrans_2_1.xsd";

        [XmlElement("ORDER_HEADER")]
        public OrderHeader OrderHeader = new OrderHeader();

        [XmlArray("ORDER_ITEM_LIST")]
        [XmlArrayItem("ORDER_ITEM")]
        public List<OrderItem> OrderItemList = new List<OrderItem>();

        [XmlElement("ORDER_SUMMARY")]
        public OrderSummary OrderSummary
        {
            get
            {
                return new OrderSummary(this);
            }
            set { /* required for xml serialization */ }
        }
    }


    public class OrderHeader
    {
        [XmlElement("CONTROL_INFO")]
        public ControlInfo ControlInfo = new ControlInfo();

        [XmlElement("ORDER_INFO")]
        public OrderInfo OrderInfo = new OrderInfo();
    }


    public class ControlInfo
    {
        [XmlElement("GENERATOR_INFO")]
        public string GeneratorInfo;

        [XmlElement("GENERATION_DATE")]
        public DateTime GenerationDate = DateTime.Now;
    }


    public class OrderInfo
    {
        [XmlElement("ORDER_ID")]
        public string OrderId;

        [XmlElement("ORDER_DATE")]
        public DateTime OrderDate;

        [XmlElement("DELIVERY_DATE")]
        public DeliveryDate DeliveryDate = new DeliveryDate();

        [XmlArray("PARTIES")]
        [XmlArrayItem("PARTY")]
        public List<Party> Parties = new List<Party>();
        

        [XmlElement("ORDER_PARTIES_REFERENCE")]
        public OrderPartiesReference OrderPartiesReference = new OrderPartiesReference();
    }


    public class DeliveryDate
    {
        [XmlAttribute("type")]
        public string Type;

        [XmlElement("DELIVERY_START_DATE")]
        public DateTime DeliveryStartDate;

        [XmlElement("DELIVERY_END_DATE")]
        public DateTime DeliveryEndDate;
    }


    public class Party
    {
        [XmlElement("PARTY_ID", Namespace = Common.Namespace.bmecat)]
        public PartyId PartyId = new PartyId();
                
        [XmlElement("PARTY_ROLE")]
        public string PartyRole;

        [XmlElement("ADDRESS")]
        public Address Address = new Address();

    }


    public class PartyId
    {
        [XmlText]
        public string Value;

        [XmlAttribute("type")]
        public string Type;
    }

        
    public class Address
    {
        [XmlElement("NAME", Namespace = Common.Namespace.bmecat)]
        public string Name;

        [XmlElement("CONTACT_DETAILS")]
        public ContactDetails ContactDetails = new ContactDetails();

        [XmlElement("STREET", Namespace = Common.Namespace.bmecat)]
        public string Street;

        [XmlElement("ZIP", Namespace = Common.Namespace.bmecat)]
        public string Zip;

        [XmlElement("CITY", Namespace = Common.Namespace.bmecat)]
        public string City;

        [XmlElement("COUNTRY", Namespace = Common.Namespace.bmecat)]
        public string Country;

        [XmlElement("PHONE", Namespace = Common.Namespace.bmecat)]
        public TypedItem Phone = new TypedItem();

        [XmlElement("FAX", Namespace = Common.Namespace.bmecat)]
        public TypedItem Fax = new TypedItem();

        [XmlElement("EMAIL", Namespace = Common.Namespace.bmecat)]
        public string Email;
    }


    public class ContactDetails
    {
        [XmlElement("CONTACT_NAME", Namespace = Common.Namespace.bmecat)]
        public string ContactName;
    }


    public class OrderPartiesReference
    {
        [XmlElement("BUYER_IDREF", Namespace = Common.Namespace.bmecat)]
        public TypedItem BuyerIdRef = new TypedItem();

        [XmlElement("SUPPLIER_IDREF", Namespace = Common.Namespace.bmecat)]
        public TypedItem SupplierIdRef = new TypedItem();

        [XmlElement("SHIPMENT_PARTIES_REFERENCE")]
        public ShipmentPartiesReference ShipmentPartiesReference = new ShipmentPartiesReference();

    }

      

    public class ShipmentPartiesReference
    {
        [XmlElement("DELIVERY_IDREF")]
        public TypedItem DeliveryIdRef = new TypedItem();
    }

        
    public class OrderItem
    {
        [XmlElement("LINE_ITEM_ID")]
        public string LineItemId;

        [XmlElement("PRODUCT_ID")]
        public ProductId ProductId = new ProductId();

        [XmlArray("PRODUCT_FEATURES")]
        public List<Feature> ProductFeatures = new List<Feature>();

        [XmlElement("QUANTITY")]
        public decimal Quantity;

        [XmlElement("ORDER_UNIT", Namespace = Common.Namespace.bmecat)]
        public string OrderUnit;

        [XmlElement("PRODUCT_PRICE_FIX")]
        public ProductPriceFix ProductPriceFix = new ProductPriceFix();

        [XmlElement("PRICE_LINE_AMOUNT")]
        public decimal PriceLineAmount
        {
            get
            {
                return Quantity * ProductPriceFix.PriceAmount;
            }
            set { /* required for xml serialization */ }
        }
    }


    public class ProductId
    {
        [XmlElement("SUPPLIER_PID", Namespace = Common.Namespace.bmecat)]
        public string SupplierPid;

        [XmlElement("BUYER_PID", Namespace = Common.Namespace.bmecat)]
        public TypedItem BuyerPid = new TypedItem();

        [XmlElement("DESCRIPTION_SHORT", Namespace = Common.Namespace.bmecat)]
        public string DescriptionShort;
    }


    public class Feature
    {
        [XmlElement("FNAME", Namespace = Common.Namespace.bmecat)]
        public string FName;

        [XmlElement("FVALUE", Namespace = Common.Namespace.bmecat)]
        public string FValue;

        [XmlElement("FUNIT", Namespace = Common.Namespace.bmecat)]
        public string FUnit;
    }

    
    public class ProductPriceFix
    {
        [XmlElement("PRICE_AMOUNT", Namespace = Common.Namespace.bmecat)]
        public decimal PriceAmount;

        [XmlElement("ALLOW_OR_CHARGES_FIX")]
        public AllowOrChargesFix AllowOrChargesFix = new AllowOrChargesFix();
    }


    public class AllowOrChargesFix
    {
        [XmlElement("ALLOW_OR_CHARGE")]
        public AllowOrCharge AllowOrCharge = new AllowOrCharge();

        [XmlElement("ALLOW_OR_CHARGES_TOTAL_AMOUNT")]
        public decimal AllowOrChargesTotalAmount;
    }


    public class AllowOrCharge
    {
        [XmlAttribute("type")]
        public string Type;

        [XmlElement("ALLOW_OR_CHARGE_TYPE")]
        public string AllowOrChargeType;

        [XmlElement("ALLOW_OR_CHARGE_VALUE")]
        public AllowOrChargeValue AllowOrChargeValue = new AllowOrChargeValue();

    }


    public class AllowOrChargeValue
    {
        [XmlElement("AOC_PERCENTAGE_FACTOR")]
        public decimal AocPercentageFactor;

    }
     
    
    public class OrderSummary
    {
        public OrderSummary()
        {
        }

        public OrderSummary(Order o)
        {
            TotalItemNum = o.OrderItemList.Count;
            TotalAmount = o.OrderItemList.Sum(oi => oi.PriceLineAmount);
        }

        [XmlElement("TOTAL_ITEM_NUM")]
        public decimal TotalItemNum;

        [XmlElement("TOTAL_AMOUNT")]
        public decimal TotalAmount;
    }
}
