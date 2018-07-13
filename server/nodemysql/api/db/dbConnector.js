const mysql = require('mysql');

// Create connection
const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: ''
});

db.connect(function (err) {
    if (err) throw err;
    console.log("Connected!");
    executeQuery("DROP DATABASE IF EXISTS GhostRace;");
    executeQuery("CREATE DATABASE IF NOT EXISTS GhostRace");
    executeQuery("USE GhostRace;");
    createUserTable();
    createFriendTable();
    createAdminTable();
    createcharacterTable();
    createSessionTable();
    createLevelTable();
    createActionTable();
    createStatisticTable();
    createStatisticTypeTable();
    addForeignKeys();
    populate();
});


function createUserTable() {
    var query = `CREATE TABLE IF NOT EXISTS Utilizador(idUtilizador bigint AUTO_INCREMENT ,
        username varchar(50) not null,
        dataNascimento Date not null,
        pw varchar(50) not null,
        pais varchar(50) not null,
        email varchar(100) not null,
        PRIMARY KEY (idUtilizador));`;
    executeQuery(query);
}

function createFriendTable() {
    var query = `CREATE TABLE IF NOT EXISTS Amigo(idAmigo bigint AUTO_INCREMENT ,
        idUtilizadorAmigo bigint not null,
        idUtilizador bigint not null,
        PRIMARY KEY (idAmigo));`;
    executeQuery(query);
}

function createAdminTable() {
    var query = `CREATE TABLE IF NOT EXISTS Administrador(idAdmin bigint AUTO_INCREMENT ,
        idUtilizador bigint not null,
        PRIMARY KEY (idAdmin));`;
    executeQuery(query);
}

function createcharacterTable() {
    var query = `CREATE TABLE IF NOT EXISTS Personagem(idPersonagem bigint AUTO_INCREMENT ,
        nome varchar(50) not null,
        força int not null,
        velocidade int not null,
        resistencia int not null,
        PRIMARY KEY (idPersonagem));`;
    executeQuery(query);
}

function createSessionTable() {
    var query = `CREATE TABLE IF NOT EXISTS Sessao(idSessao bigint AUTO_INCREMENT ,
        dataSessao datetime not null,
        tempoPassado float not null,
        idNivel bigint not null,
        idSessaoContra bigint,
        personagem varchar(50) not null,
        idUtilizador bigint not null,
        PRIMARY KEY (idSessao));
        `;
    executeQuery(query);
}

function createLevelTable() {
    var query = `CREATE TABLE IF NOT EXISTS Nivel(idNivel bigint AUTO_INCREMENT,
        nome varchar(20) not null,
        PRIMARY KEY (idNivel));
        `;
    executeQuery(query);
}

function createActionTable() {
    var query = `CREATE TABLE IF NOT EXISTS AçaoJogador(idAçoesJogador bigint AUTO_INCREMENT,
        açao varchar(30) not null, 
        tempoAtual float not null,
        idSessao bigint not null,
        anteriorExecucao int not null,
        PRIMARY KEY (idAçoesJogador));
        `;
    executeQuery(query);
}

function createStatisticTable() {
    var query = `CREATE TABLE IF NOT EXISTS Estatistica(idEstatistica bigint AUTO_INCREMENT ,
        valor varchar(50) not null,
        idTipoEstatistica bigint not null,
        idSessao bigint not null,
        PRIMARY KEY (idEstatistica));
        `;
    executeQuery(query);
}

function createStatisticTypeTable() {
    var query = `CREATE TABLE IF NOT EXISTS TipoEstatistica(idTipoEstatistica bigint AUTO_INCREMENT ,
        nome varchar(50) not null,
        descriçao  varchar(200) not null,
        PRIMARY KEY (idTipoEstatistica));
        `;
    executeQuery(query);
}

function addForeignKeys() {
    var query = `ALTER TABLE Administrador 
    ADD CONSTRAINT administrador_utilizador_fk
    FOREIGN KEY (idUtilizador) REFERENCES Utilizador (idUtilizador); `;
    executeQuery(query);

    query = `ALTER TABLE Sessao 
    ADD CONSTRAINT sessao_utilizador_fk
    FOREIGN KEY (idUtilizador) REFERENCES Utilizador (idUtilizador); `;
    executeQuery(query);

    query = `ALTER TABLE Sessao 
    ADD CONSTRAINT sessao_nivel_fk
    FOREIGN KEY (idNivel) REFERENCES Nivel (idNivel); `;
    executeQuery(query);

    query = `ALTER TABLE AçaoJogador 
    ADD CONSTRAINT açoesJogador_sessao_fk
    FOREIGN KEY (idSessao) REFERENCES Sessao (idSessao); `;
    executeQuery(query);

    query = `ALTER TABLE Estatistica 
    ADD CONSTRAINT estatistica_sessao_fk
    FOREIGN KEY (idSessao) REFERENCES Sessao (idSessao); `;
    executeQuery(query);

    query = `ALTER TABLE Estatistica 
    ADD CONSTRAINT estatistica_tipoEstatistica_fk
    FOREIGN KEY (idTipoEstatistica) REFERENCES TipoEstatistica (idTipoEstatistica); `;
    executeQuery(query);

    query = `ALTER TABLE Amigo 
    ADD CONSTRAINT amigo_utilizadorAmigo_fk
    FOREIGN KEY (idUtilizadorAmigo) REFERENCES Utilizador (idUtilizador); `;
    executeQuery(query);

    query = `ALTER TABLE Amigo 
    ADD CONSTRAINT amigo_utilizador_fk
    FOREIGN KEY (idUtilizador) REFERENCES Utilizador (idUtilizador);`;
    executeQuery(query);
}

function executeQuery(query) {
    db.query(query, function (err, result) {
        if (err) throw err;
    });
}

function populate(){
    var query=`insert into Utilizador(username,dataNascimento,pw,pais,email) values("hugo",curdate(),"1","Portugal","hugo@hotmail.com");`
    executeQuery(query);
    query=`insert into Utilizador(username,dataNascimento,pw,pais,email)  values("ruben",curdate(),"1","Portugal","ruben@hotmail.com");`;
    executeQuery(query);
    query=`insert into Utilizador(username,dataNascimento,pw,pais,email)  values("tiago",curdate(),"1","Portugal","tiago@hotmail.com");`;
    executeQuery(query);
    query=`insert into administrador(idUtilizador) values(1);`;
    executeQuery(query);
    query=`insert into administrador(idUtilizador) values(2);`;
    executeQuery(query);
    query=`insert into nivel(nome) values("Nivel 1");`;
    executeQuery(query);
    query=`insert into nivel(nome) values("Nivel 2");`;
    executeQuery(query);
    query=`insert into sessao(dataSessao,tempoPassado,idNivel,idSessaoContra,personagem,idUtilizador)values(now(),2.93,1,-1,"Scout",1);`;
    executeQuery(query);
    query=`insert into sessao(dataSessao,tempoPassado,idNivel,idSessaoContra,personagem,idUtilizador)values(now(),3.93,1,1,"Scout",2);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("JUMP",1.2,1,1);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("MOVE_LEFT",2,1,1);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("JUMP",2.5,1,1);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("JUMP",1.2,1,2);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("MOVE_LEFT",2,1,2);`;
    executeQuery(query);
    query=`insert into AçaoJogador(açao,tempoAtual,anteriorExecucao,idSessao)values("JUMP",2.5,1,2);`;
    executeQuery(query);
    query=`insert into TipoEstatistica(nome,descriçao)values("Resultado","Jogador ganhou ou não")`;
    executeQuery(query);
    query=`insert into TipoEstatistica(nome,descriçao)values("Tempo","Tempo que demorou a acabar o nivel");`;
    executeQuery(query);
    query=`insert into estatistica(valor,idTipoEstatistica,idSessao) values("Derrota",1,1);`;
    executeQuery(query);
    query=`insert into estatistica(valor,idTipoEstatistica,idSessao) values("2.93",2,1);`;
    executeQuery(query);
    query=`insert into estatistica(valor,idTipoEstatistica,idSessao) values("3.93",2,2);`;
    executeQuery(query);
    query=`insert into amigo(idUtilizadorAmigo,idUtilizador) values(2,1);`;
    executeQuery(query);
    query=`insert into amigo(idUtilizadorAmigo,idUtilizador) values(1,2);`;
    executeQuery(query);
}

module.exports = db;