# **ip-alchemist-cli**

ip-alchemist-cli is a command-line tool that provides a variety of useful features for subnetting IPv4 addresses.

<p align="center">
 <img width=200px height=200px src="assets\ip-alchemist-cli.png" alt="Project logo"></a>
</p>

## **Getting Started**

### **Prerequisites**

-   .NET 7.0 SDK or later

### **Installation**

-   Clone or download the repository to your local machine.
-   Open a terminal or command prompt and navigate to the project directory `(./src/ip-alchemist-cli)`

To build and run, execute the following commands:

```
dotnet build
dotnet run
```

### **Usage**

To use the app, simply run it from the command line and follow the prompts. You can choose from the available features by selecting a from the menu.

**Menu**

![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/menu.png)

**Network Information**

```
eg. 192.168.1.0/24
```

![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/network-information.png)

**Fixed-length Subnet Mask**

```
eg. 192.168.1.0/24, 4 subnets
```

![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/flsm-1.png)
![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/flsm-2.png)
![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/flsm-3.png)

**Variable-length Subnet Mask**

```
eg. 192.168.1.0/24, 2 subnets [100 hosts, 45 hosts]
```

![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/vlsm-1.png)
![image](https://github.com/mk-milly02/ip-alchemist-cli/blob/master/assets/vlsm-2.png)

## **Contributing**

Contributions are welcome! If you have any suggestions, bug reports, or feature requests, please open an issue or submit a pull request.

## **License**

This project is licensed under the MIT License. See the `LICENSE` file for details.
