CREATE SCHEMA `ReactTemplate` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci ;

CREATE TABLE AppUser (
	Id varchar(255) not null,
	FirstName varchar(255),
	LastName varchar(255),
	DateOfBirth Date not null,
	IsActive bool not null,
	AvatarUrl varchar(255),
	UserName varchar(255) unique not null,
	EmailConfirmed bool not null,
	Password varchar(255),
	PhoneNumber varchar(255),
	PhoneNumberConfirmed bool not null,
	CreationDate datetime,
	UniquePath varchar(255) unique not null,
	LowerUserName varchar(255) not null,
    Primary Key (Id)
);

CREATE TABLE AppRole (
	Id varchar(255) not null,
	Name varchar(20),
    primary key(Id)
);

CREATE TABLE AppUserRole (
	UserId varchar(255) not null,
	RoleId varchar(255) not null,
	primary key (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES AppUser(Id),
    FOREIGN KEY (RoleId) REFERENCES AppRole(Id)
);

