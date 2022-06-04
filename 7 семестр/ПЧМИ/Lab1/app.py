import datetime
import os
import ssl

from flask import *
import pymongo

CONNECTION_STRING = "ur mongo-db connection string"

mongo_client = pymongo.MongoClient(
    CONNECTION_STRING,
    ssl_cert_reqs=ssl.CERT_NONE
)
lab_db = mongo_client["hmi_lab_1"]
results_collection = lab_db["results"]

app = Flask(__name__)


@app.route('/')
def index():  # put application's code here
    return render_template("index.html")


@app.post('/results')
def results():
    content = request.get_json(silent=True)
    content['datetime'] = datetime.datetime.now()
    content['test'] = os.name == 'nt'
    results_collection.insert_one(content)
    return json.dumps({'success': True}), 200, {'ContentType': 'application/json'}


if __name__ == '__main__':
    app.run(host='0.0.0.0')
