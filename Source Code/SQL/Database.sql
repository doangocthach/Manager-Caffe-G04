 drop database VTCAcaffe;
create database if not exists VTCAcaffe;

use VTCAcaffe;

create table if not exists Customers(
CusID int auto_increment primary key,
CusName nvarchar(50) not null,
userName varchar(50) not null unique,
userPassword varchar(50) not null,
Address nvarchar(255) not null,
PhoneNumber int not null
);
insert into Customers(CusName, userName,userPassword,Address,PhoneNumber) values
('Thach','Thach123','123456789','84 Kim Ngưu','0392127339'),
('Toan','Toan123','123456789','Tân Tiến, Hải Dương','01213309289');

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
insert into Items(ItemName, ItemPrice,ItemDescription,Size) values
('Caffe đen','15.00','Caffe đen với hương vị đậm đà , được chọn lọc từ loại caffe hảo hạn nguyên chất mang đến cho bạn 
trải nghiệm khác biệt . ','M,L,XL'),
('Caffe nâu','15.00','Caffe hảo hạng với vị nữa ngọt bùi , sữa cùng với vị  đắng của caffe sẽ mang đến cho bạn một trải nghiệm khó quên  ','M,L,XL'),
('Caffe hương chồn','25.00',' là loại caffe mà có thể bạn đã nghe ở nhiều nơi, với quy trình làm khá cầu kì , vẫn giữ được hương vị caffe vốn có , vị bùi bùi nồng nồng ,
 thật tuyệt vời phải không nào ? ','M,L,XL'),
('Caffe rượu','20.00','Sự độc đáo kết hợp từ rượu với caffe , thật thú vị phải không nào ? ','M,L,XL'),
('Caffe đen','20.00',' ','M,L,XL'),
('Trà lipton','10.00','Có lẽ không phải ai cũng uống được caffe , một chú dịu nhẹ nhàng với một cốc lipton, tại sao không ? ','M,L,XL'),
('Hoa quả thập cẩm','25.00','Gồm táo, cam, mận, xoài giưps bạn có gì đó tráng miệng sau khi thử 1 ly caffe và ngẫm nghĩ quá nhiều ,
một chú chua và ngọt thanh sẽ giúp bạn thoải mái hơn . ','M,L,XL');



create table if not exists OrderDetail(
OrderID int,
ItemID int ,
ItemCount int,
constraint pk_OrderDetail primary key(OrderID,ItemID),
constraint fk_OrderDetail_Orders foreign key(OrderID) references Orders(OrderID),
constraint fk_OrderDetail_Items foreign key(ItemID) references Items(ItemID)

);
