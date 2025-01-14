create database artspot

use artspot

create table tbl_admin

(
ad_id int identity primary key,
ad_username nvarchar(50) not null unique,
ad_password nvarchar(50) not null,
)

insert into tbl_admin
values('root','admin123')
insert into tbl_admin
values('admin','admin123')
insert into tbl_admin
values('test','admin123')

select * from tbl_admin

/* admin table complete */

create table tbl_category
(
cat_id int identity primary key,
cat_name nvarchar(50) not null unique,
cat_image nvarchar(max) not null ,
cat_fk_ad int foreign key references tbl_admin(ad_id)
)

/----------------------------------------------/

create table tbl_user
(
u_id int identity primary key,
u_name nvarchar(50) not null,
u_email nvarchar(50) not null unique,
u_password nvarchar(50) not null,
u_image nvarchar(max) not null ,
u_contact nvarchar(50) not null unique,
)

/*  -----------------------------------------------*/

create table tbl_product
(
pro_id int identity primary key,
pro_name nvarchar(50) not null,
pro_image nvarchar(max) not null ,
pro_des nvarchar(max) not null ,
pro_price int,
pro_contact nvarchar(50) not null unique,
pro_fk_cat int foreign key references tbl_category(cat_id),
pro_fk_user int foreign key references tbl_user(u_id)
)

/---------------------------------------------------/



ALTER TABLE tbl_category ADD cat_status int default 1;