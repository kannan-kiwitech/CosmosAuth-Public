# CosmosAuth

## About the project

This repository contains three projects

1.  Backend API
2.  Unit Test Project for Backend API
3.  Frontend Web Interface


### 1. Backend API

#### Techstacks

- .Net 5
- C#
- CosmosDb
- Web API

This is a simple `.Net 5` `Web API` project using `CosmosDb` as a backend data store and has some basic implementation of `signin`, `signup` and `profile` API. In the registration process system will send a welcome email to the user using `SendGrid`

#### How to run the project

```
git clone https://github.com/kannan-kiwitech/CosmosAuth.git
cd .\CosmosAuth\CosmosAuth\
dotnet run
```

Now you can open the `swagger` documentation by visiting the following page.

```
https://localhost:5001/swagger/index.html
```

### 2. Unit Test Project for Backend API

Just a basic implementation of unit test using `xUnit` framework. There are only three tests are implemented for `signin` API.

#### How to run the project

```
git clone https://github.com/kannan-kiwitech/CosmosAuth.git
cd .\CosmosAuth\CosmosAuth.Tests\
dotnet test
```

### 3. Frontend Web Interface

#### Techstacks

- Angular 12x

This frontend application is basically consuming the Backend APIs to `signin`, `signup` and fetching he `profile`.

#### How to run the project

Assuming that `nodejs` and `angular cli` are pre-installed

```
git clone https://github.com/kannan-kiwitech/CosmosAuth.git
cd .\CosmosAuth\FrontendWeb\
npm install
ng serve
```
Application will be available on the following url

```
http://localhost:4200/
```

