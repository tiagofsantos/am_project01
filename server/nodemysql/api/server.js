"use strict";
 
var http = require("http");
const express = require('express');
const app = express(); 

/* body parsers*/
var bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

/* Routes */
const usersRoutes=require('./routes/users');
const sessionsRoutes=require('./routes/sessions');
const levelsRoutes=require('./routes/levels');
const charactersRoutes=require('./routes/characters');
const actionsRoutes=require('./routes/playerActions');
const statisticsRoutes=require('./routes/statistics');
const friendsRoutes=require('./routes/friends');

class Server
{
    constructor()
    {
        this.port = 8080;
        this.ip = "localhost";
        this.start();
    }
 
    start()
    {
        this.server = http.createServer(app);
        app.use('/users',usersRoutes);
        app.use('/sessions',sessionsRoutes);
        app.use('/levels',levelsRoutes);
        app.use('/characters',charactersRoutes);
        app.use('/actions',actionsRoutes);
        app.use('/statistics',statisticsRoutes);
        app.use('/friends',friendsRoutes);
        
        console.log("Server created");
    }
 
    listen()
    {
        this.server.listen(this.port, this.ip);
        console.log("Server listening for connections");
    }
 
    
}
 
module.exports.Server = Server;