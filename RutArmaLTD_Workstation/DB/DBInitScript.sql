If DB_ID('StoreAssistant_DB') Is not null Begin
	Use master;
	Drop database StoreAssistant_DB;
End;

Create database StoreAssistant_DB;
Go

Use StoreAssistant_DB;
Go

Create type dbo.[setupKey] From varchar(5);
Create type dbo.[string] From varchar(500);
Create type dbo.[shortString] From varchar(100);
Create type dbo.[volcount] From decimal (7,3);
Create type dbo.[phone] From varchar(9); /* 29 123 45 67 = 9 знаков (без запятых) */
Go

Create table [User] (
	login shortString not null unique,
	password shortString not null,
	surname shortString not null, 
	name shortString not null,
	fathername shortString null,
	access setupKey not null,
	ban bit not null default(0),
	Check (LEN(login) > 0),
	Check (LEN(password) >= 6),
	Check (access Like '[0-9]-[0-9]-[0-9]'),
	Constraint User_PK Primary key (login)
);
Go

/*
	Если добавляемая категория может быть удалена: eraseble = 1;
	Если не должна удаляться (как дефолтная - "Не распределено"/id=1): eraseble = 0.
	parent - ключ на категорию в своей же таблице для создания иерархии
*/
Create table [Category] (
	id int not null identity(1, 1) unique,
	parent int null,
	name shortString not null,
	imgRelPath string null,
	erasable bit not null default(1),
	Check (LEN(name) > 1),
	Constraint Category_PK Primary key (id),
	Constraint Category_self_FK Foreign key (parent) References [Category] (id)
);
Go

/* Сохранение иерархии и элементов при удалении её звеньев (категория) */
Create trigger dbo.[Category_AfterDel_trg]
On [Category]
After Delete
As Begin
	Declare @Deleted as Table(
		[index] int not null identity(1, 1),
		id int not null,
		parent int not null
	);

	Insert Into @Deleted Select id, parent From deleted;
	Declare @Count int = (Select COUNT(*) From @Deleted);
	Declare @Index int = 1;

	While (@Index <= @Count) Begin
		Update new
		Set new.parent = old.parent
		From [Category] as new
		Inner Join @Deleted as old On new.parent = old.id
		Where old.[index] = @Index

		/* тут код возвращения индексов объектам Item при удалении категории Parent. Для этого нужен дублирующий id у Item */

		Set @Index = @Index + 1;
	End
End
Go

/* Та самая дефолтная неудаляемая категория */
Insert Into [Category] Values (null, 'Не распределено', null, 0);
Go

/*
	actualCostOpt (...Option): правила установки цены для предмета в формате "*-*-*"
	1-0-0 = цена товара равна последней поставке на склад;
	0-1-0 = цена товара равна максимальной за весь период поставок;
	0-0-1 = цена товара устанавливается самостоятельно в customCost (например на время акций или с целью доп зароботка).
*/
Create table [Item] (
	id int not null identity(1, 1) unique,
	id_category int not null default(1),
	name shortString not null,
	imgRelPath string null,
	actualCostOpt setupKey not null default('1-0-0'),
	customCost money not null default(0),
	Check (actualCostOpt Like '[0-9]-[0-9]-[0-9]'),
	Check (LEN(name) > 1),
	Check (customCost >= 0),
	Constraint Item_PK Primary key (id),
	Constraint Item_Category_FK Foreign key (id_category) References [Category] (id) On Update Cascade On Delete Set Default
);
Go

/* GRMLI: gr/ml/i, то есть счисление предмета происходит в граммах/миллилитрах/кол-во относительно указанной стоимости */
Create table [Stock] (
	id int not null identity(1, 1),
	id_item int null,
	itemName shortString not null,
	cost money not null,
	GRMLI volcount not null,
	count volcount not null,
	inStock volcount not null,
	onTheWayToStockFrom datetime not null,
	whenStocked datetime null,
	receptionist_login shortString not null,
	Check (cost > 0 And GRMLI > 0 And count > 0),
	Check (inStock >= 0),
	Check (whenStocked Is null Or whenStocked >= onTheWayToStockFrom),
	Constraint Stock_PK Primary key (id),
	Constraint Stock_Item_FK Foreign key (id_item) References [Item] (id) On Update Cascade On Delete Set Null,
	Constraint Stock_User_FK Foreign key (receptionist_login) References [User] (login) On Update Cascade
);
Go

Create table [Client] (
	bonusСardID int not null identity(1, 1),
	phoneNumber phone not null,
	surname shortString not null,
	name shortString not null,
	fathername shortString null,
	cardBonus money not null default(0),
	Check (LEN(phoneNumber) = 9),
	Constraint Client_PK Primary key (bonusСardID)
);

Create table [Purchase] (
	id int not null identity(1, 1),
	id_client int null,
	login_manager shortString not null,
	datetime datetime not null,
	totalPrice money not null,
	byBonuses money not null default(0),
	Constraint Purchase_PK Primary key (id),
	Constraint Purchase_Client_FK Foreign key (id_client) References [Client] (bonusСardID) On Update Cascade On Delete Set Null,
	Constraint Purchase_User_FK Foreign key (login_manager) References [User] (login) On Update Cascade
);

Create table [PaidBasket] (
	id int not null identity(1, 1),
	id_purchase int not null,
	itemName shortString not null,
	cost money not null,
	GRMLI volcount not null,
	count volcount not null,
	totalCost money not null,
	Constraint PaidBasket_PK Primary key (id),
	Constraint PaidBasket_Purchase_FK Foreign key (id_purchase) References [Purchase] (id) On Update Cascade On Delete Cascade
);
Go