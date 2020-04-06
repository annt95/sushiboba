


exec AddMenu 'Sushi Small (12 pcs)','2 Salmon, 1 Tuna, 1 Shrimp, 8 Salmon Maki', 'https://cdn.huongnghiepaau.com/wp-content/uploads/2019/01/mon-sushi-chay-don-gian.jpg','20',1,0
select * from menu

Alter PROCEDURE AddMenu
@name varchar(255),
@description varchar(MAX),
@images varchar(MAX),
@price varchar(255),
@issushi bit,
@ismilktea bit
AS
INSERT INTO Menu (name, description, images, price, active,issushi,ismilktea,isdelete,ishot)
VALUES (@name, @description, @images,@price,1,@issushi,@ismilktea,0,0);
GO