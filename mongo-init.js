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