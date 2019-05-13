# Hair Salon ![alt text](https://github.com/adam-p/markdown-here/raw/master/src/common/images/icon48.png "Logo Title Text 1")
#### An MVC web application for a hair salon. The owner is able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist. , 05.10.2019






[ By _**Mariia Stashuk**_](https://www.linkedin.com/in/mariia-stashuk-66754816a/)

## Description

A program that gathers both a word and sentence from a user, then checks how frequently the word appears in the sentence. It should check for full word matches only. (For instance, if provided the word "cat" and sentence "I'm walking to the cathedral." the first three letters of the word cathedral would not be counted.)



## Setup/Installation Requirements

_To run this project,install it locally:_


* Download .NET Core 2.1.3 SDK and .NET Core Runtime 2.0.9 and install them. Download Mono and Install it.
* Clone this repository https://github.com/Marrri4ka/HairSalon.Solution.git
* Open the project in your preferred text editor to modify the files.
* To run the program, navigate to WordCounter.Solution/WordCounter and use $dotnet build &&  $dotnet run
* Navigate to localhost:5000


## Specifications

 List of specs from the simplest possible behavior to the most complex behavior:

| Behavior       |
| ------------- |
|As a salon employee, I need to be able to see a list of all our stylists.  |
|As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist |
| As an employee, I need to add new stylists to our system when they are hired. |  
|As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.|




| Setup instructions with all commands necessary to re-create  databases, columns, and tables:     |
| ------------- |
|Start MAMP and click Open WebStart page in the MAMP window.  |
|In the website you're taken to, select phpMyAdmin from the Tools dropdown.|
| Select the Import tab. |  
|Note that it's important to make sure you're not importing to a database that already exists, so make sure that a database with the same name as the one you're importing isn't already present.|
|In MySQL:

* CREATE DATABASE hair_salon;
* USE hair_salon;
* CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
* CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255),appointment DATE(), stylist_id(INT11));|


## Homepage


![Alt text](/img1/1.png)

## List of stylists

![Alt text](/img1/2.png)

## List of clients

![Alt text](/img1/3.png)

## Registration form

![Alt text](/img1/4.png)


## Known Bugs

No bugs.

## Support and contact details

If you have any questions or problems, please contact Mariia (mariiapopovych@gmail.com)

## Technologies Used

* C#
* .NET


### License

*License under MIT*

[![MIT Licence](https://badges.frapsoft.com/os/mit/mit.svg?v=103)](https://opensource.org/licenses/mit-license.php)

Copyright (c) 2019 **_Mariia Stashuk_**
