﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

[assembly: EdmSchemaAttribute()]
namespace AmbleClient.Order.PoMgr
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class PoEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“PoEntities”部分中的连接字符串初始化新 PoEntities 对象。
        /// </summary>
        public PoEntities() : base("name=PoEntities", "PoEntities")
        {
            ChangeString();
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 PoEntities 对象。
        /// </summary>
        public PoEntities(string connectionString) : base(connectionString, "PoEntities")
        {
            ChangeString();
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 PoEntities 对象。
        /// </summary>
        public PoEntities(EntityConnection connection) : base(connection, "PoEntities")
        {
            ChangeString();
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
        private void ChangeString()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder(((EntityConnection)Connection).StoreConnection.ConnectionString);
            sb.UserID = ServerInfo.GetUserId();
            sb.Password = ServerInfo.GetPassword();
            sb.Server = ServerInfo.GetServerAddress();
            sb.Database = "shenzhenerp";
            ((EntityConnection)Connection).StoreConnection.ConnectionString = sb.ConnectionString;

        }

        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<po> po
        {
            get
            {
                if ((_po == null))
                {
                    _po = base.CreateObjectSet<po>("po");
                }
                return _po;
            }
        }
        private ObjectSet<po> _po;
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<poitems> poitems
        {
            get
            {
                if ((_poitems == null))
                {
                    _poitems = base.CreateObjectSet<poitems>("poitems");
                }
                return _poitems;
            }
        }
        private ObjectSet<poitems> _poitems;

        #endregion

        #region AddTo 方法
    
        /// <summary>
        /// 用于向 po EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddTopo(po po)
        {
            base.AddObject("po", po);
        }
    
        /// <summary>
        /// 用于向 poitems EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddTopoitems(poitems poitems)
        {
            base.AddObject("poitems", poitems);
        }

        #endregion

    }

    #endregion

    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PoModel", Name="po")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class po : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 po 对象。
        /// </summary>
        /// <param name="poId">poId 属性的初始值。</param>
        /// <param name="vendorName">vendorName 属性的初始值。</param>
        /// <param name="contact">contact 属性的初始值。</param>
        /// <param name="pa">pa 属性的初始值。</param>
        /// <param name="poDate">poDate 属性的初始值。</param>
        public static po Createpo(global::System.Int32 poId, global::System.String vendorName, global::System.String contact, global::System.Int16 pa, global::System.DateTime poDate)
        {
            po po = new po();
            po.poId = poId;
            po.vendorName = vendorName;
            po.contact = contact;
            po.pa = pa;
            po.poDate = poDate;
            return po;
        }

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 poId
        {
            get
            {
                return _poId;
            }
            set
            {
                if (_poId != value)
                {
                    OnpoIdChanging(value);
                    ReportPropertyChanging("poId");
                    _poId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("poId");
                    OnpoIdChanged();
                }
            }
        }
        private global::System.Int32 _poId;
        partial void OnpoIdChanging(global::System.Int32 value);
        partial void OnpoIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String vendorName
        {
            get
            {
                return _vendorName;
            }
            set
            {
                OnvendorNameChanging(value);
                ReportPropertyChanging("vendorName");
                _vendorName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("vendorName");
                OnvendorNameChanged();
            }
        }
        private global::System.String _vendorName;
        partial void OnvendorNameChanging(global::System.String value);
        partial void OnvendorNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String contact
        {
            get
            {
                return _contact;
            }
            set
            {
                OncontactChanging(value);
                ReportPropertyChanging("contact");
                _contact = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("contact");
                OncontactChanged();
            }
        }
        private global::System.String _contact;
        partial void OncontactChanging(global::System.String value);
        partial void OncontactChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 pa
        {
            get
            {
                return _pa;
            }
            set
            {
                OnpaChanging(value);
                ReportPropertyChanging("pa");
                _pa = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("pa");
                OnpaChanged();
            }
        }
        private global::System.Int16 _pa;
        partial void OnpaChanging(global::System.Int16 value);
        partial void OnpaChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String vendorNumber
        {
            get
            {
                return _vendorNumber;
            }
            set
            {
                OnvendorNumberChanging(value);
                ReportPropertyChanging("vendorNumber");
                _vendorNumber = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("vendorNumber");
                OnvendorNumberChanged();
            }
        }
        private global::System.String _vendorNumber;
        partial void OnvendorNumberChanging(global::System.String value);
        partial void OnvendorNumberChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime poDate
        {
            get
            {
                return _poDate;
            }
            set
            {
                OnpoDateChanging(value);
                ReportPropertyChanging("poDate");
                _poDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("poDate");
                OnpoDateChanged();
            }
        }
        private global::System.DateTime _poDate;
        partial void OnpoDateChanging(global::System.DateTime value);
        partial void OnpoDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String poNo
        {
            get
            {
                return _poNo;
            }
            set
            {
                OnpoNoChanging(value);
                ReportPropertyChanging("poNo");
                _poNo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("poNo");
                OnpoNoChanged();
            }
        }
        private global::System.String _poNo;
        partial void OnpoNoChanging(global::System.String value);
        partial void OnpoNoChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String paymentTerms
        {
            get
            {
                return _paymentTerms;
            }
            set
            {
                OnpaymentTermsChanging(value);
                ReportPropertyChanging("paymentTerms");
                _paymentTerms = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("paymentTerms");
                OnpaymentTermsChanged();
            }
        }
        private global::System.String _paymentTerms;
        partial void OnpaymentTermsChanging(global::System.String value);
        partial void OnpaymentTermsChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipMethod
        {
            get
            {
                return _shipMethod;
            }
            set
            {
                OnshipMethodChanging(value);
                ReportPropertyChanging("shipMethod");
                _shipMethod = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipMethod");
                OnshipMethodChanged();
            }
        }
        private global::System.String _shipMethod;
        partial void OnshipMethodChanging(global::System.String value);
        partial void OnshipMethodChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String freight
        {
            get
            {
                return _freight;
            }
            set
            {
                OnfreightChanging(value);
                ReportPropertyChanging("freight");
                _freight = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("freight");
                OnfreightChanged();
            }
        }
        private global::System.String _freight;
        partial void OnfreightChanging(global::System.String value);
        partial void OnfreightChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipToLocation
        {
            get
            {
                return _shipToLocation;
            }
            set
            {
                OnshipToLocationChanging(value);
                ReportPropertyChanging("shipToLocation");
                _shipToLocation = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipToLocation");
                OnshipToLocationChanged();
            }
        }
        private global::System.String _shipToLocation;
        partial void OnshipToLocationChanging(global::System.String value);
        partial void OnshipToLocationChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String billTo
        {
            get
            {
                return _billTo;
            }
            set
            {
                OnbillToChanging(value);
                ReportPropertyChanging("billTo");
                _billTo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("billTo");
                OnbillToChanged();
            }
        }
        private global::System.String _billTo;
        partial void OnbillToChanging(global::System.String value);
        partial void OnbillToChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String shipTo
        {
            get
            {
                return _shipTo;
            }
            set
            {
                OnshipToChanging(value);
                ReportPropertyChanging("shipTo");
                _shipTo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("shipTo");
                OnshipToChanged();
            }
        }
        private global::System.String _shipTo;
        partial void OnshipToChanging(global::System.String value);
        partial void OnshipToChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.SByte> poStates
        {
            get
            {
                return _poStates;
            }
            set
            {
                OnpoStatesChanging(value);
                ReportPropertyChanging("poStates");
                _poStates = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("poStates");
                OnpoStatesChanged();
            }
        }
        private Nullable<global::System.SByte> _poStates;
        partial void OnpoStatesChanging(Nullable<global::System.SByte> value);
        partial void OnpoStatesChanged();

        #endregion



        public int soStates { get; set; }
    }
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PoModel", Name="poitems")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class poitems : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 poitems 对象。
        /// </summary>
        /// <param name="poItemsId">poItemsId 属性的初始值。</param>
        /// <param name="poId">poId 属性的初始值。</param>
        /// <param name="soItemId">soItemId 属性的初始值。</param>
        /// <param name="partNo">partNo 属性的初始值。</param>
        /// <param name="mfg">mfg 属性的初始值。</param>
        /// <param name="dc">dc 属性的初始值。</param>
        /// <param name="qty">qty 属性的初始值。</param>
        /// <param name="dockDate">dockDate 属性的初始值。</param>
        /// <param name="salesAgent">salesAgent 属性的初始值。</param>
        /// <param name="poItemState">poItemState 属性的初始值。</param>
        public static poitems Createpoitems(global::System.Int32 poItemsId, global::System.Int32 poId, global::System.Int32 soItemId, global::System.String partNo, global::System.String mfg, global::System.String dc, global::System.Int32 qty, global::System.DateTime dockDate, global::System.SByte salesAgent, global::System.SByte poItemState)
        {
            poitems poitems = new poitems();
            poitems.poItemsId = poItemsId;
            poitems.poId = poId;
            poitems.soItemId = soItemId;
            poitems.partNo = partNo;
            poitems.mfg = mfg;
            poitems.dc = dc;
            poitems.qty = qty;
            poitems.dockDate = dockDate;
            poitems.salesAgent = salesAgent;
            poitems.poItemState = poItemState;
            return poitems;
        }

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 poItemsId
        {
            get
            {
                return _poItemsId;
            }
            set
            {
                if (_poItemsId != value)
                {
                    OnpoItemsIdChanging(value);
                    ReportPropertyChanging("poItemsId");
                    _poItemsId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("poItemsId");
                    OnpoItemsIdChanged();
                }
            }
        }
        private global::System.Int32 _poItemsId;
        partial void OnpoItemsIdChanging(global::System.Int32 value);
        partial void OnpoItemsIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 poId
        {
            get
            {
                return _poId;
            }
            set
            {
                OnpoIdChanging(value);
                ReportPropertyChanging("poId");
                _poId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("poId");
                OnpoIdChanged();
            }
        }
        private global::System.Int32 _poId;
        partial void OnpoIdChanging(global::System.Int32 value);
        partial void OnpoIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 soItemId
        {
            get
            {
                return _soItemId;
            }
            set
            {
                OnsoItemIdChanging(value);
                ReportPropertyChanging("soItemId");
                _soItemId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("soItemId");
                OnsoItemIdChanged();
            }
        }
        private global::System.Int32 _soItemId;
        partial void OnsoItemIdChanging(global::System.Int32 value);
        partial void OnsoItemIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String partNo
        {
            get
            {
                return _partNo;
            }
            set
            {
                OnpartNoChanging(value);
                ReportPropertyChanging("partNo");
                _partNo = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("partNo");
                OnpartNoChanged();
            }
        }
        private global::System.String _partNo;
        partial void OnpartNoChanging(global::System.String value);
        partial void OnpartNoChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String mfg
        {
            get
            {
                return _mfg;
            }
            set
            {
                OnmfgChanging(value);
                ReportPropertyChanging("mfg");
                _mfg = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("mfg");
                OnmfgChanged();
            }
        }
        private global::System.String _mfg;
        partial void OnmfgChanging(global::System.String value);
        partial void OnmfgChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String dc
        {
            get
            {
                return _dc;
            }
            set
            {
                OndcChanging(value);
                ReportPropertyChanging("dc");
                _dc = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("dc");
                OndcChanged();
            }
        }
        private global::System.String _dc;
        partial void OndcChanging(global::System.String value);
        partial void OndcChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String vendorIntPartNo
        {
            get
            {
                return _vendorIntPartNo;
            }
            set
            {
                OnvendorIntPartNoChanging(value);
                ReportPropertyChanging("vendorIntPartNo");
                _vendorIntPartNo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("vendorIntPartNo");
                OnvendorIntPartNoChanged();
            }
        }
        private global::System.String _vendorIntPartNo;
        partial void OnvendorIntPartNoChanging(global::System.String value);
        partial void OnvendorIntPartNoChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String coo
        {
            get
            {
                return _coo;
            }
            set
            {
                OncooChanging(value);
                ReportPropertyChanging("coo");
                _coo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("coo");
                OncooChanged();
            }
        }
        private global::System.String _coo;
        partial void OncooChanging(global::System.String value);
        partial void OncooChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 qty
        {
            get
            {
                return _qty;
            }
            set
            {
                OnqtyChanging(value);
                ReportPropertyChanging("qty");
                _qty = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qty");
                OnqtyChanged();
            }
        }
        private global::System.Int32 _qty;
        partial void OnqtyChanging(global::System.Int32 value);
        partial void OnqtyChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qtyRecd
        {
            get
            {
                return _qtyRecd;
            }
            set
            {
                OnqtyRecdChanging(value);
                ReportPropertyChanging("qtyRecd");
                _qtyRecd = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qtyRecd");
                OnqtyRecdChanged();
            }
        }
        private Nullable<global::System.Int32> _qtyRecd;
        partial void OnqtyRecdChanging(Nullable<global::System.Int32> value);
        partial void OnqtyRecdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qtyCorrected
        {
            get
            {
                return _qtyCorrected;
            }
            set
            {
                OnqtyCorrectedChanging(value);
                ReportPropertyChanging("qtyCorrected");
                _qtyCorrected = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qtyCorrected");
                OnqtyCorrectedChanged();
            }
        }
        private Nullable<global::System.Int32> _qtyCorrected;
        partial void OnqtyCorrectedChanging(Nullable<global::System.Int32> value);
        partial void OnqtyCorrectedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qtyAccept
        {
            get
            {
                return _qtyAccept;
            }
            set
            {
                OnqtyAcceptChanging(value);
                ReportPropertyChanging("qtyAccept");
                _qtyAccept = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qtyAccept");
                OnqtyAcceptChanged();
            }
        }
        private Nullable<global::System.Int32> _qtyAccept;
        partial void OnqtyAcceptChanging(Nullable<global::System.Int32> value);
        partial void OnqtyAcceptChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qtyRejected
        {
            get
            {
                return _qtyRejected;
            }
            set
            {
                OnqtyRejectedChanging(value);
                ReportPropertyChanging("qtyRejected");
                _qtyRejected = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qtyRejected");
                OnqtyRejectedChanged();
            }
        }
        private Nullable<global::System.Int32> _qtyRejected;
        partial void OnqtyRejectedChanging(Nullable<global::System.Int32> value);
        partial void OnqtyRejectedChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qtyRTV
        {
            get
            {
                return _qtyRTV;
            }
            set
            {
                OnqtyRTVChanging(value);
                ReportPropertyChanging("qtyRTV");
                _qtyRTV = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qtyRTV");
                OnqtyRTVChanged();
            }
        }
        private Nullable<global::System.Int32> _qtyRTV;
        partial void OnqtyRTVChanging(Nullable<global::System.Int32> value);
        partial void OnqtyRTVChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> qcPending
        {
            get
            {
                return _qcPending;
            }
            set
            {
                OnqcPendingChanging(value);
                ReportPropertyChanging("qcPending");
                _qcPending = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("qcPending");
                OnqcPendingChanged();
            }
        }
        private Nullable<global::System.Int32> _qcPending;
        partial void OnqcPendingChanging(Nullable<global::System.Int32> value);
        partial void OnqcPendingChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.SByte> currency
        {
            get
            {
                return _currency;
            }
            set
            {
                OncurrencyChanging(value);
                ReportPropertyChanging("currency");
                _currency = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("currency");
                OncurrencyChanged();
            }
        }
        private Nullable<global::System.SByte> _currency;
        partial void OncurrencyChanging(Nullable<global::System.SByte> value);
        partial void OncurrencyChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Single> unitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                OnunitPriceChanging(value);
                ReportPropertyChanging("unitPrice");
                _unitPrice = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("unitPrice");
                OnunitPriceChanged();
            }
        }
        private Nullable<global::System.Single> _unitPrice;
        partial void OnunitPriceChanging(Nullable<global::System.Single> value);
        partial void OnunitPriceChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime dockDate
        {
            get
            {
                return _dockDate;
            }
            set
            {
                OndockDateChanging(value);
                ReportPropertyChanging("dockDate");
                _dockDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("dockDate");
                OndockDateChanged();
            }
        }
        private global::System.DateTime _dockDate;
        partial void OndockDateChanging(global::System.DateTime value);
        partial void OndockDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> receiveDate
        {
            get
            {
                return _receiveDate;
            }
            set
            {
                OnreceiveDateChanging(value);
                ReportPropertyChanging("receiveDate");
                _receiveDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("receiveDate");
                OnreceiveDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _receiveDate;
        partial void OnreceiveDateChanging(Nullable<global::System.DateTime> value);
        partial void OnreceiveDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String stepCode
        {
            get
            {
                return _stepCode;
            }
            set
            {
                OnstepCodeChanging(value);
                ReportPropertyChanging("stepCode");
                _stepCode = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("stepCode");
                OnstepCodeChanged();
            }
        }
        private global::System.String _stepCode;
        partial void OnstepCodeChanging(global::System.String value);
        partial void OnstepCodeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.SByte salesAgent
        {
            get
            {
                return _salesAgent;
            }
            set
            {
                OnsalesAgentChanging(value);
                ReportPropertyChanging("salesAgent");
                _salesAgent = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("salesAgent");
                OnsalesAgentChanged();
            }
        }
        private global::System.SByte _salesAgent;
        partial void OnsalesAgentChanging(global::System.SByte value);
        partial void OnsalesAgentChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String noteToVendor
        {
            get
            {
                return _noteToVendor;
            }
            set
            {
                OnnoteToVendorChanging(value);
                ReportPropertyChanging("noteToVendor");
                _noteToVendor = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("noteToVendor");
                OnnoteToVendorChanged();
            }
        }
        private global::System.String _noteToVendor;
        partial void OnnoteToVendorChanging(global::System.String value);
        partial void OnnoteToVendorChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.SByte poItemState
        {
            get
            {
                return _poItemState;
            }
            set
            {
                OnpoItemStateChanging(value);
                ReportPropertyChanging("poItemState");
                _poItemState = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("poItemState");
                OnpoItemStateChanged();
            }
        }
        private global::System.SByte _poItemState;
        partial void OnpoItemStateChanging(global::System.SByte value);
        partial void OnpoItemStateChanged();

        #endregion

    
    }

    #endregion

    
}
