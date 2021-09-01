# AspNetMicroservices
AspNetMicroservices

* create blank solution and change solution folder name to 'src'
* create Catalog.API project. disable https, enable Open API

## MongoDB Docker
https://hub.docker.com/_/mongo
* open powershell in solution src folder
* also visualize in docker desktop

    > docker ps

    > docker pull mongo

    * -d >detach mode
    * -p > port
    * docker run -d -p 27017:20717 --name {DB Name} {Image Name}
    > docker run -d -p 27017:20717 --name shopping-mongo mongo

    
    * shows currently running container eg:shopping-mongo 
    > docker ps

    * show stopped containers
    > docker ps -a

    * see list if docker images present
    > docker images

    * for toubleshooting
    >docker logs -f shopping-mongo
    
    * for getting into mongo interactive terminal for shopping-mongo container
    >docker exec -it shopping-mongo /bin/bash 

    >ls

    >mongo

    * show databases
    >show dbs

    * create CatalogDb database
    >use CatalogDb

    * create Products collection (table)
    >db.createCollection('Products')
    >db.Products.insertMany([
      {
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
    
    >db.Products.find({}).pretty()

    * remove collection (drop table)
    >db.Products.remove({})

    >show databases

    * to see all collections like Products,..
    >show collections

* after running docker container and closed. again running shoes  error "the container name  '/shopping-mongo' already in use by container".to rerun
    > docker start {container name}
    * docker start a141

    > docker ps

  
# Catalog API Microservices
* Asp.Net Core web API application
* REST API, CRUD Operations
* MongoDB database connection and contanorization
* repository pattern
* Contanerize Microservices with MongoDB using Docker Compose.

Method      |    Request Url       |   UseCase
------------|----------------------|---------------
GET         | api/v1/Catalog    |  listing products and Categories
GET         | api/v1/Catalog/{id} | Get product with product Id
GET         | api/v1/Catalog/GetProductByCategory/{category}|Get Products by category
POST      |api/v1/Catalog       | Create new product
PUT       |api/v1/Catalog       | Update product
DELETE    |api/v1/Catalog/{id}  | delete product

* use layered architechture
    * Data Access layer
    * Business Login Layer
    * Presentation layer

* package manager console (nuget install) in Catalog-API MS
    > Install-Package MongoDB.Driver

    * update swagger nuget
    >Update-Package -ProjectName catalog.API

### Containerize Catalog Microservice with MongoDB using Docker compose
* Docker compose is a tool that enables application to be defined and run.
* it can define multiple container definitions in single file and run the application by raising all the requirements that the application needs with single command

* right click on catalog.API project>Add> container orchestration support.
* Visual studio creates dockerfile in project and docker compose in solution. docker compose overrides  yaml file
* from application will start and runs in docker with play button

* Docker Compose settings in Visual studio>tools>options>docker compose
    * pull required docker images on project open > false
    * pull updated docker images on project open > never
    * remove containers on project close > true
    * run containers on project open > false

* docker-compose.override.yml file can be created different environments
* in docker-compose.yml
    * add mongodb image microservice. (officil name in docker hub 'mongo')
    * add volumes> docker volumes is a datastorage that are outside of containers and exists on the host file system.ie., even if mongo image is removed, docker will save database records,tables inside mongo_data (any name)

* override docker-compose.override.yml with catalogdb,catalog.api containers
* right click dockerCompose project > terminal powershell
    > ls

    > docker ps

    * stop already created containers
    > docker stop {container_id}

    * remove the container
    > docker rm {container_id}

    * show created images
    >docker images

    * remove image
    > docker rmi {image_id}

* docker-compose.yml file pulls the image of mongo & .net api
* docker-compose.override.yml injects configuration for them
    
    * run docker compose select file and is override. up in decth mode.check docker desktop for new containers
    > docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

    > docker ps

    >docker images

Now Open http://localhost:8000/swagger/index.html to access catalog.api running in docker container



