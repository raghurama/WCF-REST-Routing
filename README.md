WCF-REST-Routing
================

Describes necessary configuration for WCF REST based routing and lists out sample files

** URL which explains WCF REST
http://www.a2zdotnet.com/View.aspx?Id=189#.U4xF1aKwH3Q

** Main Assembly References
System.ServiceModel - 
System.ServiceModel.Activation - 
System.ServiceModel.Web


** Note: WCF REST uses MVC routing. Thereby if we are registering "Projects" service in Global.asax file then at runtime it will check for "Projects" folder in UI source code. So its important to have UI file .aspx in "Projects" folder itself for the code to run.
