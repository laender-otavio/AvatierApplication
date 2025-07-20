# AvatierApplication
Project for Avatier

* To execute this application, you can only Compile and start, to use the endpoints there is Swagger to help.
* The database is local, so will be generated on compilation time and "deleted" when the application finish, that means that to assign a role to an user, every time that you start the application you will need to create an user again.
* There is only 3 accepted roles: "ADMIN", "SELLER", "RECEPTIONIST".
* The name, password and email fields were created with a 100 character limit, so this will be validated.
* The email should have between 6 and 100 characters and at least one "." to be valid