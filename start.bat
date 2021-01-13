ECHO OFF


ECHO Build Docker image GATEWAY
cd src/Gateway/OcelotGateway.API/OcelotGateway.API
docker build -t order.gateway.api .
cd ..\..\..\..

ECHO Build Docker image ORDER COMMAND
cd src/OrderCommand/Order.Command.API/Order.Command.API
docker build -t order.command.api .
cd ..\..\..\..

ECHO Build Docker image Query GO
cd src/OrderQueryWithGo
docker build -t order.query.go.api .
cd ..\..

ECHO Build Docker image Query PYTHON

cd src/OrderQueryWithPython
docker build -t order.query.py.api .


ECHO UP Docker COMPOSE

docker-compose up

PAUSE