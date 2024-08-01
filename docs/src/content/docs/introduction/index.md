---
title: Purpose
description: A general description of why FeatureManagement.UI exist.
---

This library is meant to create, use and provide something developers are calling `Feature Flag` or `Feature Toggle`. It can be viewed as a configuration system. The main benefit over using a simple configuration file like `appsettings.json` is that:

1. You can change any configuration value dynamically
2. You get a simple UI to configure your app

## Do's and Don'ts

This library has for only purpose to let you create, use and provide Feature Flags inside your ASP.NET Web API. It is NOT meant to become a CMS or any storage system (file, SQL or NoSQL). Here are some examples of what you can or cannot do with the library:

- ✅ create features that can be manually deactivated at any time
- ✅ create a light/dark theme feature
- ✅ temporarily store a welcome message
- ❌ store a list of blog articles
- ❌ store chat messages
- ❌ store sensitive data (credentials, connection strings, etc...)
