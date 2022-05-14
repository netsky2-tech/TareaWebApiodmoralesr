CREATE PROCEDURE Production.InsertProductCategory
@ProductCategoryID int output,
@Name NVARCHAR(50),
@return tinyint output,
@return_message varchar(500) output
as
if not exists (select 1 from Production.ProductCategory aty where aty.Name = @Name)
begin 
	begin transaction
	begin try
		insert into Production.ProductCategory([Name])
		values (@Name)
		--
		set @ProductCategoryID = SCOPE_IDENTITY();
		set @return = 0;
		set @return_message = 'Registro grabado correctamente';
		--
		if @@TRANCOUNT>0
			commit transaction
	end	try
	begin catch
		if @@TRANCOUNT>0
			rollback transaction
		set @return = 2;
		set @ProductCategoryID = 0;
		set @return_message = ERROR_MESSAGE();
	end catch
end
else
begin
	set @return = 1;
	set @return_message = 'Ya existe un registro con ese nombre.';
end
go

--declare @i int,
--		@r tinyint,
--		@rm nvarchar(500)
--exec Production.InsertProductCategory @ProductCategoryID = @i output, @Name = 'Motocycles', @return = @r output, @return_message = @rm output
--select @i,@r,@rm

--select * from Production.ProductCategory where ProductCategoryID = 5;

CREATE PROCEDURE Production.UpdateProductCategory
@ProductCategoryID int,
@Name NVARCHAR(50),
@return tinyint output,
@return_message varchar(500) output
as
if not exists (select 1 from Production.ProductCategory aty where aty.Name = @Name and aty.ProductCategoryID  != @ProductCategoryID)
begin 
	begin transaction
	begin try
		update Production.ProductCategory
		set [Name] = @Name,
		ModifiedDate = GETDATE()
		where ProductCategoryID = @ProductCategoryID
		--
		set @ProductCategoryID = SCOPE_IDENTITY();
		set @return = 0;
		set @return_message = 'Registro actualizado correctamente';
		--
		if @@TRANCOUNT>0
			commit transaction
	end	try
	begin catch
		if @@TRANCOUNT>0
			rollback transaction
		set @return = 2;
		set @ProductCategoryID = 0;
		set @return_message = ERROR_MESSAGE();
	end catch
end
else
begin
	set @return = 1;
	set @return_message = 'Ya existe un registro con ese nombre.';
end
go

--declare @i int,
--		@r tinyint,
--		@rm nvarchar(500)
--exec Production.UpdateProductCategory @ProductCategoryID = 5, @Name = 'Motocycle', @return = @r output, @return_message = @rm output
--select @i,@r,@rm

--select * from Production.ProductCategory where ProductCategoryID = 5;
--drop procedure Production.DeleteProductCategory
CREATE PROCEDURE Production.DeleteProductCategory
@ProductCategoryID int,
@return tinyint output,
@return_message varchar(500) output
as
if exists (select 1 from Production.ProductCategory aty where aty.ProductCategoryID  = @ProductCategoryID)
begin 
	begin transaction
	begin try
		delete from  Production.ProductCategory
		where ProductCategoryID = @ProductCategoryID
		--
		set @ProductCategoryID = SCOPE_IDENTITY();
		set @return = 0;
		set @return_message = 'Registro eliminado correctamente';
		--
		if @@TRANCOUNT>0
			commit transaction
	end	try
	begin catch
		if @@TRANCOUNT>0
			rollback transaction
		set @return = 2;
		set @ProductCategoryID = 0;
		set @return_message = ERROR_MESSAGE();
	end catch
end
else
begin
	set @return = 1;
	set @return_message = 'Ya existe un registro con ese nombre.';
end
go

declare @i int,
		@r tinyint,
		@rm nvarchar(500)
exec Production.DeleteProductCategory @ProductCategoryID = 5, @return = @r output, @return_message = @rm output
select @i,@r,@rm

--select * from Production.ProductCategory where ProductCategoryID = 5;