show databases;
create database connected_Users;

use connected_Users;

drop table users;
drop table reltionship;
drop table mulaqat;
drop table events;
drop table customer_address;
drop table customers;

select * from Customers





create table customers( customerID int NOT NULL AUTO_INCREMENT,Name  varchar(150) NOT NULL,dob date NOT NULL,GenderID int NOT NULL,maritalStatusID int NOT NULL,
mobile_number long NOT NULL,occupationID int,arabicEducationID int,educationID int,educationDetail varchar(50),occupdationDetails varchar(50),
 PRIMARY KEY (customerID),foreign key (occupationID) references occupation(occupationID),
 foreign key (educationID) references Education(educationID),
  foreign key (arabicEducationID) references ArabicEducation(arabicEducationID),
    foreign key (GenderID) references Gender(GenderID),
    foreign key (maritalStatusID) references MaritalStatus(maritalStatusID));

select * from Customers
drop table ReltionShip
create table ReltionShip (id int not null, userID int not null , childernID int, wifeID int,
foreign key(userID) references customers(customerID),
foreign key(childernID) references customers(customerID),
foreign key(wifeID) references customers(customerID));


create table Mulaqat (userID int not null primary key, lastMetUserID int,lastMetDate datetime,
foreign key(userID) references customers(customerID),
foreign key(lastMetUserID) references customers(customerID));


create table Events (eventID int not null primary key, eventType varchar(100),targetAudience varchar(150), responsiblePerson int,
status varchar(50),Feeback varchar(250),
foreign key(responsiblePerson) references customers(customerID));

create table customer_address(customerID int not null primary key, address1 varchar(150),
address2 varchar(150),Area varchar(150),stateID int,pinID int,cityID int,foreign key(customerID) references customers (customerID),
foreign key(stateID) references states (stateID),
foreign key(pinID) references Pincode (pinID),
foreign key(cityID) references city (cityID));


create table users(userID int not null primary key, password varchar(150) not null,  roleID int not null,foreign key (userID) references customers(customerID),foreign key (roleID) references roles(roleID));


create table states(stateID int NOT NULL primary key ,  state varchar(150));
create table Pincode(pinID int NOT NULL primary key ,  pin varchar(50));
create table city(cityID int NOT NULL primary key,  city varchar(150));
create table roles(roleID int NOT NULL primary key,roleName varchar(50));
create table occupation(occupationID int NOT NULL primary key ,  occuptionName varchar(50));
create table Education(educationID int NOT NULL primary key ,  educationName varchar(50));
create table ArabicEducation(arabicEducationID int NOT NULL primary key ,  arabicEducationName varchar(50));
create table MaritalStatus(maritalStatusID int NOT NULL primary key , maritalStatus  varchar(50));
Create table Gender(GenderID int NOT NULL primary key , GENDER  varchar(10));


//Master Data
insert into states values(0,'KARNATAKA');
insert into pincode values(0,'560099');
insert into city values(0,'BANGALORE');

insert into roles values(0,'SUPER_USER');
insert into roles values(1,'ADMIN');
insert into roles values(2,'MEMBER');

insert into occupation values(0,'STUDYING');
insert into occupation values(1,'WORKING');
insert into occupation values(2,'NIL');

insert into Education values(0,'PRE k.G');
insert into Education values(1,'L.k.G');
insert into Education values(2,'U.k.G');
insert into Education values(3,'1');
insert into Education values(4,'2');
insert into Education values(5,'3');
insert into Education values(6,'4');
insert into Education values(7,'5');
insert into Education values(8,'6');
insert into Education values(9,'7');
insert into Education values(10,'8');
insert into Education values(11,'9');
insert into Education values(12,'10');
insert into Education values(13,'BACHELORS');
insert into Education values(14,'MASTERS');
insert into Education values(15,'DOCTORATE');

insert into ArabicEducation values(0,'READ');
insert into ArabicEducation values(1,'WRITE');
insert into ArabicEducation values(2,'SPEAK');
insert into ArabicEducation values(3,'READ+SPEAK');
insert into ArabicEducation values(4,'READ+WRITE');
insert into ArabicEducation values(5,'READ+WRITE+SPEAK');
insert into ArabicEducation values(6,'HIFZ');
insert into ArabicEducation values(7,'ULEMA');
insert into ArabicEducation values(8,'MUFTI');



insert into MaritalStatus values(0,'SINGLE');
insert into MaritalStatus values(1,'MARRIED');
insert into MaritalStatus values(2,'WIDOW');
insert into MaritalStatus values(3,'WIDOWER');

insert into Gender values(0,'MALE');
insert into Gender values(1,'FEMALE');

Alter table customers add column(updatedBy int, createdBy int not null,createdTime dateTime ,updatedTime dateTime  );
Alter table users add column(updatedBy int, createdBy int not null,createdTime dateTime ,updatedTime dateTime  );
ALTER TABLE customers ADD UNIQUE (Name,dob);

select * from Gender
select * from users