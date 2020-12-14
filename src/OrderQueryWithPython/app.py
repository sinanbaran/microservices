import os
from flask import Flask
from flask import jsonify
from flask import request
from flask_pymongo import PyMongo

app = Flask(__name__)

app.config['MONGO_DBNAME'] = 'OrderAggregate'
app.config['MONGO_URI'] = 'mongodb://root:password@mongodb:27017/OrderAggregate?authSource=admin'

mongo = PyMongo(app)

@app.route('/api/order/list', methods=['GET'])
def get_all_stars():
  star = mongo.db.OrderMaterializedView
  output = []
  for s in star.find():
    output.append(s)
  return jsonify(output)

if __name__ == '__main__':
      app.run(host='0.0.0.0', port=os.getenv('PORT'),debug=False)
      