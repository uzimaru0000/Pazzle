using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadStage {

    static ReadStage instance;

    public Dictionary<string, int[,]> stageDatas;

    public static ReadStage Instence {
        get {
            if (instance == null) instance = new ReadStage();
            return instance;
        }
    }

    public ReadStage(string path) {
        var data = Resources.Load(path);
    }

    public ReadStage() {
        stageDatas = new Dictionary<string, int[,]>();
    }

}
