const express = require('express');
const router = express.Router();
const mysql = require('mysql');
const db = require('../db/dbConnector');

router.get('/:id', (req, res) => {
    let sql = `SELECT u.idUtilizador,username,dataNascimento,pais,email FROM Utilizador u
    inner join Amigo a on a.idUtilizador=u.idUtilizador where a.idUtilizador in 
    (select idUtilizadorAmigo from Amigo where idUtilizador=${req.params.id})`;
    let query = db.query(sql, (err, result) => {
        if (err) throw res.json({ success: false, message: "user not found" });;
        res.json({ success: true, message: result });
    });
});

router.post('/', (req, res) => {
    let sql = 'SELECT idUtilizador FROM Utilizador where username= "' + req.body.username + '"';
    let query = db.query(sql, (err, result) => {
        if (err) throw err;

        if (result.length == 0) {
            res.json({ success: false, message: "username not found" });
        } else {
            var id = result[0]
            let sql = 'insert into amigo(idUtilizadorAmigo,idUtilizador) values(' + result[0] + ',' + req.body.idUtilizador + ');';
            let query = db.query(sql, (err, result) => {
                if (err) throw err;
                if (result.length == 0) {
                    res.json({ success: false, message: "erro" });
                } else {
                    let sql = 'insert into amigo(idUtilizadorAmigo,idUtilizador) values(' + req.body.idUtilizador + ',' + id + ');';
                    let query = db.query(sql, (err, result) => {
                        if (err) throw err;
                        if (result.length == 0) {
                            res.json({ success: false, message: "erro" });
                        } else {
                            res.json({ success: true, message: "Friend Adiconado!" });
                        }
                    });
                }
            });
        }
    });
});

module.exports = router;