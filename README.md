# Inventory Manager

This project is an inventory/stock manager made using C# and MySql. You are able to register a new company, then register as a new user for that company. You can also register as a new user for a company that already exists. Once you register, you can then login to your company's inventory. 

You will be able to view all of your departments. You can add a department or remove it. You can view the inventory for each department and view and add products. You will see information such as the ID, the name of the product, the department, the quantity, the price per item and the total value. You are able to edit any of the product details using the edit button attached to each product. At the bottom you will see the total number of products, the total quantity, and total stock value for that department.

You are also able to view a supplier list where you will see the supplier ID, name, and the number of products they supply you. You can edit any of the details using the edit button attached to each supplier. 

You are also able to view statements about your inventory. Whenever you increase the quantity of one of your products, it will be added to a purchases receipt where you will see a transaction number, the date, the time, the product bought, the amount bought, the price per item, and the total amount paid for that transaction. At the bottom you will see the total amount you have paid for all the transactions.

An identical process happens when you decrease the quantity of an item, it will be added to a sales receipt where you will be able to see all the same information as the purchase receipt. 

Finally, you will be able to view a statement about the inventory information. It contains the number of departments, number of suppliers, the total products, the total quantity of items, and the total value of your entire inventory all on one statement.

This application will allow you to manage an inventory and see all relevant information about that inventory. It uses a MySql database to store all the information which is accessed through queries in the C# code.

## Screenshots

### Login
![Login](https://raw.githubusercontent.com/alex-govier5/Inventory-Manager/master/Login.PNG "Login") 

### Departments
![Departments](Deps.png "Departments")

### Inventory
![Inventory](Inv.png "Inventory")

### Receipt
![Receipt](rec.png "Receipt")

## Installation

You can install the application for your Windows machine by clicking [here](InventoryManager.zip). Unzip the file and click the setup.exe file to begin installation. The setup wizard will help with the rest. 