# Customer-GraphQL-Hotchocolate-.NETCore

## Technology Stack:
●	C#, .NET 5.0

●	The API should be exposed via GraphQL using HotChocolate library (https://chillicream.com/) v11

●	Entity Framework Core 5

●	SQL Server as database (mssqllocaldb)

●	xUnit v2

## Configuration:

In appSetting.json file, verify the connection string. Default connection is set for (localdb)\\mssqllocaldb. 

 "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GraphQLDemo;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  
#### Note: If database doesn't exist, application will created it automatically with default data.

## How to run:
●	Build and run application in Visual Studio.

●	It will open built-in GraphQL IDE Banana Cake Pop.

● Use below query to get list of customers:

query{
  customers{
    id,
    name, 
    email,
    createdAt,
    code,
    status,
    isBlocked
  }  
}
 
![image](https://user-images.githubusercontent.com/32801172/128982723-e99962ac-c4c0-4e1b-bb60-53ea4bf1ffe6.png)


● Use below mutation to create customer:

{
   "inputType":{
   "Email":"abc@gmail.com",
   "Name":"Abc",
   "Code":7500,
   "Status": "ACTIVE",
   "IsBlocked": false
  }
}

mutation ($inputType:AddCustomer ){
 create(input :$inputType) {
   id
 }
}


![image](https://user-images.githubusercontent.com/32801172/128982931-42c6ad90-7c7e-4654-bb31-4bd146cfe453.png)


● Use below mutation to update customer:

{
  "inputType":{
   "id": "16",
   "name": "Abc - UPDATED",
   "email": "abc@gmail.com",
   "createdAt": "2021-08-09T21:12:39.913+05:30",
   "code": 7500,
   "status": "ACTIVE",
   "isBlocked": false
   }
}

mutation ($inputType:CustomerInput ){
 update(customer :$inputType)
}

![image](https://user-images.githubusercontent.com/32801172/128983131-d37472b5-0a05-4467-a087-b52980c8310a.png)

![image](https://user-images.githubusercontent.com/32801172/128983165-89d00997-1e24-464e-9570-e722c14ed58a.png)



● Use below mutation to delete customer:

{
  "inputType":{
   "Id": 16   
   }
}

mutation ($inputType:DeleteCustomerById ){
 deletedbyid(deleteCustomerById :$inputType)
}

![image](https://user-images.githubusercontent.com/32801172/128983205-ef554263-6d05-4d35-b99e-2ff2d0d3047e.png)

![image](https://user-images.githubusercontent.com/32801172/128983225-b0808e6b-934a-470a-b211-3b7fa9d011cd.png)

