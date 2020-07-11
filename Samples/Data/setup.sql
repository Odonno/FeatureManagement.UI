/*

Enter custom T-SQL here that would run after SQL Server has started up. 

*/

IF DB_ID (N'SampleDb') IS NULL
    CREATE DATABASE SampleDb;
GO
