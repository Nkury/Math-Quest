using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public delegate void AttackActionHandler(int[] values);
    public event AttackActionHandler AttackButtonClickedEvent;

    public Sprite[] DefaultSprites;
    public Sprite[] SelectedSprites;

    internal ActionButton[] ActionButtons {get; set;}
    internal AttackButton AttackButton;

	void Start ()
    {
        SetButtons();
        ShouldIncludeValue = new bool[9];
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void ActionButtonClicked(ActionButton button)
    {
        bool buttonIsSelected = button.Clicked();

        if (buttonIsSelected)
        {
            NumberOfSelectedButtons++;
            AddValue(button.Value);
        }
        else
        {
            NumberOfSelectedButtons--;
            RemoveValue(button.Value);
        }
        
        if(NumberOfSelectedButtons > 2)
        {
            AttackButton.IsEnabled = true;
        }
        else if (AttackButton.IsEnabled)
        {
            AttackButton.IsEnabled = false;
        }
    }
    public void AttackButtonClicked(AttackButton button)
    {
        if (AttackButtonClickedEvent != null)
        {
            AttackButtonClickedEvent(GetValues());
        }
    }

    internal void InitActionButtons(int[] values)
    {
        if (values.Length == 3)
        {
            int actionButtonIndex = 0;

            foreach (int value in values)
            {
                int spriteIndex = value - 1;

                ActionButton ab = ActionButtons[actionButtonIndex];
                ab.SelectedSprite = SelectedSprites[spriteIndex];
                ab.Button.image.sprite = DefaultSprites[spriteIndex];
                ab.Value = value;
            }
        }
        else
        {
            //throw new UnityException(
            //    string.Format("Invalid array length. There exist three action buttons so 'values' int[] parameter Length must equal 3. Length = {0}", values.Length));
        }        
    }

    void SetButtons()
    {
        ActionButtons = gameObject.GetComponentsInChildren<ActionButton>();
        AttackButton = gameObject.GetComponentInChildren<AttackButton>();
    }

    void AddValue(int value)
    {
        if (value < 1 || value > ShouldIncludeValue.Length)
        {
            //throw new UnityException(string.Format("'value' parammeter is out of bounds. 0 < 'value' <= {1}? 'value' = {0}", value, ShouldIncludeValue.Length));
        }
        else
        {
            ShouldIncludeValue[value - 1] = true;
        }        
    }
    void RemoveValue(int value)
    {
        if (value < 1 || value > ShouldIncludeValue.Length)
        {
            //throw new UnityException(string.Format("'value' parammeter is out of bounds. 0 < 'value' <= {1} ? 'value' = {0}", value, ShouldIncludeValue.Length));
        }
        else
        {
            ShouldIncludeValue[value - 1] = false;
        }
    }

    int[] GetValues()
    {
        //Can only have a maximum of three actions.
        int[] values = new int[3];
        int valuesIndex = 0;

        //Determine the values that were selected by the user and add them to the array that will be returned.
        for (int i = 0; i < ShouldIncludeValue.Length && valuesIndex < 2; i++)
        {
            if (ShouldIncludeValue[i])
            {
                values[valuesIndex] = i + 1;
                valuesIndex++;
            }
        }

        return values;
    }

    int NumberOfSelectedButtons { get; set; }
    /// <summary>
    /// An array of size n. Where N is the largest possible value. The array stores (0 || 1) an index which indicates whether the value (= index + 1) should be included in an attack calculation.
    /// </summary>
    bool[] ShouldIncludeValue { get; set; }
}
