const express = require('express');
const router= express.Router();
const mysql = require('mysql');
const db = require('../db/dbConnector');

router.post('/', (req, res) => {
    var list=JSON.parse(req.body.list);
    
        for(var i=0; i< list.length ;i++){
            let sql = 'insert into AçaoJogador(açao,tickInicial,tickFinal,idSessao) values("'+
                list[i].action+'",'+parseInt(list[i].tickInicial)+','+parseInt(list[i].tickFinal)+
                ','+parseInt(list[i].sessionID)+');';
            let query = db.query(sql, (err, result) => {
                if(err) throw res.json({success: false, message: err});       
            });
        }
        res.json({success: true, message: 'Guardado!'});
});

router.get('/:id', (req, res) => {
    let sql=`SELECT * FROM AçaoJogador where idSessao=${req.params.id}`;
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: "session not found"});;
        res.json({success: true, message: result});
    });
});

module.exports=router;