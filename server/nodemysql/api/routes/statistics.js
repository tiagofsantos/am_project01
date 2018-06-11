const express = require('express');
const router= express.Router();
const mysql = require('mysql');
const db = require('../db/dbConnector');


router.post('/', (req, res) => {
    console.log('insert into estatistica(valor,idTipoEstatistica,idSessao) values("'+
    req.body.value+'",'+req.body.statisticTypeID+','+req.body.sessionID+')');
    let sql='insert into estatistica(valor,idTipoEstatistica,idSessao) values("'+
    req.body.value+'",'+req.body.statisticTypeID+','+req.body.sessionID+')';
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: err});;
        res.json({success: true, message: result["insertId"]});
    });
});


router.get('/:id', (req, res) => {
    let sql=`SELECT * FROM estatistica where idSessao=${req.params.id}`;
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: err});;
        res.json({success: true, message: result});
    });
});

module.exports=router;