package main

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/gorilla/mux"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type ErrorResponse struct {
	StatusCode   int    `json:"status"`
	ErrorMessage string `json:"message"`
}

func GetError(err error, w http.ResponseWriter) {

	log.Fatal(err.Error())
	var response = ErrorResponse{
		ErrorMessage: err.Error(),
		StatusCode:   http.StatusInternalServerError,
	}

	message, _ := json.Marshal(response)

	w.WriteHeader(response.StatusCode)
	w.Write(message)
}

type OrderView struct {
	CreatedOn   time.Time     `json:"CreatedOn,omitempty" bson:"CreatedOn,omitempty"`
	Items       []ProductItem `json:"Items"`
	OrderNumber string        `json:"OrderNumber,omitempty" bson:"OrderNumber,omitempty"`
	Owner       string        `json:"Owner,omitempty" bson:"Owner,omitempty"`
	State       string        `json:"State,omitempty" bson:"State,omitempty"`
	Id          string
}
type ProductItem struct {
	ProdutId   string
	Name       string  `json:"Name,omitempty" bson:"Name,omitempty"`
	Quantity   int     `json:"Quantity,omitempty" bson:"Quantity,omitempty"`
	TotalPrice float32 `json:"TotalPrice,omitempty" bson:"TotalPrice,omitempty"`
	UnitPrice  float32 `json:"UnitPrice,omitempty" bson:"UnitPrice,omitempty"`
}

func ConnectDB() *mongo.Collection {

	clientOptions := options.Client().ApplyURI("mongodb://root:password@mongodb:27017/OrderAggregate?authSource=admin")
	client, err := mongo.Connect(context.TODO(), clientOptions)
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println("Connected to MongoDB!")

	collection := client.Database("OrderAggregate").Collection("OrderMaterializedView")

	return collection
}

var collection = ConnectDB()

func TestEndPoint(w http.ResponseWriter, r *http.Request) {

	w.Header().Set("Content-Type", "application/json")
	w.Header().Set("Source", "Go")
	var orders []OrderView
	cur, err := collection.Find(context.TODO(), bson.M{})
	if err != nil {
		GetError(err, w)
		return
	}
	defer cur.Close(context.TODO())

	for cur.Next(context.TODO()) {
		var order OrderView
		err := cur.Decode(&order)
		if err != nil {
			log.Fatal(err)
		}
		orders = append(orders, order)
	}
	if err := cur.Err(); err != nil {
		log.Fatal(err)
	}
	json.NewEncoder(w).Encode(orders)
}

func main() {

	r := mux.NewRouter()
	r.HandleFunc("/api/order/list", TestEndPoint).Methods("GET")
	http.ListenAndServe(":8081", r)

}
