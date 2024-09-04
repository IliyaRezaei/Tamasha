# Tamasha

# Startup
Open SSMS (SQL Server Management Studio), right-click on Databases, select Import Data-tier Application, and import the Tamasha.bacpac file (You can do this with any DBMS). This will load the tables and stored procedures into a new database. (If you encounter an error, you may need to update SSMS.)
Go to the main project folder, open Database/SQL.cs, and update the connection string with the new database you imported.
