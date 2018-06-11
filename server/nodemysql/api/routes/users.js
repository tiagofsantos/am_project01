const express = require('express');
const router= express.Router();
const mysql = require('mysql');
const db = require('../db/dbConnector');
  
router.get('/', (req, res) => {
    let sql='SELECT * FROM Utilizador';
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: err});;
        console.log(result[0].idUtilizador);
        res.json({success: true, message: result});
    });
});

router.get('/:id', (req, res) => {
    let sql=`SELECT * FROM Utilizador where idUtilizador=${req.params.id}`;
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: "user not found"});;
        console.log(result[0].idUtilizador);
        res.json({success: true, message: result});
    });
});

router.post('/', (req, res) => {
    console.log('SELECT idUtilizador FROM Utilizador where username="'+req.body.username+'"');
    let sql='SELECT idUtilizador FROM Utilizador where username= "'+req.body.username+'"';
    let query = db.query(sql, (err, result) => {
        if(err) throw err;
        console.log(result);
        if(result.length==0){
            res.json({success: false, message: "username not found"});
        }else{
            let sql='SELECT * FROM Utilizador where idUtilizador='+result[0].idUtilizador+' and pw='+req.body.password;
            let query = db.query(sql, (err, result) => {
            if(err) throw err;
                if(result.length==0){
                    res.json({success: false, message: "password incorrect"});
                }else{
                    res.json({success: true, message: result[0]});
                }
            });
        }
    });
});

module.exports=router;