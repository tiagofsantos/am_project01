const express = require('express');
const router= express.Router();
const mysql = require('mysql');
const db = require('../db/dbConnector');

router.get('/', (req, res) => {
    let sql=`SELECT * FROM nivel`;
    let query = db.query(sql, (err, result) => {
        if(err) throw res.json({success: false, message: err});;
        //res.send(result);
        res.json({success: true, message: result});
    });
});

module.exports=router;