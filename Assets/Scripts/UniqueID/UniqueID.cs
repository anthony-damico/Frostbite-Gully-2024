
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueID
{
    public string GenerateUuid()
    {
        string uuid = Guid.NewGuid().ToString();
        return uuid;
    }

}
