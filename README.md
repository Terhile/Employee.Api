# Employee.Api


Installation :
1: start you kafka zookeeper and server as in this guide https://kafka.apache.org/quickstart
2: createa a topic called "quickstart-events" using  .\bin\windows\kafka-topics.bat --create --topic quickstart-events --bootstrap-server localhost:9092  (on WIndows)
3: update appsettings.json with server and topic details if you are not using the defaults
4: start app using Visual studio or dotnet run command

The api is connecting to test sql server on Azure, sql user is and password are in the appsettings.json file

 
