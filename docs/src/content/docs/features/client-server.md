---
title: Client vs. server feature
description: A feature can be either server-managed or client-managed.
sidebar:
  order: 1
---

## Client feature

A client feature has a different value for each user of the application. Each user will see the default value but they can update it at anytime (default behavior).

```csharp
var themes = new List<string>
{
    "light",
    "dark"
};

configuration.ClientFeature("Theme", themes[0], "Choose a theme for the frontend", themes);
```

## Server feature

A server feature is defined globally and it will have the same value for every user of the application.

```csharp
configuration.ServerFeature("Beta", true);
```

Server features are often only managed by an admin.
