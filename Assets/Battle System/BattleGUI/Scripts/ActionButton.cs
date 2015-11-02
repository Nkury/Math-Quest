using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

public class ActionButton : MonoBehaviour {

    public Sprite SelectedSprite;

    internal Button Button { get; set; }
    internal int Value { get; set; }
    Color DefaultColor { get; set; }

	void Start () {
        Button = gameObject.GetComponent<Button>();
        DefaultColor = new Color(.85f, .85f, .85f, 1);
        IsSelected = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public bool Clicked()
    {
        IsSelected = !IsSelected;
        return IsSelected;
    }

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            if (isSelected)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }
    }

    public void Select()
    {
        Button.image.color = Color.white;
        Button.image.overrideSprite = SelectedSprite;
    }

    public void Deselect()
    {
        Button.image.color = DefaultColor;
        Button.image.overrideSprite = null;
    }

    private bool isSelected = false;
}
