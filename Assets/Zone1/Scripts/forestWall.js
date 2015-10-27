#pragma strict

var forest1hint : UI.Text;

function OnTriggerEnter(col : Collider) {
    if(col.gameObject.tag == "Player") {
        forest1hint.text = "The forest is too think here to move through. You must follow the path.";
    }
}

function OnTriggerExit(col : Collider) {
     if(col.gameObject.tag == "Player") {
         forest1hint.text = "";
        }
}