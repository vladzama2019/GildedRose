# GildedRose

In addition to this file, the project has ReadMe.docx file with more details and images.

## Introduction
Gilded Rose solution implements a set of APIs for Gilded Rose inn. These APIs allows the client application to retrieve JWT token for authentication, get the current inventory, and buy an item. It is a client application responsibility to store the token and pass it to the server. Gilded Rose solution has two projects: GildedRose (APIs) and GildedRoseTest. Testing was done by  GildedRoseTest and Postman software. APIs, except GetToken, return data in JSON format. This format was selected because it allows to have a smaller message size, more readable, easier to parse, and easier to handle with Javascript, which benefit the API client.

## Environment:
1.	Windows 10 professional
2.	Visual Studio 2017
3.	Postman

## Authentication
Gilded Rose solution uses JWT security token for API authentication. Application creates a token based on provided user name. Any client application must request the token using the following uri: …/api/token/username. In addition to JWT security token, in order to make the application more secure, usage of https is strongly recommended. Authentication with JWT was selected because it has several advantages. JWT doesn’t use sessions, has no problems with mobile, it doesn’t need CSRF and it works very well with CORS too. Since JWT does not require session data to be kept on the server to perform authentication, for applications running on multiple servers, this alleviates the need for sharing session data across the servers.

## Retrieve the current inventory
GetAllItems API allows the customer to retrieve the entire collection of available items. The customer does not need to be authorize. If there is no data in the collection, this API returns an error message. In order to execute BuyItem API, clients should use the following uri: 
…/api/Inventory

## Buy an item
BuyItem API allows the customer to by an item. Application checks if the user is authorize or not. The user must be authorized to be able to use BuyItem API. The API has [Authorize] attribute and use the token. After the item is bought, the API removes it from the collection of items. API also checks if the item exists in the collection and returns an error message if the collection does not have this item. In order to execute BuyItem API, clients should use the following uri: 
…/api/Inventory/itenname

## Test Data
GildedRose application does not use any database, but uses a collection of the hard coded data. Use the name of the items below for testing BuyItem API.
1.	Name= "C Programming Language", 
    Description= "The authors present the complete guide to ANSI standard C language programming"
    Price = 16

2.	Name = "Cracking the Coding Interview: 189 Programming Questions and Solutions"
    Description = "Cracking the Coding Interview, 6th Edition is here to teach you what you need to know and enabling you to perform at your very best."
    Price = 32 

3.	Name = "Microsoft Visual C# Step by Step (9th Edition) (Developer Reference)"
    Description = "Expand your expertise--and teach yourself the fundamentals of programming with the latest version of Visual C# with Visual Studio 2017."
    Price = 34
