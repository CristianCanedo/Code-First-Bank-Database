# Code-First-Bank-Database
Creating and developing bank application logic in console using code first entity framework model.

Creating a one-to-one relationship from Customers table to Account table. In order to make more sense in the logic I have to instead add a migration to the database scheme that will remove all columns except for Username and Password in the Customers table, and take them to the Account table.

Refactoring the code will be necessary for the CreateCustomer() method.
