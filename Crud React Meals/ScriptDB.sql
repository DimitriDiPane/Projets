create database if not exists schoolMealsDB;
use schoolMealsDB;

create table if not exists provider (
	 providerId int not null primary key auto_increment,
    providerName varchar(50) not null
);

create table if not exists category (
	categoryId int not null primary key auto_increment,
    categoryName varchar(50) not null
);

create table if not exists meal (
	mealId int not null primary key auto_increment,
    mealName varchar(50) not null,
    price decimal(4,2) not null,
    providerId int not null,
    categoryId int not null,
    foreign key (providerId) references provider(providerId),
    foreign key (categoryId) references category(categoryId)
);

insert into provider(providerName) values ('Sysco'),('Regalgel'),('Azdistribution');
insert into category(categoryName) values ('Sandwichs'),('Plats chauds');
insert into meal(mealName, price, providerId, categoryId) values ('Pizza 4 fromages', '3', 1, 1),
('Boulettes sauce tomates', '5.5', 2, 2),
('Pâtes Carbonarra', '5.5', 3, 1);