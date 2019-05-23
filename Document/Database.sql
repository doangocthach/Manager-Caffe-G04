drop database VTCAcaffe;
create database if not exists VTCAcaffe;

use VTCAcaffe;

create table if not exists Customers(
CusName nvarchar(50) not null,
CusID int auto_increment primary key,
userName varchar(50) not null unique,
userPassword varchar(50) not null,
Address nvarchar(255) not null,
PhoneNumber int not null
);

create table if not exists Orders(

OrderID int auto_increment primary key ,
OrderDate datetime not null,
Note nvarchar(255) not null,
OrderStatus nvarchar(20) not null,
CusID int,
constraint fk_Customers_Orders foreign key(CusID) references Customers(CusID)
);

create table if not exists Items(
ItemID int auto_increment primary key,
ItemName nvarchar(255),
ItemPrice decimal(10,2) ,
ItemDescription text ,
Size varchar(10)
);

create table if not exists OrderDetail(
OrderID int,
ItemID int ,
ItemCount int,
constraint pk_OrderDetail primary key(OrderID,ItemID),
constraint fk_OrderDetail_Orders foreign key(OrderID) references Orders(OrderID),
constraint fk_OrderDetail_Items foreign key(ItemID) references Items(ItemID)
 
);
