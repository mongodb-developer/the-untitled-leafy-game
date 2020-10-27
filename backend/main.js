const { MongoClient } = require("mongodb");
const Express = require("express");
const BodyParser = require('body-parser');

const server = Express();
const client = new MongoClient(process.env["SKUNKWORKS_URI"]);

server.use(BodyParser.json());
server.use(BodyParser.urlencoded({ extended: true }));

var collection;

server.post("/answer", async (request, response, next) => {
    try {
        let result = await collection.findOne({ "problem_id": request.body.problem_id, "answer": request.body.answer });
        response.send(result || {});
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/question", async (request, response, next) => {
    try {
      let result = await collection.find({}).toArray();
      response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/question/:question", async (request, response, next) => {
    try {
        let result = await collection.findOne({ "problem_id": request.params.question });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.listen("3000", async () => {
    try {
        await client.connect();
        collection = client.db("game").collection("questions");
        console.log("Listening at :3000...");
    } catch (e) {
        console.error(e);
    }
});