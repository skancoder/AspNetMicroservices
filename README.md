# AspNetMicroservices

AspNetMicroservices

- create blank solution and change solution folder name to 'src'
- create Catalog.API project. disable https, enable Open API

## MongoDB Docker

https://hub.docker.com/_/mongo

- open powershell in solution src folder
- also visualize in docker desktop

  > docker ps

  > docker pull mongo

  - -d >detach mode
  - -p > port
  - docker run -d -p 27017:20717 --name {DB Name} {Image Name}

    > docker run -d -p 27017:20717 --name shopping-mongo mongo

  - shows currently running container eg:shopping-mongo

    > docker ps

  - show stopped containers

    > docker ps -a

  - see list if docker images present

    > docker images

  - for toubleshooting

    > docker logs -f shopping-mongo

  - for getting into mongo interactive terminal for shopping-mongo container
    > docker exec -it shopping-mongo /bin/bash

  > ls

  > mongo

  - show databases

    > show dbs

  - create CatalogDb database

    > use CatalogDb

  - create Products collection (table)
    > db.createCollection('Products')
    > db.Products.insertMany([
    > {
        "Name":"Asus Laptop",
        "Category":"Computers",
        "Summary":"Summary",
        "Description":"Description",
        "ImageFile":"ImageFile",
        "Price":54.93
    },{
    "Name":"Hp Laptop",
    "Category":"Computers",
    "Summary":"Summary",
    "Description":"Description",
    "ImageFile":"ImageFile",
    "Price":84.93
    }
    ])

  > db.Products.find({}).pretty()

  - remove collection (drop table)
    > db.Products.remove({})

  > show databases

  - to see all collections like Products,..
    > show collections

- after running docker container and closed. again running shoes error "the container name '/shopping-mongo' already in use by container".to rerun

  > docker start {container name}

  - docker start a141

  > docker ps

# Catalog API Microservices

- Asp.Net Core web API application
- REST API, CRUD Operations
- MongoDB database connection and contanorization
- repository pattern
- Contanerize Microservices with MongoDB using Docker Compose.

| Method | Request Url                                    | UseCase                         |
| ------ | ---------------------------------------------- | ------------------------------- |
| GET    | api/v1/Catalog                                 | listing products and Categories |
| GET    | api/v1/Catalog/{id}                            | Get product with product Id     |
| GET    | api/v1/Catalog/GetProductByCategory/{category} | Get Products by category        |
| POST   | api/v1/Catalog                                 | Create new product              |
| PUT    | api/v1/Catalog                                 | Update product                  |
| DELETE | api/v1/Catalog/{id}                            | delete product                  |

- use layered architechture

  - Data Access layer
  - Business Login Layer
  - Presentation layer

- package manager console (nuget install) in Catalog-API MS

  > Install-Package MongoDB.Driver

  - update swagger nuget
    > Update-Package -ProjectName catalog.API

### Containerize Catalog Microservice with MongoDB using Docker compose

- Docker compose is a tool that enables application to be defined and run.
- it can define multiple container definitions in single file and run the application by raising all the requirements that the application needs with single command

- right click on catalog.API project>Add> container orchestration support.
- Visual studio creates dockerfile in project and docker compose in solution. docker compose overrides yaml file
- from application will start and runs in docker with play button

- Docker Compose settings in Visual studio>tools>options>docker compose

  - pull required docker images on project open > false
  - pull updated docker images on project open > never
  - remove containers on project close > true
  - run containers on project open > false

- docker-compose.override.yml file can be created different environments
- in docker-compose.yml

  - add mongodb image microservice. (officil name in docker hub 'mongo')
  - add volumes> docker volumes is a datastorage that are outside of containers and exists on the host file system.ie., even if mongo image is removed, docker will save database records,tables inside mongo_data (any name)

- override docker-compose.override.yml with catalogdb,catalog.api containers
- right click dockerCompose project > terminal powershell

  > ls

  > docker ps

  - stop already created containers

    > docker stop {container_id}

  - remove the container

    > docker rm {container_id}

  - show created images

    > docker images

  - remove image
    > docker rmi {image_id}

- docker-compose.yml file pulls the image of mongo & .net api
- docker-compose.override.yml injects configuration for them

  - run docker compose select file and is override. up in decth mode.check docker desktop for new containers
    > docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

  > docker ps

  > docker images

Now Open http://localhost:8000/swagger/index.html to access catalog.api running in docker container

### Debugging Docker-Compose

- add containers window in visula studio. view> other windows>containers
- Now Build solution > build docker image using docker-compose commands. and clean solution> runs docker-compose down commands which stop and clean containers from local environment
- before running dockerComponse button> stop all containers in docker compose. in src folder
  > docker-compose -f docker-comppose.yml -f docker-compose.override.yml down
- clicking run dockerComponse button > creates container from docker file image and runs container. this open catalog api swagger which is debuggable.
- launch will be changed automatically for docker or goto properties>debug
- while debugging mongoDb may throw error.this may be due to api application considering appsettings.json. so keep db configs in appsettings.development.json with catalogdb container name not localhost
- clean solution kills all running containers
- to run do build run

or

- to start containers (run)
  > docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
- to kill containers (clean solution)

  > docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down

- check running containers
  > docker ps
- there will 2 images for catalogApi 1.dev,2.latest
  _ dockerCompose run button uses 'dev' image with appsettings.development.json
  _ manually running docker-compose commands use 'latest' image \* dockerCompose run button release mode also uses 'latest' image

- to get list of containers
  > docker ps -aq
- to stop all images
  > docker stop $(docker ps -aq)
- to remove all the container from computer
  > docker rm $(docker ps -aq)
- to remove all the images from computer
  > docker rmi $(docker images -q)
- to remove all unused images, containers,networks,cache
  > docker system prune
- to run again use docker up command

### get mongo client image for mongodb dashbord

- https://hub.docker.com/r/mongoclient/mongoclient

  > docker pull mongoclient/mongoclient

  directly run below

  > docker run -d -p 3000:3000 mongoclient/mongoclient

- open http://localhost:3000/ , click connect to 27017 port mongodb

# Basket API Microservices

- create Asp.NetWebApi project(.net5, without https,with swagger openapi)
- change launch settings (properties>debug) from iis to project. change AppUrl port from 5000 to 5001. for docker port is 8001

### Redis:

- Opensourse No SQL Database
- Remote Dictonary Server
- Key-value pairs
- Data Structure server: can store high level data stuctures
- Extremely Fast because it works synchronously
- Save data both on RAM and disk according to configuration

* has features like: shrading, clusturing, sentinal, replication
* disadvantages:

  - doesn't work asynchronously, so performance hit
  - need RAM according to data size
  - doesn't support complex queries like relational databases
  - if a transation receives an error, there is no return

* in this API we store, basket and basket items data in Redis cache system as JSON object.

* in docker compose projects> right click terminal> in src folder

  - install redis docker image https://hub.docker.com/_/redis
    > docker pull redis

  > docker images

  > docker run -d -p 6379:6379 --name aspnetrun-redis redis

* check running containers
  > docker ps

#### for troubleshooting

- check image logs
  > docker logs -f aspnetrun-redis

* for redis interactive terminal (/bin/bash is to open bash script in interactive terminal)

  > docker exec -it aspnetrun-redis /bin/bash

  > redis-cli

  - ping gives response PONG. so redis server is working

    > ping

  * set {keyName} {value}
    > set key value

  - to return "value"
    > get key

### Basket API

| Method | Request URI        | Use Case                                              |
| ------ | ------------------ | ----------------------------------------------------- |
| GET    | api/v1/Basket      | Get Basket and items with username                    |
| POST   | api/v1/Basket      | Update basket and items (add - remove item on basket) |
| DELETE | api/v1/Basket/{id} | delete basket                                         |
| POST   | api/v1/Basket      | checkout basket                                       |

### Basket N layerd Architecture

- Data access layer
- Business logic layer
- Presentaion layer

### why Redis for Basket.API ?

- this api stores and manage baskets and basket item data in their own operations. it is best fit for caching this info.
- for Distributed Cache> its a cache shared by multiple app servers and typically maintained as external service to app servers that access it. eg: Redis cache is in basket.API microservice, all other microservices can reach this for distributed cahing

* Distributed Cache> improves performance, increase scalability especially when app is hosted by cloud service or server farm. ie, it can be served in docker desktop and can be shifted to kubernetes.

* Distributed Cache> used for storing individual application and when cache data is sorting to distributed, the data could be consisted across request from multiple services.

* nuget install

  > Install-Package Microsoft.Extensions.Caching.StackExchangeRedis

  > Update-package -ProjectName Basket.API

* CRUD basket.API from redis.

### Add Docker using visual studio container orchestration support

- Basket.API> right click> add> container orchestration support>Docker compose>Linux
  - this creates docker file in Basket.API project and updates docker-compose.yml file in solution

* override docker-compose yaml file
* before running , remove existing redis image used in development as it also exposes on same port 6379, it cause port conflict error.

> docker ps

> docker stop {redis_containerId}

> docker ps -a

> docker rm {redis_containerId}

- redis image completed removed
  > docker ps -a

* run
  > docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
  - http://localhost:8001/swagger/index.html

# Portainer

- open source
- manages container based applications
- Kubernetes, Docker, Docker swarm, Azure ACI and edge environments
- manages environments, deploy applications, monitor app performance and triage problems

* less code, easy management and visualize docker components in dashboard
* https://portainer.readthedocs.io/en/master/deployment.html
* portainer management dashboard> https://hub.docker.com/r/portainer/portainer-ce
* add poratiner in docker-compose.yml and its override. then run docker up
  > docker ps
* open http://localhost:9000/#!/init/admin and create user ("admin"|"admin1234")and login >select docker

# Discount.API Microservice

- create Asp .Net Core Web API and set port to 5002

* setup Postgres DB in docker environment
  - https://hub.docker.com/_/postgres official image
  * add postgres in docker-compose yaml and its override file
  * get pgAdmin management tool https://hub.docker.com/r/dpage/pgadmin4 image. pull this image to docker compose and change dockerCompose files and up
    - open http://localhost:5050/ > 'admin@aspnetrun.com|admin1234'
    * addNewserver>
      - general> name:DiscountServer
      - connection> name :discountdb, username:admin, password:admin1234
      * this details match with discountdb in docker-compose.override.yml
      * pgadmin>tools>Query tool>
        CREATE TABLE Coupon(
        ID SERIAL PRIMARY KEY NOT NULL,
        ProductName VARCHAR(24) NOT NULL,
        Description TEXT,
        Amount INT
        ); INSERT INTO Coupon (Productname,description,amount) VALUES('IPhone X','IPhone Discount',150);INSERT INTO Coupon (Productname,description,amount) VALUES('Samsung 10','Samsung Discount',100); SELECT \* FROM public.coupon ORDER BY id ASC
  * do Discount.API crud repository pattern and test
* create docker file in Discount.API and modify docker-compose.yml and its override
* do initial migration of postgres in Program.cs file with retry logic.
* check retry logic by stopping postgreddb and starting

  > docker stop {postgreddb_containerId}

  > docker start {postgreddb_containerId}

* docker up
