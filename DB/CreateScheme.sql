`offer``shenzhenerp`CREATE DATABASE shenzhenERP ;
USE shenzhenERP ;

CREATE TABLE account (
  id SMALLINT AUTO_INCREMENT PRIMARY KEY,
  accountName VARCHAR (255) ,
  accountPassword VARCHAR (255),
  email VARCHAR (255),
  job TINYINT,
  /*0:admin, 1:boss,2:sales, 3:saleManager,4:buyer,5:buyerManager. 6:warehouse 7,warehouseManager. 8:financial 9;financial manager
*/
  superviser SMALLINT
) ;




CREATE TABLE custVendor (
  cvtype TINYINT,
  /*0,customer, 1, vendor*/
  cvname VARCHAR (255),
  country VARCHAR (255),
  cvnumber VARCHAR(255),
  rate TINYINT,
  term VARCHAR (255),
  contact1 VARCHAR (65535),
  contact2 VARCHAR (65535),
  phone1 VARCHAR (255),
  phone2 VARCHAR (255),
  cellphone VARCHAR (255),
  fax VARCHAR (255),
  email1 VARCHAR (255),
  email2 VARCHAR (255),
  ownerName SMALLINT,  
  lastUpdateName SMALLINT,
  lastUpdateDate DATETIME,
  blacklisted TINYINT,
  /*0: no, 1:yes*/3
  amount INT,
  notes VARCHAR (65535),
  CONSTRAINT pk_cvtype_cvname_ownerName PRIMARY KEY (cvtype,cvname,ownerName)
) ;

CREATE TABLE rfq(

rfqNo INT PRIMARY KEY AUTO_INCREMENT,
customerName VARCHAR(255)  NOT NULL,
partNo VARCHAR(255) NOT NULL,
salesId SMALLINT NOT NULL,
contact VARCHAR(65535) NOT NULL,
project VARCHAR(255),
rohs TINYINT, /* 0 no-rohs, 1:rohs*/
phone VARCHAR(255),
fax VARCHAR(255),
email VARCHAR(255),
rfqdate DATE,
priority TINYINT, /*0:cost down, 1:shortage, 2:history*/
dockdate DATE NOT NULL,
mfg VARCHAR(20) NOT NULL,
dc VARCHAR(20) NOT NULL,
custPartNo VARCHAR(255),
genPartNo VARCHAR(255),
alt VARCHAR(255),
qty INT NOT NULL,
packaging VARCHAR(255),
targetPrice FLOAT,
resale FLOAT,
cost FLOAT,
firstPA SMALLINT,
secondPA SMALLINT,
rfqStates TINYINT, /*0 new : 1,Routed 2.Offered 3,Quote 4, has SO, 5,closed*/
infoToCustomer MEDIUMTEXT,
infoToInternal MEDIUMTEXT,
routingHistory MEDIUMTEXT,
closeReason TINYINT  /*0 Price to high;1 L/T too long; 2 D/C too old; 3 Packing issue; 4 Demand gone; 5 Others*/
);


CREATE TABLE rfqCopy(
salesId SMALLINT PRIMARY KEY,
rfqNo INT
);


CREATE TABLE offer(
offerId INT PRIMARY KEY AUTO_INCREMENT,
rfqNo INT,
mpn VARCHAR(255),
mfg VARCHAR(255),
vendorName VARCHAR(255),
contact VARCHAR(65535),
phone VARCHAR(255),
fax VARCHAR(255),
email VARCHAR(255),
amount INT,
price FLOAT,
deliverTime INT,
timeUnit TINYINT, /*0:day,1,week,2:month,3 year*/
buyerId SMALLINT,
offerDate DATE,
offerStates TINYINT, /* 0,new 1.routed */
notes MEDIUMTEXT
);


CREATE TABLE So(
soId INT PRIMARY KEY AUTO_INCREMENT,
rfqId INT,
customerName VARCHAR(255),
contact VARCHAR(255),
salesId SMALLINT,
approverId SMALLINT,
approveDate DATE,
salesOrderNo VARCHAR(255),
orderDate DATE,
customerPo VARCHAR(255),
paymentTerm VARCHAR(255),
freightTerm VARCHAR(255),
customerAccount VARCHAR(255),
specialInstructions VARCHAR(65525),
billTo VARCHAR(65535),
shipTo VARCHAR(65535),
soStates TINYINT /*0 new,1:approved 2:rejected 3:closed*/
);

CREATE TABLE SoItems(
soItemsId INT PRIMARY KEY AUTO_INCREMENT,
soId INT,
saleType TINYINT,  /*OEM EXCESS; OWN STOCK; OTHERS   */
partNo VARCHAR(255),
mfg VARCHAR(20),
rohs TINYINT,
dc VARCHAR(20),
intPartNo VARCHAR(255),
shipFrom VARCHAR(50),
shipMethod VARCHAR(50),
trackingNo VARCHAR(50),
qty INT,
qtyShipped INT,
currency TINYINT, /*0:USD ,1:CNY 2:EUR 3:HK 4JP */
unitPrice FLOAT,
dockDate DATE,
shippedDate DATE,
shippingInstruction VARCHAR(65535),
packingInstruction VARCHAR(65535)
);


CREATE TABLE Po(

poId INT PRIMARY KEY AUTO_INCREMENT,
soId INT,
vendorName VARCHAR(255),
contact VARCHAR(255),
pa SMALLINT,
paDate DATE,
vendorNumber VARCHAR(50),
poDate DATE,
poNo VARCHAR(50),
paymentTerms VARCHAR(255),
shipMethod VARCHAR(50),
freight VARCHAR(50),
shipToLocation VARCHAR(255),
billTo VARCHAR(65535),
shipTo VARCHAR(65535),
poStates TINYINT /* 0 new. 1:approved 2:rejected 3:closed */
);

CREATE TABLE PoItems(

poItemsId INT PRIMARY KEY AUTO_INCREMENT,
poId INT,
partNo VARCHAR(255),
mfg VARCHAR(20),
dc VARCHAR(20),
vendorIntPartNo VARCHAR(255),
org VARCHAR(20),
qty INT,
qtyRecd INT,
qtyCorrected INT,
qtyAccept INT,
qtyRejected INT,
qtyRTV INT,
qcPending INT,
currency TINYINT,
unitPrice FLOAT,
dueDate DATE,
receiveDate DATE,
stepCode VARCHAR(255),
salesAgent TINYINT,
noteToVendor VARCHAR(65535)
);


CREATE TABLE PublicCustVen(
custVenId INT PRIMARY KEY AUTO_INCREMENT,
custVendorType TINYINT, /*0 */
custVenName VARCHAR(255) NOT NULL,
contact VARCHAR(255),
tel VARCHAR(255),
email VARCHAR(255),
userID SMALLINT`publiccustven` NOT NULL REFERENCES account ,
enterDay DATETIME
);

CREATE TABLE PublicBomOffer(
bomOfferId INT PRIMARY KEY AUTO_INCREMENT,
BomCustVendId INT NOT NULL REFERENCES PublicCustVen,
mfg VARCHAR(255),
mpn VARCHAR(255),
qty INT,
price FLOAT,
cpn VARCHAR(255),
userID SMALLINT NOT NULL REFERENCES account,
enerDay DATETIME
);

