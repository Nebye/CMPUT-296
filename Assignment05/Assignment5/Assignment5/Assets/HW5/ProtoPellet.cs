using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPellet
{
    public bool powerPellet = false;//Whether or not this is a power pellet, false by default
    public Vector3 worldLocation;//Where in the world (level) this pellet is located

    //Two constructors based on what information you wish to specify on construction
    public ProtoPellet(Vector3 _worldLocation)
    {
        worldLocation = _worldLocation;
    }

    public ProtoPellet(Vector3 _worldLocation, bool _powerPellet)
    {
        worldLocation = _worldLocation;
        powerPellet = _powerPellet;
    }

    //Make a copy of this object
    public ProtoPellet Clone() {
        return new ProtoPellet(worldLocation, powerPellet);
    }

}
