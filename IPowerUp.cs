using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp {
    void Effect(ModelPlayer player);

    string Message();
}
