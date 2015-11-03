#pragma strict

var banditBossText : UI.Text;
public var enableDynamite : boolean = false;
var banditBossScene : boolean = false;


function OnTriggerEnter(col : Collider) {
    if(col.gameObject.tag == "Player") {
        if(banditBossScene == false){
            banditBossText.text = "The bandit boss is here!. After defeating him go find the dynamite to blow up the rock.";
            banditBossScene = true;
            enableDynamite = true;
            
        }
        else if (!GameObject.Find("findDynamite").GetComponent.<dynamite>().hasDynamite) {
            banditBossText.text = "Stop stalling! Go find the dynamite!";
        }
        else {
            banditBossText.text = "Now blow up the rock!";
        }
    }
}

    function OnTriggerExit(col : Collider) {
        if(col.gameObject.tag == "Player") {
            banditBossText.text = "";
        }
    }