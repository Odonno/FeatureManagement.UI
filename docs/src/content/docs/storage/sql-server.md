---
title: SQL Server
description: Usage of the SQL Server provider for FeatureManagement.UI.
sidebar:
  order: 2
---

```cs
services.AddFeatures(c =>
{
    string connectionString = Configuration.GetConnectionString("DefaultConnection");
    c.AddSqlServerStorage(connectionString);
});
```
