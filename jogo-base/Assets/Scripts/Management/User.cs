using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {

    public int id;
    public string name;
    public string email;
    public DateTime birthdate;
    public String password;
    public String country;

    public User(int id, string name, string email, DateTime birthdate, string passwordInput, string country) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.birthdate = birthdate;
        this.country = country;
        password = hash(passwordInput);

    }

    /* Stub, subsituir mais tarde por algoritmo de hashing */
    private String hash(String password) {
        return "";
    }

}
