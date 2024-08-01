---
title: REST API
description: Features can be consumed via a GraphQL server.
sidebar:
  order: 2
---

Once you map the features UI, you give everyone the ability to call an API the features. As an example, it can be pretty handy if you write a React application and you want to enable/disable some features dynamically.

The API is defined like this:

### Retrieve all features

```bash
GET - /features
```

### Set feature value

```bash
POST - /features/{featureName}/set
Payload: { value: boolean | number | string }
```

### Retrieve authentication schemes

```bash
GET - /features/auth/schemes
```
