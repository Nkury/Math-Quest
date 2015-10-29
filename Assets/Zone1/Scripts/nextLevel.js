#pragma strict
var levelToLoad : String;

function LoadLevel() {
    if (GameObject.Find("findDynamite").GetComponent.<dynamite>().hasDynamite) {
        Application.LoadLevel(levelToLoad);
    }
    else {
        //error
    }
}