using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class AttackButton : MonoBehaviour {

    public Sprite SelectedSprite;

    internal Button Button { get; set; }

    Color DisabledColor { get; set; }

    void Start () {
        DisabledColor = new Color(1f, 1f, 1f, .5f);
        Button = gameObject.GetComponent<Button>();
        Button.image.color = DisabledColor;
	}
	
	void Update ()
    {

	}

    public void Click()
    {
        //Should fire off an event that can be subscribed to.
    }

    public bool IsEnabled
    {
        get
        {
            return isEnabled;
        }
        set
        {
            isEnabled = value;
            if (isEnabled)
            {
                Button.transition = Selectable.Transition.SpriteSwap;
                SpriteState sprites = new SpriteState();
                sprites.pressedSprite = SelectedSprite;
                Button.spriteState = sprites;
                Button.image.color = Color.white;
            }
            else
            {
                Button.image.color = DisabledColor;
                Button.transition = Selectable.Transition.None;
            }
        }
    }

    private bool isEnabled = false;
}
