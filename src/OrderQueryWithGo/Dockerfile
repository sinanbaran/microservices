FROM golang:alpine AS builder
ADD . /go/src/
RUN apk add git
RUN go get github.com/gorilla/mux
RUN go get go.mongodb.org/mongo-driver/bson
RUN go get go.mongodb.org/mongo-driver/mongo
RUN go get go.mongodb.org/mongo-driver/mongo/options
WORKDIR /go/src
RUN go build -o main .
CMD ["./main"]