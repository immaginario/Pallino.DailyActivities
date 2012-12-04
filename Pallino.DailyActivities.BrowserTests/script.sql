
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DailyReports_Customers]') AND parent_object_id = OBJECT_ID('DailyReports'))
alter table DailyReports  drop constraint FK_DailyReports_Customers


    if exists (select * from dbo.sysobjects where id = object_id(N'Customers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Customers

    if exists (select * from dbo.sysobjects where id = object_id(N'DailyReports') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DailyReports

    create table Customers (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) not null,
       VATNumber NVARCHAR(13) not null,
       primary key (Id)
    )

    create table DailyReports (
        Id INT IDENTITY NOT NULL,
       Date DATETIME not null,
       MorningStart NVARCHAR(5) not null,
       MorningEnd NVARCHAR(5) not null,
       AfternoonStart NVARCHAR(5) not null,
       AfternoonEnd NVARCHAR(5) not null,
       Notes NVARCHAR(4000) not null,
       Offsite BIT not null,
       Customer_id INT not null,
       primary key (Id)
    )

    alter table DailyReports 
        add constraint FK_DailyReports_Customers 
        foreign key (Customer_id) 
        references Customers
