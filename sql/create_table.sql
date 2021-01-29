create table TB_User(
 Account varchar(20) not null ,
 Password varchar(20) not null,
 CreateDate datetime not null default now(),
 UpdateDate datetime null,
 Status tinyint not null default 1,
 primary key (Account)
);

create table TB_Order (
	OrderID varchar(30) not null,
	Account varchar(20) not null,
	CreateDate datetime not null default now(),
	UpdateDate datetime null,
	Status tinyint not null default 1,
	primary key (OrderID),
	constraint fk_order_user_account foreign key (Account) references TB_User(Account)
);

create table TB_Product (
	ProductID int  not null auto_increment,
	Name nvarchar(50) not null,
	Price int not null,
	Cost int not null,
	ProductDesc nvarchar(200) null,
    CreateDate datetime not null default now(),
	UpdateDate datetime null,
	Status tinyint not null default 1,
	primary key (ProductID)
);

create table TB_OrderItem (
	OrderID varchar(20) not null,
	ProductID int  not null,
	Count int  not null,
	 primary key (OrderID, ProductID),
	 constraint fk_order_item_order foreign key (OrderID) references TB_Order(OrderID),
	 constraint fk_order_item_product foreign key (ProductID) references TB_Product(ProductID)
);


