using UnityEngine;
using System.Collections;

public interface IScreen {

    void Activate();
    void Deactivate();

    string Free();

}