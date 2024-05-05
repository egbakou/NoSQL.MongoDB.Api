# Documentation

- [Quick Start - C#/.NET v2.25 (mongodb.com)](https://www.mongodb.com/docs/drivers/csharp/current/quick-start/)
- [Quick Reference - C#/.NET v2.25 (mongodb.com)](https://www.mongodb.com/docs/drivers/csharp/current/quick-reference/)

- [Query Your Data - MongoDB Compass](https://www.mongodb.com/docs/compass/current/query/filter/)

- [MongoDB Schema Design Best Practices | MongoDB](https://www.mongodb.com/developer/products/mongodb/mongodb-schema-design-best-practices/)

- https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/poco/

- https://www.mongodb.com/docs/manual/reference/limits/#naming-restrictions

- [Massive Arrays | MongoDB](https://www.mongodb.com/developer/products/mongodb/schema-design-anti-pattern-massive-arrays/)

# Run MongoDB locally

> [Difference between mongo and mongodb/community-server : r/mongodb (reddit.com)](https://www.reddit.com/r/mongodb/comments/1658qs8/difference_between_mongo_and/)

- Official docker image maintained by MongoDB, the build repo is not open source [mongodb/mongodb-community-server](https://hub.docker.com/r/mongodb/mongodb-community-server)
- Official docker image maintained by the Docker community: [mongo](https://hub.docker.com/_/mongo)
- Bitnami version: [bitnami/mongodb](https://hub.docker.com/r/bitnami/mongodb)



```yaml
services:
  nosqlmongodbapi:
    container_name: nosqlmongodbapi
    image: nosqlmongodbapi
    build:
      context: .
      dockerfile: NoSQL.MongoDB.Api/Dockerfile
    ports:
        - "8082:8080"
        - "8083:8081"
    environment:
        - ASPNETCORE_ENVIRONMENT=Development
        # https://www.mongodb.com/docs/manual/reference/connection-string/#connection-string-components
        # when using specific user other than admin, database name should be specified and authSource should not be used
        - MongoDB__ConnectionString=mongodb://dev:S3cr3tPFGDdcv34@mongo:27017/sample_mflix
        - MongoDB__Database=sample_mflix
    depends_on:
      - mongo
  
  # mongo
  mongo:
    container_name: mongo
    image: bitnami/mongodb:latest
    ports:
      - "27017:27017"
    environment:
      - MONGODB_DATABASE=sample_mflix
      - MONGODB_ROOT_USERNAME=root
      - MONGODB_ROOT_PASSWORD=S3cr3tPFGDssw0rd
    volumes:
      - mongo-data:/bitnami/mongodb
      - ./mongo-init.js:/docker-entrypoint-initdb.d/mongo-init.js
  
  # mongo-express
  mongo-express:
    container_name: mongo-express
    image: mongo-express
    ports:
      - "8081:8081"
    environment:
      - ME_CONFIG_MONGODB_SERVER=mongo
      - ME_CONFIG_MONGODB_PORT=27017
      - ME_CONFIG_BASICAUTH_USERNAME=admin
      - ME_CONFIG_BASICAUTH_PASSWORD=StrongPassword
      - ME_CONFIG_MONGODB_ENABLE_ADMIN=true
      - ME_CONFIG_MONGODB_ADMINUSERNAME=root
      - ME_CONFIG_MONGODB_ADMINPASSWORD=S3cr3tPFGDssw0rd


volumes:
    mongo-data: {}
```

```javascript
// mongo-init.js is the script that will be run when the container is started. https://stackoverflow.com/questions/63172735/mongodb-database-could-not-be-created-on-docker-container-startup

// Move to the admin database, always created by default: https://stackoverflow.com/a/68253550
db = db.getSiblingDB('admin');
// log in as the root user
db.auth('root', 'S3cr3tPFGDssw0rd');

// create and move to the new database
db = db.getSiblingDB('sample_mflix');

// create the user with read and write access
db.createUser({
    user: 'dev',
    pwd: 'S3cr3tPFGDdcv34',
    roles: [
        {
            role: 'readWrite',
            db: 'sample_mflix'
        }
    ]
});

// create the collection
db.createCollection('movies');
db.createCollection('actors');
```

bitnami/mongodb could be replaced by mongo

```
  # mongo
  mongo:
    container_name: mongo
    image: mongo:7.0.10-rc0-jammy
    ports:
      - "27017:27017"
    environment:
      - MONGO_INITDB_ROOT_USERNAME=root
      - MONGO_INITDB_ROOT_PASSWORD=S3cr3tPFGDssw0rd
      - MONGO_INITDB_DATABASE=sample_mflix
    volumes:
      - mongo-data:/data/db
      - ./mongo-init.js:/docker-entrypoint-initdb.d/mongo-init.js
```

